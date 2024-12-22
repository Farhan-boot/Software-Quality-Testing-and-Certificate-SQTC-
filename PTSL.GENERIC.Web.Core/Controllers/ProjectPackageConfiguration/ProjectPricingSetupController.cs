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
using PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Implementation.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class ProjectPricingSetupController : Controller
    {
        private readonly IProjectPricingSetupService _ProjectPricingSetupService;
        private readonly IProjectModuleNameService _ProjectModuleNameService;
        private readonly IProjectPackageService _ProjectPackageService;
        private readonly IPaymentCalculationRowService _PaymentCalculationRowService;

        public ProjectPricingSetupController(HttpHelper httpHelper)
        {
            _ProjectPricingSetupService = new ProjectPricingSetupService(httpHelper);
            _ProjectModuleNameService = new ProjectModuleNameService(httpHelper);
            _ProjectPackageService = new ProjectPackageService(httpHelper);
            _PaymentCalculationRowService = new PaymentCalculationRowService(httpHelper);
        }
        // GET: ProjectPricingSetup
        public ActionResult Index()
        {
            (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) returnResponse = _ProjectPricingSetupService.List();
            var count = _PaymentCalculationRowService?.List().entity?.Count()??0;

            if (returnResponse.entity != null)
            {
                foreach (var item in returnResponse.entity)
                {
                    var isExisitsCheck = false;
                    if (count > 0)
                    {
                        isExisitsCheck = _PaymentCalculationRowService?.List().entity.Any(x => x?.ProjectModuleNameId == item?.ProjectModuleNameId && x?.ProjectPackageId == item?.ProjectPackageId) ?? false;
                    }

                    if (isExisitsCheck == true)
                    {
                        item.IsExists = true;
                    }
                }
            }
            
            return View(returnResponse.entity ?? new List<ProjectPricingSetupVM>());
        }

        // GET: ProjectPricingSetup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ProjectPricingSetupVM entity = new ProjectPricingSetupVM();
            entity.IsActive = true;
            (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) returnResponse = _ProjectPricingSetupService.List();
            ViewBag.ProjectModuleNameId = new SelectList(_ProjectModuleNameService.List().entity ?? new List<ProjectModuleNameVM>(), "Id", "Name");
            ViewBag.ProjectPackageId = new SelectList(_ProjectPackageService.List().entity ?? new List<ProjectPackageVM>(), "Id", "PackageName");
            
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(ProjectPricingSetupVM entity)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                if (entity.IsActive == false)
                {
                    entity.IsActive = false;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponseFalseCreate = _ProjectPricingSetupService.Create(entity);
                    return RedirectToAction("Index");
                }
                else
                {
                    var returnResponse1Check = _ProjectPricingSetupService.List().entity.Where(x => x.ProjectModuleNameId == entity.ProjectModuleNameId).ToList();
                    if (returnResponse1Check != null)
                    {
                        foreach (var item in returnResponse1Check)
                        {
                            item.IsActive = false;
                            item.CreatedAt = DateTime.Now;
                            (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.Update(item);
                        }
                    }

                    entity.IsActive = entity.IsActive;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse1 = _ProjectPricingSetupService.Create(entity);
                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) divisionLists = _ProjectPricingSetupService.List();

                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }


                //}

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


        // GET: ProjectPricingSetup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.GetById(id);
            ViewBag.ProjectModuleNameId = new SelectList(_ProjectModuleNameService.List().entity ?? new List<ProjectModuleNameVM>(), "Id", "Name", (long)returnResponse.entity.ProjectModuleNameId);
            ViewBag.ProjectPackageId = new SelectList(_ProjectPackageService.List().entity ?? new List<ProjectPackageVM>(), "Id", "PackageName", (long)returnResponse.entity.ProjectPackageId);

            return View(returnResponse.entity);
        }

        // POST: ProjectPricingSetup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectPricingSetupVM entity)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ProjectPricingSetupController.Index), "ProjectPricingSetup");
                    }
                    entity.IsActive = entity.IsActive;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.Update(entity);
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
               // }

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

        public JsonResult Delete(int id)
        {
            var result = _ProjectPricingSetupService.SoftDelete(id);
            if (result.isDeleted)
            {
                result.message = "Item deleted successfully.";
            }
            else
            {
                result.message = "Failed to delete this item.";
            }

            return Json(new { Message = result.message, result.executionState }, SerializerOption.Default);
        }



        //// GET: ProjectPricingSetup/Delete/5
        //public JsonResult Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot = _ProjectPricingSetupService.DoesExist(id);
        //    string message = "Failed, You can't delete this item.";

        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
        //    }

        //    (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.Delete(id);
        //    if (returnResponse.executionState.ToString() == "Updated")
        //    {
        //        returnResponse.message = "Item deleted successfully.";
        //    }
        //    else
        //    {
        //        returnResponse.message = "Failed to delete this item.";
        //    }

        //    return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
        //}

        //// POST: ProjectPricingSetup/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, ProjectPricingSetupVM entity)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        if (id != entity.Id)
        //        {
        //            return RedirectToAction(nameof(ProjectPricingSetupController.Index), "ProjectPricingSetup");
        //        }
        //        //entity.IsActive = true;
        //        entity.IsDeleted = true;
        //        entity.UpdatedAt = DateTime.Now;
        //        (ExecutionState executionState, ProjectPricingSetupVM entity, string message) returnResponse = _ProjectPricingSetupService.Update(entity);
        //        //                Session["Message"] = returnResponse.message;
        //        //                Session["executionState"] = returnResponse.executionState;
        //        //return View(returnResponse.entity);
        //        // return RedirectToAction("Edit?id="+id);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult GetProjectPackageByProjectModuleNameId(string id)
        {
            long ProjectModuleNameId = Convert.ToInt64(id);
            var data = _ProjectPackageService.GetProjectPackageByProjectModuleNameId(ProjectModuleNameId);

            return Json(
                       new { Success = true, Data = data.entity },
                       SerializerOption.Default);
        }

    }
}
