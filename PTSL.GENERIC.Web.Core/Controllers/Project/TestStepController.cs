using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class TestStepController : Controller
    {
        private readonly ITestStepService _TestStepService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly ITaskService _TaskService;
        private readonly ITestCaseService _TestCaseService;

        public TestStepController(HttpHelper httpHelper)
        {
            _TestStepService = new TestStepService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _TestCaseService= new TestCaseService(httpHelper);
        }
        // GET: TestStep
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
            (ExecutionState executionState, List<TestStepVM> entity, string message) returnResponse = _TestStepService.List();
            return View(returnResponse.entity);
        }

        // GET: TestStep/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: TestStep/Create
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

            TestStepVM entity = new TestStepVM();
            return View(entity);
        }

        // POST: TestStep/Create
        [HttpPost]
        public ActionResult Create(TestStepVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;

                var testScenarios = JsonConvert.DeserializeObject<List<TestStepVM>>(HttpContext.Request.Form["TestSteps"]!);

                (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.CreateOfList(testScenarios);


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


        // GET: TestStep/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.GetById(id);
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.GetProjectAcceptedList();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName",returnResponse.entity.ProjectRequestId);
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
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponseTestCase =  _TestCaseService.List();
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

        // POST: TestStep/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TestStepVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(TestStepController.Index), "TestStep");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.Update(entity);
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

        // GET: TestStep/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _TestStepService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.Delete(id);
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

        // POST: TestStep/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TestStepVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(TestStepController.Index), "TestStep");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, TestStepVM entity, string message) returnResponse = _TestStepService.Update(entity);
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
        public async Task<ActionResult> Search(long? ProjectRequestId,long? TaskOfProjectId,long? TestCaseId)
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

            (ExecutionState executionState, IList<TestStepVM> entity, string message) returnResponse = await _TestStepService.Search(ProjectRequestId, TaskOfProjectId, TestCaseId);

            return View("Index", returnResponse.entity);
        }
    }
}
