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
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _FeedbackService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IReconciliationService _ReconciliationService;
        private readonly IUserService _userService;
        private readonly IProjectRequestService _projectRequestService;

        public FeedbackController(HttpHelper httpHelper)
        {
            _FeedbackService = new FeedbackService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _ReconciliationService = new ReconciliationService(httpHelper);
            _userService = new UserService(httpHelper);
            _projectRequestService = new ProjectRequestService(httpHelper);
        }
        // GET: Feedback
        public ActionResult Index()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            if (clientId != 0)
            {
                return RedirectToAction("ClientFeedbackList");
            }
            else
            {
                (ExecutionState executionState, List<FeedbackVM> entity, string message) returnResponse = _FeedbackService.List();
                return View(returnResponse.entity);
            }
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.GetById(id);
            return View(returnResponse.entity);
        }


        // GET: District/Create
        public ActionResult Create()
        {
            FeedbackVM entity = new FeedbackVM();
            (ExecutionState executionState, List<FeedbackVM> entity, string message) returnResponse = _FeedbackService.List();
            //ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName");

            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            var ClientProject = _projectRequestService.GetProjectListByClientId(clientId).Result.entity ?? new List<ProjectRequestVM>();
            ViewBag.ProjectRequestId = new SelectList(ClientProject ?? new List<ProjectRequestVM>(), "Id", "ProjectName");


            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(FeedbackVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = HttpContext.Session.GetString(SessionKey.UserId) ?? "0";
                    var clientId = _userService.GetById(Convert.ToInt64(userId)).entity?.ClientId ?? 0;
                    entity.UserId = Convert.ToInt64(clientId);
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                   
                    var checkUserId = _FeedbackService.List().entity?.Any(x => x?.UserId == entity.UserId);
                    if (checkUserId == true)
                    {
                        return Json(
                                 new { Success = false, Data = "" },
                                 SerializerOption.Default);
                    }

                    // TODO: Add insert logic here
                    (ExecutionState executionState, FeedbackVM entity, string message) returnResponse1 = _FeedbackService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<FeedbackVM> entity, string message) divisionLists = _FeedbackService.List();

                        //return View(entity);
                        return Json(
                                  new { Success = true, Data = returnResponse1.entity },
                                  SerializerOption.Default);
                    }
                    else
                    {
                        //return RedirectToAction("Index");
                        return Json(
                                  new { Success = true, Data = returnResponse1.entity },
                                  SerializerOption.Default);
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
                //return View(entity);
                return Json(
                                  new { Success = true, Data = false },
                                  SerializerOption.Default);
            }
        }


        // GET: Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.GetById(id);
            //ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName",returnResponse.entity?.ProjectRequestId ?? 0);


            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            var ClientProject = _projectRequestService.GetProjectListByClientId(clientId).Result.entity ?? new List<ProjectRequestVM>();
            ViewBag.ProjectRequestId = new SelectList(ClientProject ?? new List<ProjectRequestVM>(), "Id", "ProjectName", returnResponse.entity?.ProjectRequestId ?? 0);

            return View(returnResponse.entity);
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FeedbackVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(FeedbackController.Index), "Feedback");
                    }
                    var userId = HttpContext.Session.GetString(SessionKey.UserId) ?? "0";
                    entity.UserId = Convert.ToInt64(userId);
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.Update(entity);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return Json(
                                 new { Success = true, Data = returnResponse.entity },
                                 SerializerOption.Default);
                        // return View(entity);
                    }
                    else
                    {
                        return Json(
                                 new { Success = true, Data = returnResponse.entity },
                                 SerializerOption.Default);
                        //return RedirectToAction("Index");
                    }
                }

                //                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
                //                Session["executionState"] = ExecutionState.Failure;
                //return View(entity);
                return Json(
                                 new { Success = true, Data = false },
                                 SerializerOption.Default);
            }
            catch
            {
                //                Session["Message"] = "Form Data Not Valid.";
                //                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }


        // GET: Feedback/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _FeedbackService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.Delete(id);
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

        // POST: Feedback/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FeedbackVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(FeedbackController.Index), "Feedback");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.Update(entity);
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


        public ActionResult SubmittedList()
        {
            (ExecutionState executionState, List<FeedbackVM> entity, string message) returnResponse = _FeedbackService.List();
            return View(returnResponse.entity?.Where(x=>x.IsApprove == true) ?? new List<FeedbackVM>());
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponseCheck = _FeedbackService.GetById(id);
            returnResponseCheck.entity.IsActive = false;
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.Update(returnResponseCheck.entity);

             var check =  _ReconciliationService.List().entity.Where(x => x?.PaymentInformation?.PaymentCalculationHeader?.ProjectRequest?.Id == returnResponseCheck.entity.ProjectRequestId);
            if (check != null)
            {
                foreach (var item in check)
                {
                    item.IsPaymentApproved = true;
                    item.PaymentInformation = null;
                    (ExecutionState executionState, ReconciliationVM entity, string message) returnResponseReconciliation = _ReconciliationService.Update(item);
                }
            }

            return Json(
                                  new { Success = true, Data = returnResponse.entity },
                                  SerializerOption.Default);
            //return RedirectToAction("SubmittedList");

        }

        public ActionResult Rejected(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponseCheck = _FeedbackService.GetById(id);
            returnResponseCheck.entity.IsActive = true;
            (ExecutionState executionState, FeedbackVM entity, string message) returnResponse = _FeedbackService.Update(returnResponseCheck.entity);

            var check = _ReconciliationService.List().entity.Where(x => x?.PaymentInformation?.PaymentCalculationHeader?.ProjectRequest?.Id == returnResponseCheck.entity.ProjectRequestId);
            if (check != null)
            {
                foreach (var item in check)
                {
                    item.IsPaymentApproved = false;
                    item.PaymentInformation = null;
                    (ExecutionState executionState, ReconciliationVM entity, string message) returnResponseReconciliation = _ReconciliationService.Update(item);
                }
            }

            return Json(
                                 new { Success = true, Data = returnResponse.entity },
                                 SerializerOption.Default);
            //return RedirectToAction("SubmittedList");

        }


        public ActionResult ClientFeedbackList()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;

            var data = _FeedbackService.List().entity.Where(x => x?.ProjectRequest?.Client?.Id == clientId);

            //(ExecutionState executionState, List<FeedbackVM> entity, string message) returnResponse = _FeedbackService.List();
            return View(data ?? new List<FeedbackVM>());
        }


    }
}
