using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Implementation.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Helper.Enum.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class ReviewCommentController : Controller
    {
        private readonly IReviewCommentService _ReviewCommentService;
        private readonly ITaskService _TaskService;
        private readonly IUserService _UserService;
        private readonly IProjectRequestService _ProjectRequestService;

        public ReviewCommentController(HttpHelper httpHelper)
        {
            _ReviewCommentService = new ReviewCommentService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _UserService = new UserService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
        }
        // GET: ReviewComment
        public ActionResult Index()
        {
            (ExecutionState executionState, List<ReviewCommentVM> entity, string message) returnResponse = _ReviewCommentService.List();
            return View(returnResponse.entity ?? new List<ReviewCommentVM>());
        }

        // GET: ReviewComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse = _ReviewCommentService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ReviewCommentVM entity = new ReviewCommentVM();
            (ExecutionState executionState, List<ReviewCommentVM> entity, string message) returnResponse = _ReviewCommentService.List();
            ViewBag.StatusEnumId = new SelectList(EnumHelper.GetEnumDropdowns<StatusEnum>(), "Id", "Name");

            ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            ViewBag.TaskOfProjectId = new SelectList(_TaskService.List().Result.entity ?? new List<TaskVM>(), "Id", "TaskTitle");

            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(ReviewCommentVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse1 = _ReviewCommentService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ReviewCommentVM> entity, string message) divisionLists = _ReviewCommentService.List();

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


        // GET: ReviewComment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse = _ReviewCommentService.GetById(id);

            ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName",(long) returnResponse.entity.ProjectRequestId);
            ViewBag.TaskOfProjectId = new SelectList(_TaskService.List().Result.entity ?? new List<TaskVM>(), "Id", "TaskTitle", (long) returnResponse.entity.TaskOfProjectId);

            ViewBag.StatusEnumId = new SelectList(EnumHelper.GetEnumDropdowns<StatusEnum>(), "Id", "Name", (long) returnResponse.entity.StatusEnumId);
            return View(returnResponse.entity);
        }

        // POST: ReviewComment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReviewCommentVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ReviewCommentController.Index), "ReviewComment");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse = _ReviewCommentService.Update(entity);
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


        // GET: ReviewComment/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _ReviewCommentService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse = _ReviewCommentService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Item deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }

            return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
        }

        // POST: ReviewComment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ReviewCommentVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ReviewCommentController.Index), "ReviewComment");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, ReviewCommentVM entity, string message) returnResponse = _ReviewCommentService.Update(entity);
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
