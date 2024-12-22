using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.eCommerce.Web.Core.Services.Interface.SecurityTestings;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SecurityTestings;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.SecurityTestings
{
    [SessionAuthorize]
    public class SecurityTestingController : Controller
    {
        private readonly ISecurityTestingService _SecurityTestingService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly FileHelper _fileHelper;
        private readonly ITaskService _taskService;

        public SecurityTestingController(HttpHelper httpHelper,FileHelper fileHelper)
        {
            _SecurityTestingService = new SecurityTestingService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _fileHelper = fileHelper;
            _taskService = new TaskService(httpHelper);
        }
        // GET: SecurityTesting
        public async Task<ActionResult> Index()
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
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _taskService.List();
            if (returnResponseTask.entity != null)
            {
                var filterdUser = returnResponseTask.entity.ToList();
                ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle");
            }
            else
            {
                ViewBag.TaskOfProjectId = new SelectList("");
            }
            ViewBag.SeverityLevel = new SelectList(EnumHelper.GetEnumDropdowns<SeverityLevel>(), "Id", "Name");
            ViewBag.EaseOfExploitation = new SelectList(EnumHelper.GetEnumDropdowns<EaseOfExploitation>(), "Id", "Name");
            (ExecutionState executionState, List<SecurityTestingVM> entity, string message) returnResponse = _SecurityTestingService.List();
            return View(returnResponse.entity);
        }

        // GET: SecurityTesting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: SecurityTesting/Create
        public async Task<ActionResult> Create()
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
            ViewBag.SeverityLevel = new SelectList(EnumHelper.GetEnumDropdowns<SeverityLevel>(), "Id", "Name");
            ViewBag.EaseOfExploitation = new SelectList(EnumHelper.GetEnumDropdowns<EaseOfExploitation>(), "Id", "Name");
            ViewBag.TaskOfProjectId = new SelectList("");

            SecurityTestingVM entity = new SecurityTestingVM();
            return View(entity);
        }

        // POST: SecurityTesting/Create
        [HttpPost]
        public ActionResult Create(SecurityTestingVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var SecurityTestingFiles = HttpContext.Request.Form.Files.GetFiles("SecurityTestingFiles");
                    if (SaveFiles(SecurityTestingFiles, ref entity, FileType.Image, out var documentFileError) == false)
                    {
                        return Json(
                            new { Success = false, Message = documentFileError },
                            SerializerOption.Default);
                    }
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.Create(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
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


        // GET: SecurityTesting/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.GetById(id);
            
            ViewBag.SeverityLevel = new SelectList(EnumHelper.GetEnumDropdowns<SeverityLevel>(), "Id", "Name", (int)returnResponse.entity.SeverityLevel);
            ViewBag.EaseOfExploitation = new SelectList(EnumHelper.GetEnumDropdowns<EaseOfExploitation>(), "Id", "Name", (int)returnResponse.entity.EaseOfExploitation);
            

            return View(returnResponse.entity);
        }

        // POST: SecurityTesting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SecurityTestingVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ExistingEntity = _SecurityTestingService.GetById(id);
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(SecurityTestingController.Index), "SecurityTesting");
                    }
                    var SecurityTestingFiles = HttpContext.Request.Form.Files.GetFiles("SecurityTestingFiles");
                    if (SaveFiles(SecurityTestingFiles, ref entity, FileType.Image, out var documentFileError) == false)
                    {
                        return Json(
                            new { Success = false, Message = documentFileError },
                            SerializerOption.Default);
                    }
                    ExistingEntity.entity.IsActive = true;
                    ExistingEntity.entity.IsDeleted = false;
                    ExistingEntity.entity.UpdatedAt = DateTime.Now;
                    ExistingEntity.entity.Recommendation = entity.Recommendation;
                    ExistingEntity.entity.SeverityLevel = entity.SeverityLevel;
                    ExistingEntity.entity.Impact = entity.Impact;
                    ExistingEntity.entity.EaseOfExploitation = entity.EaseOfExploitation;
                    ExistingEntity.entity.Issuedetail = entity.Issuedetail;
                    ExistingEntity.entity.CVSS_Score = entity.CVSS_Score;
                    ExistingEntity.entity.EaseOfExploitation = entity.EaseOfExploitation;
                    ExistingEntity.entity.Vulnerability = entity.Vulnerability;
                    ExistingEntity.entity.VulnerableSection = entity.VulnerableSection;
                    (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.Update(ExistingEntity.entity);
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

        // GET: SecurityTesting/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _SecurityTestingService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "SecurityTesting deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: SecurityTesting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SecurityTestingVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(SecurityTestingController.Index), "SecurityTesting");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse = _SecurityTestingService.Update(entity);
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

        private bool SaveFiles(IReadOnlyList<IFormFile> files, ref SecurityTestingVM entity, FileType fileType, out string error)
        {
            foreach (var file in files)
            {
                var (isSaved, fileName, _) = _fileHelper.SaveFile(file, fileType, "SecurityTesting", out var errorMessage);
                if (isSaved == false)
                {
                    error = errorMessage;
                    return false;
                }

                var entityFile = new SecurityTestingFileVM
                {
                    FileName = file.FileName,
                    FileNameUrl = fileName,
                };

                entity?.SecurityTestingFiles?.Add(entityFile);
            }

            error = string.Empty;
            return true;
        }


        [HttpGet]
        public async Task<ActionResult> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
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
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _taskService.List();
            if (returnResponseTask.entity != null)
            {
                var filterdUser = returnResponseTask.entity.ToList();
                ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle");
            }
            else
            {
                ViewBag.TaskOfProjectId = new SelectList("");
            }

            ViewBag.SeverityLevel = new SelectList(EnumHelper.GetEnumDropdowns<SeverityLevel>(), "Id", "Name");
            ViewBag.EaseOfExploitation = new SelectList(EnumHelper.GetEnumDropdowns<EaseOfExploitation>(), "Id", "Name");
            (ExecutionState executionState, IList<SecurityTestingVM> entity, string message) returnResponse = await _SecurityTestingService.Search(ProjectRequestId, TaskOfProjectId, Vulnerability, SeverityLevel, EaseOfExploitation);

            return View("Index", returnResponse.entity);
        }
    }
}
