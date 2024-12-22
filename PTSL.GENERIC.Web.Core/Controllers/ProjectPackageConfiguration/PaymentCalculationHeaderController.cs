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
using Newtonsoft.Json;

using PaymentCalculationHeaderVM = PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration.PaymentCalculationHeaderVM;
using PaymentCalculationRowVM = PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration.PaymentCalculationRowVM;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Model;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using SautinSoft;
using static iText.IO.Util.IntHashtable;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class PaymentCalculationHeaderController : Controller
    {
        private readonly IPaymentCalculationHeaderService _PaymentCalculationHeaderService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IProjectModuleNameService _ProjectModuleNameService;
        private readonly IProjectPackageService _ProjectPackageService;
        private readonly IProjectPricingSetupService _ProjectPricingSetupService;
        private readonly ITaskService _TaskService;
        private readonly IUserService _UserService;

        public PaymentCalculationHeaderController(HttpHelper httpHelper)
        {
            _PaymentCalculationHeaderService = new PaymentCalculationHeaderService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _ProjectModuleNameService = new ProjectModuleNameService(httpHelper);
            _ProjectPackageService = new ProjectPackageService(httpHelper);
            _ProjectPricingSetupService = new ProjectPricingSetupService(httpHelper);
            _TaskService = new TaskService(httpHelper);
            _UserService = new UserService(httpHelper);
        }
        // GET: PaymentCalculationHeader
        public async Task<ActionResult> Index()
        {
            ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            ViewBag.TaskOfProjectId = new SelectList(_TaskService.List().Result.entity ?? new List<TaskVM>(), "Id", "TaskTitle");
            ViewBag.UserNameId = new SelectList(_UserService.List().entity.Where(x => x.ClientId != null).ToList() ?? new List<UserVM>(), "Id", "UserName");


            (ExecutionState executionState, List<PaymentCalculationHeaderVM> entity, string message) returnResponse = await _PaymentCalculationHeaderService.List();
            foreach (var item in returnResponse.entity)
            {
                item.CreatedByName = _UserService.GetById(item.CreatedBy).entity?.UserName ?? "";
            }

            return View(returnResponse.entity);
        }

        // GET: PaymentCalculationHeader/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {

            PaymentCalculationHeaderVM entity = new PaymentCalculationHeaderVM();
            //(ExecutionState executionState, List<PaymentCalculationHeaderVM> entity, string message) returnResponse = _PaymentCalculationHeaderService.List();
            //ViewBag.ProjectTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");

            ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            ViewBag.TaskOfProjectId = new SelectList(_TaskService.List().Result.entity ?? new List<TaskVM>(), "Id", "TaskTitle");
            ViewBag.ProjectModuleNameId = new SelectList(_ProjectModuleNameService.List().entity, "Id", "Name");
            ViewBag.ProjectPackageId = new SelectList(_ProjectPackageService.List().entity, "Id", "PackageName");


            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public async Task<ActionResult> Create(PaymentCalculationHeaderVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    // TODO: Add insert logic here

                    entity.PaymentCalculationRows = JsonConvert.DeserializeObject<List<PaymentCalculationRowVM>>(entity.PaymentCalculationRowsJson);


                    (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse1 = _PaymentCalculationHeaderService.Create(entity);

                    //                    Session["Message"] = returnResponse1.message;
                    //                    Session["executionState"] = returnResponse1.executionState;

                    if (returnResponse1.executionState.ToString() != "Created")
                    {
                        (ExecutionState executionState, List<PaymentCalculationHeaderVM> entity, string message) divisionLists = await _PaymentCalculationHeaderService.List();
                        return RedirectToAction("Index");
                        //return View(entity);
                    }
                    else
                    {
                        return Json(
                       new { Success = true, Data = returnResponse1.entity },
                       SerializerOption.Default);
                        //return RedirectToAction("Index");
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


        //// GET: PaymentCalculationHeader/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.GetById(id);

        //    ViewBag.ProjectTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name",(long) returnResponse.entity.ProjectTypeId);

        //    return View(returnResponse.entity);
        //}

        //// POST: PaymentCalculationHeader/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, PaymentCalculationHeaderVM entity)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // TODO: Add update logic here
        //            if (id != entity.Id)
        //            {
        //                return RedirectToAction(nameof(PaymentCalculationHeaderController.Index), "PaymentCalculationHeader");
        //            }
        //            entity.IsActive = true;
        //            entity.IsDeleted = false;
        //            entity.UpdatedAt = DateTime.Now;
        //            (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.Update(entity);
        //            //                    Session["Message"] = returnResponse.message;
        //            //                    Session["executionState"] = returnResponse.executionState;
        //            if (returnResponse.executionState.ToString() != "Updated")
        //            {
        //                return View(entity);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index");
        //            }
        //        }

        //        //                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
        //        //                Session["executionState"] = ExecutionState.Failure;
        //        return View(entity);
        //    }
        //    catch
        //    {
        //        //                Session["Message"] = "Form Data Not Valid.";
        //        //                Session["executionState"] = ExecutionState.Failure;
        //        return View(entity);
        //    }
        //}


        //// GET: PaymentCalculationHeader/Delete/5
        //public JsonResult Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot = _PaymentCalculationHeaderService.DoesExist(id);
        //    string message = "Failed, You can't delete this item.";

        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
        //    }

        //    (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.Delete(id);
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

        //// POST: PaymentCalculationHeader/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, PaymentCalculationHeaderVM entity)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        if (id != entity.Id)
        //        {
        //            return RedirectToAction(nameof(PaymentCalculationHeaderController.Index), "PaymentCalculationHeader");
        //        }
        //        //entity.IsActive = true;
        //        entity.IsDeleted = true;
        //        entity.UpdatedAt = DateTime.Now;
        //        (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.Update(entity);
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


        //Privet Method



        public JsonResult Delete(int id)
        {
            var result = _PaymentCalculationHeaderService.SoftDelete(id);
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


        public ActionResult GetProjectPackageByProjectModuleNameId(string id)
        {
            long ProjectModuleNameId = Convert.ToInt64(id);
            var data = _ProjectPackageService.GetProjectPackageByProjectModuleNameId(ProjectModuleNameId);

            return Json(
                       new { Success = true, Data = data.entity },
                       SerializerOption.Default);
        }

        [HttpPost]
        public ActionResult GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(data Data)
        {
            //long projectModuleNameId = Convert.ToInt64(ProjectModuleNameId);
            //long projectPackageId = Convert.ToInt64(ProjectPackageId);
            //var data = _ProjectPricingSetupService.GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(Data.ProjectModuleNameId, Data.ProjectPackageId);
            var data = _ProjectPricingSetupService.List().entity.Where(x => x.ProjectModuleNameId == Data.ProjectModuleNameId && x.ProjectPackageId == Data.ProjectPackageId);

            return Json(
                       new { Success = true, Data = data.FirstOrDefault() },
                       SerializerOption.Default);
        }

        public ActionResult ViewReport(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) returnResponse = _PaymentCalculationHeaderService.GetById(id);
            return View(returnResponse.entity);
        }


        public async Task<ActionResult> Search(string? ProjectRequestId, string? TaskOfProjectId, string UserNameId, DateTime? CreatedAt)
        {
            ViewBag.ProjectRequestId = new SelectList(_ProjectRequestService.List().Result.entity ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            ViewBag.TaskOfProjectId = new SelectList(_TaskService.List().Result.entity ?? new List<TaskVM>(), "Id", "TaskTitle");
            ViewBag.UserNameId = new SelectList(_UserService.List().entity ?? new List<UserVM>(), "Id", "UserName");

            (ExecutionState executionState, List<PaymentCalculationHeaderVM> entity, string message) returnResponse = await _PaymentCalculationHeaderService.List();
            foreach (var item in returnResponse.entity)
            {
                item.CreatedByName = _UserService.GetById(item.CreatedBy).entity?.UserName ?? "";
            }

            var entityList = returnResponse.entity;
            if (ProjectRequestId != null)
            {
                entityList = entityList.Where(x => x.ProjectRequestId == Convert.ToInt64(ProjectRequestId ?? "0")).ToList();
            }
            if (TaskOfProjectId != null)
            {
                entityList = entityList.Where(x => x.TaskOfProjectId == Convert.ToInt64(TaskOfProjectId ?? "0")).ToList();
            }
            if (UserNameId != null)
            {
                entityList = entityList.Where(x => x.CreatedBy == Convert.ToInt64(UserNameId)).ToList();
            }
            if (CreatedAt != null)
            {
                entityList = entityList.Where(x => x.CreatedAt.Date == CreatedAt.Value.Date).ToList();
            }


            return View("Index", entityList);

        }



        [HttpPost]
        public async Task<ActionResult> ConvertHtmlToDoc([FromBody] HtmlContentModel model)
        {
            var localFilePath = "";
            var storeFileName = "";
            var storeFilePath = "";
            bool isSuccess = false;
            var message = "";
            var htmlstring = model.HtmlContent;
            byte[] docFileByte;
            try
            {
                var newGuid = Guid.NewGuid().ToString();
                docFileByte = ConvertHtmlToWordByteArray(htmlstring);
                var testPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");
                var DoccurrentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var DOcnewGuid = Guid.NewGuid().ToString();
                var DocnewDiskFileName = $"{newGuid}_initialPayment.doc";
                var DocfilePath = Path.Combine(testPath, DocnewDiskFileName);

                localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/" + DocnewDiskFileName;
                storeFilePath = "/DocumentFile/" + DocnewDiskFileName;
                storeFileName = DocnewDiskFileName;
                // Write the byte array to the file
                System.IO.File.WriteAllBytes(DocfilePath, docFileByte);

                // Return the combined PDF file with the file path
                //return Json(new { filepath = filePath, Filename = "combine.pdf" });
                //var doclocalFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + DocfilePath;

                //File(filePath, "application/pdf", "combined.pdf");
                return Json(new { data = localFilePath });
            }
            catch (Exception ex)
            {
            }
            // Create a memory stream from the file bytes



            return View();
        }

        private byte[] ConvertHtmlToWordByteArray(string htmlContent)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a Wordprocessing document
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                {
                    // Add a main document part
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();
                    mainPart.Document.Append(body);

                    // Create a new paragraph and run
                    Paragraph paragraph = new Paragraph();
                    Run run = new Run();
                    paragraph.Append(run)
    ;
                    body.Append(paragraph);

                    // Insert HTML content
                    AlternativeFormatImportPart formatImportPart = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Html);
                    using (MemoryStream htmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlContent)))
                    {
                        formatImportPart.FeedData(htmlStream);
                    }

                    AltChunk altChunk = new AltChunk();
                    altChunk.Id = mainPart.GetIdOfPart(formatImportPart);
                    mainPart.Document?.Body?.Append(altChunk);
                    mainPart.Document?.Save();
                }

                return memoryStream.ToArray();
            }
        }


    }

    public class data
    {
        public long ProjectModuleNameId { get; set; }
        public long ProjectPackageId { get; set; }
    }

    public class HtmlContentModel
    {
        public string HtmlContent { get; set; }
    }
}
