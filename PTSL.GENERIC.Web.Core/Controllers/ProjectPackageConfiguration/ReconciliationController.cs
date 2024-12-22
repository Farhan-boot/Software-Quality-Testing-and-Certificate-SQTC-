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
    public class ReconciliationController : Controller
    {
        private readonly IReconciliationService _ReconciliationService;
        private readonly IUserService _userService;

        public ReconciliationController(HttpHelper httpHelper)
        {
            _ReconciliationService = new ReconciliationService(httpHelper);
            _userService = new UserService(httpHelper);
        }
        // GET: Reconciliation
        public ActionResult Index()
        {
            (ExecutionState executionState, List<ReconciliationVM> entity, string message) returnResponse = _ReconciliationService.List();
            return View(returnResponse.entity);
        }

        // GET: Reconciliation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse = _ReconciliationService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ReconciliationVM entity = new ReconciliationVM();
            (ExecutionState executionState, List<ReconciliationVM> entity, string message) returnResponse = _ReconciliationService.List();
            ViewBag.ProjectTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(ReconciliationVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = _ReconciliationService?.List().entity.Any(x=>x.PaymentInformationId == entity.PaymentInformationId && x.IsPaymentApproved == true);
                    if (check == true)
                    {
                        return Json(
                                  new { Success = false, Data = false },
                                  SerializerOption.Default);
                    }


                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse1 = _ReconciliationService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ReconciliationVM> entity, string message) divisionLists = _ReconciliationService.List();


                        return Json(
                                   new { Success = true, Data = returnResponse1.entity },
                                   SerializerOption.Default);
                        //return View(entity);
                    }
                    else
                    {
                        return Json(
                                   new { Success = true, Data = true },
                                   SerializerOption.Default);
                        // return RedirectToAction("Index");
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


        // GET: Reconciliation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse = _ReconciliationService.GetById(id);

            

            return View(returnResponse.entity);
        }

        // POST: Reconciliation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReconciliationVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ReconciliationController.Index), "Reconciliation");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse = _ReconciliationService.Update(entity);
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


        // GET: Reconciliation/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _ReconciliationService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse = _ReconciliationService.Delete(id);
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

        // POST: Reconciliation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ReconciliationVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ReconciliationController.Index), "Reconciliation");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, ReconciliationVM entity, string message) returnResponse = _ReconciliationService.Update(entity);
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


        public ActionResult ClientReconciliationList()
        {
            (ExecutionState executionState, List<ReconciliationVM> entity, string message) returnResponse = _ReconciliationService.List();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;

            var data = returnResponse.entity.Where(x => x.PaymentInformation?.PaymentCalculationHeader?.ProjectRequest?.ClientId == clientId) ?? new List<ReconciliationVM>();
            return View(data);
        }

    }
}
