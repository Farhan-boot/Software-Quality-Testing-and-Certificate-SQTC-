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
    public class ProjectModuleNameController : Controller
    {
        private readonly IProjectModuleNameService _ProjectModuleNameService;

        public ProjectModuleNameController(HttpHelper httpHelper)
        {
            _ProjectModuleNameService = new ProjectModuleNameService(httpHelper);
        }
        // GET: ProjectModuleName
        public ActionResult Index()
        {
            (ExecutionState executionState, List<ProjectModuleNameVM> entity, string message) returnResponse = _ProjectModuleNameService.List();
            return View(returnResponse.entity);
        }

        // GET: ProjectModuleName/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse = _ProjectModuleNameService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            ProjectModuleNameVM entity = new ProjectModuleNameVM();
            (ExecutionState executionState, List<ProjectModuleNameVM> entity, string message) returnResponse = _ProjectModuleNameService.List();
            ViewBag.ProjectTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(ProjectModuleNameVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse1 = _ProjectModuleNameService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<ProjectModuleNameVM> entity, string message) divisionLists = _ProjectModuleNameService.List();

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


        // GET: ProjectModuleName/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse = _ProjectModuleNameService.GetById(id);

            ViewBag.ProjectTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name",(long) returnResponse.entity.ProjectTypeId);

            return View(returnResponse.entity);
        }

        // POST: ProjectModuleName/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectModuleNameVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ProjectModuleNameController.Index), "ProjectModuleName");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse = _ProjectModuleNameService.Update(entity);
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


        // GET: ProjectModuleName/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _ProjectModuleNameService.DoesExist(id);
            string message = "Failed, You can't delete this item.";

            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
            }

            (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse = _ProjectModuleNameService.Delete(id);
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

        // POST: ProjectModuleName/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectModuleNameVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ProjectModuleNameController.Index), "ProjectModuleName");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, ProjectModuleNameVM entity, string message) returnResponse = _ProjectModuleNameService.Update(entity);
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
