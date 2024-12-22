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

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class ProjectPackageController : Controller
    {
        private readonly IProjectPackageService _ProjectPackageService;
        private readonly IProjectModuleNameService _ProjectModuleNameService;
        public ProjectPackageController(HttpHelper httpHelper)
        {
            _ProjectPackageService = new ProjectPackageService(httpHelper);
            _ProjectModuleNameService = new ProjectModuleNameService(httpHelper);
        }
        // GET: ProjectPackage
        public ActionResult Index()
        {
            (ExecutionState executionState, List<ProjectPackageVM> entity, string message) returnResponse = _ProjectPackageService.List();
            return View(returnResponse.entity);
        }

        // GET: ProjectPackage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse = _ProjectPackageService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ProjectPackageVM entity = new ProjectPackageVM();
            (ExecutionState executionState, List<ProjectPackageVM> entity, string message) returnResponse = _ProjectPackageService.List();
            ViewBag.ProjectModuleNameId = new SelectList(_ProjectModuleNameService.List().entity ?? new List<ProjectModuleNameVM>(), "Id", "Name");
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(ProjectPackageVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse1 = _ProjectPackageService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ProjectPackageVM> entity, string message) divisionLists = _ProjectPackageService.List();

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


        // GET: ProjectPackage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse = _ProjectPackageService.GetById(id);
            ViewBag.ProjectModuleNameId = new SelectList(_ProjectModuleNameService.List().entity ?? new List<ProjectModuleNameVM>(), "Id", "Name", (long)returnResponse.entity.ProjectModuleNameId);

            return View(returnResponse.entity);
        }

        // POST: ProjectPackage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectPackageVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ProjectPackageController.Index), "ProjectPackage");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse = _ProjectPackageService.Update(entity);
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

        public JsonResult Delete(int id)
        {
            var result = _ProjectPackageService.SoftDelete(id);
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



        //// GET: ProjectPackage/Delete/5
        //public JsonResult Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot = _ProjectPackageService.DoesExist(id);
        //    string message = "Failed, You can't delete this item.";

        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
        //    }

        //    (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse = _ProjectPackageService.Delete(id);
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

        //// POST: ProjectPackage/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, ProjectPackageVM entity)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        if (id != entity.Id)
        //        {
        //            return RedirectToAction(nameof(ProjectPackageController.Index), "ProjectPackage");
        //        }
        //        //entity.IsActive = true;
        //        entity.IsDeleted = true;
        //        entity.UpdatedAt = DateTime.Now;
        //        (ExecutionState executionState, ProjectPackageVM entity, string message) returnResponse = _ProjectPackageService.Update(entity);
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
