using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Helper.Enum.Project;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Documents;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;
using DocumentApprovalStatus = PTSL.GENERIC.Web.Core.Helper.Enum.DocumentApprovalStatus;
using DocumentType = PTSL.GENERIC.Web.Core.Enum.Documents.DocumentType;
using TestingType = PTSL.GENERIC.Web.Core.Enum.Documents.TestingType;

namespace PTSL.GENERIC.Web.Core.Controllers.Documents
{
    public class AllTypesDocumentController : Controller
    {
        private readonly IAllTypesDocumentService _AllTypesDocumentService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IApprovalForAllDocumentService _ApprovalForAllDocumentService;
        private readonly IUserRoleService _UserRoleService;
        private readonly IUserService _UserService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPermissionHeaderSettingsService _permissionHeaderSettingsService;
        private readonly IProjectStateLogService _projectStateLogService;
        private readonly IProjectCertificationService _projectCertificationService;
        private readonly ICertificationAmendmentService _docAmendmentService;


        public AllTypesDocumentController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _AllTypesDocumentService = new AllTypesDocumentService(httpHelper);
            _ApprovalForAllDocumentService = new ApprovalForAllDocumentService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _UserRoleService = new UserRoleService(httpHelper);
            _UserService = new UserService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _permissionHeaderSettingsService = new PermissionHeaderSettingsService(httpHelper);
            _projectStateLogService = new ProjectStateLogService(httpHelper);
            _projectCertificationService = new ProjectCertificationService(httpHelper);
            _docAmendmentService = new CertificationAmendmentService(httpHelper);
        }
        public async Task<IActionResult> Index()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();

            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            var allDocumentApprovalLog = await _ApprovalForAllDocumentService.List();
            var filteredApprovalLog = allDocumentApprovalLog.entity ?? new List<ApprovalForAllDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType != DocumentType.Agreement).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                DocumentAmendmentState = x.DocumentAmendmentState,
                ViewVersionNo = x.ViewVersionNo,
                HasEditAndDltPrmsn = filteredApprovalLog.Where(s=>s.AllTypesOfDocumentId == x.Id).Any() ? false : true,
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();
            return View(entityList);

        }

        public async Task<IActionResult> PendingDocuments()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();

            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();

            var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            bool hasEditDeletePermission = false;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.SQTC_Admin || userType == UserType.SQTC_User)
                {
                    ViewBag.HasCreatePermission = true;
                    hasEditDeletePermission = true;
                }
                else
                {
                    ViewBag.HasCreatePermission = false;
                    hasEditDeletePermission = false;
                }
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                hasEditDeletePermission = false;
            }

            var allDocumentApprovalLog = await _ApprovalForAllDocumentService.List();
            var filteredApprovalLog = allDocumentApprovalLog.entity ?? new List<ApprovalForAllDocumentVM>();

            if (filteredApprovalLog.Any())
            {
                long moduleEnumId = (long)ModuleEnum.ReportApproval;

                var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault();
                var NewResultRoleId = roleResult?.PermissionRowSettings?.LastOrDefault()?.UserRoleId;
                foreach (var item in returnResponse.entity.Where(x => x.DocumentApprovalStatus == DocumentApprovalStatus.Pending && x.DocumentType != DocumentType.Agreement))
                {
                    var findCurrDocApprovalLog = filteredApprovalLog.Where(x => x.AllTypesOfDocumentId == item.Id).LastOrDefault();
                    hasEditDeletePermission = filteredApprovalLog.Where(x => x.AllTypesOfDocumentId == item.Id).Any() ? false : true;
                    //var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var CurrentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

                    if (findCurrDocApprovalLog != null && findCurrDocApprovalLog.ReceiverId == userId && CurrentUserRoleId != NewResultRoleId)
                        item.IsApprovalShow = true;
                    else if (item.DocumentAmendmentState == DocumentAmendmentState.Amendmented)
                    {
                        if (findCurrDocApprovalLog is not null && findCurrDocApprovalLog?.IsAmmendment == true && findCurrDocApprovalLog.ReceiverId == userId &&
                            CurrentUserRoleId == NewResultRoleId)
                        {
                            item.IsApprovalShow = false;
                            item.IsAcceptOrReject = true;
                        }
                        else if (findCurrDocApprovalLog?.ReceiverId is null)
                            item.IsApprovalShow = true;
                        else if(findCurrDocApprovalLog.ReceiverId == userId && CurrentUserRoleId == NewResultRoleId)
                            item.IsApprovalShow = true;
                        else
                            item.IsApprovalShow = false;
                    }
                    else if (findCurrDocApprovalLog != null && findCurrDocApprovalLog.ReceiverId == userId && CurrentUserRoleId == NewResultRoleId)
                    {
                        item.IsApprovalShow = false;
                        item.IsAcceptOrReject = true;
                    }
                    else if (findCurrDocApprovalLog == null)
                        item.IsApprovalShow = true;
                        
                    item.ApprovalMessage = findCurrDocApprovalLog?.Description;
                    item.HasEditAndDltPrmsn = hasEditDeletePermission;

                }
            }
            else
            {
                foreach (var item in returnResponse.entity.Where(x => x.DocumentApprovalStatus == DocumentApprovalStatus.Pending && x.DocumentType != DocumentType.Agreement))
                {
                    item.IsApprovalShow = true;
                    item.HasEditAndDltPrmsn = true;
                }

            }


            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");

            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentApprovalStatus == DocumentApprovalStatus.Pending && s.DocumentType != DocumentType.Agreement).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                ApprovalMessage = x.ApprovalMessage,
                IsAcceptOrReject = x.IsAcceptOrReject,
                IsApprovalShow = x.IsApprovalShow,
                DocumentAmendmentState = x.DocumentAmendmentState,
                ViewVersionNo = x.ViewVersionNo,
                HasEditAndDltPrmsn = x.HasEditAndDltPrmsn
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();
            return View(entityList);
        }

        public async Task<JsonResult> GetApprovalProcessModalData(long id)
        {

            // Ensure it's a list
            long moduleEnumId = (long)ModuleEnum.ReportApproval;
            var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault(); // Retrieve first or default
            var currentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var getAllTypeDoc = await _AllTypesDocumentService.GetById(id);
            if (roleResult != null && roleResult.PermissionRowSettings != null && currentUserRoleId != 1)
            {
                var logResult = await _ApprovalForAllDocumentService.List();
                var FilteredlogResult = logResult.entity is not null ? logResult.entity.Where(x => x.AllTypesOfDocumentId == id).ToList() : new List<ApprovalForAllDocumentVM>();

                var permissionRowIds = getAllTypeDoc.entity.DocumentAmendmentState == DocumentAmendmentState.Amendmented ? FilteredlogResult.Where(s=>s.IsAmmendment).Select(x => x.PermissionRowSettingsId).ToList() :
                    FilteredlogResult.Select(x => x.PermissionRowSettingsId).ToList();
                // Filter the PermissionRowSettings based on PermissionRowId
                var newResult = roleResult.PermissionRowSettings.FirstOrDefault(row => !permissionRowIds.Contains(row.Id));

                if (newResult != null && newResult.UserRole != null)
                {
                    // Access properties from UserRole
                    var RoleName = newResult.UserRole.RoleName;
                    var RoleId = newResult.UserRole.Id;
                    var PermissionRowSettingsId = newResult.Id;
                    var userList = _UserService.GetUserInfoByUserRoleId(RoleId);
                    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    // Use roleName and roleId as needed
                }
                else
                {
                    newResult = roleResult.PermissionRowSettings?.FirstOrDefault();
                    if (newResult != null && newResult.UserRole != null)
                    {
                        var RoleName = newResult.UserRole.RoleName;
                        var RoleId = newResult.UserRole.Id;
                        var PermissionRowSettingsId = newResult.Id;
                        var userList = _UserService.GetUserInfoByUserRoleId(RoleId);
                        var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                        return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    }
                }

            }
            else
            {
                var newResult = roleResult.PermissionRowSettings?.FirstOrDefault();

                if (newResult != null && newResult.UserRole != null)
                {
                    // Access properties from UserRole
                    var RoleName = newResult.UserRole.RoleName;
                    var RoleId = newResult.UserRole.Id;
                    var PermissionRowSettingsId = newResult.Id;
                    var userList = _UserService.GetUserInfoByUserRoleId(RoleId);
                    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    // Use roleName and roleId as needed
                }
            }


            return Json(new { Data = "null", Message = "" }, SerializerOption.Default);
        }
        [HttpPost]
        public async Task<JsonResult> SaveDocApproval(ApprovalForAllDocumentVM entity)
        {
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;
            entity.StatusForPdf = ApprovalStatusForPdf.Verified;
            entity.ProcessFlowType = ProcessFlowType.Forward;
            entity.SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ReceiverName = _UserService.GetById(entity.ReceiverId).entity.UserName;
            entity.Description = "Project Forwarded To" + " " + ReceiverName;
            bool isSuccess = false;
            string message = "";
            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse = _ApprovalForAllDocumentService.Create(entity);
            if (returnResponse.executionState == ExecutionState.Created && returnResponse.entity.Id != 0)
            {
                var getListOfApproval = await _ApprovalForAllDocumentService.DocumentCommentHistoryById(entity.AllTypesOfDocumentId);
                if (getListOfApproval.entity != null)
                {
                    var getFilteredUsers = getListOfApproval.entity.Where(s => s.StatusForPdf == ApprovalStatusForPdf.Verified && s.ProcessFlowType == ProcessFlowType.Forward).ToList();
                    var getDocument = await _AllTypesDocumentService.GetById(entity.AllTypesOfDocumentId);
                    if (getFilteredUsers.Any() && getDocument.entity != null)
                    {
                         isSuccess = await UpdateDocumentOnApproval(getDocument.entity);
                    }
                }
                
            }
            isSuccess = returnResponse.executionState == ExecutionState.Created ? true : false;
            message = "Document forwarded successfull";
            return Json(new { success = isSuccess, Data = returnResponse, Message = message}, SerializerOption.Default);
        }
        [HttpPost]
        public async Task<ActionResult> Accept(int id)
        {
            try
            {
                var DocResult = await _AllTypesDocumentService.GetById(id);
                DocResult.entity.IsActive = true;
                DocResult.entity.DocumentApprovalStatus = DocumentApprovalStatus.Accept;
                if (DocResult.entity.DocumentAmendmentState == DocumentAmendmentState.Amendmented)
                    DocResult.entity.DocumentAmendmentState = DocumentAmendmentState.Approved;
                else
                    DocResult.entity.DocumentAmendmentState = DocumentAmendmentState.Generated;

                (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(DocResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingDocuments");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var ApproverName = _UserService.GetById(SenderId).entity.UserName;
                    ApprovalForAllDocumentVM acceptedDocLog = new ApprovalForAllDocumentVM();
                    acceptedDocLog.AllTypesOfDocumentId = returnResponse.entity.Id;
                    acceptedDocLog.DocumentApprovalStatus = DocumentApprovalStatus.Accept;
                    acceptedDocLog.StatusForPdf = ApprovalStatusForPdf.Approved;
                    acceptedDocLog.ProcessFlowType = ProcessFlowType.Forward;
                    acceptedDocLog.Description = "Accepted by " + "" + ApproverName;
                    acceptedDocLog.Remarks = "Accepted by " + "" + ApproverName;
                    acceptedDocLog.SenderId = SenderId;
                    bool isSuccess = false;
                    (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponseLog = _ApprovalForAllDocumentService.Create(acceptedDocLog);
                    if (returnResponseLog.executionState == ExecutionState.Created && returnResponseLog.entity.Id != 0)
                    {

                        var getDocument = await _AllTypesDocumentService.GetById(id);
                        if (getDocument.entity != null)
                        {
                            isSuccess = await UpdateDocumentOnApproval(getDocument.entity);
                        }
                    }

                    if (returnResponseLog.entity != null)
                    {
                        return Json(new { success = isSuccess, Data = returnResponse, Message = "Document approved successfully." }, SerializerOption.Default);
                    }
                    else
                    {
                        return Json(new { success = false, Data = "", Message = "" }, SerializerOption.Default);
                    }

                }

            }
            catch
            {
                return RedirectToAction("PendingDocuments");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AcceptCertifaicate(int id)
        {
            try
            {
                var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                var userType = _UserService.GetById(userId).entity?.UserType;

                var DocResult = await _projectCertificationService.List();
                bool isSuccess = false;
                if(DocResult.entity is not null)
                {
                    var getCertificate = DocResult.entity.Where(s => s.AllTypesOfDocumentId == id).FirstOrDefault();
                    if (getCertificate != null)
                    {
                        getCertificate.IsActive = true;
                        if(userType == UserType.SQTC_Admin || userType == UserType.SQTC_User)
                            getCertificate.CertificationStatus = CertificationStatus.ApprovedByAdmin;
                        else if(userType == UserType.ClientAdmin || userType == UserType.Client_User)
                            getCertificate.CertificationStatus = CertificationStatus.ApprovedByClient;
                        else
                            getCertificate.CertificationStatus = CertificationStatus.ApprovedByAdmin;

                        (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse = await _projectCertificationService.Update(getCertificate);
                        if (returnResponse.executionState.ToString() == "Updated")
                        {
                            isSuccess = true;
                        }

                    }
                }
                return Json(new { Success = isSuccess });

            }
            catch
            {
                return Json(new { Success = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AcceptForwardedDocument(int id)
        {
            try
            {
                var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                var userType = _UserService.GetById(userId).entity?.UserType;

                var getDocument = await _AllTypesDocumentService.GetById(id);

                bool isSuccess = false;
                if (getDocument.entity is not null)
                {
                    getDocument.entity.IsActive = true;
                    getDocument.entity.DocumentAmendmentState =  DocumentAmendmentState.Approved;
                    getDocument.entity.DocumentApprovalStatus = DocumentApprovalStatus.Accept;

                    (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(getDocument.entity);
                    if (returnResponse.executionState.ToString() == "Updated")
                    {
                        isSuccess = true;
                    }

                }
                return Json(new { Success = isSuccess });

            }
            catch
            {
                return Json(new { Success = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Reject(int id, AllTypesOfDocumentVM entity)
        {
            try
            {
                var DocResult = await _AllTypesDocumentService.GetById(id);
                DocResult.entity.IsActive = true;
                DocResult.entity.DocumentApprovalStatus = DocumentApprovalStatus.Reject;
                DocResult.entity.RejectionComment = entity.RejectionComment;

                (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(DocResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingDocuments");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var RejectorName = _UserService.GetById(SenderId).entity.UserName;
                    ApprovalForAllDocumentVM acceptedDocLog = new ApprovalForAllDocumentVM();
                    acceptedDocLog.AllTypesOfDocumentId = returnResponse.entity.Id;
                    acceptedDocLog.DocumentApprovalStatus = DocumentApprovalStatus.Reject;
                    acceptedDocLog.Description = "Rejected by " + "" + RejectorName;
                    acceptedDocLog.Remarks = "Rejected by " + "" + RejectorName;
                    acceptedDocLog.SenderId = SenderId;
                    (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponseLog = _ApprovalForAllDocumentService.Create(acceptedDocLog);


                    if (returnResponseLog.entity != null)
                    {
                        return RedirectToAction("PendingDocuments");
                    }
                    else
                    {
                        return RedirectToAction("PendingDocuments");
                    }


                }

            }
            catch
            {
                return RedirectToAction("PendingList");
            }
        }

        public async Task<IActionResult> Create()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }

            var documentTypes = EnumHelper.GetEnumDropdowns<DocumentType>();

            ViewBag.DocumentType = new SelectList(documentTypes.Where(s => s.Id != 1), "Id", "Name");
            ViewBag.TestingType = new SelectList(EnumHelper.GetEnumDropdowns<TestingType>(), "Id", "Name");
            return View(new AllTypesOfDocumentVM());
        }


        public async Task<IActionResult> Edit(long id)
        {
            (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.GetById(id);
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName", returnResponse.entity.ProjectRequestId);
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }

            ViewBag.DocumentType = new SelectList(EnumHelper.GetEnumDropdowns<DocumentType>(), "Id", "Name", (int)returnResponse.entity.DocumentType);
            ViewBag.TestingType = new SelectList(EnumHelper.GetEnumDropdowns<TestingType>(), "Id", "Name", (int)returnResponse.entity.TestingType);
            return View(returnResponse.entity);
        }

        public async Task<ActionResult> UpdateDocument(AllTypesOfDocumentVM model)
        {
            //   var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                bool isSuccess = false;
                var message = "";
                if (model is not null)
                {
                    (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnDocResponse = await _AllTypesDocumentService.GetById(model.Id);
                    if(returnDocResponse.entity is null)
                        return Ok(new { Success = false, Message = "No Document found for update." });

                    AllTypesOfDocumentVM updateEntity = returnDocResponse.entity;
                    //updateEntity.ProjectRequestId = model.ProjectRequestId;
                    //updateEntity.DocumentType = model.DocumentType;
                    //updateEntity.TestingType = model.TestingType;
                    updateEntity.EditorContent = model.EditorContent;
                    updateEntity.FileName = model.FileName;
                    updateEntity.FilePath = model.FilePath;

                    if (String.IsNullOrEmpty(model.FileName) && String.IsNullOrEmpty(model.FilePath))
                    {
                        var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                        var generatedUser = _UserService.GetById(SenderId).entity;
                        var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
                        var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
                        if (generatedUser != null)
                        {
                            generatedUser.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + generatedUser.SignatureUrl;
                            generatedUser.RoleName = RoleName;
                        }

                        var getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, generatedUser ?? new UserVM(), DateTime.Now.ToString("dd/MM/yyyy"));

                        List<string> allContents = new List<string>
                            {
                                model.EditorContent,
                                getFooterString
                            };

                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            // Create an iText PdfWriter with the MemoryStream
                            using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                            {
                                // Create an iText PdfDocument with the PdfWriter
                                using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                                {
                                    foreach (var item in allContents)
                                    {
                                        if (!String.IsNullOrEmpty(item))
                                        {
                                            // Create a new PdfDocument for each HTML content
                                            using (MemoryStream tempStream = new MemoryStream())
                                            {
                                                using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                                {
                                                    using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                                    {
                                                        // Create a new page in the temporary PDF document
                                                        tempPdfDocument.AddNewPage();

                                                        // Create an iText ConverterProperties object
                                                        ConverterProperties properties = new ConverterProperties();

                                                        // Convert the HTML content to PDF and add it to the temporary PDF document
                                                        HtmlConverter.ConvertToPdf(item, tempPdfDocument, properties);
                                                    }
                                                }

                                                // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                                using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                                {
                                                    using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                                    {
                                                        // Merge the temporary PDF document into the combined PDF document
                                                        tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }

                            // Get the byte array from the MemoryStream
                            pdfBytes = outputStream.ToArray();
                        }

                        var uploadDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile"));
                        if (Directory.Exists(uploadDirectory) == false)
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }


                        //var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");

                        var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                        var newGuid = Guid.NewGuid().ToString();
                        var documentType = (DocumentType)model.DocumentType;
                        var versionNoPdf = String.IsNullOrEmpty(model.VersionNo) ? "V1" : "V" + (Convert.ToInt32(model.VersionNo.Split(' ')[1]) + 1).ToString();

                        var newDiskFileName = $"{documentType.ToString()}_{model.ProjectRequestId}_{newGuid}_{versionNoPdf}.pdf";

                        var filePath = Path.Combine(uploadDirectory, newDiskFileName);
                        // Write the byte array to the file
                        System.IO.File.WriteAllBytes(filePath, pdfBytes);

                        localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/" + newDiskFileName;

                        updateEntity.FilePath = "/DocumentFile/" + newDiskFileName;
                        updateEntity.FileName = newDiskFileName;
                    }

                    var versionNo = String.IsNullOrEmpty(model.VersionNo) ? "V1" : "Version " + (Convert.ToInt32(model.VersionNo.Split(' ')[1]) + 1).ToString();
                    updateEntity.VersionNo = versionNo;
                    

                    (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(updateEntity);
                    isSuccess = returnResponse.executionState == ExecutionState.Updated ? true : false;
                    message = returnResponse.message;


                }
                return Ok(new { Success = isSuccess, Message = message });

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> UpdateDocumentOnApproval(AllTypesOfDocumentVM model)
        {
            //   var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                bool isSuccess = false;
                var message = "";
                if (model is not null)
                {

                   

                    var getListOfApproval = await _ApprovalForAllDocumentService.DocumentCommentHistoryById(model.Id);
                    UserVM generatedUser = new UserVM();
                    List<UserVM> verifiedUsers = new List<UserVM>();
                    UserVM approvedUser = new UserVM();
                    if (getListOfApproval.entity != null)
                    {
                        
                        var getGeneratedUser = getListOfApproval.entity.Where(s => s.StatusForPdf == ApprovalStatusForPdf.Generated && s.ProcessFlowType == ProcessFlowType.Forward).OrderByDescending(s=>s.Id).FirstOrDefault();
                        //Generated User
                        if (getGeneratedUser != null)
                        {
                            generatedUser = _UserService.GetById(getGeneratedUser.SenderId).entity;
                            var RoleName = _UserRoleService.GetById(generatedUser.UserRoleId).entity?.RoleName;

                            generatedUser.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + generatedUser.SignatureUrl;
                            generatedUser.RoleName = RoleName;
                            generatedUser.CreatedAt = getGeneratedUser.CreatedAt;
                        }

                        //Verified User List
                        var getFilteredUsers = new List<ApprovalForAllDocumentVM>();
                        if(model.DocumentAmendmentState == DocumentAmendmentState.Amendmented || model.DocumentAmendmentState == DocumentAmendmentState.Approved)
                            getFilteredUsers.AddRange(getListOfApproval.entity.Where(s => s.StatusForPdf == ApprovalStatusForPdf.Verified && s.IsAmmendment && s.ProcessFlowType == ProcessFlowType.Forward).ToList());
                        else
                            getFilteredUsers.AddRange(getListOfApproval.entity.Where(s => s.StatusForPdf == ApprovalStatusForPdf.Verified && s.ProcessFlowType == ProcessFlowType.Forward).ToList());

                        foreach (var user in getFilteredUsers)
                        {
                            UserVM userVM = new UserVM();
                            userVM = _UserService.GetById(user.SenderId).entity;
                            if (userVM != null)
                            {
                                var verifiedRoleName = _UserRoleService.GetById(userVM.UserRoleId).entity?.RoleName;
                                userVM.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + userVM.SignatureUrl;
                                userVM.RoleName = verifiedRoleName;
                                userVM.CreatedAt = user.CreatedAt;
                                verifiedUsers.Add(userVM);
                            }
                        }

                        //Get Approved User
                        var getApprovedUser = getListOfApproval.entity.Where(s => s.StatusForPdf == ApprovalStatusForPdf.Approved && s.ProcessFlowType == ProcessFlowType.Forward).FirstOrDefault();
                        if (getApprovedUser != null)
                        {
                            UserVM userVM = new UserVM();
                            userVM = _UserService.GetById(getApprovedUser.SenderId).entity;
                            if (userVM != null)
                            {
                                var verifiedRoleName = _UserRoleService.GetById(userVM.UserRoleId).entity?.RoleName;
                                approvedUser.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + userVM.SignatureUrl;
                                approvedUser.RoleName = verifiedRoleName;
                                approvedUser.CreatedAt = getApprovedUser.CreatedAt;
                                //verifiedUsers.Add(userVM);
                            }
                        }

                    }

                    var getFooterString = DocumentHelper.GetVerifiedFooterByDocumentType(model.DocumentType, generatedUser ?? new UserVM(), verifiedUsers, approvedUser);

                    List<string> allContents = new List<string>
                            {
                                model.EditorContent,
                                getFooterString
                            };

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        // Create an iText PdfWriter with the MemoryStream
                        using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                        {
                            // Create an iText PdfDocument with the PdfWriter
                            using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                            {
                                foreach (var item in allContents)
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        // Create a new PdfDocument for each HTML content
                                        using (MemoryStream tempStream = new MemoryStream())
                                        {
                                            using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                                {
                                                    // Create a new page in the temporary PDF document
                                                    tempPdfDocument.AddNewPage();

                                                    // Create an iText ConverterProperties object
                                                    ConverterProperties properties = new ConverterProperties();

                                                    // Convert the HTML content to PDF and add it to the temporary PDF document
                                                    HtmlConverter.ConvertToPdf(item, tempPdfDocument, properties);
                                                }
                                            }

                                            // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                            using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                                {
                                                    // Merge the temporary PDF document into the combined PDF document
                                                    tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }

                        // Get the byte array from the MemoryStream
                        pdfBytes = outputStream.ToArray();
                    }

                    var uploadDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile"));
                    if (Directory.Exists(uploadDirectory) == false)
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }


                    //var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");

                    var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var newGuid = Guid.NewGuid().ToString();
                    var documentType = (DocumentType)model.DocumentType;
                    var versionNoPdf = String.IsNullOrEmpty(model.VersionNo) ? "V1" : "V" + (Convert.ToInt32(model.VersionNo.Split(' ')[1]) + 1).ToString();

                    var newDiskFileName = $"{documentType.ToString()}_{model.ProjectRequestId}_{newGuid}_{versionNoPdf}.pdf";

                    var filePath = Path.Combine(uploadDirectory, newDiskFileName);
                    // Write the byte array to the file
                    System.IO.File.WriteAllBytes(filePath, pdfBytes);

                    localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/" + newDiskFileName;

                    model.FilePath = "/DocumentFile/" + newDiskFileName;
                    model.FileName = newDiskFileName;

                    var versionNo = String.IsNullOrEmpty(model.VersionNo) ? "V1" : "Version " + (Convert.ToInt32(model.VersionNo.Split(' ')[1]) + 1).ToString();
                    model.VersionNo = versionNo;

                    (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(model);
                    isSuccess = returnResponse.executionState == ExecutionState.Updated ? true : false;
                    message = returnResponse.message;


                }
                return isSuccess;

            }
            catch (Exception ex)
            {
                throw;
            }

        }



        [HttpPost]
        public async Task<ActionResult> CreateDocument(AllTypesOfDocumentVM model)
        {
            //   var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                bool isSuccess = false;
                var message = "";
                if (string.IsNullOrEmpty(model.EditorContent))
                {
                    isSuccess = false;
                    message = "Please add your content.";
                    return Ok(new { Success = isSuccess, Message = message });
                }
                if (model is not null)
                {
                    if (String.IsNullOrEmpty(model.FileName) && String.IsNullOrEmpty(model.FilePath))
                    {
                        var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                        var generatedUser = _UserService.GetById(SenderId).entity;
                        var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
                        var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
                        if (generatedUser != null)
                        {
                            generatedUser.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + generatedUser.SignatureUrl;
                            generatedUser.RoleName = RoleName;
                        }
                        var getFooterString = "";
                        if(model.DocumentType == DocumentType.Agreement)
                        {
                            var userList = _UserService.List().entity;
                            var project = _ProjectRequestService.GetById(model.ProjectRequestId).Result.entity;
                            var clientUser = userList.Where(s => s.ClientId == project.ClientId).FirstOrDefault();
                            if(clientUser != null) {
                                clientUser.SignatureUrl = clientUser is null ? "" : $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + clientUser?.SignatureUrl;
                            }

                            getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, clientUser ?? new UserVM(), DateTime.Now.ToString("dd/MMM/yyyy"));

                        }
                        else
                            getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, generatedUser ?? new UserVM(), DateTime.Now.ToString("dd/MM/yyyy"));

                        List<string> allContents = new List<string>
                            {
                                model.EditorContent,
                                getFooterString
                            };

                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            // Create an iText PdfWriter with the MemoryStream
                            using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                            {
                                // Create an iText PdfDocument with the PdfWriter
                                using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                                {
                                    foreach (var item in allContents)
                                    {
                                        if (!String.IsNullOrEmpty(item))
                                        {
                                            // Create a new PdfDocument for each HTML content
                                            using (MemoryStream tempStream = new MemoryStream())
                                            {
                                                using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                                {
                                                    using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                                    {
                                                        // Create a new page in the temporary PDF document
                                                        tempPdfDocument.AddNewPage();

                                                        // Create an iText ConverterProperties object
                                                        ConverterProperties properties = new ConverterProperties();

                                                        // Convert the HTML content to PDF and add it to the temporary PDF document
                                                        HtmlConverter.ConvertToPdf(item, tempPdfDocument, properties);
                                                    }
                                                }

                                                // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                                using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                                {
                                                    using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                                    {
                                                        // Merge the temporary PDF document into the combined PDF document
                                                        tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }

                            // Get the byte array from the MemoryStream
                            pdfBytes = outputStream.ToArray();
                        }

                        var uploadDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile"));
                        if (Directory.Exists(uploadDirectory) == false)
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }


                        //var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");

                        var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                        var newGuid = Guid.NewGuid().ToString();
                        var documentType = (DocumentType)model.DocumentType;
                        var newDiskFileName = $"{documentType.ToString()}_{model.ProjectRequestId}_{newGuid}_V1.pdf";

                        var filePath = Path.Combine(uploadDirectory, newDiskFileName);
                        // Write the byte array to the file
                        System.IO.File.WriteAllBytes(filePath, pdfBytes);

                        localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/" + newDiskFileName;

                        model.FilePath = "/DocumentFile/" + newDiskFileName;
                        model.FileName = newDiskFileName;
                    }
                    model.VersionNo = "Version 1";
                    (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Create(model);
                    isSuccess = returnResponse.executionState == ExecutionState.Created ? true : false;
                    message = returnResponse.message;
                    if(returnResponse.executionState == ExecutionState.Created )
                    {
                        var type = returnResponse.entity.DocumentType;
                        long projectRequestId = returnResponse.entity.ProjectRequestId;
                        InsertLogData(projectRequestId, type);
                    }


                }
                return Ok(new { Success = isSuccess, Message = message });

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        [HttpPost]
        public ActionResult ViewDocuemnt(AllTypesOfDocumentVM model)
        {
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                var storeFileName = "";
                var storeFilePath = "";
                bool isSuccess = false;
                var message = "";
                if (model is not null)
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var generatedUser = _UserService.GetById(SenderId).entity;
                    var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
                    var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
                    if (generatedUser != null)
                    {
                        generatedUser.SignatureUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + generatedUser.SignatureUrl;
                        generatedUser.RoleName = RoleName;
                    }

                    var getFooterString = "";
                    if (model.DocumentType == DocumentType.Agreement)
                    {
                        var userList = _UserService.List().entity;
                        var project = _ProjectRequestService.GetById(model.ProjectRequestId).Result.entity;
                        var clientUser = userList.Where(s => s.ClientId == project.ClientId).FirstOrDefault();
                        if (clientUser != null)
                        {
                            clientUser.SignatureUrl = clientUser is null ? "" : $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + clientUser?.SignatureUrl;
                        }

                        getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, clientUser ?? new UserVM(), DateTime.Now.ToString("dd/MMM/yyyy"));

                    }
                    else
                        getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, generatedUser ?? new UserVM(), DateTime.Now.ToString("dd/MM/yyyy"));


                    //var getFooterString = DocumentHelper.GetInitialFooterByDocumentType(model.DocumentType, generatedUser ?? new UserVM(), DateTime.Now.ToString("dd/MM/yyyy"));

                    List<string> allContents = new List<string>
                        {
                            model.EditorContent,
                            getFooterString
                        };

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        // Create an iText PdfWriter with the MemoryStream
                        using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                        {
                            // Create an iText PdfDocument with the PdfWriter
                            using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                            {
                                foreach (var item in allContents)
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        // Create a new PdfDocument for each HTML content
                                        using (MemoryStream tempStream = new MemoryStream())
                                        {
                                            using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                                {
                                                    // Create a new page in the temporary PDF document
                                                    tempPdfDocument.AddNewPage();

                                                    // Create an iText ConverterProperties object
                                                    ConverterProperties properties = new ConverterProperties();

                                                    // Convert the HTML content to PDF and add it to the temporary PDF document
                                                    HtmlConverter.ConvertToPdf(item, tempPdfDocument, properties);
                                                }
                                            }

                                            // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                            using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                                {
                                                    // Merge the temporary PDF document into the combined PDF document
                                                    tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }

                        // Get the byte array from the MemoryStream
                        pdfBytes = outputStream.ToArray();
                    }

                    var uploadDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile"));
                    if (Directory.Exists(uploadDirectory) == false)
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }


                    //var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");

                    var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var newGuid = Guid.NewGuid().ToString();
                    var documentType = (DocumentType)model.DocumentType;
                    var versionNo = String.IsNullOrEmpty(model.VersionNo) ? "V1" : "V" + (Convert.ToInt32(model.VersionNo.Split(' ')[1]) + 1).ToString();
                    var newDiskFileName = $"{documentType.ToString()}_{model.ProjectRequestId}_{newGuid}_{versionNo}.pdf";

                    var filePath = Path.Combine(uploadDirectory, newDiskFileName);
                    // Write the byte array to the file
                    System.IO.File.WriteAllBytes(filePath, pdfBytes);

                    localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/" + newDiskFileName;
                    storeFilePath = "/DocumentFile/" + newDiskFileName;
                    storeFileName = newDiskFileName;
                }
                return Json(new { Url = localFilePath, FilePath = storeFilePath, FileName = storeFileName });

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> GenerateCertificate(long? projectId)
        {
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                var storeFileName = "";
                var storeFilePath = "";
                bool isSuccess = false;
                var message = "";
                if (projectId is not null)
                {
                    var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    var project = await _ProjectRequestService.GetById(projectId);

                    var getFooterString = DocumentHelper.GetCertificateString(project.entity is not null ? project.entity.ProjectName : "", url);
                    //var getFooterString = RenderRazorViewToString(this, "_CertificateView", project.entity);
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        // Create an iText PdfWriter with the MemoryStream
                        using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                        {
                            // Create an iText PdfDocument with the PdfWriter
                            using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                            {
                                  if (!String.IsNullOrEmpty(getFooterString))
                                    {
                                        // Create a new PdfDocument for each HTML content
                                        using (MemoryStream tempStream = new MemoryStream())
                                        {
                                            using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                                {
                                                    // Create a new page in the temporary PDF document
                                                    tempPdfDocument.AddNewPage();

                                                    // Create an iText ConverterProperties object
                                                    ConverterProperties properties = new ConverterProperties();

                                                    // Convert the HTML content to PDF and add it to the temporary PDF document
                                                    HtmlConverter.ConvertToPdf(getFooterString, tempPdfDocument, properties);
                                                }
                                            }

                                            // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                            using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                            {
                                                using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                                {
                                                    // Merge the temporary PDF document into the combined PDF document
                                                    tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                                }
                                            }
                                        }
                                    }

                                }
                        }

                        // Get the byte array from the MemoryStream
                        pdfBytes = outputStream.ToArray();
                    }

                    var uploadDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile\Certificates"));
                    if (Directory.Exists(uploadDirectory) == false)
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }


                    //var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");

                    var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var newGuid = Guid.NewGuid().ToString();
                    var newDiskFileName = $"{project.entity?.Id.ToString()}_{newGuid}_{currentDateTimeString}.pdf";

                    var filePath = Path.Combine(uploadDirectory, newDiskFileName);
                    // Write the byte array to the file
                    System.IO.File.WriteAllBytes(filePath, pdfBytes);

                    localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/Certificates/" + newDiskFileName;
                    storeFilePath = "/DocumentFile/Certificates/" + newDiskFileName;
                    storeFileName = newDiskFileName;
                    if(storeFileName!=null)
                    {
                        long EnumId = (long)ProjectState.CertificateGenerateAndDownload;
                        var logres = _projectStateLogService.GetLogData(project.entity.Id, EnumId);
                    if (logres.entity == null)
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM
                        {
                            ProjectRequestId = project.entity.Id,
                            ProjectState = ProjectState.CertificateGenerateAndDownload,
                            IsStateCompleted = true
                        };
                        var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                    }
                    }

                    
                }
                return Json(new { Url = localFilePath, FilePath = storeFilePath, FileName = storeFileName });


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<ActionResult> GenerateCertificateAndCreate(long projectId, long allTypeDocId)
        {
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var project = await _ProjectRequestService.GetById(projectId);
            ViewBag.Url = url;

            (ExecutionState executionState, List<ProjectCertificationVM> entity, string message) certificationList = await _projectCertificationService.List();
            //Create Certification
            if (projectId != 0 && allTypeDocId != 0 )
            {
                ProjectCertificationVM certificationVM = new ProjectCertificationVM();
                if (certificationList.entity is not null)
                {
                    bool hasAnyCertificate = certificationList.entity.Where(s => s.AllTypesOfDocumentId == allTypeDocId).Any();
                    if (!hasAnyCertificate)
                    {
                        certificationVM.AllTypesOfDocumentId = allTypeDocId;
                        certificationVM.CertificationStatus = CertificationStatus.GeneratedWithDraft;
                        certificationVM.CertificateFileName = project.entity.ProjectType.ToString() + "_" + project.entity.Id.ToString();
                    }
                }
                else
                {
                    certificationVM.AllTypesOfDocumentId = allTypeDocId;
                    certificationVM.CertificationStatus = CertificationStatus.GeneratedWithDraft;
                    certificationVM.CertificateFileName = project.entity.ProjectType.ToString() + "_" + project.entity.Id.ToString();
                }
                (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse = await _projectCertificationService.Create(certificationVM);


            }

            return View("_CertificateView", project.entity);
        }

        public async Task<IActionResult> GetProjectCompletedList()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();
            (ExecutionState executionState, List<ProjectCertificationVM> entity, string message) certificationResponse = await _projectCertificationService.List();
            var allCertificates = certificationResponse.entity is not null ? certificationResponse.entity : new List<ProjectCertificationVM>();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            List<ProjectCompletedListVM> entityList = new List<ProjectCompletedListVM>();
            var filteredList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentApprovalStatus == DocumentApprovalStatus.Accept).ToList() : new List<AllTypesOfDocumentVM>();

            foreach (var item in filteredList)
            {
                ProjectCompletedListVM project = new ProjectCompletedListVM();
                project.ProjectId = item.ProjectRequest?.Id;
                project.ProjectCode = item.ProjectRequest?.ProjectCode;
                project.ProjectName = item.ProjectRequest?.ProjectName;
                project.ProjectType = item.ProjectRequest?.ProjectType;
                project.ClientName = item.ProjectRequest?.Client?.ClientName;
                project.AllTypesOfDocumentId = item.Id;
                project.CertificationStatus = allCertificates.Count != 0 ? allCertificates.Where(s => s.AllTypesOfDocumentId.Equals(item.Id)).LastOrDefault()?.CertificationStatus : null;
                project.CertificationStatusInt = allCertificates.Count != 0 ? (int)allCertificates.Where(s => s.AllTypesOfDocumentId.Equals(item.Id)).LastOrDefault()?.CertificationStatus : null;
                project.RequestDate = ConvertUTCToBdTime(item.ProjectRequest.RequestDate).ToString("dd/MM/yyyy");
                if(userType != null)
                {
                    if(userType == UserType.ClientAdmin || userType == UserType.Client_User)
                    {
                        if(project.CertificationStatus == null)
                            project.IsShowCertificate = false;
                        else
                            project.IsShowCertificate = true;
                    }
                    else
                        project.IsShowCertificate = true;
                }
                entityList.Add(project);
            };
            return View(entityList.OrderByDescending(s => s.ProjectId).ToList());

        }

        [HttpPost]
        public async Task<ActionResult> CreateAmendment(DocumentAmendmentVM entity)
        {
            try
            {
                bool isSuccess = false;
                if(entity != null)
                {
                    (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse = await _docAmendmentService.Create(entity);
                    isSuccess = returnResponse.executionState == ExecutionState.Created ? true : false;
                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return Json(new { Success = false });
                    }
                }

                return Json(new { Success = isSuccess });
            }
            catch
            {
                return RedirectToAction("PendingList");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDocumentAmendment(DocumentAmendmentVM entity)
        {
            try
            {
                bool isSuccess = false;
                if (entity != null)
                {
                    (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse = await _docAmendmentService.CreateDocAmendment(entity);
                    isSuccess = returnResponse.executionState == ExecutionState.Success ? true : false;
                    if (returnResponse.executionState.ToString() != "Success")
                    {
                        return Json(new { Success = false });
                    }
                }

                return Json(new { Success = isSuccess });
            }
            catch
            {
                return Json(new { Success = false });
            }
        }

        public static string RenderRazorViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine? viewEngine =
                    controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as
                        ICompositeViewEngine;
                ViewEngineResult? viewResult = viewEngine?.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }



        public async Task<IActionResult> ForwardedList()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentAmendmentState == DocumentAmendmentState.Forwarded).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }
        public async Task<IActionResult> ForwardedListForClient()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.ListByClientId(clientId);
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentAmendmentState == DocumentAmendmentState.Forwarded).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }

        public async Task<IActionResult> AmendmentedList()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentAmendmentState == DocumentAmendmentState.Amendmented).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }

        public async Task<IActionResult> AmendmentedListForClient()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.ListByClientId(clientId);
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentAmendmentState == DocumentAmendmentState.Amendmented).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }

        public async Task<IActionResult> ApprovedList()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentApprovalStatus == DocumentApprovalStatus.Accept && s.DocumentAmendmentState == DocumentAmendmentState.Approved).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }
        public async Task<IActionResult> ApprovedListForClient()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ClientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.ListByClientId(ClientId);
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentApprovalStatus == DocumentApprovalStatus.Accept && s.DocumentAmendmentState == DocumentAmendmentState.Approved).Select(x => new AllTypesOfDocumentVM()
            {
                Id = x.Id,
                ProjectRequest = x.ProjectRequest,
                ProjectRequestId = x.ProjectRequestId,
                DocumentType = x.DocumentType,
                TestingType = x.TestingType,
                VersionNo = x.VersionNo,
                EditorContent = x.EditorContent,
                CreatedAt = ConvertUTCToBdTime(x.CreatedAt),
                FileName = x.FileName,
                FilePath = x.FilePath,
                DocumentAmendmentState = x.DocumentAmendmentState,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                ViewVersionNo = x.ViewVersionNo
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();

            return View(entityList);
        }

        public async Task<ActionResult> ForwardDocToClient(int id)
        {
            try
            {
                var DocResult = await _AllTypesDocumentService.GetById(id);
                DocResult.entity.IsActive = true;
                DocResult.entity.DocumentAmendmentState = DocumentAmendmentState.Forwarded;

                (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(DocResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return Json(new { success = false, Data = "", Message = "" }, SerializerOption.Default);
                }
                else
                {
                    return Json(new { success = true, Data = returnResponse, Message = "Document forwarded successfully." }, SerializerOption.Default);
                }

            }
            catch
            {
                return Json(new { success = false, Data = "", Message = "" }, SerializerOption.Default);
            }
        }

        public async Task<ActionResult> AmendmentDetails(int id)
        {
            try
            {
                var DocResult = await _docAmendmentService.DocumentAmendmentByDocId(id);
                return View(DocResult.entity);
            }
            catch
            {
                return View(new DocumentAmendmentVM());
            }
        }

        #region Private Methods
        private DateTime ConvertUTCToBdTime(DateTime dateTime)
        {
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, BdZone);
            return localDateTime;
        }
        #endregion

        #region private method for project state log

        private string InsertLogData(long projectRequestId, DocumentType type)
        {
            long enumId;
            ProjectState projectState;

            switch (type)
            {
                case DocumentType.Agreement:
                    enumId = (long)ProjectState.Agreement;
                    projectState = ProjectState.Agreement;
                    break;
                case DocumentType.Payment:
                    enumId = (long)ProjectState.PaymentAgreement;
                    projectState = ProjectState.PaymentAgreement;
                    break;
                case DocumentType.FunctionalClosure:
                    enumId = (long)ProjectState.FinalClosureReporting;
                    projectState = ProjectState.FinalClosureReporting;
                    break;
                case DocumentType.LoadTesting:
                    enumId = (long)ProjectState.InitialTestReporting;
                    projectState = ProjectState.InitialTestReporting;
                    break;
                default:
                return "Invalid Document Type";
            }

            var logres = _projectStateLogService.GetLogData(projectRequestId, enumId);
            if (logres.entity == null)
            {
                ProjectStateLogVM projectStateLog = new ProjectStateLogVM
                {
                    ProjectRequestId = projectRequestId,
                    ProjectState = projectState,
                    IsStateCompleted = true
                };
                var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
            }

            return "Success";
        }
        #endregion
    }
}
