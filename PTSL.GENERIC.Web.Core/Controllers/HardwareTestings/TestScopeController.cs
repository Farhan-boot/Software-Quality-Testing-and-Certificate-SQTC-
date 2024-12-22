using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.HardwareTestings;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.HardwareTestings;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.HardwareTestings
{
    [SessionAuthorize]
    public class TestScopeController : Controller
    {
        private readonly ITestScopeService _TestScopeService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly ITaskService _TaskService;
        private readonly ITestCaseService _TestCaseService;

        public TestScopeController(HttpHelper httpHelper)
        {
            _TestScopeService = new TestScopeService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _TestCaseService= new TestCaseService(httpHelper);
        }
        // GET: TestScope
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
            
            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponse = _TestScopeService.List();
            return View(returnResponse.entity);
        }

        // GET: TestScope/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: TestScope/Create
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

            TestScopeVM entity = new TestScopeVM();
            return View(entity);
        }

        // POST: TestScope/Create
        [HttpPost]
        public ActionResult Create(TestScopeVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;

                var testScope = JsonConvert.DeserializeObject<List<TestScopeVM>>(HttpContext.Request.Form["TestScopes"]!);

                (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.CreateOfList(testScope!);


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


        // GET: TestScope/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.GetById(id);
            //(ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            //if (returnResponseProject.entity != null)
            //{
            //    var filterdUser = returnResponseProject.entity.ToList();
            //    ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName",returnResponse.entity.ProjectRequestId);
            //}
            //else
            //{
            //    ViewBag.ProjectRequestId = new SelectList("");
            //}


            //(ExecutionState executionState, List<TaskVM> entity, string message) returnResponseTask = await _TaskService.List();
            //if (returnResponseProject.entity != null)
            //{
            //    var filterdUser = returnResponseTask.entity.ToList();
            //    ViewBag.TaskOfProjectId = new SelectList(filterdUser ?? new List<TaskVM>(), "Id", "TaskTitle", returnResponse.entity.TaskOfProjectId);
            //}
            //else
            //{
            //    ViewBag.TaskOfProjectId = new SelectList("");
            //}

            return View(returnResponse.entity);
        }

        // POST: TestScope/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, TestScopeVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ExisitingEntity =  _TestScopeService.GetById(id).entity;

                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(TestScopeController.Index), "TestScope");
                    }
                    ExisitingEntity.IsActive = true;
                    ExisitingEntity.IsDeleted = false;
                    ExisitingEntity.UpdatedAt = DateTime.Now;
                    ExisitingEntity.TenderID = entity.TenderID;
                    ExisitingEntity.TestItem = entity.TestItem;
                    ExisitingEntity.Description = entity.Description;
                    (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.Update(ExisitingEntity);
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

        // GET: TestScope/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _TestScopeService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "TestScope deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: TestScope/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TestScopeVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(TestScopeController.Index), "TestScope");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, TestScopeVM entity, string message) returnResponse = _TestScopeService.Update(entity);
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
        public async Task<ActionResult> Search(long? ProjectRequestId,long? TaskOfProjectId,string? TestItem, string? TenderID, string? SerialNo)
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
            ViewBag.TestItem = TestItem;
            ViewBag.TenderID = TenderID;
            ViewBag.SerialNo = SerialNo;

            (ExecutionState executionState, IList<TestScopeVM> entity, string message) returnResponse = await _TestScopeService.Search(ProjectRequestId, TaskOfProjectId, TestItem,TenderID,SerialNo);

            return View("Index", returnResponse.entity);
        }
    }
}
