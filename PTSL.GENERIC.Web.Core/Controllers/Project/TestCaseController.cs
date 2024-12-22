using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class TestCaseController : Controller
    {
        private readonly ITestCaseService _TestCaseService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly ITaskService _TaskService;
        private readonly ITestCategoryService _TestCategoryService;
        private readonly IUserService _UserService;
        private readonly ITestScenarioService _TestScenarioService;

        public TestCaseController(HttpHelper httpHelper)
        {
            _TestCaseService = new TestCaseService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _TestCategoryService = new TestCategoryService(httpHelper);
            _UserService = new UserService(httpHelper);
            _TestScenarioService = new TestScenarioService(httpHelper);
        }
        // GET: TestCase
        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser.Where(x=>x.ProjectApprovalStatus == ProjectApprovalStatus.Accept) ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponseScenario = await _TestScenarioService.List();
            if (returnResponseScenario.entity != null)
            {
                var filterdUser = returnResponseScenario.entity.ToList();
                ViewBag.TestScenarioId = new SelectList(filterdUser ?? new List<TestScenarioVM>(), "Id", "TestScenarioNo");
            }
            else
            {
                ViewBag.TestScenarioId = new SelectList("");
            }

            (ExecutionState executionState, List<TestCategoryVM> entity, string message) returnResponseCategory = _TestCategoryService.List();
            if (returnResponseCategory.entity != null)
            {
                ViewBag.TestCategoryId = new SelectList(returnResponseCategory.entity, "Id", "Name");
            }
            else
            {
                ViewBag.TestCategoryId = new SelectList("");
            }
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse = _TestCaseService.List();
            return View(returnResponse.entity);
        }

        // GET: TestCase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: TestCase/Create
        public async Task<ActionResult> Create()
        {
            TestCaseVM entity = new TestCaseVM();
            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Accept) ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId= new SelectList("");
            }
            (ExecutionState executionState, List<TestCategoryVM> entity, string message) returnResponseCategory =  _TestCategoryService.List();
            if (returnResponseCategory.entity != null)
            {
                ViewBag.TestCategoryId = new SelectList(returnResponseCategory.entity, "Id", "Name");
            }
            else
            {
                ViewBag.TestCategoryId = new SelectList("");
            }

            (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
            var NewUserResponse = returnResponseUser.entity.Where(x=>(x.UserType == UserType.SQTC_User)||(x.UserType == UserType.SQTC_Admin));
            if (NewUserResponse != null)
            {
                ViewBag.ExecutedByUserId = new SelectList(NewUserResponse, "Id", "UserName");
            }
            else
            {
                ViewBag.ExecutedByUserId = new SelectList("");
            }
            ViewBag.TestResult = new SelectList(EnumHelper.GetEnumDropdowns<TestResult>(),"Id", "Name");
            ViewBag.TaskOfProjectId = new SelectList("");
            ViewBag.TestScenarioId = new SelectList("");

            return View(entity);
        }

        // POST: TestCase/Create
        [HttpPost]
        public ActionResult Create(TestCaseVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;

                var testScenarios = JsonConvert.DeserializeObject<List<TestCaseVM>>(HttpContext.Request.Form["TestCases"]!);

                (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.CreateOfList(testScenarios);
                

                if (returnResponse.executionState.ToString() != "Created")
                {
                    return Json(
                     new
                     {
                         Success = returnResponse.executionState == ExecutionState.Retrieved,
                         Data = returnResponse.entity,
                         Message = returnResponse.message
                     },
                     SerializerOption.Default);

                }
                else
                {
                    return Json(
                    new
                    {
                        Success = returnResponse.executionState == ExecutionState.Retrieved,
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


        // GET: TestCase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.GetById(id);
            ViewBag.TestResult = new SelectList(EnumHelper.GetEnumDropdowns<TestResult>(), "Id", "Name", (int)returnResponse.entity.TestResult);
            
            (ExecutionState executionState, List<UserVM> entity, string message) InsResponse = _UserService.List();
            var NewUserResponse = InsResponse.entity.Where(x => (x.UserType == UserType.SQTC_User) || (x.UserType == UserType.SQTC_Admin));
            ViewBag.ExecutedByUserId = new SelectList(NewUserResponse, "Id", "UserName", returnResponse.entity.ExecutedByUserId);

            return View(returnResponse.entity);
        }

        // POST: TestCase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TestCaseVM entity)
        {
            try
            {
               
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(TestCaseController.Index), "TestCase");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.Update(entity);
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
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }

        // GET: TestCase/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _TestCaseService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "TestCase deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: TestCase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TestCaseVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(TestCaseController.Index), "TestCase");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, TestCaseVM entity, string message) returnResponse = _TestCaseService.Update(entity);
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
        public async Task<ActionResult> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
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
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponseScenario = await _TestScenarioService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseScenario.entity.ToList();
                ViewBag.TestScenarioId = new SelectList(filterdUser ?? new List<TestScenarioVM>(), "Id", "TestScenarioNo");
            }
            else
            {
                ViewBag.TestScenarioId = new SelectList("");
            }

            (ExecutionState executionState, List<TestCategoryVM> entity, string message) returnResponseCategory = _TestCategoryService.List();
            if (returnResponseCategory.entity != null)
            {
                ViewBag.TestCategoryId = new SelectList(returnResponseCategory.entity, "Id", "Name");
            }
            else
            {
                ViewBag.TestCategoryId = new SelectList("");
            }

            ViewBag.TestCaseNo = TestCaseNo;
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse = await _TestCaseService.Search(TestCaseNo, ProjectRequestId, TestScenarioId, TestCategoryId, ActualExecutionDate, PlannedExecutionDate);

            return View("Index", returnResponse.entity);
        }
        [HttpGet]
        public ActionResult GetTestCasesByTaskofProjectId(long id)
        {
            var result = _TestCaseService.GetTestCasesByTaskofProjectId(id);
            if (result.entity == null)
            {
                return Json(new TestCaseVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }
        [HttpGet]
        public async Task<ActionResult> GetTestCaseListByProjectRequestId(long id)
        {
            var result = await _TestCaseService.GetTestCaseListByProjectRequestId(id);
            if (result.entity == null)
            {
                return Json(new TestCaseVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }
    }
}
