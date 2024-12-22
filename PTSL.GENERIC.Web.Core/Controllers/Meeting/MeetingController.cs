using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class MeetingController : Controller
    {
        private readonly IMeetingService _MeetingService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IMeetingTypeService _MeetingTypeService;
        private readonly FileHelper _fileHelper;
        private readonly IProjectStateLogService _projectStateLogService;
        public MeetingController(HttpHelper httpHelper, FileHelper fileHelper)
        {
            _MeetingService = new MeetingService(httpHelper);
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _MeetingTypeService = new MeetingTypeService(httpHelper);
            _fileHelper = fileHelper;
            _projectStateLogService = new ProjectStateLogService(httpHelper);
        }
        // GET: Meeting
        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, List<MeetingVM> entity, string message) returnResponse = await _MeetingService.List();
            var response = returnResponse.entity is not null ? returnResponse.entity.Where(x => x.MeetingStatus == MeetingStatus.Accept) : new List<MeetingVM>();
            return View(response);
        }

        // GET: Meeting/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.GetById(id);
            return View(returnResponse.entity);
        }

        
        public async Task<ActionResult> Create()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var filterdUser = returnResponseProject.entity.ToList();
                ViewBag.ProjectRequestId = new SelectList(filterdUser ?? new List<ProjectRequestVM>(), "Id", "ProjectName");
            }
            else
            {
                ViewBag.ProjectRequestId = new SelectList("");
            }
            (ExecutionState executionState, List<MeetingTypeVM> entity, string message) returnResponseMeeting =  _MeetingTypeService.List();
            if (returnResponseMeeting.entity != null)
            {
                var filterdUser = returnResponseMeeting.entity.ToList();
                ViewBag.MeetingTypeId = new SelectList(filterdUser ?? new List<MeetingTypeVM>(), "Id", "MeetingTypeName");
            }
            else
            {
                ViewBag.MeetingTypeId = new SelectList("");
            }
            ViewBag.ClientUserId = new SelectList("");
            var qetSqtcUser = await _MeetingService.GetSqtcUser();
            ViewBag.SqtcUserId = new SelectList(qetSqtcUser.entity.ToList(),"Id","UserName");
            MeetingVM entity = new MeetingVM();
            entity.SqtcUser = qetSqtcUser.entity;
            return View(entity);
        }

        // POST: Meeting/Create
        [HttpPost]
        public async Task<ActionResult> Create(MeetingVM entity)
        {
            try
            {
                var accptedUserMeetings = JsonConvert.DeserializeObject<List<AttendedUserMeetingVM>>(HttpContext.Request.Form["accptedUserMeetings"]!);
                    entity.AttendedUsers = accptedUserMeetings;
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.MeetingStatus = MeetingStatus.Accept;
                    entity.IsInitiatedBySqtc = true;
                    entity.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    // TODO: Add insert logic here
                    (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.Create(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return View(entity);
                    }
                    else
                    {
                    long EnumId = (long)ProjectState.InitialKnowledgeSharingMeeting;
                    var logres = _projectStateLogService.GetLogData(returnResponse.entity.ProjectRequestId, EnumId);
                    if(logres.entity == null)
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                        projectStateLog.ProjectRequestId = returnResponse.entity.ProjectRequestId;
                        projectStateLog.ProjectState = ProjectState.InitialKnowledgeSharingMeeting;
                        projectStateLog.IsStateCompleted = true;
                        var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                    }
                       return Json(returnResponse.message="Success", SerializerOption.Default);
                    }
                
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }


        // GET: Meeting/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, MeetingVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(MeetingController.Index), "Meeting");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.Update(entity);
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

        [HttpGet]
        public async Task<ActionResult> GetClientUserByProjectID(long projectId)
        {
            var result = await _MeetingService.GetClientUser(projectId);
            if (result.entity == null)
            {
                return Json(new TestCaseVM(), SerializerOption.Default);
            }

            return Json(result.entity, SerializerOption.Default);
        }

        public async Task<ActionResult> PendingList()
        {
            (ExecutionState executionState, IList<MeetingVM> entity, string message) returnResponse =  await _MeetingService.pendingMeetingList();
            return View(returnResponse.entity);
        }
        public async Task<ActionResult> RejectedList()
        {
            (ExecutionState executionState, IList<MeetingVM> entity, string message) returnResponse = await _MeetingService.List();
            return View(returnResponse.entity.Where(x=>x.MeetingStatus == MeetingStatus.Reject));
        }

        public JsonResult Delete(int id)
        {
            var result = _MeetingService.SoftDelete(id);
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

        // POST: Meeting/Delete/5
        //        [HttpPost]
        //        public ActionResult Delete(int id, MeetingVM entity)
        //        {
        //            try
        //            {
        //                // TODO: Add update logic here
        //                if (id != entity.Id)
        //                {
        //                    return RedirectToAction(nameof(MeetingController.IndexAsync), "Meeting");
        //                }
        //                //entity.IsActive = true;
        //                entity.IsDeleted = true;
        //                entity.UpdatedAt = DateTime.Now;
        //                (ExecutionState executionState, MeetingVM entity, string message) returnResponse = _MeetingService.Update(entity);
        ////                Session["Message"] = returnResponse.message;
        ////                Session["executionState"] = returnResponse.executionState;
        //                //return View(returnResponse.entity);
        //                // return RedirectToAction("Edit?id="+id);
        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        [HttpPost]
        public async Task<ActionResult> Accept(int id)
        {
             if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    
                    var Response = await _MeetingService.GetById(id);
                    Response.entity.IsActive = true;
                    Response.entity.IsDeleted = false;
                    Response.entity.UpdatedAt = DateTime.Now;
                    Response.entity.MeetingStatus = MeetingStatus.Accept;
                    (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.Update(Response.entity);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(Response.entity);
                    }
                    else
                    {
                        return Json(new { Data = returnResponse, Message = "" }, SerializerOption.Default);
                    }
             }
          
            return RedirectToAction("PendingList");

        }
        [HttpPost]
        public async Task<ActionResult> Reject(int id)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here

                var Response = await _MeetingService.GetById(id);
                Response.entity.IsActive = true;
                Response.entity.IsDeleted = false;
                Response.entity.UpdatedAt = DateTime.Now;
                Response.entity.MeetingStatus = MeetingStatus.Reject;
                (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.Update(Response.entity);
                //                    Session["Message"] = returnResponse.message;
                //                    Session["executionState"] = returnResponse.executionState;
                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return View(Response.entity);
                }
                else
                {
                    return Json(new { Data = returnResponse, Message = "" }, SerializerOption.Default);
                }
            }

            return RedirectToAction("PendingList");

        }

        [HttpPost]
        public async Task<ActionResult> AddMeetingMinutes(int id,MeetingVM entity)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here

                var Response = await _MeetingService.GetById(id);
                var documentFiles = HttpContext.Request.Form.Files.GetFiles("MeetingFiles");
                if (SaveFiles(documentFiles, ref entity, FileType.Document, out var documentFileError) == false)
                {
                    return Json(
                        new { Success = false, Message = documentFileError },
                        SerializerOption.Default);
                }
                Response.entity.MeetingFiles = entity.MeetingFiles;
                Response.entity.IsActive = true;
                Response.entity.Remarks = entity.Remarks;
                Response.entity.IsDeleted = false;
                Response.entity.UpdatedAt = DateTime.Now;
                Response.entity.ProjectRequest= null;

                (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.Update(Response.entity);
                //                    Session["Message"] = returnResponse.message;
                //                    Session["executionState"] = returnResponse.executionState;
                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return View(Response.entity);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        private bool SaveFiles(IReadOnlyList<IFormFile> files, ref MeetingVM entity, FileType fileType, out string error)
        {
            foreach (var file in files)
            {
                var (isSaved, fileName, _) = _fileHelper.SaveFile(file, fileType, "Meeting", out var errorMessage);
                if (isSaved == false)
                {
                    error = errorMessage;
                    return false;
                }

                var entityFile = new MeetingFilesVM
                {
                    FileName = file.FileName,
                    FileNameUrl = fileName,
                };

                entity?.MeetingFiles?.Add(entityFile);
            }

            error = string.Empty;
            return true;
        }

        [HttpGet]
        public async Task<ActionResult> MeetingListByDate(DateTime firstDate, DateTime lastDate)
        {
            (ExecutionState executionState, IList<MeetingVM> entity, string message) returnResponse = await _MeetingService.MeetingListByDate(firstDate,lastDate);
            return Json(new
            {
                Data = returnResponse.entity.OrderByDescending(s => s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);
        }
    }
}
