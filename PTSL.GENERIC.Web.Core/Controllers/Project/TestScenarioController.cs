using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
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
    public class TestScenarioController : Controller
    {
        public const string Uploads = "uploads";
        private readonly ITestScenarioService _TestScenarioService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IUserService _UserService;
        private readonly IProjectStateLogService _ProjectStateLogService;
        public TestScenarioController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _TestScenarioService = new TestScenarioService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _ProjectStateLogService = new ProjectStateLogService(httpHelper);
            _UserService = new UserService(httpHelper);
        }

        public async Task<ActionResult> Index()
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
            ViewBag.TaskPriority = new SelectList(EnumHelper.GetEnumDropdowns<TaskPriority>(), "Id", "Name");
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponse = await _TestScenarioService.List();

            ViewBag.TaskOfProjectId = new SelectList("");
            //ViewBag.TaskStatusId = new SelectList(EnumHelper.GetEnumDropdowns<TaskOfProjectStatus>(), "Id", "Name");
            return View(returnResponse.entity);
        }

        public async Task<ActionResult> Create()
        {
            TestScenarioVM entity = new TestScenarioVM();
            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

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
            
            return View(entity);
        }

        [HttpPost]
        public ActionResult Create(TestScenarioVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;

                var testScenarios = JsonConvert.DeserializeObject<List<TestScenarioVM>>(HttpContext.Request.Form["TestScenarios"]!);

                (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse = _TestScenarioService.CreateOfList(testScenarios);
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
                    long EnumId = (long)ProjectState.TestPlanning;

                    var logres = _ProjectStateLogService.GetLogData(returnResponse.entity.ProjectRequestId, EnumId);
                    if (logres.entity == null)
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                        projectStateLog.ProjectRequestId = returnResponse.entity.ProjectRequestId;
                        projectStateLog.ProjectState = ProjectState.TestPlanning;
                        projectStateLog.IsStateCompleted = true;
                        var ProjectStateResult = _ProjectStateLogService.Create(projectStateLog);
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
            catch
            {
                return View(entity);
            }
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse =  _TestScenarioService.GetById(id);
            return View(returnResponse.entity);
        }


        [HttpPost]
        public JsonResult SaveTestScenarios(List<TestScenarioVM> entity)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse = _TestScenarioService.CreateOfList(entity);

            return Json(new { Data = returnResponse, Success = returnResponse.executionState == ExecutionState.Created ? true : false,  Message = returnResponse.message }, SerializerOption.Default);
        }

        public JsonResult GetTestScenarioById(long scenarioId)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse = _TestScenarioService.GetById(scenarioId);
            var responseModel = returnResponse.entity;
            responseModel.PlannedExecutionDateString = responseModel.PlannedExecutionDate.HasValue ? responseModel.PlannedExecutionDate.Value.ToString("MM/dd/yyyy") : "";
            responseModel.ActualExecutionDateString = responseModel.ActualExecutionDate.HasValue ? responseModel.ActualExecutionDate.Value.ToString("MM/dd/yyyy") : "";
            return Json(new { Data = returnResponse.entity, Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false, Message = returnResponse.message }, SerializerOption.Default);
        }

        public ActionResult UpdateTestScenario(TestScenarioVM model)
        {
            model.ScenarioDescription = model.ScenarioDescription is null ? string.Empty : model.ScenarioDescription;
            Task<(ExecutionState executionState, TestScenarioVM entity, string message)> returnResponse = _TestScenarioService.Update(model);
            var responseModel = returnResponse.Result.entity;
            return Json(new { Data = returnResponse.Result.entity, Success = returnResponse.Result.executionState == ExecutionState.Updated ? true : false, Message = returnResponse.Result.executionState == ExecutionState.Updated ? "Test Scenario update successfully." : returnResponse.Result.message }, SerializerOption.Default);
        }

        public ActionResult GetTestScenarioByTaskId(long id)
        {
            var result = _TestScenarioService.GetTestScenarioByTaskId(id);
            if (result.entity == null)
            {
                return Json(new TaskVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }

        // GET: TestStep/Delete/5
        public async Task<JsonResult> Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = await  _TestScenarioService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse = await _TestScenarioService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Test Scenario deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search(long? ProjectRequestId, string? TestScenarioNo, TaskPriority? taskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
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

            ViewBag.TaskPriority = new SelectList(EnumHelper.GetEnumDropdowns<TaskPriority>(), "Id", "Name");
            ViewBag.TaskOfProjectId = new SelectList("");
            ViewBag.TestScenarioNo = TestScenarioNo;
            ViewBag.ActualExecutionDate = ActualExecutionDate;
            ViewBag.PlannedExecutionDate = PlannedExecutionDate;
            (ExecutionState executionState, IList<TestScenarioVM> entity, string message) returnResponse = await _TestScenarioService.Search(ProjectRequestId, TestScenarioNo, taskPriority, CreatedBy, PlannedExecutionDate, ActualExecutionDate);
            
            return View("Index", returnResponse.entity);
        }
    }
}
