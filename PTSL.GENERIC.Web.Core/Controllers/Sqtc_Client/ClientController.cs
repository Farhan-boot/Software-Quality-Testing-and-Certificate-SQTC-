using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client.ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Web.Core.EmailServices;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForRegisteredClientLogVM;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Implementation.Sqtc_Client.ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;
using System.Data;
using System.Globalization;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    //[SessionAuthorize]
    public class ClientController : Controller
    {
        public const string Uploads = "uploads";
        private readonly IClientService _ClientService;
        private readonly IDesignationService _DesignationService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPermissionHeaderSettingsService _permissionHeaderSettingsService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IApprovalForRegisteredClientLogService _approvalForRegisteredClientLogService;
        private readonly IPermissionRowSettingsService _permissionRowSettingsService;

        public ClientController(HttpHelper httpHelper, IWebHostEnvironment hostEnvironment)
        {
            _ClientService = new ClientService(httpHelper);
            _DesignationService = new DesignationService(httpHelper);
            _hostEnvironment = hostEnvironment;
            _permissionHeaderSettingsService = new PermissionHeaderSettingsService(httpHelper);
            _userService = new UserService(httpHelper);
            _approvalForRegisteredClientLogService = new ApprovalForRegisteredClientLogService(httpHelper);
            _userRoleService = new UserRoleService(httpHelper);
            _permissionRowSettingsService = new PermissionRowSettingsService(httpHelper);
        }
        // GET: Client
        public async Task<ActionResult> Index()
        {
            var designation = _DesignationService.List();

            ViewBag.ClientStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            //ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name");
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
            return View(returnResponse.entity?.Where(x => x.ClientStatus == ClientStatus.Active) ?? new List<ClientVM>());
        }

        // GET: Client/Details/5
        public async Task<ActionResult> Details(int? id, string backUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.backUrl = backUrl;
            (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: Client/Create
        public async Task<ActionResult> Create()
        {
            var designation = _DesignationService.List();
            ViewBag.UserType = new SelectList(EnumHelper.GetEnumDropdowns<UserType>(), "Id", "Name");
            ViewBag.ClientStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            ViewBag.ClientApprovalStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientApprovalStatus>(), "Id", "Name");
            ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name");
            ClientVM entity = new ClientVM();
            return View(entity);
        }

        // POST: Client/Create
        [HttpPost]
        public async Task<ActionResult> Create(ClientVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.UserName = entity.WorkingEmail;
                    var OfficialLetterFile = HttpContext.Request.Form.Files.GetFile("OfficialLetter");
                    var result = SaveFile(OfficialLetterFile!, "Client", out var errorMessage);
                    if (errorMessage != null)
                    {
                        // errror
                    }

                    entity.OfficialLetter = result.Url;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Create(entity);
                    HttpContext.Session.SetString("Message", returnResponse.message);
                    HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return RedirectToAction("Create");
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


        // GET: Client/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.GetById(id);
            var designation = _DesignationService.List();
            ViewBag.UserType = new SelectList(EnumHelper.GetEnumDropdowns<UserType>(), "Id", "Name", returnResponse.entity.UserType);
            ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name", returnResponse.entity.DesignationId);
            ClientVM entity = new ClientVM();
            return View(returnResponse.entity);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ClientVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ClientController.Index), "Client");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Update(entity);
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

        // GET: Client/Delete/5
        public async Task<JsonResult> Delete(int id)
        {
            (ExecutionState executionState, string message) CheckDataExistOrNot = await _ClientService.DoesExist(id);
            string message = "Faild, You can't delete this item.";
            if (CheckDataExistOrNot.executionState.ToString() != "Success")
            {
                return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

            }
            (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "Client deleted successfully.";
            }
            else
            {
                returnResponse.message = "Failed to delete this item.";
            }
            return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, ClientVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ClientController.Index), "Client");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Update(entity);
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

        public (bool IsSaved, string Url, string FileName) SaveFile(IFormFile file, string directoryName, out string errorMessage)
        {
            if (file is null)
            {
                errorMessage = "File not found";
                return (false, string.Empty, string.Empty);
            }
            if (string.IsNullOrEmpty(directoryName))
            {
                errorMessage = "Directory name must not be empty";
                return (false, string.Empty, string.Empty);
            }

            // Create upload directory is not exists
            var uploadDirectory = Path.GetFullPath(Path.Combine(_hostEnvironment.ContentRootPath, "..", Uploads));
            if (Directory.Exists(uploadDirectory) == false)
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            //var fileName = file.FileName;
            var fileExtension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);

            // New file name
            var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var newGuid = Guid.NewGuid().ToString();
            var newDiskFileName = $"{currentDateTimeString}_{newGuid}{fileExtension}";

            // Create save directory is not exists
            var saveDirectory = Path.Combine(uploadDirectory, directoryName);
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Save file
            var newDiskFilePath = Path.Combine(saveDirectory, newDiskFileName);
            var publicFileUrl = $"/{Uploads}/{directoryName}/{newDiskFileName}";
            try
            {
                using var fileStream = new FileStream(newDiskFilePath, FileMode.Create, FileAccess.Write);
                file.CopyTo(fileStream);
            }
            catch (Exception)
            {
                errorMessage = "Unable to save file unknown error occurred";
                return (false, string.Empty, string.Empty);
            }

            errorMessage = string.Empty;
            return (true, publicFileUrl, file.FileName);
        }

        [HttpGet]
        public async Task<ActionResult> Search(string? OrganizationName, ClientStatus? clientStatus, string? MobileNumber, DateTime? CreatedAt)
        {
            var designation = _DesignationService.List();
            //ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name");
            ViewBag.ClientStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            ViewBag.OrganizationName = OrganizationName;
            ViewBag.MobileNumber = MobileNumber;
            ViewBag.CreatedAt = CreatedAt;
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.Search(OrganizationName, clientStatus, MobileNumber, CreatedAt);

            return View("Index", returnResponse.entity);
        }

        public async Task<ActionResult> RejectedList()
        {
            ViewBag.ClientTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
            return View(returnResponse.entity?.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Reject) ?? new List<ClientVM>());
        }
        public async Task<ActionResult> AcceptedList()
        {
            ViewBag.ClientTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
            return View(returnResponse.entity.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Accept));
        }

        public async Task<ActionResult> PendingList()
        {

            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
            ViewBag.ClientTypeId = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            var userRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            var RoleName = _userRoleService.GetById(userRoleId).entity.RoleName;
            var allClientLog = _approvalForRegisteredClientLogService.List().entity;
            if (allClientLog != null)
            {
                long moduleEnumId = (long)ModuleEnum.RegistrationApproval;
                var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                    .entity.FirstOrDefault();
                var NewResultRoleId = roleResult?.PermissionRowSettings?.LastOrDefault()?.UserRoleId;
                foreach (var item in returnResponse.entity.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Pending))
                {
                    var findCurClientLog = allClientLog.Where(x => x.ClientID == item.Id).LastOrDefault();
                    var findBackwardLog = allClientLog.Where(x => x.ClientID == item.Id && x.ProcessFlowType == ProcessFlowType.backward).Any();
                    var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var CurrentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
                    var currentUserOrder = roleResult?.PermissionRowSettings?.Where(s => s.UserRoleId == CurrentUserRoleId).FirstOrDefault()?.OrderId;
                    if (currentUserOrder != 1 && !findBackwardLog)
                    {
                        item.IsBackwardShow = true;
                    }

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
                    //else if(CurrentUserRoleId == NewResultRoleId)
                    //{
                    //   item.IsApprovalShow = false;
                    //   item.IsAcceptOrReject = true;
                    //   item.ApprovalMessage = findCurClientLog?.Description;
                    //}
                    else
                    {
                        item.ApprovalMessage = findCurClientLog?.Description;
                    }
                }

            }
            else
            {
                if (returnResponse.entity != null)
                {
                    foreach (var item in returnResponse.entity.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Pending))
                    {
                        item.IsApprovalShow = true;
                    }
                }
               
            }

            return View(returnResponse.entity?.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Pending) ?? new List<ClientVM>());
        }

        public async Task<ActionResult> ClientRegistration()
        {
            var designation = _DesignationService.List();
            ViewBag.UserType = new SelectList(EnumHelper.GetEnumDropdowns<UserType>(), "Id", "Name");
            ViewBag.ClientStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            ViewBag.ClientApprovalStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientApprovalStatus>(), "Id", "Name");
            ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name");
            ClientVM entity = new ClientVM();
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> ClientRegistration(ClientVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.UserName = entity.WorkingEmail;
                    var OfficialLetterFile = HttpContext.Request.Form.Files.GetFile("OfficialLetter");
                    var result = SaveFile(OfficialLetterFile!, "Client", out var errorMessage);
                    if (errorMessage != null)
                    {
                        // errror
                    }

                    entity.OfficialLetter = result.Url;

                    // TODO: Add insert logic here
                    (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.ClientRegistration(entity);
                    //                    Session["Message"] = returnResponse.message;
                    //                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        ViewBag.DesignationId = new SelectList(_DesignationService.List().entity, "Id", "Name", returnResponse.entity.DesignationId);
                        TempData["Message"] = returnResponse.message;
                        return RedirectToAction("ExternalClientRegister", "Account");
                    }
                    else
                    {
                        var UserEmail = returnResponse.entity.WorkingEmail;
                        List<string> UserEmails = new List<string>();
                        List<string> BccList = new List<string>();
                        UserEmails.Add(UserEmail);
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
                                                            <h6>Your registration request is successful.Please wait for approval.</h6>
                                                        </div>
                                                        <div class='footer'>
                                                            <p>&copy; 2024 SHQTC. All rights reserved.</p>
                                                        </div>
                                                    </div>
                                                </body>
                                                </html>";

                        var Emailresult = EmailService.SendEmailAsync(UserEmails, BccList, "Registration", body, "");
                        TempData["Message"] = "Registration Successful.Please wait for approval";
                        return RedirectToAction("Login", "Account");
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
        public async Task<ActionResult> SaveMapToUser(ApprovalForRegisteredClientLogVM entity)
        {
            entity.IsActive = true;
            entity.SendingDate = DateTime.Now.Date;
            entity.SendingTime = DateTime.Now.ToLocalTime();
            entity.CreatedAt = DateTime.Now;
            entity.ClientApprovalStatus = ClientApprovalStatus.Pending;
            entity.ProcessFlowType = ProcessFlowType.Forward;
            entity.SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ReceiverName = _userService.GetById(entity.ReceiverId).entity.UserName;
            entity.Description = "Forwarded To" + " " + ReceiverName;
            //entity.SenderRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = await _approvalForRegisteredClientLogService.Create(entity);

            return Json(new { Data = returnResponse, Message = "Success" }, SerializerOption.Default);
        }

        [HttpPost]
        public async Task<ActionResult> SaveBackwardProcess(ApprovalForRegisteredClientLogVM entity)
        {
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;
            entity.ClientApprovalStatus = ClientApprovalStatus.Pending;
            entity.ProcessFlowType = ProcessFlowType.backward;
            entity.SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var ReceiverName = _userService.GetById(entity.ReceiverId).entity.UserName;
            entity.Description = "Backwared To" + " " + ReceiverName;
            entity.SenderRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse = await _approvalForRegisteredClientLogService.CreateBackwardProcess(entity);

            return Json(new { Data = returnResponse, Message = "" }, SerializerOption.Default);
        }

        public JsonResult GetApprovalProcessModalData(long id)
        {

            // Ensure it's a list
            long moduleEnumId = (long)ModuleEnum.RegistrationApproval;
            var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault(); // Retrieve first or default
            var currentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));

            if (roleResult != null && roleResult.PermissionRowSettings != null && currentUserRoleId != 1)
            {
                var logResult = _approvalForRegisteredClientLogService.List().entity.Where(x => x.ClientID == id);
                var permissionRowIds = logResult.Select(x => x.PermissionRowSettingsId).ToList();
                // Filter the PermissionRowSettings based on PermissionRowId
                var newResult = roleResult.PermissionRowSettings.FirstOrDefault(row => !permissionRowIds.Contains(row.Id));

                if (newResult != null && newResult.UserRole != null)
                {
                    // Access properties from UserRole
                    var RoleName = newResult.UserRole.RoleName;
                    var RoleId = newResult.UserRole.Id;
                    var PermissionRowSettingsId = newResult.Id;
                    var userList = _userService.GetUserInfoByUserRoleId(RoleId);
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
                    var userList = _userService.GetUserInfoByUserRoleId(RoleId);
                    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                    // Use roleName and roleId as needed
                }
            }


            return Json(new { Data = "null", Message = "" }, SerializerOption.Default);
        }

        public async Task<ActionResult> GetBackwardProcessModalData(long id)
        {

            // Ensure it's a list
            long moduleEnumId = (long)ModuleEnum.RegistrationApproval;
            var userRoleList = _userRoleService.List().entity;
            var roleResult = _permissionHeaderSettingsService.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId)
                .entity.FirstOrDefault(); // Retrieve list
            var currentUserRoleId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserRoleId));
            //var backwardroleList = roleResult.
            if (roleResult is not null && currentUserRoleId != 1)
            {
                var currentRoleItem = roleResult.PermissionRowSettings?.Where(s => s.UserRoleId == currentUserRoleId).FirstOrDefault();

                var logResult = _approvalForRegisteredClientLogService.List().entity.Where(x => x.ClientID == id && x.ProcessFlowType == ProcessFlowType.backward).ToList();
                var permissionRowIds = logResult.Select(x => x.PermissionRowSettingsId).ToList();
                // Filter the PermissionRowSettings based on PermissionRowId

                List<long> roleIds = new List<long>();

                if (currentRoleItem is not null)
                {
                    long currentRoleOrder = currentRoleItem.OrderId;
                    var rolesExceptCurrent = roleResult.PermissionRowSettings?.Where(s => s.UserRoleId != currentUserRoleId && s.OrderId < currentRoleOrder).Select(m => m.UserRoleId).ToList();
                    roleIds.AddRange(rolesExceptCurrent?.Where(s => !permissionRowIds.Contains(s)).ToList());
                }
                var roleItems = userRoleList.Where(x => roleIds.Contains(x.Id)).ToList();
                var Data = new { roles = roleItems };
                return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                //foreach (var rows in rolesExceptCurrent)
                //{
                //    var newResult = role.PermissionRowSettings.FirstOrDefault(row => !permissionRowIds.Contains(row.Id));
                //    if (newResult != null && newResult.UserRole != null)
                //    { 
                //        if()
                //    }
                //}

                //if (newResult != null && newResult.UserRole != null)
                //{
                //    // Access properties from UserRole
                //    var RoleName = newResult.UserRole.RoleName;
                //    var RoleId = newResult.UserRole.Id;
                //    var PermissionRowSettingsId = newResult.Id;
                //    var userList = _userService.GetUserInfoByUserRoleId(RoleId);
                //    var Data = new { RoleName, RoleId, PermissionRowSettingsId, userList.entity };

                //    return Json(new { Data = Data, Message = "" }, SerializerOption.Default);
                //    // Use roleName and roleId as needed
                //}

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
                    var userList = _userService.GetUserInfoByUserRoleId(RoleId);
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
                var ClientResult = await _ClientService.GetById(id);
                ClientResult.entity.IsActive = true;
                ClientResult.entity.ClientStatus = ClientStatus.Active;
                ClientResult.entity.ClientApprovalStatus = ClientApprovalStatus.Accept;

                (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Update(ClientResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingList");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var ApproverName = _userService.GetById(SenderId).entity.UserName;
                    ApprovalForRegisteredClientLogVM acceptedClientLog = new ApprovalForRegisteredClientLogVM();
                    acceptedClientLog.ClientID = returnResponse.entity.Id;
                    acceptedClientLog.ClientApprovalStatus = ClientApprovalStatus.Accept;
                    acceptedClientLog.Description = "Approved by " + "" + ApproverName;
                    acceptedClientLog.Remark = "Approved by " + "" + ApproverName;
                    acceptedClientLog.SenderId = SenderId;
                    (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponseLog = await _approvalForRegisteredClientLogService.Create(acceptedClientLog);
                    if (returnResponseLog.executionState == ExecutionState.Created)
                    {
                        //Get User Roles
                        var allRoles = _userRoleService.List();
                        long? clientAdminRoleId = null;
                        if(allRoles.entity is not null){
                            clientAdminRoleId = allRoles.entity.Where(s => s.RoleName.ToLower().Trim() == "Client Admin".ToLower().Trim()).FirstOrDefault()?.Id;
                        }

                        UserVM ClientUser = new UserVM();
                        ClientUser.FirstName = returnResponse.entity.ClientName;
                        ClientUser.UserName = returnResponse.entity.UserName;
                        ClientUser.UserEmail = returnResponse.entity.WorkingEmail;
                        ClientUser.UserPassword = returnResponse.entity.Password;
                        ClientUser.UserPhone = returnResponse.entity.MobileNumber;
                        ClientUser.UserType = UserType.ClientAdmin;
                        ClientUser.UserRoleId = 5;
                        ClientUser.CreatedAt = DateTime.Now;
                        ClientUser.IsActive = true;
                        ClientUser.ClientId = returnResponse.entity.Id;
                        ClientUser.UserRoleId = clientAdminRoleId;
                        (ExecutionState executionState, UserVM entity, string message) returnResponseClient = _userService.Create(ClientUser);

                        if (returnResponseClient.executionState == ExecutionState.Created)
                        {
                            var UserEmail = returnResponseClient.entity.UserEmail;
                            var userPassword = returnResponseClient.entity.UserPassword;
                            List<string> UserEmails = new List<string>();
                            List<string> BccList = new List<string>();
                            UserEmails.Add(UserEmail);
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
                                                            <h6>Your registration request is accepted.</h6>
                                                            <p><strong>Email:</strong> {UserEmail}</p>
                                                            <p><strong>Password:</strong> {userPassword}</p>
                                                        </div>
                                                        <div class='footer'>
                                                            <p>&copy; 2024 SHQTC. All rights reserved.</p>
                                                        </div>
                                                    </div>
                                                </body>
                                                </html>";

                            var result = EmailService.SendEmailAsync(UserEmails, BccList, "Registration", body, "");
                        }
                    }

                    if (returnResponseLog.entity != null)
                    {
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
        public async Task<ActionResult> Reject(int id)
        {
            try
            {
                var ClientResult = await _ClientService.GetById(id);
                ClientResult.entity.IsActive = true;
                ClientResult.entity.ClientStatus = ClientStatus.Inactive;
                ClientResult.entity.ClientApprovalStatus = ClientApprovalStatus.Reject;

                (ExecutionState executionState, ClientVM entity, string message) returnResponse = await _ClientService.Update(ClientResult.entity);

                if (returnResponse.executionState.ToString() != "Updated")
                {
                    return RedirectToAction("PendingList");
                }
                else
                {
                    var SenderId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
                    var RejectorName = _userService.GetById(SenderId).entity.UserName;
                    ApprovalForRegisteredClientLogVM acceptedClientLog = new ApprovalForRegisteredClientLogVM();
                    acceptedClientLog.ClientID = returnResponse.entity.Id;
                    acceptedClientLog.ClientApprovalStatus = ClientApprovalStatus.Accept;
                    acceptedClientLog.Description = "Rejected by " + "" + RejectorName;
                    acceptedClientLog.Remark = "Rejected by " + "" + RejectorName;
                    (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponseLog = await _approvalForRegisteredClientLogService.Create(acceptedClientLog);
                    if (returnResponseLog.executionState == ExecutionState.Created)
                    {
                        var UserEmail = ClientResult.entity.WorkingEmail;
                        List<string> UserEmails = new List<string>();
                        List<string> BccList = new List<string>();
                        UserEmails.Add(UserEmail);
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
                                                            <h6>Your registration request is Rejected.</h6>
                                                        </div>
                                                        <div class='footer'>
                                                            <p>&copy; 2024 SHQTC. All rights reserved.</p>
                                                        </div>
                                                    </div>
                                                </body>
                                                </html>";
                        var result = EmailService.SendEmailAsync(UserEmails, BccList, "Registration", body, "");
                    }

                    if (returnResponseLog.entity != null)
                    {
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

        public ActionResult GetClientLogHistoryById(long clientId)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, List<ClientLogVM> entity, string message) returnResponse = _ClientService.ClientLogHistoryById(clientId);
            return Json(new
            {
                Data = returnResponse.entity.OrderByDescending(s => s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);
        }

        public async Task<ActionResult> GetClientCommentHistoryById(long clientId)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) returnResponse = await _approvalForRegisteredClientLogService.ClientCommentHistoryById(clientId);
            return Json(new
            {
                Data = returnResponse.entity.OrderByDescending(s => s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);
        }

        //public async Task<ActionResult> ClientUserList()
        //{
        //    (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse = await _ClientService.List();
        //    return View(returnResponse.entity);
        //}

        public async Task<ActionResult> GetUsersByRoleId(long roleId)
        {
            //entity.IsActive = true;
            //entity.CreatedAt = DateTime.Now;
            //entity.TrackingDescription = entity.TrackingDescription is null ? string.Empty : entity.TrackingDescription;
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse = _userService.GetUserInfoByUserRoleId(roleId);
            return Json(new
            {
                Data = returnResponse.entity.OrderByDescending(s => s.Id),
                Success = returnResponse.executionState == ExecutionState.Retrieved ? true : false,
                Message = returnResponse.message
            }, SerializerOption.Default);
        }
    }
}
