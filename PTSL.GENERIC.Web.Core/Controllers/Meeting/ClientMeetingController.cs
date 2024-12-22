using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    [SessionAuthorize]
    public class ClientMeetingController : Controller
    {
        private readonly IMeetingService _MeetingService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IMeetingTypeService _MeetingTypeService;
        private readonly IUserService _userService;
        private readonly IProjectStateLogService _projectStateLogService;
        private readonly IClientService _clientService;

        public ClientMeetingController(HttpHelper httpHelper)
        {
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _MeetingTypeService = new MeetingTypeService(httpHelper);
            _MeetingService = new MeetingService(httpHelper);
            _userService = new UserService(httpHelper);
            _projectStateLogService = new ProjectStateLogService(httpHelper);
            _clientService = new ClientService(httpHelper);
        }
        // GET: Meeting
        public async Task<ActionResult> Index()
        {
            var currentUser = _userService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
            var ClientId = currentUser.entity.ClientId ?? 0;
            (ExecutionState executionState, IList<MeetingVM> entity, string message) returnResponse = await _MeetingService.MeetingListByClientId(ClientId);
            ViewBag.ClientName = _clientService.GetById(ClientId).Result.entity?.ClientName ?? "";

            return View(returnResponse.entity ?? new List<MeetingVM>());
        }

        // GET: Meeting/Details/5
        public async Task<ActionResult> Details(int? id, string backUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.BackUrl = backUrl;
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse = await _MeetingService.GetById(id);
            return View(returnResponse.entity);
        }




        // GET: Meeting/Create
        public async Task<ActionResult> Create()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponseProject = await _ProjectRequestService.List();
            if (returnResponseProject.entity != null)
            {
                var currentUser = _userService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
                var ClientId = currentUser.entity?.ClientId ?? 0;
                var filterdUser = returnResponseProject.entity.ToList().Where(x=>x.ClientId == ClientId);
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
                    entity.MeetingStatus = MeetingStatus.Pending;
                    entity.IsInitiatedBySqtc = false;
                    entity.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var currentUser = _userService.GetById(entity.CreatedBy);
                    var ClientId = currentUser.entity.ClientId??0;
                    entity.ClientId = ClientId;
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
                    if (logres.entity == null)
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                        projectStateLog.ProjectRequestId = returnResponse.entity.ProjectRequestId;
                        projectStateLog.ProjectState = ProjectState.InitialKnowledgeSharingMeeting;
                        projectStateLog.IsStateCompleted = true;
                        var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                    }
                    return Json(returnResponse.message = "Success", SerializerOption.Default);

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
                        return RedirectToAction(nameof(ClientMeetingController.Index), "ClientMeeting");
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


        // GET: Meeting/Delete/5
        //public async Task<JsonResult> Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot =  await _MeetingService.DoesExist(id);
        //    string message = "Faild, You can't delete this item.";
        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

        //    }
        //    (ExecutionState executionState, MeetingVM entity, string message) returnResponse = _MeetingService.Delete(id);
        //    if (returnResponse.executionState.ToString() == "Updated")
        //    {
        //        returnResponse.message = "Meeting deleted successfully.";
        //    }
        //    else
        //    {
        //        returnResponse.message = "Failed to delete this item.";
        //    }
        //    return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
        //    //return View();
        //}

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
    }
}
