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
    public class HardwareTestingController : Controller
    {
        private readonly IHardwareTestingService _HardwareTestingService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly ITaskService _TaskService;
        private readonly ITestCaseService _TestCaseService;
        private readonly ITestScopeService _TestScopeService;

        public HardwareTestingController(HttpHelper httpHelper)
        {
            _HardwareTestingService = new HardwareTestingService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _TestCaseService= new TestCaseService(httpHelper);
            _TestScopeService = new TestScopeService(httpHelper);
        }
        // GET: HardwareTesting
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
            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponseTestScope =  _TestScopeService.List();
            if (returnResponseTestScope.entity != null)
            {
                var filterdUser = returnResponseTestScope.entity.ToList();
                ViewBag.TestScopeId = new SelectList(filterdUser ?? new List<TestScopeVM>(), "Id", "TestItem");
            }
            else
            {
                ViewBag.TestScopeId = new SelectList("");
            }

            (ExecutionState executionState, List<HardwareTestingVM> entity, string message) returnResponse = _HardwareTestingService.List();
            return View(returnResponse.entity);
        }

        // GET: HardwareTesting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: HardwareTesting/Create
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
            ViewBag.TaskOfProjectId = new SelectList("");

            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponseTestScope =  _TestScopeService.List();
            if (returnResponseTestScope.entity != null)
            {
                var filterdUser = returnResponseTestScope.entity.ToList();
                ViewBag.TestScopeId = new SelectList(filterdUser ?? new List<TestScopeVM>(), "Id", "TestItem");
            }
            else
            {
                ViewBag.TestScopeId = new SelectList("");
            }
            ViewBag.TestResult = new SelectList(EnumHelper.GetEnumDropdowns<TestResult>(),"Id","Name");
            HardwareTestingVM entity = new HardwareTestingVM();
            return View(entity);
        }

        // POST: HardwareTesting/Create
        [HttpPost]
        public ActionResult Create(HardwareTestingVM entity)
        {
            try
            {

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;

                var HardwareTesting = JsonConvert.DeserializeObject<List<HardwareTestingVM>>(HttpContext.Request.Form["HardwareTestings"]!);

                (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.CreateOfList(HardwareTesting);


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


        // GET: HardwareTesting/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.GetById(id);
            ViewBag.TestResult = new SelectList(EnumHelper.GetEnumDropdowns<TestResult>(), "Id", "Name",(int)returnResponse.entity.TestResult);

            return View(returnResponse.entity);
        }

        // POST: HardwareTesting/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, HardwareTestingVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ExisitingEntity =  _HardwareTestingService.GetById(id).entity;

                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(HardwareTestingController.Index), "HardwareTesting");
                    }
                    ExisitingEntity.IsActive = true;
                    ExisitingEntity.IsDeleted = false;
                    ExisitingEntity.UpdatedAt = DateTime.Now;
                    ExisitingEntity.SubItem = entity.SubItem;
                    ExisitingEntity.RequiredSpecification = entity.RequiredSpecification;
                    ExisitingEntity.SpecificationAsPerContract = entity.SpecificationAsPerContract;
                    ExisitingEntity.TestResult = entity.TestResult;
                    ExisitingEntity.Remarks = entity.Remarks;
                    (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.Update(ExisitingEntity);
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

        // GET: HardwareTesting/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _HardwareTestingService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "HardwareTesting deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: HardwareTesting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, HardwareTestingVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(HardwareTestingController.Index), "HardwareTesting");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse = _HardwareTestingService.Update(entity);
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
        public async Task<ActionResult> Search(long? ProjectRequestId,long? TaskOfProjectId,long? TestScopeId, string? SubItem)
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
            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponseTestScope = _TestScopeService.List();
            if (returnResponseTestScope.entity != null)
            {
                var filterdUser = returnResponseTestScope.entity.ToList();
                ViewBag.TestScopeId = new SelectList(filterdUser ?? new List<TestScopeVM>(), "Id", "TestItem");
            }
            else
            {
                ViewBag.TestScopeId = new SelectList("");
            }

            ViewBag.TestItem = SubItem;

            (ExecutionState executionState, IList<HardwareTestingVM> entity, string message) returnResponse = await _HardwareTestingService.Search(ProjectRequestId, TaskOfProjectId, TestScopeId,SubItem);

            return View("Index", returnResponse.entity);
        }
    }
}
