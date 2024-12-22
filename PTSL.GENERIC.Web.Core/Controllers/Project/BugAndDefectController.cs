using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Bugzilla;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;
using static iText.IO.Util.IntHashtable;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class BugAndDefectController : Controller
    {
        private readonly IBugAndDefectService _BugAndDefectService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly ITaskService _TaskService;
        private readonly ITestCaseService _TestCaseService;
        private readonly FileHelper _fileHelper;
        private IProjectStateLogService _projectStateLogService;

        public BugAndDefectController(HttpHelper httpHelper, FileHelper fileHelper)
        {
            _BugAndDefectService = new BugAndDefectService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _TestCaseService = new TestCaseService(httpHelper);
            _fileHelper = fileHelper;
            _projectStateLogService = new ProjectStateLogService(httpHelper);
        }
        // GET: BugAndDefect
        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectAcceptedList();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
                ViewBag.ApprovedProjectId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
                ViewBag.ApprovedProjectId = new SelectList("");
            }
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _TaskService.List();
            if (returnResponseTask.entity != null)
            {
                var filterdUser = returnResponseTask.entity.ToList();
                ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle");
            }
            else
            {
                ViewBag.TaskOfProjectId = new SelectList("");
            }
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponseTestCase = _TestCaseService.List();
            if (returnResponseTestCase.entity != null)
            {
                var filterdUser = returnResponseTestCase.entity.ToList();
                ViewBag.TestCaseId = new SelectList(filterdUser ?? new List<TestCaseVM>(), "Id", "TestCaseNo");
            }
            else
            {
                ViewBag.TestCaseId = new SelectList("");
            }
            ViewBag.BugAndDefectSeverity = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectSeverity>(), "Id", "Name");
            ViewBag.BugAndDefectStatus = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectStatus>(), "Id", "Name");
            (ExecutionState executionState, List<BugAndDefectVM> entity, string message) returnResponse = _BugAndDefectService.List();
            return View(returnResponse.entity);
        }

        // GET: BugAndDefect/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: BugAndDefect/Create
        public async Task<ActionResult> Create()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectAcceptedList();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }
            ViewBag.TaskOfProjectId = new SelectList("");
            ViewBag.TestCaseId = new SelectList("");
            ViewBag.BugAndDefectSeverity = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectSeverity>(), "Id", "Name");
            ViewBag.BugAndDefectStatus = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectStatus>(), "Id", "Name");
            BugAndDefectVM entity = new BugAndDefectVM();
            return View(entity);
        }

        // POST: BugAndDefect/Create
        [HttpPost]
        public ActionResult Create(BugAndDefectVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;
                var documentFiles = HttpContext.Request.Form.Files.GetFiles("BugAndDefectFiles");
                if (SaveFiles(documentFiles, ref entity, FileType.Document, out var documentFileError) == false)
                {
                    return Json(
                        new { Success = false, Message = documentFileError },
                        SerializerOption.Default);
                }
                (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.Create(entity);


                if (returnResponse.executionState.ToString() != "Created")
                {
                    return View(entity);
                }
                else
                {
                    long EnumId = (long)ProjectState.TestingInProgress;
                    var logres = _projectStateLogService.GetLogData(returnResponse.entity.ProjectRequestId, EnumId);
                    if (logres.entity == null)
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM
                        {
                            ProjectRequestId = returnResponse.entity.ProjectRequestId,
                            ProjectState = ProjectState.TestingInProgress,
                            IsStateCompleted = true
                        };
                        var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                    }
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View(entity);
            }
        }


        // GET: BugAndDefect/Edit/5
        public async Task <ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.GetById(id);
            ViewBag.BugAndDefectSeverity = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectSeverity>(), "Id", "Name", (int)returnResponse.entity.BugAndDefectSeverity);
            ViewBag.BugAndDefectStatus = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectStatus>(), "Id", "Name", (int)returnResponse.entity.BugAndDefectStatus);
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectAcceptedList();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName", returnResponse.entity.ProjectRequestId);
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }


            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _TaskService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseTask.entity.ToList();
                ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle", returnResponse.entity.TaskOfProjectId);
            }
            else
            {
                ViewBag.TaskOfProjectId = new SelectList("");
            }
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponseTestCase = _TestCaseService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseTestCase.entity.ToList();
                ViewBag.TestCaseId = new SelectList(filterdUser ?? new List<TestCaseVM>(), "Id", "TestCaseNo", returnResponse.entity.TestCaseId);
            }
            else
            {
                ViewBag.TestCaseId = new SelectList("");
            }

            return View(returnResponse.entity);
        }

        // POST: BugAndDefect/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BugAndDefectVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(BugAndDefectController.Index), "BugAndDefect");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.Update(entity);
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

        // GET: BugAndDefect/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _BugAndDefectService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "BugAndDefect deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: BugAndDefect/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BugAndDefectVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(BugAndDefectController.Index), "BugAndDefect");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = _BugAndDefectService.Update(entity);
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

        [HttpGet]
        public async Task<ActionResult> Search(long? ProjectRequestId,long? TaskOfProjectId,long? TestCaseId,string? bugzillaId,string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectAcceptedList();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
                ViewBag.ApprovedProjectId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
                ViewBag.ApprovedProjectId = new SelectList("");
            }
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _TaskService.List();
            if (returnResponseTask.entity != null)
            {
                var filterdUser = returnResponseTask.entity.ToList();
                ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle");
            }
            else
            {
                ViewBag.TaskOfProjectId = new SelectList("");
            }
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponseTestCase =  _TestCaseService.List();
            if (returnResponseTestCase.entity != null)
            {
                var filterdUser = returnResponseTestCase.entity.ToList();
                ViewBag.TestCaseId = new SelectList(filterdUser ?? new List<TestCaseVM>(), "Id", "TestCaseNo");
            }
            else
            {
                ViewBag.TestCaseId = new SelectList("");
            }
            ViewBag.bugzillaId = bugzillaId;
            ViewBag.defectId = defectId;
            ViewBag.BugAndDefectSeverity = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectSeverity>(), "Id", "Name");
            ViewBag.BugAndDefectStatus = new SelectList(EnumHelper.GetEnumDropdowns<BugAndDefectStatus>(), "Id", "Name");
            (ExecutionState executionState, IList<BugAndDefectVM> entity, string message) returnResponse = await _BugAndDefectService.Search(ProjectRequestId, TaskOfProjectId, TestCaseId,bugzillaId,defectId, bugAndDefectStatus, bugAndDefectSeverity);
            
            return View("Index", returnResponse.entity);
        }
        public async Task<ActionResult> SyncBugsByProjectId (long projectId)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse = await _BugAndDefectService.SyncBugsFromBugzillaByProjId(projectId);
            return Json(new
            {
                Data = returnResponse.entity,
                Success = returnResponse.executionState == ExecutionState.Updated ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);

        }

        public async Task<ActionResult> GetSyncViewWithBugzillaById(long projectId)
        {
            (ExecutionState executionState, BugzillaSyncVM entity, string message) returnResponse = await _BugAndDefectService.SyncBugsDataViewByProjId(projectId);
            return Json(new
            {
                Data = returnResponse.entity,
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);

        }
        private bool SaveFiles(IReadOnlyList<IFormFile> files, ref BugAndDefectVM entity, FileType fileType, out string error)
        {
            foreach (var file in files)
            {
                var (isSaved, fileName, _) = _fileHelper.SaveFile(file, fileType, "CIP", out var errorMessage);
                if (isSaved == false)
                {
                    error = errorMessage;
                    return false;
                }

                var entityFile = new BugAndDefectFileVM
                {
                    FileName = file.FileName,
                    FileNameUrl = fileName,
                };

                entity?.BugAndDefectFiles?.Add(entityFile);
            }

            error = string.Empty;
            return true;
        }

    }
}
