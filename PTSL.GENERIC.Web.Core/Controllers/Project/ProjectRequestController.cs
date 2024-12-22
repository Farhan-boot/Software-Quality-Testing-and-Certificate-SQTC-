using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForProjectLogVM;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;
using PTSL.GENERIC.Web.Core.EmailServices;
using static iText.IO.Util.IntHashtable;

namespace PTSL.GENERIC.Web.Core.Controllers.Project
{
    public class ProjectRequestController : Controller
    {
        public const string Uploads = "uploads";
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IClientService _ClientService;
        private readonly IUserRoleService   _UserRoleService;
        private readonly IUserService   _UserService;
        private readonly IApprovalForProjectLogService _approvalForProjectLogService;
        private readonly IPermissionHeaderSettingsService _permissionHeaderSettingsService;
        private readonly IProjectStateLogService _projectStateLogService;
        public ProjectRequestController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _ProjectRequestService = new ProjectRequestService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _ClientService = new ClientService(httpHelper);
            _UserRoleService = new UserRoleService(httpHelper);
            _UserService = new UserService(httpHelper);
            _approvalForProjectLogService = new ApprovalForProjectLogService(httpHelper);
            _permissionHeaderSettingsService = new PermissionHeaderSettingsService(httpHelper);
            _projectStateLogService = new ProjectStateLogService(httpHelper);
        }

        public async Task<ActionResult> Index()
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if (returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.List();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.Client_User || userType == UserType.ClientAdmin)
                    ViewBag.HasCreatePermission = true;
                else
                    ViewBag.HasCreatePermission = false;

