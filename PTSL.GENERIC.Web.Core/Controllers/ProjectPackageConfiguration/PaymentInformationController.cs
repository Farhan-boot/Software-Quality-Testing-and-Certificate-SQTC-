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
using PTSL.GENERIC.Web.Core.Helper.Enum.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class PaymentInformationController : Controller
    {
        private readonly IPaymentInformationService _PaymentInformationService;
        private readonly IPaymentCalculationHeaderService _PaymentCalculationHeaderService;
        private readonly FileHelper _fileHelper;
        private readonly IReconciliationService _ReconciliationService;
        private readonly IProjectStateLogService _projectStateLogService;
        private readonly IUserService _userService;
        private readonly IProjectRequestService _projectRequestService;


        public PaymentInformationController(HttpHelper httpHelper, FileHelper fileHelper)
        {
            _PaymentInformationService = new PaymentInformationService(httpHelper);
            _PaymentCalculationHeaderService = new PaymentCalculationHeaderService(httpHelper);
            _fileHelper = fileHelper;
            _ReconciliationService = new ReconciliationService(httpHelper);
            _projectStateLogService = new ProjectStateLogService(httpHelper);
            _userService = new UserService(httpHelper);
            _projectRequestService = new ProjectRequestService(httpHelper);
        }
        // GET: PaymentInformation
        public ActionResult Index()
        {
            (ExecutionState executionState, List<PaymentInformationVM> entity, string message) returnResponse = _PaymentInformationService.List();

            if (returnResponse.entity != null)
            {
                foreach (var item in returnResponse.entity)
                {
                    var isPaymentApproved = false;
                    if (_ReconciliationService?.List().entity?.Count() > 0)
                    {
                        isPaymentApproved = _ReconciliationService?.List().entity.Any(x => x?.PaymentInformationId == item.Id && x.IsPaymentApproved == true) ?? false;
                    }

                    if (isPaymentApproved == true)
                    {
                        item.IsPaymentApproved = true;
                    }
                    else
                    {
                        item.IsPaymentApproved = false;
                    }
                }
            }


            return View(returnResponse.entity ?? new List<PaymentInformationVM>());
        }

        // GET: PaymentInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            PaymentInformationVM entity = new PaymentInformationVM();
            (ExecutionState executionState, List<PaymentInformationVM> entity, string message) returnResponse = _PaymentInformationService.List();

            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            var ClientProject = _projectRequestService.GetProjectListByClientId(clientId).Result.entity ?? new List<ProjectRequestVM>();
            ViewBag.ProjectRequestId = new SelectList(ClientProject ?? new List<ProjectRequestVM>(), "Id", "ProjectName");

            ViewBag.PaymentMethodEnumId = new SelectList(EnumHelper.GetEnumDropdowns<PaymentMethodEnum>(), "Id", "Name");
            return View(entity);
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(PaymentInformationVM entity)
        {
            try
            {
                //if (ModelState.IsValid)
                //{

                if (_PaymentInformationService?.List().entity?.Count() > 0)
                {
                    var checkData = _PaymentInformationService?.List().entity?.Any(x => x.PaymentCalculationHeaderId == entity.PaymentCalculationHeaderId);
                    if (checkData == true)
                    {
                        return RedirectToAction("Create");
                    }
                }


                if (entity?.file?.Count() > 0)
                {
                    // Save files
                    if (SaveFiles(entity.file, ref entity, FileType.Image, out var imageFileError) == false)
                    {
                        return Json(
                            new { Success = false, Message = imageFileError },
                            SerializerOption.Default);
                    }
                }

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;
                // TODO: Add insert logic here
                (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse1 = _PaymentInformationService.Create(entity);

                //                    Session["Message"] = returnResponse1.message;
                //                    Session["executionState"] = returnResponse1.executionState;

                if (returnResponse1.executionState.ToString() != "Created")
                {
                    (ExecutionState executionState, List<PaymentInformationVM> entity, string message) divisionLists = _PaymentInformationService.List();

                    return View(entity);
                }
                else
                {
                    if (returnResponse1.entity.PaymentCalculationHeader != null)
                    {
                        long EnumId = (long)ProjectState.Payment;
                        long ProjectRequestId = returnResponse1.entity.PaymentCalculationHeader.ProjectRequestId.Value;
                        var logres = _projectStateLogService.GetLogData(ProjectRequestId, EnumId);
                        if (logres.entity == null)
                        {
                            ProjectStateLogVM projectStateLog = new ProjectStateLogVM
                            {
                                ProjectRequestId = ProjectRequestId,
                                ProjectState = ProjectState.Payment,
                                IsStateCompleted = true
                            };
                            var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                        }

                    }

                    return RedirectToAction("ClientIndex");
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


        // GET: PaymentInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.GetById(id);


            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            var ClientProject = _projectRequestService.GetProjectListByClientId(clientId).Result.entity ?? new List<ProjectRequestVM>();
            ViewBag.ProjectRequestId = new SelectList(ClientProject ?? new List<ProjectRequestVM>(), "Id", "ProjectName", (long)returnResponse.entity.ProjectRequestId);
            //ViewBag.PaymentCalculationHeaderId = new SelectList(_PaymentCalculationHeaderService.List().Result.entity ?? new List<PaymentCalculationHeaderVM>(), "Id", "ProjectRequest.ProjectName", returnResponse.entity.PaymentCalculationHeaderId);

            ViewBag.PaymentMethodEnumId = new SelectList(EnumHelper.GetEnumDropdowns<PaymentMethodEnum>(), "Id", "Name", (long)returnResponse.entity?.PaymentMethodEnumId);
            return View(returnResponse.entity);
        }

        // POST: PaymentInformation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PaymentInformationVM entity)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(PaymentInformationController.Index), "PaymentInformation");
                }
                if (entity?.file?.Count() > 0)
                {
                    // Save files
                    if (SaveFiles(entity.file, ref entity, FileType.Image, out var imageFileError) == false)
                    {
                        return Json(
                            new { Success = false, Message = imageFileError },
                            SerializerOption.Default);
                    }
                }

                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.Update(entity);
                //                    Session["Message"] = returnResponse.message;
                //                    Session["executionState"] = returnResponse.executionState;
                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return View(entity);
                }
                else
                {
                    return RedirectToAction("ClientIndex");
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


        //// GET: PaymentInformation/Delete/5
        //public JsonResult Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot = _PaymentInformationService.DoesExist(id);
        //    string message = "Failed, You can't delete this item.";

        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
        //    }

        //    (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.Delete(id);
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

        //// POST: PaymentInformation/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, PaymentInformationVM entity)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        if (id != entity.Id)
        //        {
        //            return RedirectToAction(nameof(PaymentInformationController.Index), "PaymentInformation");
        //        }
        //        //entity.IsActive = true;
        //        entity.IsDeleted = true;
        //        entity.UpdatedAt = DateTime.Now;
        //        (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.Update(entity);
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

        public ActionResult GetPaymentByProjectRequestId(string id)
        {
            long ProjectRequestId = Convert.ToInt64(id);
            var data = _PaymentCalculationHeaderService.GetById(ProjectRequestId);

            return Json(
                       new { Success = true, Data = data.entity.GrandTotal },
                       SerializerOption.Default);
        }

        public ActionResult GetPaymentCalculationHeaderByProjectRequestId(string id)
        {
            long ProjectRequestId = Convert.ToInt64(id);

            var data = _PaymentCalculationHeaderService.List().Result.entity.Where(x => x.ProjectRequestId == ProjectRequestId);

            return Json(
                       new { Success = true, Data = data?.FirstOrDefault() ?? new PaymentCalculationHeaderVM() },
                       SerializerOption.Default);
        }

        public JsonResult Delete(int id)
        {
            var result = _PaymentInformationService.SoftDelete(id);
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

        public ActionResult RejectedPayments()
        {
            var RejectedPaymentData = _ReconciliationService.List().entity?.Where(x => x.IsPaymentApproved == false).Select(x => x?.PaymentInformation).ToList();
            // if (RejectedPaymentData != null)
            //{
            return View(RejectedPaymentData ?? new List<PaymentInformationVM>());
            //}
        }


        public ActionResult ClientIndex()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var clientId = _userService.GetById(userId).entity?.ClientId ?? 0;
            if (clientId == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                (ExecutionState executionState, List<PaymentInformationVM> entity, string message) returnResponse = _PaymentInformationService.List();

                if (returnResponse.entity != null)
                {
                    foreach (var item in returnResponse.entity)
                    {
                        var isPaymentApproved = false;
                        if (_ReconciliationService?.List().entity?.Count() > 0)
                        {
                            isPaymentApproved = _ReconciliationService?.List().entity.Any(x => x?.PaymentInformationId == item.Id && x.IsPaymentApproved == true) ?? false;
                        }

                        if (isPaymentApproved == true)
                        {
                            item.IsPaymentApproved = true;
                        }
                        else
                        {
                            item.IsPaymentApproved = false;
                        }
                    }
                }
                else
                {
                   return View( returnResponse.entity = new List<PaymentInformationVM>());
                }
                return View(returnResponse.entity.Where(x=>x?.PaymentCalculationHeader.ProjectRequest?.ClientId == clientId) ?? new List<PaymentInformationVM>());
            }
        }


        public ActionResult RejectedPaymentDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, PaymentInformationVM entity, string message) returnResponse = _PaymentInformationService.GetById(id);
            return View(returnResponse.entity);
        }

        private bool SaveFiles(IReadOnlyList<IFormFile> files, ref PaymentInformationVM entity, FileType fileType, out string error)
        {
            foreach (var file in files)
            {
                var (isSaved, fileName, _) = _fileHelper.SaveFile(file, fileType, "PaymentInformation", out var errorMessage);
                if (isSaved == false)
                {
                    error = errorMessage;
                    return false;
                }

                var entityFile = new DepositSlipFileVM
                {
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    PaymentInformationId = entity.Id,
                    //FileName = file.FileName,
                    //FileType = fileType,
                    FilePathUrl = fileName,
                };

                entity.DepositSlipFiles.Add(entityFile);
            }

            error = string.Empty;
            return true;
        }

    }
}
