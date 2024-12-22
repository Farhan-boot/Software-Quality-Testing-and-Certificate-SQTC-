using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class TaskTypeController : Controller
    {
        private readonly ITaskTypeService _TaskTypeService;

        public TaskTypeController(HttpHelper httpHelper)
        {
            _TaskTypeService = new TaskTypeService(httpHelper);
        }
        // GET: TaskType
        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, List<TaskTypeVM> entity, string message) returnResponse = await _TaskTypeService.List();
            return View(returnResponse.entity);
        }

        // GET: TaskType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: TaskType/Create
        public ActionResult Create()
        {
            TaskTypeVM entity = new TaskTypeVM();
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            ViewBag.TaskAuthority = new SelectList(EnumHelper.GetEnumDropdowns<TaskAuthority>(), "Id", "Name");
            return View(entity);
        }

        // POST: TaskType/Create
        [HttpPost]
        public ActionResult Create(TaskTypeVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.Create(entity);
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


        // GET: TaskType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.GetById(id);
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(),"Id", "Name",(int)returnResponse.entity.ProjectType);
            ViewBag.TaskAuthority = new SelectList(EnumHelper.GetEnumDropdowns<TaskAuthority>(), "Id", "Name", (int)returnResponse.entity.TaskAuthority);
            return View(returnResponse.entity);
        }

        // POST: TaskType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TaskTypeVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(TaskTypeController.Index), "TaskType");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.Update(entity);
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

        // GET: TaskType/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _TaskTypeService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Task Type deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: TaskType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TaskTypeVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(TaskTypeController.Index), "TaskType");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse = _TaskTypeService.Update(entity);
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
    }
}