                if (userType == UserType.SQTC_Admin || userType == UserType.ClientAdmin)
                    ViewBag.HasEditDeletePermission = true;
                else
                    ViewBag.HasEditDeletePermission = false;
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                ViewBag.HasEditDeletePermission = false;
            }

            var filterResponse = returnResponse.entity.OrderByDescending(s => s.Id).ToList();

            return View(filterResponse);
        }

        public async Task<ActionResult> ClientProject()
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if (returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _UserService.GetById(userId).entity?.ClientId??0;

            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectListByClientId(clientId);
            var userType = _UserService.GetById(userId).entity?.UserType;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.Client_User || userType == UserType.ClientAdmin)
                    ViewBag.HasCreatePermission = true;
                else
                    ViewBag.HasCreatePermission = false;

                if (userType == UserType.SQTC_Admin || userType == UserType.ClientAdmin)
                    ViewBag.HasEditDeletePermission = true;
                else
                    ViewBag.HasEditDeletePermission = false;
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                ViewBag.HasEditDeletePermission = false;
            }

            var filterResponse = returnResponse.entity.OrderByDescending(s => s.Id).ToList();
            foreach (var item in returnResponse.entity)
            {
                var result = await _approvalForProjectLogService.ProjectCommentHistoryById(item.Id);
                if(result.entity.Count()>0)
                {
                    item.editFlag = false;
                }
                else
                {
                    item.editFlag = true;
                }
            }
            return View(filterResponse);
        }

        public async Task<ActionResult> Create()
       {
            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

            (ExecutionState executionState, List<UserVM> entity, string message) returnUserResponse = _UserService.List();
            (ExecutionState executionState, List<ClientVM> entity, string message) returnClientResponse = await _ClientService.List();

            var signedClient = returnUserResponse.entity.Where(x => x.Id == userId).FirstOrDefault();
            var clientModel = returnClientResponse.entity.Where(s => s.Id == signedClient?.ClientId).FirstOrDefault();

            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            ProjectRequestVM entity = new ProjectRequestVM();
            ViewBag.RequestedBy = signedClient != null ? clientModel?.ClientName /*+ "-" +signedClient.c*/ : "";
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectRequestVM entity)
        {
            try
            {
                _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

                (ExecutionState executionState, List<UserVM> entity, string message) returnUserResponse = _UserService.List();

                var signedClient = returnUserResponse.entity.Where(x => x.Id == userId).FirstOrDefault();
                if(signedClient != null && !signedClient.ClientId.HasValue)
                {
                    HttpContext.Session.SetString("Message", "Client Not Found");
                    HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());
                    return View(entity);
                }
                //Remove Validation for Description
                ModelState.Remove("ProjectDescription");
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.ProjectApprovalStatus = ProjectApprovalStatus.Pending;
                    var projectFile = HttpContext.Request.Form.Files.GetFile("ProjectFile");
                    if (!string.IsNullOrEmpty(Request.Form["RequestedDate"]))
                    {
                        entity.RequestDate = Convert.ToDateTime(Request.Form["RequestedDate"]);
                    }
                    var fileHelper = new FileHelper(_hostEnvironment);
                    var result = fileHelper.SaveFileAll(projectFile!, "ProjectRequest", out var errorMessage);
                    if (errorMessage != null)
                    {
                        // errror
                    }

                    entity.FilePath = result.Item2;
                    entity.FileName = result.Item3;
                    entity.ClientId = signedClient != null ? signedClient.ClientId.Value : 0;
                    entity.ProjectType = entity.ProjectType;
                    entity.ProjectDescription = entity.ProjectDescription == null ? string.Empty : entity.ProjectDescription;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse = await _ProjectRequestService.Create(entity);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        HttpContext.Session.SetString("Message", returnResponse.message);
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        return View(entity);
                    }
                    else
                    {
                        ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                        projectStateLog.ProjectRequestId = returnResponse.entity.Id;
                        projectStateLog.ProjectState = ProjectState.ProjectRequestSubmitted;
                        projectStateLog.IsStateCompleted = true;
                        var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                        HttpContext.Session.SetString("Message", "Project successfully created.");
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                        return RedirectToAction("ClientProject");
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

        [HttpPost]
        public JsonResult SaveMapToUser(ApprovalForProjectLogVM entity)
        {
            entity.IsActive = true;
            entity.SendingTime = DateTime.Now.ToLocalTime();
            entity.SendingDate = DateTime.Now;
            entity.CreatedAt = DateTime.Now;
            entity.ProjectApprovalStatus = ProjectApprovalStatus.Pending;
            entity.ProcessFlowType = ProcessFlowType.Forward;
            entity.SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ReceiverName = _UserService.GetById(entity.ReceiverId).entity.UserName;
            entity.Description = "Project Forwarded To" + " " + ReceiverName;
            //entity.SenderRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponse = _approvalForProjectLogService.Create(entity);

            return Json(new { Data = returnResponse, Message = "Success" }, SerializerOption.Default);
        }

        public JsonResult GetApprovalProcessModalData(long id)
        {

             // Ensure it's a list
            long moduleEnumId = (long)ModuleEnum.TestProjectRequestApproval;
            var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault(); // Retrieve first or default
            var currentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            if (roleResult != null && roleResult.PermissionRowSettings != null && currentUserRoleId != 1)
            {
                var logResult = _approvalForProjectLogService.List().entity.Where(x => x.ProjectID == id);

                var permissionRowIds = logResult.Select(x => x.PermissionRowSettingsId).ToList();
                // Filter the PermissionRowSettings based on PermissionRowId
                var newResult = roleResult.PermissionRowSettings.FirstOrDefault(row => !permissionRowIds.Contains(row.Id));

                if (newResult != null && newResult.UserRole != null)
                {
                    // Access properties from UserRole
                    var RoleName = newResult.UserRole.RoleName;
                    var RoleId = newResult.UserRole.Id;
                    var PermissionRowSettingsId = newResult.Id;
                    var userList = _UserService.GetUserInfoByUserRoleId(RoleId);
                    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    // Use roleName and roleId as needed
                }

            }
            else
            {
                var newResult = roleResult.PermissionRowSettings?.FirstOrDefault();

                if (newResult != null && newResult.UserRole != null)
                {
                    // Access properties from UserRole
                    var RoleName = newResult.UserRole.RoleName;
                    var RoleId = newResult.UserRole.Id;
                    var PermissionRowSettingsId = newResult.Id;
                    var userList = _UserService.GetUserInfoByUserRoleId(RoleId);
                    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    // Use roleName and roleId as needed
                }
            }


            return Json(new { Data = "null", Message = "" }, SerializerOption.Default);
        }

        [HttpPost]
        public async Task<ActionResult> Accept(int id)
        {
            try
            {
                var ProjectResult = await _ProjectRequestService.GetById(id);
                ProjectResult.entity.IsActive = true;
                ProjectResult.entity.ProjectApprovalStatus = ProjectApprovalStatus.Accept;

                (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse = await _ProjectRequestService.Update(ProjectResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingList");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var ApproverName = _UserService.GetById(SenderId).entity.UserName;
                    ApprovalForProjectLogVM acceptedProjectLog = new ApprovalForProjectLogVM();
                    acceptedProjectLog.ProjectID = returnResponse.entity.Id;
                    acceptedProjectLog.ProjectApprovalStatus = ProjectApprovalStatus.Accept;
                    acceptedProjectLog.Description = "Approved by " + "" + ApproverName;
                    acceptedProjectLog.Remark = "Approved by " + "" + ApproverName;
                    acceptedProjectLog.SenderId = SenderId;
                    (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponseLog = _approvalForProjectLogService.Create(acceptedProjectLog);
                    if(returnResponseLog.executionState == ExecutionState.Created)
                    {
                        var ClientEmail = _ClientService.GetById(ProjectResult.entity.ClientId).Result.entity.WorkingEmail;
                        List<string> UserEmails = new List<string>();
                        List<string> BccList = new List<string>();
                        UserEmails.Add(ClientEmail);
                        var body = $@"
                                                <!DOCTYPE html>
                                                <html lang='en'>
                                                <head>
                                                    <meta charset='UTF-8'>
                                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                                    <style>
                                                        body {{
                                                            font-family: Arial, sans-serif;
                                                            background-color: #f6f6f6;
                                                            margin: 0;
                                                            padding: 0;
                                                        }}
                                                        .email-container {{
                                                            max-width: 600px;
                                                            margin: 0 auto;
                                                            background-color: #ffffff;
                                                            padding: 20px;
                                                            border-radius: 10px;
                                                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                                        }}
                                                        .header {{
                                                            text-align: center;
                                                            padding: 10px 0;
                                                            border-bottom: 1px solid #eeeeee;
                                                        }}
                                                        .content {{
                                                            padding: 20px;
                                                        }}
                                                        .content h5 {{
                                                            color: #333333;
                                                            font-size: 18px;
                                                            margin-bottom: 10px;
                                                        }}
                                                        .content h6 {{
                                                            color: #666666;
                                                            font-size: 16px;
                                                            margin-bottom: 10px;
                                                        }}
                                                        .content p {{
                                                            color: #666666;
                                                            line-height: 1.6;
                                                            font-size: 14px;
                                                        }}
                                                        .footer {{
                                                            text-align: center;
                                                            padding: 10px 0;
                                                            border-top: 1px solid #eeeeee;
                                                            color: #aaaaaa;
                                                            font-size: 12px;
                                                        }}
                                                    </style>
                                                </head>
                                                <body>
                                                    <div class='email-container'>
                                                        <div class='header'>
                                                            <h2>SHQTC</h2>
                                                        </div>
                                                        <div class='content'>
                                                            <h5>Welcome to SHQTC</h5>
                                                            <h6>Your request for {ProjectResult.entity.ProjectName} project has been aceepted.</h6>
                                                        </div>
                                                        <div class='footer'>
                                                            <p>&copy; 2024 SHQTC. All rights reserved.</p>
                                                        </div>
                                                    </div>
                                                </body>
                                                </html>";
                        var result = EmailService.SendEmailAsync(UserEmails, BccList, "Project Approval", body, "");
                    }

                    
                    if (returnResponseLog.entity != null)
                    {
                        long EnumId = (long)ProjectState.ProjectAccepted;

                        var logres = _projectStateLogService.GetLogData(returnResponseLog.entity.Id, EnumId);
                        if (logres.entity == null)
                        {
                            ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                            projectStateLog.ProjectRequestId = returnResponse.entity.Id;
                            projectStateLog.ProjectState = ProjectState.ProjectAccepted;
                            projectStateLog.IsStateCompleted = true;
                            var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                        }
                        return Json(new { Data = returnResponse, Message = "Success" }, SerializerOption.Default);
                    }
                    else
                    {
                        return Json(new { Data = "", Message = "" }, SerializerOption.Default);
                    }

                }

            }
            catch
            {
                return RedirectToAction("PendingList");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Reject(int id, ProjectRequestVM entity)
        {
            try
            {
                var ProjectResult = await _ProjectRequestService.GetById(id);
                ProjectResult.entity.IsActive = true;
                ProjectResult.entity.ProjectApprovalStatus = ProjectApprovalStatus.Reject;
                ProjectResult.entity.RejectionComment = entity.RejectionComment;

                (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse = await _ProjectRequestService.Update(ProjectResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingList");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var RejectorName = _UserService.GetById(SenderId).entity.UserName;
                    ApprovalForProjectLogVM acceptedProjectLog = new ApprovalForProjectLogVM();
                    acceptedProjectLog.ProjectID = returnResponse.entity.Id;
                    acceptedProjectLog.ProjectApprovalStatus = ProjectApprovalStatus.Accept;
                    acceptedProjectLog.Description = "Rejected by " + "" + RejectorName;
                    acceptedProjectLog.Remark = "Rejected by " + "" + RejectorName;
                    acceptedProjectLog.SenderId = SenderId;
                    (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponseLog = _approvalForProjectLogService.Create(acceptedProjectLog);
                    if (returnResponseLog.executionState == ExecutionState.Created)
                    {
                        var ClientEmail = _ClientService.GetById(ProjectResult.entity.ClientId).Result.entity.WorkingEmail;
                        List<string> UserEmails = new List<string>();
                        List<string> BccList = new List<string>();
                        UserEmails.Add(ClientEmail);
                        var body = $@"
                                                <!DOCTYPE html>
                                                <html lang='en'>
                                                <head>
                                                    <meta charset='UTF-8'>
                                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                                    <style>
                                                        body {{
                                                            font-family: Arial, sans-serif;
                                                            background-color: #f6f6f6;
                                                            margin: 0;
                                                            padding: 0;
                                                        }}
                                                        .email-container {{
                                                            max-width: 600px;
                                                            margin: 0 auto;
                                                            background-color: #ffffff;
                                                            padding: 20px;
                                                            border-radius: 10px;
                                                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                                        }}
                                                        .header {{
                                                            text-align: center;
                                                            padding: 10px 0;
                                                            border-bottom: 1px solid #eeeeee;
                                                        }}
                                                        .content {{
                                                            padding: 20px;
                                                        }}
                                                        .content h5 {{
                                                            color: #333333;
                                                            font-size: 18px;
                                                            margin-bottom: 10px;
                                                        }}
                                                        .content h6 {{
                                                            color: #666666;
                                                            font-size: 16px;
                                                            margin-bottom: 10px;
                                                        }}
                                                        .content p {{
                                                            color: #666666;
                                                            line-height: 1.6;
                                                            font-size: 14px;
                                                        }}
                                                        .footer {{
                                                            text-align: center;
                                                            padding: 10px 0;
                                                            border-top: 1px solid #eeeeee;
                                                            color: #aaaaaa;
                                                            font-size: 12px;
                                                        }}
                                                    </style>
                                                </head>
                                                <body>
                                                    <div class='email-container'>
                                                        <div class='header'>
                                                            <h2>SHQTC</h2>
                                                        </div>
                                                        <div class='content'>
                                                            <h5>Welcome to SHQTC</h5>
                                                            <h6>Your request for {ProjectResult.entity.ProjectName} project has been rejected.</h6>
                                                        </div>
                                                        <div class='footer'>
                                                            <p>&copy; 2024 SHQTC. All rights reserved.</p>
                                                        </div>
                                                    </div>
                                                </body>
                                                </html>";
                        var result = EmailService.SendEmailAsync(UserEmails, BccList, "Project Approval", body, "");
                    }

                    if (returnResponseLog.entity != null)
                    {
                        long EnumId = (long)ProjectState.ProjectRejected;

                        var logres = _projectStateLogService.GetLogData(returnResponseLog.entity.Id, EnumId);
                        if (logres.entity == null)
                        {
                            ProjectStateLogVM projectStateLog = new ProjectStateLogVM();
                            projectStateLog.ProjectRequestId = returnResponse.entity.Id;
                            projectStateLog.ProjectState = ProjectState.ProjectRejected;
                            projectStateLog.IsStateCompleted = true;
                            var ProjectStateResult = _projectStateLogService.Create(projectStateLog);
                        }
                        return RedirectToAction("PendingList");
                    }
                    else
                    {
                        return RedirectToAction("PendingList");
                    }


                }

            }
            catch
            {
                return RedirectToAction("PendingList");
            }
        }
        public async Task<ActionResult> PendingList()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectPendingList();
            var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.Client_User || userType == UserType.ClientAdmin)
                    ViewBag.HasCreatePermission = true;
                else
                    ViewBag.HasCreatePermission = false;

                if (userType == UserType.SQTC_Admin || userType == UserType.ClientAdmin)
                    ViewBag.HasEditDeletePermission = true;
                else
                    ViewBag.HasEditDeletePermission = false;
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                ViewBag.HasEditDeletePermission = false;
            }

            var allProjectLog = _approvalForProjectLogService.List().entity;

            if (allProjectLog != null)
            {
                long moduleEnumId = (long)ModuleEnum.TestProjectRequestApproval;

                var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault();
                var NewResultRoleId = roleResult?.PermissionRowSettings?.LastOrDefault()?.UserRoleId;
                foreach (var item in returnResponse.entity.Where(x=>x.ProjectApprovalStatus == ProjectApprovalStatus.Pending))
                {
                    var findCurClientLog = allProjectLog.Where(x => x.ProjectID == item.Id).LastOrDefault();
                    //var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var CurrentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));


                    if (findCurClientLog != null && findCurClientLog.ReceiverId == userId && CurrentUserRoleId != NewResultRoleId)
                    {
                        item.IsApprovalShow = true;
                    }
                    else if (findCurClientLog != null && findCurClientLog.ReceiverId == userId && CurrentUserRoleId == NewResultRoleId)
                    {
                        item.IsApprovalShow = false;
                        item.IsAcceptOrReject = true;
                    }
                    else if (findCurClientLog == null && RoleName == "SystemAdmin")
                    {
                        item.IsApprovalShow = true;
                    }
                    else
                    {
                        item.ApprovalMessage = findCurClientLog?.Description;
                    }
                }
            }
            else
            {
                foreach (var item in returnResponse.entity.Where(x=>x.ProjectApprovalStatus == ProjectApprovalStatus.Pending))
                {
                    item.IsApprovalShow = true;
                }

            }

            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if (returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");

            return View(returnResponse.entity.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending).OrderByDescending(s=>s.Id));
        }

        public async Task<ActionResult> RejectedList()
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if (returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectRejectedList();
            var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var RoleName = _UserRoleService.GetById(userRoleId).entity?.RoleName;
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.Client_User || userType == UserType.ClientAdmin)
                    ViewBag.HasCreatePermission = true;
                else
                    ViewBag.HasCreatePermission = false;

                if (userType == UserType.SQTC_Admin || userType == UserType.ClientAdmin)
                    ViewBag.HasEditDeletePermission = true;
                else
                    ViewBag.HasEditDeletePermission = false;
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                ViewBag.HasEditDeletePermission = false;
            }

            return View(returnResponse.entity.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Reject));
        }

        //public async Task<ActionResult> Details()
        //{
        //    return View();
        //}

        public ActionResult GetProjectLogHistoryById(long projectid)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, List<ProjectRequestLogVM> entity, string message) returnResponse = _ProjectRequestService.LogHistoryById(projectid);
            return Json(new { Data = returnResponse.entity.OrderByDescending(s=>s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message }, SerializerOption.Default);
        }

        public async Task<ActionResult> Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ProjectRequestVM entity, string message) editResponse = await _ProjectRequestService.GetById(id);

            _ = long.TryParse(HttpContext.Session.GetString("UserId"), out var userId);

            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
            var signedClient = returnResponse.entity.Where(x => x.UserId == userId).FirstOrDefault();

            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name", (int)editResponse.entity.ProjectType);
            ProjectRequestVM entity = new ProjectRequestVM();
            ViewBag.RequestedBy = signedClient != null ? signedClient.ClientName /*+ "-" +signedClient.c*/ : "";
            ViewBag.ReturnUrl = returnUrl;
            

            return View(editResponse.entity);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProjectRequestVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    //entity.CreatedAt = DateTime.Now;

                    var projectFile = HttpContext.Request.Form.Files.GetFile("ProjectFile");
                    if (!string.IsNullOrEmpty(Request.Form["RequestedDate"]))
                    {
                        entity.RequestDate = Convert.ToDateTime(Request.Form["RequestedDate"]);
                    }
                    // Save image files
                    var fileHelper = new FileHelper(_hostEnvironment);
                    var result = fileHelper.SaveFileAll(projectFile!, "ProjectRequest", out var errorMessage);
                    //if (!result.Item1)
                    //{
                    //    return View(entity);
                    //    //return Json(
                    //    //    new { Success = false, Message = errorMessage },
                    //    //    SerializerOption.Default);
                    //}

                    entity.FilePath = result.Item2;
                    entity.FileName = result.Item3;
                    (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse = await _ProjectRequestService.Update(entity);

                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        HttpContext.Session.SetString("Message", returnResponse.message);
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                        return View(entity);

                    }
                    else
                    {
                        HttpContext.Session.SetString("Message", "Project Request has been updated successfully");
                        HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                        return RedirectToAction("Index");
                        //return Json(
                        //    new { Success = true, Message = returnResponse.message, RedirectUrl = "/ProjectRequest/Index" },
                        //    SerializerOption.Default);
                    }
                }
                return View(entity);
            }
            catch
            {
                return View(entity);

                //return Json(
                //    new { Success = false, Message = "Unexpected error occurred" },
                //    SerializerOption.Default);
            }
        }

        public async Task<ActionResult> AcceptedList()
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if (returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.List();
            return View(returnResponse.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Accept) ?? new List<ProjectRequestVM>());
        }

        [HttpGet]
        public async Task<ActionResult> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate)
        {

            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponseClient = await _ClientService.List();
            if(returnResponseClient.entity != null)
            {
                ViewBag.ClientId = new SelectList(returnResponseClient.entity, "Id", "ClientName");
            }
            else
            {
                ViewBag.ClientId = new SelectList("");
            }
            ViewBag.ProjectType = new SelectList(EnumHelper.GetEnumDropdowns<ProjectType>(), "Id", "Name");
            ViewBag.ProjectName = ProjectName;
            ViewBag.ProjectCode = ProjectCode;
            ViewBag.RequestDate = RequestDate;
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var userType = _UserService.GetById(userId).entity?.UserType;
            if (userType != 0 && userType != null)
            {
                if (userType == UserType.Client_User || userType == UserType.ClientAdmin)
                    ViewBag.HasCreatePermission = true;
                else
                    ViewBag.HasCreatePermission = false;

                if (userType == UserType.SQTC_Admin || userType == UserType.ClientAdmin)
                    ViewBag.HasEditDeletePermission = true;
                else
                    ViewBag.HasEditDeletePermission = false;
            }
            else
            {
                ViewBag.HasCreatePermission = false;
                ViewBag.HasEditDeletePermission = false;
            }
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.Search(ProjectName,ProjectType,ProjectCode,ClientId,RequestDate);
            return View("Index", returnResponse.entity);
        }

        public async Task<ActionResult> Details(int? id, string backUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.backUrl = backUrl;
            (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse = await  _ProjectRequestService.GetById(id);
            ViewBag.ProjectState = System.Enum.GetValues(typeof(ProjectState)).Cast<ProjectState>().ToList();
            return View(returnResponse.entity);
        }

        public async Task<ActionResult> GetProjectCommentHistoryById(long projectId)
        {
            (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) returnResponse = await _approvalForProjectLogService.ProjectCommentHistoryById(projectId);
            return Json(new
            {
                Data = returnResponse.entity.OrderByDescending(s => s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);
        }

        public JsonResult Delete(int id)
        {
            var result = _ProjectRequestService.SoftDelete(id);
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

        public async Task<ActionResult> ClientPendingList()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectListByClientId(clientId);
            

            return View(returnResponse.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending) ?? new List<ProjectRequestVM>().OrderByDescending(s => s.Id));
        }
        public async Task<ActionResult> ClientRejectedList()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectListByClientId(clientId);


            return View(returnResponse.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Reject) ?? new List<ProjectRequestVM>().OrderByDescending(s => s.Id));
        }
        public async Task<ActionResult> ClientAcceptedList()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectListByClientId(clientId);


            return View(returnResponse.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Accept) ?? new List<ProjectRequestVM>().OrderByDescending(s => s.Id));
        }
        public async Task<ActionResult> ClientCompletedList()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));

            var clientId = _UserService.GetById(userId).entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse = await _ProjectRequestService.GetProjectListByClientId(clientId);


            return View(returnResponse.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Completed) ?? new List<ProjectRequestVM>().OrderByDescending(s => s.Id));
        }
    }
}
