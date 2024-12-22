﻿using Microsoft.AspNetCore.Mvc;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class MeetingTypeController : Controller
    {
        private readonly IMeetingTypeService _MeetingTypeService;

        public MeetingTypeController(HttpHelper httpHelper)
        {
            _MeetingTypeService = new MeetingTypeService(httpHelper);
        }
        // GET: MeetingType
        public ActionResult Index()
        {
            (ExecutionState executionState, List<MeetingTypeVM> entity, string message) returnResponse = _MeetingTypeService.List();
            return View(returnResponse.entity);
        }

        // GET: MeetingType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: MeetingType/Create
        public ActionResult Create()
        {
            MeetingTypeVM entity = new MeetingTypeVM();
            return View(entity);
        }

        // POST: MeetingType/Create
        [HttpPost]
        public ActionResult Create(MeetingTypeVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.Create(entity);
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


        // GET: MeetingType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: MeetingType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MeetingTypeVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(MeetingTypeController.Index), "MeetingType");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.Update(entity);
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

        // GET: MeetingType/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _MeetingTypeService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Meeting Type deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: MeetingType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MeetingTypeVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(MeetingTypeController.Index), "MeetingType");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse = _MeetingTypeService.Update(entity);
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
