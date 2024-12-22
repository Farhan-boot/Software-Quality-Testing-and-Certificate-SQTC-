using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels;
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
using static iText.IO.Util.IntHashtable;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using DocumentApprovalStatus = PTSL.GENERIC.Web.Core.Helper.Enum.DocumentApprovalStatus;
using DocumentType = PTSL.GENERIC.Web.Core.Enum.Documents.DocumentType;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class AgreementDocumentController : Controller
    {
        private readonly IAgreementDocumentService _AgreementDocumentService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IAllTypesDocumentService _AllTypesDocumentService;
        private readonly IUserRoleService _UserRoleService;
        private readonly IUserService _UserService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPermissionHeaderSettingsService _permissionHeaderSettingsService;
        private readonly IApprovalForAllDocumentService _ApprovalForAllDocumentService;
        private readonly IDefaultDocContentService _DefaultDocContentService;
        public AgreementDocumentController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _AgreementDocumentService = new AgreementDocumentService(httpHelper);
            _AllTypesDocumentService = new AllTypesDocumentService(httpHelper);
            _ApprovalForAllDocumentService = new ApprovalForAllDocumentService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _UserRoleService = new UserRoleService(httpHelper);
            _UserService = new UserService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _permissionHeaderSettingsService = new PermissionHeaderSettingsService(httpHelper);
            _DefaultDocContentService = new DefaultDocContentService(httpHelper);
        }
        // GET: AgreementDocument
        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();

            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            var allDocumentApprovalLog = await _ApprovalForAllDocumentService.List();
            var filteredApprovalLog = allDocumentApprovalLog.entity ?? new List<ApprovalForAllDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s=>s.DocumentType == DocumentType.Agreement).Select(x => new AllTypesOfDocumentVM()
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
                ViewVersionNo = x.ViewVersionNo,
                DocumentApprovalStatus = x.DocumentApprovalStatus,
                DocumentAmendmentState = x.DocumentAmendmentState,
                HasEditAndDltPrmsn = filteredApprovalLog.Where(s => s.AllTypesOfDocumentId == x.Id).Any() ? false : true,
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();
            return View(entityList);
        }

        public async Task<IActionResult> PendingAgreementDocs()
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
                long moduleEnumId = (long)ModuleEnum.AgreementApproval;

                var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault();
                var NewResultRoleId = roleResult?.PermissionRowSettings?.LastOrDefault()?.UserRoleId;
                foreach (var item in returnResponse.entity.Where(x => x.DocumentApprovalStatus == DocumentApprovalStatus.Pending && x.DocumentType == DocumentType.Agreement))
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
                        else if (findCurrDocApprovalLog.ReceiverId == userId && CurrentUserRoleId == NewResultRoleId)
                            item.IsApprovalShow = true;
                        else
                            item.IsApprovalShow = false;
                    }
                    else if(item.DocumentAmendmentState == DocumentAmendmentState.Amendmented && findCurrDocApprovalLog?.ReceiverId is null)
                        item.IsApprovalShow = true;
                    else if (findCurrDocApprovalLog != null && findCurrDocApprovalLog.ReceiverId == userId && CurrentUserRoleId == NewResultRoleId)
                    {
                        item.IsApprovalShow = false;
                        item.IsAcceptOrReject = true;
                    }
                    else if (findCurrDocApprovalLog == null)
                        item.IsApprovalShow = true;
                   
                    item.HasEditAndDltPrmsn = hasEditDeletePermission;
                    item.ApprovalMessage = findCurrDocApprovalLog?.Description;

                }
            }
            else
            {
                if (returnResponse.entity != null)
                {
                    foreach (var item in returnResponse.entity.Where(x => x?.DocumentApprovalStatus == DocumentApprovalStatus.Pending && x?.DocumentType == DocumentType.Agreement) ?? new List<AllTypesOfDocumentVM>())
                    {
                        item.IsApprovalShow = true;
                        item.HasEditAndDltPrmsn = true;
                    }
                }
  
            }


            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");

            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentApprovalStatus == DocumentApprovalStatus.Pending && s.DocumentType == DocumentType.Agreement).Select(x => new AllTypesOfDocumentVM()
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
                ApprovalMessage = x.ApprovalMessage,
                IsAcceptOrReject = x.IsAcceptOrReject,
                IsApprovalShow = x.IsApprovalShow,
                ViewVersionNo = x.ViewVersionNo,
                HasEditAndDltPrmsn = x.HasEditAndDltPrmsn
            }).OrderByDescending(s => s.Id).ToList() : new List<AllTypesOfDocumentVM>();
            return View(entityList ?? new List<AllTypesOfDocumentVM>());
        }

        public async Task<JsonResult> GetApprovalProcessModalData(long id)
        {

            // Ensure it's a list
            long moduleEnumId = (long)ModuleEnum.AgreementApproval;
            var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault(); // Retrieve first or default
            var currentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var getAllTypeDoc = await _AllTypesDocumentService.GetById(id);

            if (roleResult != null && roleResult.PermissionRowSettings != null && currentUserRoleId != 1)
            {
                var logResult = await _ApprovalForAllDocumentService.List();
                var FilteredlogResult = logResult.entity is not null ? logResult.entity.Where(x => x.AllTypesOfDocumentId == id).ToList() : new List<ApprovalForAllDocumentVM>();

                var permissionRowIds = getAllTypeDoc.entity.DocumentAmendmentState == DocumentAmendmentState.Amendmented ? FilteredlogResult.Where(s => s.IsAmmendment).Select(x => x.PermissionRowSettingsId).ToList() :
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
        public JsonResult SaveAgreementDocApproval(ApprovalForAllDocumentVM entity)
        {
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;
            entity.DocumentApprovalStatus = DocumentApprovalStatus.Pending;
            entity.ProcessFlowType = ProcessFlowType.Forward;
            entity.DocumentType = DocumentType.Agreement;
            entity.SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ReceiverName = _UserService.GetById(entity.ReceiverId).entity.UserName;
            entity.Description = "Project Forwarded To" + " " + ReceiverName;

            //var senderUserSign = _UserService.GetById(entity.SenderId).entity.SignatureUrl;
            //var fullUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + senderUserSign;
            //entity.SenderRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse = _ApprovalForAllDocumentService.Create(entity);
            //if (returnResponse.executionState == ExecutionState.Created)
            //{
            //    //var getListOfApproval = _ApprovalForAllDocumentService.DocumentCommentHistoryById(entity.AllTypesOfDocumentId);
            //    //List<UserVM> users = new List<UserVM>();
            //    var getDocument = await _AllTypesDocumentService.GetById(entity.AllTypesOfDocumentId);
            //    if (getDocument.entity != null)
            //    {
            //        bool isSuccess = await UpdateDocumentForApproval(getDocument.entity, fullUrl);
            //    }
            //}
            return Json(new { Data = returnResponse, Message = "" }, SerializerOption.Default);
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

                (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse = await _AllTypesDocumentService.Update(DocResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingAgreementDocs");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var ApproverName = _UserService.GetById(SenderId).entity.UserName;
                    ApprovalForAllDocumentVM acceptedDocLog = new ApprovalForAllDocumentVM();
                    acceptedDocLog.AllTypesOfDocumentId = returnResponse.entity.Id;
                    acceptedDocLog.DocumentApprovalStatus = DocumentApprovalStatus.Accept;
                    acceptedDocLog.Description = "Accepted by " + "" + ApproverName;
                    acceptedDocLog.Remarks = "Accepted by " + "" + ApproverName;
                    acceptedDocLog.SenderId = SenderId;
                    acceptedDocLog.DocumentType = DocumentType.Agreement;


                    (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponseLog = _ApprovalForAllDocumentService.Create(acceptedDocLog);
                    if (returnResponseLog.executionState == ExecutionState.Created)
                    {
                        var getDocument = await _AllTypesDocumentService.GetById(acceptedDocLog.AllTypesOfDocumentId);
                        if (getDocument.entity != null)
                        {
                            bool isSuccess = await UpdateDocumentForApproval(getDocument.entity, acceptedDocLog.SenderId.Value);
                        }
                    }

                    if (returnResponseLog.entity != null)
                    {
                        return Json(new { Data = returnResponse, Message = "" }, SerializerOption.Default);
                    }
                    else
                    {
                        return Json(new { Data = "", Message = "" }, SerializerOption.Default);
                    }

                }

            }
            catch
            {
                return RedirectToAction("PendingAgreementDocs");
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
                    return RedirectToAction("PendingAgreementDocs");
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
                        return RedirectToAction("PendingAgreementDocs");
                    }
                    else
                    {
                        return RedirectToAction("PendingAgreementDocs");
                    }


                }

            }
            catch
            {
                return RedirectToAction("PendingList");
            }
        }
        public async Task<bool> UpdateDocumentForApproval(AllTypesOfDocumentVM model, long senderId)
        {
            //   var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);
            try
            {
                byte[] pdfBytes;

                var localFilePath = "";
                bool isSuccess = false;
                var message = "";

                var userList = _UserService.List().entity;
                var project = _ProjectRequestService.GetById(model.ProjectRequestId).Result.entity;
                var clientUser = userList.Where(s => s.ClientId == project.ClientId).FirstOrDefault();
                var clientUserSign = clientUser is null ? "" : $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + clientUser?.SignatureUrl;

                var sqtcUser = _UserService.GetById(senderId).entity;
                var sqtcUserSign = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + sqtcUser.SignatureUrl;

                if (model is not null)
                {
                    var getFooterString = DocumentHelper.GetAgreementFooterByDocumentType(model.DocumentType, clientUser ?? new UserVM(), sqtcUser, clientUserSign, sqtcUserSign);

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
                    var documentType = (Core.Enum.Documents.DocumentType)model.DocumentType;
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

        // GET: AgreementDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: AgreementDocument/Create
        public async Task<ActionResult> CreateAgreementDoc()
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

            ViewBag.TestingType = new SelectList(EnumHelper.GetEnumDropdowns<Core.Enum.Documents.TestingType>(), "Id", "Name");
            return View(new AllTypesOfDocumentVM());
        }
        public async Task<ActionResult> CreateDocAsync()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.GetById(2);
            return View(returnResponse.entity);
        }
        //POST: AgreementDocument/Create
        //[HttpPost]
        //public ActionResult Create(List<AgreementDocumentsVM>)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            entity.IsActive = true;
        //            entity.CreatedAt = DateTime.Now;
        //        TODO: Add insert logic here
        //       (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.Create(entity);
        //            Session["Message"] = returnResponse.message;
        //            Session["executionState"] = returnResponse.executionState;

        //            if (returnResponse.executionState.ToString() != "Created")
        //            {
        //                return View(entity);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
        //        Session["executionState"] = ExecutionState.Failure;
        //        return View(entity);
        //    }
        //    catch
        //    {
        //        Session["Message"] = "Form Data Not Valid.";
        //        Session["executionState"] = ExecutionState.Failure;
        //        return View(entity);
        //    }
        //}



        [HttpPost]
        public ActionResult CreateDocumentList(AgreementDocumentsVM model)
        {
         //   var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);

            byte[] pdfBytes;
            string htmlString = @"<!DOCTYPE html>
                                <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Signature</title>
                                    <!-- Bootstrap CSS -->
                                    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"" rel=""stylesheet"">
                                </head>
                                <body>
                                    <div class=""container"">
                                        <div class=""row"">
                                            <div class=""col"">
                                                <h4>Project Manager</h4>
                                                <img src=""https://upload.wikimedia.org/wikipedia/en/d/d4/Samantha_Signature.jpg"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
                                            </div>
                                            <div class=""col"">
                                                <h4>Project Manager</h4>
                                                <img src=""https://onlinepngtools.com/images/examples-onlinepngtools/marilyn-monroe-signature.png"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
                                            </div>
                                            <div class=""col"">
                                                <h4>Managing Director</h4>
                                                <img src=""https://upload.wikimedia.org/wikipedia/commons/3/38/Alice_Sara_Ott_-_Signature.jpg"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Bootstrap JS (Optional) -->
                                    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js""></script>
                                </body>
                                </html>";
            

            model.Agreement_LastPage = htmlString;
            model.ClientId = 31;
            var result = _AgreementDocumentService.Create(model);
            List<string> pages = new List<string>();
            pages.Add(model.Agreement_FirstPage);
            pages.Add(model.Agreement_SecondPage);
            pages.Add(model.Agreement_ThirdPage);
            pages.Add(model.Agreement_ForthPage);
            pages.Add(model.Agreement_LastPage);


            using (MemoryStream outputStream = new MemoryStream())
            {
                // Create an iText PdfWriter with the MemoryStream
                using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                {
                    // Create an iText PdfDocument with the PdfWriter
                    using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                    {
                        foreach (var htmlContent in pages)
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
                                        HtmlConverter.ConvertToPdf(htmlContent, tempPdfDocument, properties);
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
            // string filePath = Path.Combine(Path.GetTempPath(), "combined.pdf");
            var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");
            var filePath = Path.Combine(testPath, "Test.pdf");
            // Write the byte array to the file
            System.IO.File.WriteAllBytes(filePath, pdfBytes);

            // Return the combined PDF file with the file path
            //return Json(new { filepath = filePath, Filename = "combine.pdf" });
            var localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/Test.Pdf";

            //File(filePath, "application/pdf", "combined.pdf");
            return Ok(localFilePath);
        }

        public ActionResult PdfView()
        {
            return View();
        }
        public ActionResult PdfViewMulti()
        {
            return View();
        }

        // GET: AgreementDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: AgreementDocument/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AgreementDocumentsVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(AgreementDocumentController.Index), "AgreementDocument");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.Update(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

//                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }

        // GET: AgreementDocument/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _AgreementDocumentService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "AgreementDocument deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: AgreementDocument/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AgreementDocumentsVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(AgreementDocumentController.Index), "AgreementDocument");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse = _AgreementDocumentService.Update(entity);
//                Session["Message"] = returnResponse.message;
//                Session["executionState"] = returnResponse.executionState;
                //return View(returnResponse.entity);
                // return RedirectToAction("Edit?id="+id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<JsonResult> GetDefaultDocByType(DocumentType documentType)
        {
            try
            {
                (ExecutionState executionState, DefaultDocumentContentVM entity, string message) returnResponse = await _DefaultDocContentService.GetDefaultDocByDocType(documentType);
                if (returnResponse.entity != null)
                {
                    return Json(new { Data = returnResponse.entity.Content, Message = "" }, SerializerOption.Default);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Data = "", Message = "" }, SerializerOption.Default);
            }
            return Json(new { Data = "", Message = "" }, SerializerOption.Default);
        }

        public async Task<IActionResult> ForwardedList()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse = await _AllTypesDocumentService.List();
            List<AllTypesOfDocumentVM> entityList = new List<AllTypesOfDocumentVM>();
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement && s.DocumentAmendmentState == DocumentAmendmentState.Forwarded).Select(x => new AllTypesOfDocumentVM()
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
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement && s.DocumentAmendmentState == DocumentAmendmentState.Forwarded).Select(x => new AllTypesOfDocumentVM()
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
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement && s.DocumentAmendmentState == DocumentAmendmentState.Amendmented).Select(x => new AllTypesOfDocumentVM()
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
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement && s.DocumentAmendmentState == DocumentAmendmentState.Amendmented).Select(x => new AllTypesOfDocumentVM()
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
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement && s.DocumentApprovalStatus == DocumentApprovalStatus.Accept && s.DocumentAmendmentState == DocumentAmendmentState.Approved).Select(x => new AllTypesOfDocumentVM()
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
            entityList = returnResponse.entity is not null ? returnResponse.entity.Where(s => s.DocumentType == DocumentType.Agreement &&  s.DocumentApprovalStatus == DocumentApprovalStatus.Accept && s.DocumentAmendmentState == DocumentAmendmentState.Approved).Select(x => new AllTypesOfDocumentVM()
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
        #region Private Methods
        private DateTime ConvertUTCToBdTime(DateTime dateTime)
        {
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, BdZone);
            return localDateTime;
        }
        #endregion
    }
}
