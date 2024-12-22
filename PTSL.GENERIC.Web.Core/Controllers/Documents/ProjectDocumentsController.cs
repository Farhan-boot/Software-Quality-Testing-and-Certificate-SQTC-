using iText.Pdfua.Checkers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Documents;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;
using System.Collections.Generic;
using System.Globalization;

namespace PTSL.GENERIC.Web.Core.Controllers.Documents
{
    public class ProjectDocumentsController : Controller
    {
        public const string Uploads = "uploads";
        private readonly IDocumentsService _documentService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IDocumentCategoriesService _documentCategoriesService;
        private readonly IProjectStateLogService _projectStateLogService;
        private readonly IUserService _userService;
        private readonly FileHelper _fileHelper;

        public ProjectDocumentsController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment, FileHelper fileHelper)
        {
            _documentService = new DocumentService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _documentCategoriesService = new DocumentCategoriesService(httpHelper);
            _projectStateLogService = new ProjectStateLogService(httpHelper);
            _userService = new UserService(httpHelper);
            _fileHelper = fileHelper;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectListByClientId(clientId);
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
                ViewBag.ProjectRequestId = new SelectList("");

            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponseDocCategory = _documentCategoriesService.List();
            if (returnResponseDocCategory.entity != null)
            {
                var filterdUser = returnResponseDocCategory.entity.ToList();
                ViewBag.DocumentCategoriesId = new SelectList(filterdUser ?? new List<DocumentCategoriesVM>(), "Id", "Name");
            }
            else
                ViewBag.DocumentCategoriesId = new SelectList("");
            (ExecutionState executionState, List<DocumentsByTypeVM> entity, string message) returnResponse = await _documentService.List();
            return View(returnResponse.entity);
        }
        public async Task<IActionResult> ClientWiseIndex()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
                ViewBag.ProjectRequestId = new SelectList("");

            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponseDocCategory = _documentCategoriesService.List();
            if (returnResponseDocCategory.entity != null)
            {
                var filterdUser = returnResponseDocCategory.entity.ToList();
                ViewBag.DocumentCategoriesId = new SelectList(filterdUser ?? new List<DocumentCategoriesVM>(), "Id", "Name");
            }
            else
                ViewBag.DocumentCategoriesId = new SelectList("");
            var ClientUserData = _userService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
            var ClientId = ClientUserData.entity.ClientId;
            (ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message) returnResponse = await _documentService.DocumentsListByClientId(ClientId.Value);
            return View(returnResponse.entity);
        }

        public async Task<IActionResult> Create() 
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectListByClientId(clientId);
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.Where(s => s.ProjectApprovalStatus == ProjectApprovalStatus.Accept).ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
                ViewBag.ProjectRequestId = new SelectList("");

            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponseDocCategory = _documentCategoriesService.List();
            if (returnResponseDocCategory.entity != null)
            {
                var filterdUser = returnResponseDocCategory.entity.ToList();
                ViewBag.DocumentCategoriesId = new SelectList(filterdUser ?? new List<DocumentCategoriesVM>(), "Id", "Name");
            }
            else
                ViewBag.DocumentCategoriesId = new SelectList("");
            return View();
        }

        [HttpPost]
        public ActionResult Create(List<DocumentsByTypeVM> entity)
        {
            try
            {

                //entity.IsActive = true;
                //entity.CreatedAt = DateTime.Now;

                var documentsByTypes = JsonConvert.DeserializeObject<List<DocumentsByTypeVM>>(HttpContext.Request.Form["ProjectDocuments"]!);
                var documentFiles = HttpContext.Request.Form.Files;

                if(documentsByTypes.Any() && documentFiles.Any())
                {
                    List<DocumentsByTypeVM> requestModel = new List<DocumentsByTypeVM>();
                    for (int item = 0; item < documentsByTypes.Count; item++)
                    {
                        DocumentsByTypeVM document = new DocumentsByTypeVM();
                        document.ProjectRequestId = documentsByTypes[item].ProjectRequestId;
                        document.DocumentCategoriesId = documentsByTypes[item].DocumentCategoriesId;
                        document.DocumentModuleType = Helper.Enum.Documents.DocumentModuleType.Project;
                        document.DocumentPurpose = documentsByTypes[item].DocumentPurpose;
                        document.DocumentTitle = documentsByTypes[item].DocumentTitle;

                        var fileHelper = new FileHelper(_hostEnvironment);
                        var documentToSave = documentFiles[item];
                        var result = fileHelper.SaveFileAll(documentToSave!, "ProjectDocuments", out var errorMessage);
                        if (errorMessage != null)
                        {
                            // errror
                        }

                        document.DoumentPath = result.Item2;
                        document.DocumentUniqueName = result.Item4;

                        requestModel.Add(document);
                    }
                    (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse = _documentService.CreateOfList(requestModel);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        //HttpContext.Session.SetString("Message", "Test Scenarios has been created successfully");
                        //HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        //return RedirectToAction("Index");

                        return Json(
                         new
                         {
                             Success = returnResponse.executionState == ExecutionState.Success ? true : false,
                             Data = returnResponse.entity,
                             Message = returnResponse.message
                         },
                         SerializerOption.Default);

                    }
                    else
                    {
                        //HttpContext.Session.SetString("Message", "Test Scenarios has been created successfully");
                        //HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        long EnumId = (long)ProjectState.InitialDocumentsShared;

                        var logres = _projectStateLogService.GetLogData(returnResponse.entity.ProjectRequestId, EnumId);
                        if (logres.entity == null)
                        {
                            ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                            projectStateLog.ProjectRequestId = returnResponse.entity.ProjectRequestId;
                            projectStateLog.ProjectState = ProjectState.InitialDocumentsShared;
                            projectStateLog.IsStateCompleted = true;
                            var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                        }

                        return Json(
                        new
                        {
                            Success = returnResponse.executionState == ExecutionState.Created ? true : false,
                            Data = returnResponse.entity,
                            Message = returnResponse.message
                        },
                        SerializerOption.Default);

                    }
                }
                else
                {
                    return Json(
                         new
                         {
                             Success = false,
                             Data = new DocumentsByTypeVM(),
                             Message = "No file found"
                         },
                         SerializerOption.Default);
                }
                

            }
            catch
            {
                return View(entity);
            }
        }

        public ActionResult Delete(long id)
        {
            try
            {
                (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse = _documentService.Delete(id);
                return Json(
                         new
                         {
                             Success = returnResponse.executionState == ExecutionState.Updated ? true : false,
                             Data = returnResponse.entity,
                             Message = returnResponse.message
                         },
                         SerializerOption.Default);
            }
            catch (Exception ex)
            {
                return Json(
                         new
                         {
                             Success = false,
                             Data = new DocumentsByTypeVM(),
                             Message = "Delete failed!!"
                         },
                         SerializerOption.Default);
            }
        }

        public ActionResult Details(int? id , string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse = _documentService.GetById(id);
            return View(returnResponse.entity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse = _documentService.GetById(id);
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName", returnResponse.entity.ProjectRequestId);
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }
            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponseDocCategory = _documentCategoriesService.List();
            if (returnResponseDocCategory.entity != null)
            {
                var filterdUser = returnResponseDocCategory.entity.ToList();
                ViewBag.DocumentCategoriesId = new SelectList(filterdUser ?? new List<DocumentCategoriesVM>(), "Id", "Name", returnResponse.entity.DocumentCategoriesId);
            }
            else
            {
                ViewBag.DocumentCategoriesId = new SelectList("");
            }

            return View(returnResponse.entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id,DocumentsByTypeVM entity)
        {
            var GetDocumentData = _documentService.GetById(id).entity;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.UpdatedAt = DateTime.Now;
            GetDocumentData.ProjectRequestId = entity.ProjectRequestId;
            GetDocumentData.DocumentCategoriesId = entity.DocumentCategoriesId;
            GetDocumentData.DocumentPurpose = entity.DocumentPurpose;
            GetDocumentData.DocumentTitle = entity.DocumentTitle;
            var DocumentFile = HttpContext.Request.Form.Files.GetFile("ProjectDoc");
            var result = SaveFile(DocumentFile!, "Client", out var errorMessage);
            GetDocumentData.DoumentPath = result.Url;

            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse =  _documentService.Update(GetDocumentData);
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

        [HttpGet]
        public async Task<ActionResult> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle)
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
            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponseDocCategory = _documentCategoriesService.List();
            if (returnResponseDocCategory.entity != null)
            {
                var filterdUser = returnResponseDocCategory.entity.ToList();
                ViewBag.DocumentCategoriesId = new SelectList(filterdUser ?? new List<DocumentCategoriesVM>(), "Id", "Name");
            }
            else
            {
                ViewBag.DocumentCategoriesId = new SelectList("");

            }

            ViewBag.DocumentTitle = DocumentTitle;

            (ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message) returnResponse = await _documentService.Search(ProjectRequestId, DocumentCategoriesId, DocumentTitle);

            return View("Index", returnResponse.entity);
        }

        public (bool IsSaved, string Url, string FileName) SaveFile(IFormFile file, string directoryName, out string errorMessage)
        {
            if (file is null)
            {
                errorMessage = "File not found";
                return (false, string.Empty, string.Empty);
            }
            if (string.IsNullOrEmpty(directoryName))
            {
                errorMessage = "Directory name must not be empty";
                return (false, string.Empty, string.Empty);
            }

            // Create upload directory is not exists
            var uploadDirectory = Path.GetFullPath(Path.Combine(_hostEnvironment.ContentRootPath, "..", Uploads));
            if (Directory.Exists(uploadDirectory) == false)
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            //var fileName = file.FileName;
            var fileExtension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);

            // New file name
            var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var newGuid = Guid.NewGuid().ToString();
            var newDiskFileName = $"{currentDateTimeString}_{newGuid}{fileExtension}";

            // Create save directory is not exists
            var saveDirectory = Path.Combine(uploadDirectory, directoryName);
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Save file
            var newDiskFilePath = Path.Combine(saveDirectory, newDiskFileName);
            var publicFileUrl = $"/{Uploads}/{directoryName}/{newDiskFileName}";
            try
            {
                using var fileStream = new FileStream(newDiskFilePath, FileMode.Create, FileAccess.Write);
                file.CopyTo(fileStream);
            }
            catch (Exception)
            {
                errorMessage = "Unable to save file unknown error occurred";
                return (false, string.Empty, string.Empty);
            }

            errorMessage = string.Empty;
            return (true, publicFileUrl, file.FileName);
        }

    }
}
