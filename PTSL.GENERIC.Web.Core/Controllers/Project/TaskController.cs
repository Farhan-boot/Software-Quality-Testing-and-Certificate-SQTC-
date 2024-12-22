using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Project;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers.Project
{
    public class TaskController : Controller
    {
        public const string Uploads = "uploads";
        private readonly ITaskService _TaskService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IUserService _UserService;
        public TaskController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _TaskService = new TaskService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _UserService = new UserService(httpHelper);
        }

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
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            if (returnResponseUser.entity != null)
            {
                var filterdUser = returnResponseUser.entity.Where(s => s.UserType == UserType.SQTC_User).ToList();
                ViewBag.AssigneeId = new SelectList(filterdUser ?? new List<UserVM>(), "Id", "UserName");
            }
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse = await _TaskService.List();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            foreach (var item in returnResponse.entity)
            {
                if(item.UserId == userId)
                {
                    item.TimeTrackShow = true;
                }
                else
                {
                    item.TimeTrackShow= false;
                }
            }
            //ViewBag.TaskStatusId = new SelectList(EnumHelper.GetEnumDropdowns<TaskOfProjectStatus>(), "Id", "Name");
            return View(returnResponse.entity ?? new List<TaskVM>());
        }

        public async Task<ActionResult> Create()
        {
            TaskVM entity = new TaskVM();
            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) returnResponseTimeTrack = _TaskService.GetTaskTimeTrackList();
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTasks = await _TaskService.List();
            if (returnResponseUser.entity != null)
            {
                var filterdUser = returnResponseUser.entity.Where(s=>s.UserType == UserType.SQTC_User).ToList();
                var allTasks = returnResponseTasks.entity;
                var allTimeTracks = returnResponseTimeTrack.entity;
                List<UserVM> users = new List<UserVM>();
                foreach (var dbuser in filterdUser)
                {
                    UserVM user = new UserVM(); 
                    user.Id = dbuser.Id; ;
                    var totalAssignedTask = allTasks.Where(s => s.UserId == user.Id).ToList();
                    var totalAssignedTaskTimes = totalAssignedTask.Sum(x=>x.TaskEstimationHour);
                    var allTaskIds = totalAssignedTask.Select(m => m.Id).ToList();
                    var totalTimeTracks = allTimeTracks.Where(s => allTaskIds.Contains(s.TaskOfProjectId)).Sum(z => z.TimeSpentHour);
                    user.UserName = "Name: " + dbuser.UserName + ", Total Task: " + totalAssignedTask.Count + " Remaining Task: " + (totalAssignedTaskTimes - totalTimeTracks) + " hr";

                    users.Add(user);
                }
                ViewBag.UserId = new SelectList(users ?? new List<UserVM>(), "Id", "UserName");
            }

            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            ViewBag.TaskPriority = new SelectList(EnumHelper.GetEnumDropdowns<TaskPriority>(), "Id", "Name");
            ViewBag.ProjectRequestId = new SelectList("");
            ViewBag.TaskTypeId = new SelectList("");
            
            return View(entity);
        }

        public async Task<ActionResult> Edit(long id)
        {
            //TaskVM entity = new TaskVM();
            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

            (ExecutionState executionState, TaskVM entity, string message) returnResponseTask = await _TaskService.GetById(id);

            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) returnResponseTimeTrack = _TaskService.GetTaskTimeTrackList();
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTasks = await _TaskService.List();
            if (returnResponseUser.entity != null)
            {
                var filterdUser = returnResponseUser.entity.Where(s => s.UserType == UserType.SQTC_User).ToList();
                var allTasks = returnResponseTasks.entity;
                var allTimeTracks = returnResponseTimeTrack.entity;
                List<UserVM> users = new List<UserVM>();
                foreach (var dbuser in filterdUser)
                {
                    UserVM user = new UserVM();
                    user.Id = dbuser.Id; ;
                    var totalAssignedTask = allTasks.Where(s => s.UserId == user.Id).ToList();
                    var totalAssignedTaskTimes = totalAssignedTask.Sum(x => x.TaskEstimationHour);
                    var allTaskIds = totalAssignedTask.Select(m => m.Id).ToList();
                    var totalTimeTracks = allTimeTracks.Where(s => allTaskIds.Contains(s.TaskOfProjectId)).Sum(z => z.TimeSpentHour);
                    user.UserName = "Name: " + dbuser.UserName + ", Total Task: "+ totalAssignedTask.Count + " Remaining Task: " + (totalAssignedTaskTimes - totalTimeTracks)+" hr";
                    users.Add(user);
                }
                ViewBag.UserId = new SelectList(users ?? new List<UserVM>(), "Id", "UserName", returnResponseTask.entity?.UserId ?? 0);
            }

            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name", (Int64)returnResponseTask.entity.ProjectType);
            ViewBag.TaskPriority = new SelectList(EnumHelper.GetEnumDropdowns<TaskPriority>(), "Id", "Name", (Int64)returnResponseTask.entity.TaskPriority);
            
            var model = ListProjectsAndTaskTypesByProjectType((Int64)returnResponseTask.entity.ProjectType);
            ViewBag.ProjectRequestId = new SelectList(model.ProjectRequests, "Id", "ProjectName", returnResponseTask.entity.ProjectRequestId);
            ViewBag.TaskTypeId = new SelectList(model.TaskTypes, "Id", "TaskTypeName", returnResponseTask.entity.TaskTypeId);

            return View(returnResponseTask.entity);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(TaskVM entity)
        {
            try
            {
                _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

                //Remove Validation for Description
                //ModelState.Remove("TaskDescription");
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    var taskFile = HttpContext.Request.Form.Files.GetFile("TaskFile");
                    if (!string.IsNullOrEmpty(Request.Form["TaskDeadline"]))
                    {
                        entity.TaskDeadline = Convert.ToDateTime(Request.Form["TaskDeadline"]);
                    }
                    var fileHelper = new FileHelper(_hostEnvironment);
                    var result = fileHelper.SaveFileAll(taskFile!, "Task", out var errorMessage);

                    entity.TaskFilePath = result.Item2;
                    entity.TaskFileName = result.Item3;
                    //entity.ProjectType = (ProjectType)Enum.ToObject(typeof(ProjectType), entity.pro);
                    entity.TaskDescription = entity.TaskDescription == null ? string.Empty : entity.TaskDescription;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, TaskVM entity, string message) returnResponse = await _TaskService.Update(entity);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        HttpContext.Session.SetString("Message", returnResponse.message);
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        return View(entity);
                    }
                    else
                    {
                        HttpContext.Session.SetString("Message", "Task has been updated successfully");
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        return RedirectToAction("Index");
                    }
                }
                
                return View(entity);
            }
            catch
            {
                return View(entity);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskVM entity)
        {
            try
            {
                _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

                //Remove Validation for Description
                ModelState.Remove("TaskDescription");
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    var taskFile = HttpContext.Request.Form.Files.GetFile("TaskFile");
                    if (!string.IsNullOrEmpty(Request.Form["TaskDeadline"]))
                    {
                        entity.TaskDeadline = Convert.ToDateTime(Request.Form["TaskDeadline"]);
                    }
                    var fileHelper = new FileHelper(_hostEnvironment);
                    var result = fileHelper.SaveFileAll(taskFile!, "Task", out var errorMessage);
                    if (errorMessage != null)
                    {
                        // errror
                    }

                    entity.TaskFilePath = result.Item2;
                    entity.TaskFileName = result.Item3;
                    //entity.ProjectType = (ProjectType)Enum.ToObject(typeof(ProjectType), entity.pro);
                    entity.TaskDescription = entity.TaskDescription == null ? string.Empty : entity.TaskDescription;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, TaskVM entity, string message) returnResponse = await _TaskService.Create(entity);
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

        public ActionResult ListProjectsAndTaskTypes(long id)
        {
            var result = _TaskService.GetProjectsAndTaskTypes(id);
            if (result.entity == null)
            {
                return Json(new TaskCascadingDDLVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }
        public TaskCascadingDDLVM ListProjectsAndTaskTypesByProjectType(long id)
        {
            var result = _TaskService.GetProjectsAndTaskTypes(id);
            if (result.entity == null)
            {
                return new TaskCascadingDDLVM();
            }

            return result.entity;
        }

        public ActionResult GetTaskListsByProJectId(long id)
        {
            var result = _TaskService.GetTaskListByProjectId(id);
            if (result.entity == null)
            {
                return Json(new TaskVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }

        [HttpPost]
        public JsonResult SaveTimeTracking(TaskTimeTrackingVM entity)
        {
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;
            entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, TaskTimeTrackingVM entity, string message) returnResponse = _TaskService.CreateTimeTracking(entity);
            if(returnResponse.executionState == ExecutionState.Created)
            {
               var result = _TaskService.GetById(entity.TaskOfProjectId);
                result.Result.entity.TaskOfProjectStatus = entity.TaskOfProjectStatus;
                Task<(ExecutionState executionState, TaskVM entity, string message)> UpdateResponse =  _TaskService.Update(result.Result.entity);
            }

            return Json(new { Data = returnResponse, Success = returnResponse.executionState == ExecutionState.Created ? true : false,  Message = returnResponse.message }, SerializerOption.Default);
        }

        public async Task<ActionResult> UserWiseIndex()
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
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            if (returnResponseUser.entity != null)
            {
                var filterdUser = returnResponseUser.entity.Where(s => s.UserType == UserType.SQTC_User).ToList();
                ViewBag.AssigneeId = new SelectList(filterdUser ?? new List<UserVM>(), "Id", "UserName");
            }
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse = await _TaskService.GetTaskListByUserId(userId);
            
            //ViewBag.TaskStatusId = new SelectList(EnumHelper.GetEnumDropdowns<TaskOfProjectStatus>(), "Id", "Name");
            return View(returnResponse.entity);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TaskVM entity, string message) returnResponse = await _TaskService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: TestStep/Delete/5
        public async Task<JsonResult> Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = await _TaskService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TaskVM entity, string message) returnResponse = await _TaskService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "TestStep deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate,DateTime? Deadline)
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
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            if (returnResponseUser.entity != null)
            {
                var filterdUser = returnResponseUser.entity.Where(s => s.UserType == UserType.SQTC_User).ToList();
                ViewBag.AssigneeId = new SelectList(filterdUser ?? new List<UserVM>(), "Id", "UserName");
            }

            ViewBag.TaskId = TaskId;
            ViewBag.CreateDate = CreateDate.ToString();
            ViewBag.Deadline = Deadline.ToString();
            (ExecutionState executionState, IList<TaskVM> entity, string message) returnResponse = await _TaskService.Search(ProjectRequestId,TaskId, AssigneeId, CreateDate,Deadline);

            return View("Index", returnResponse.entity);
        }
    }
}
