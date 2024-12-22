using Microsoft.AspNetCore.Mvc;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client.ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForRegisteredClientLogVM;
using PTSL.GENERIC.Web.Core.Services.Implementation.Sqtc_Client.ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class ApprovalForRegisteredClientLogController : Controller
    {
        private readonly IApprovalForRegisteredClientLogService _ApprovalForRegisteredClientLogService;

        public ApprovalForRegisteredClientLogController(HttpHelper httpHelper)
        {
            _ApprovalForRegisteredClientLogService = new ApprovalForRegisteredClientLogService(httpHelper);
        }
        // GET: ApprovalForRegisteredClientLog
        public  ActionResult Index()
        {
            (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) returnResponse =  _ApprovalForRegisteredClientLogService.List();
            return View(returnResponse.entity);
        }

        // GET: ApprovalForRegisteredClientLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = _ApprovalForRegisteredClientLogService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: ApprovalForRegisteredClientLog/Create
        public ActionResult Create()
        {
            ApprovalForRegisteredClientLogVM entity = new ApprovalForRegisteredClientLogVM();
            return View(entity);
        }

        // POST: ApprovalForRegisteredClientLog/Create
        [HttpPost]
        public async Task<ActionResult> Create(ApprovalForRegisteredClientLogVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = await _ApprovalForRegisteredClientLogService.Create(entity);
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


        // GET: ApprovalForRegisteredClientLog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = _ApprovalForRegisteredClientLogService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: ApprovalForRegisteredClientLog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ApprovalForRegisteredClientLogVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ApprovalForRegisteredClientLogController.Index), "ApprovalForRegisteredClientLog");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = _ApprovalForRegisteredClientLogService.Update(entity);
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

        // GET: ApprovalForRegisteredClientLog/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _ApprovalForRegisteredClientLogService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = _ApprovalForRegisteredClientLogService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "ApprovalForRegisteredClientLog deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: ApprovalForRegisteredClientLog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ApprovalForRegisteredClientLogVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ApprovalForRegisteredClientLogController.Index), "ApprovalForRegisteredClientLog");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = _ApprovalForRegisteredClientLogService.Update(entity);
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
