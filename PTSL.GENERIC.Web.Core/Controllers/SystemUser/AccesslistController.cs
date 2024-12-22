using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers.SystemUser
{
    [SessionAuthorize]
    public class AccesslistController : Controller
    {
        private readonly IAccesslistService _AccesslistService;
        private readonly IModuleService _ModuleService;

        public AccesslistController(HttpHelper httpHelper)
        {
            _AccesslistService = new AccesslistService(httpHelper);
            _ModuleService = new ModuleService(httpHelper);
        }
        // GET: Accesslist
        public ActionResult Index()
        {
            (ExecutionState executionState, List<AccesslistVM> entity, string message) returnResponse = _AccesslistService.List();
            return View(returnResponse.entity);
        }

        public ActionResult IndexForApproval()
        {
            (ExecutionState executionState, List<AccesslistVM> entity, string message) returnResponse = _AccesslistService.ListForApproval();
            return View(returnResponse.entity);
        }

        // GET: Accesslist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: Accesslist/Create
        public ActionResult Create()
        {
            ViewBag.ModuleId = new SelectList(_ModuleService.List().entity ?? new List<ModuleVM>(), "Id", "ModuleName");

            return View(new AccesslistVM());
        }

        public ActionResult CreateForApproval()
        {
            return View(new AccesslistVM());
        }

        [HttpPost]
        public ActionResult Create(AccesslistVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;

                    (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Create(entity);
                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ModuleVM> entity, string message) ModuleLists = _ModuleService.List();
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateForApproval(AccesslistVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.IsAvailableForApproval = true;
                    entity.ModuleId = null;

                    (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Create(entity);
                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("IndexForApproval");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Accesslist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(id);

            ViewBag.ModuleId = new SelectList(_ModuleService.List().entity ?? new List<ModuleVM>(), "Id", "ModuleName", returnResponse.entity.ModuleId);

            return View(returnResponse.entity);
        }

        [HttpPost]
        public ActionResult Edit(int id, AccesslistVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(AccesslistController.Index), "Accesslist");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Update(entity);

                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        (ExecutionState executionState, List<ModuleVM> entity, string message) ModuleLists = _ModuleService.List();
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Accesslist/Edit/5
        public ActionResult EditForApproval(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.GetById(id);

            ViewBag.ModuleId = new SelectList(_ModuleService.List().entity ?? new List<ModuleVM>(), "Id", "ModuleName", returnResponse.entity.ModuleId);

            return View(returnResponse.entity);
        }

        [HttpPost]
        public ActionResult EditForApproval(int id, AccesslistVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(AccesslistController.Index), "Accesslist");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    entity.IsAvailableForApproval = true;
                    entity.ModuleId = null;

                    (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Update(entity);

                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("IndexForApproval");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Accesslist/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Accesslist deleted successfully.";
            }
            return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: Accesslist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AccesslistVM entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(AccesslistController.Index), "Accesslist");
                }
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, AccesslistVM entity, string message) returnResponse = _AccesslistService.Update(entity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
