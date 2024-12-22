using Microsoft.AspNetCore.Mvc;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class DocumentCategoriesController : Controller
    {
        private readonly IDocumentCategoriesService _DocumentCategoriesService;

        public DocumentCategoriesController(HttpHelper httpHelper)
        {
            _DocumentCategoriesService = new DocumentCategoriesService(httpHelper);
        }
        // GET: DocumentCategories
        public ActionResult Index()
        {
            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponse = _DocumentCategoriesService.List();
            return View(returnResponse.entity);
        }

        // GET: DocumentCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: DocumentCategories/Create
        public ActionResult Create()
        {
            DocumentCategoriesVM entity = new DocumentCategoriesVM();
            return View(entity);
        }

        // POST: DocumentCategories/Create
        [HttpPost]
        public ActionResult Create(DocumentCategoriesVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.Create(entity);
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


        // GET: DocumentCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: DocumentCategories/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DocumentCategoriesVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(DocumentCategoriesController.Index), "DocumentCategories");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.Update(entity);
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

        // GET: DocumentCategories/Delete/5
        public JsonResult Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = _DocumentCategoriesService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Document Categories deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: DocumentCategories/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DocumentCategoriesVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(DocumentCategoriesController.Index), "DocumentCategories");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse = _DocumentCategoriesService.Update(entity);
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
