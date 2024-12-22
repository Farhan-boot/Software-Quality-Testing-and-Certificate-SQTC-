using Microsoft.AspNetCore.Mvc;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.Documents;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly IPmsGroupService _PmsGroupService;
        private readonly IAccessMapperService _AccessMapperService;
        private readonly IAccesslistService _AccesslistService;
        private readonly IModuleService _ModuleService;
        private readonly IUserService _UserService;
        private readonly IUserRoleService _userRoleService;
        private readonly IProjectRequestService _projectRequestService;
        private readonly IClientService _clientService;
        private readonly IAllTypesDocumentService _allTypesDocumentService;
        private readonly ITaskService _taskService;
        private readonly IPaymentCalculationHeaderService _paymentCalculationHeaderService;
        private readonly IMeetingService _meetingService;
        public HomeController(HttpHelper httpHelper)
        {
            _PmsGroupService = new PmsGroupService(httpHelper);
            _AccessMapperService = new AccessMapperService(httpHelper);
            _AccesslistService = new AccesslistService(httpHelper);
            _ModuleService = new ModuleService(httpHelper);
            _UserService = new UserService(httpHelper);
            _userRoleService = new UserRoleService(httpHelper);
            _projectRequestService = new ProjectRequestService(httpHelper);
            _clientService = new ClientService(httpHelper);
            _allTypesDocumentService = new AllTypesDocumentService(httpHelper);
            _taskService = new TaskService(httpHelper);
            _paymentCalculationHeaderService = new PaymentCalculationHeaderService(httpHelper);
            _meetingService = new MeetingService(httpHelper);
        }

        public async Task<IActionResult> Index()
        {
            AdminDashBoardVM entity = new AdminDashBoardVM();
           
            var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
            var UserDetails = _UserService.GetById(userId);
            entity.UserName = UserDetails.entity?.UserName??"";

            if(UserDetails.entity.UserType == UserType.SQTC_Admin)
            {
                entity.TotalRegistredClients = _clientService.List().Result.entity?.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Accept).Count() ?? 0;
                entity.TotalCompletedProjects = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Completed).Count() ?? 0;
                entity.TotalEmployees = _UserService.List().entity?.Where(x => x.UserType == UserType.SQTC_Admin || x.UserType == UserType.SQTC_User).Count() ?? 0;
                entity.TotalAgreements = _allTypesDocumentService.List().Result.entity?.Where(x => x.DocumentType == DocumentType.Agreement).Count() ?? 0;
                entity.PendingProjectRequests = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending).Count() ?? 0;
                entity.totaltestRequest = _projectRequestService.List().Result.entity?.Count() ?? 0;
                var taskofPr = _taskService.List().Result.entity;
                entity.TaskOfProjects = taskofPr;
                var paymentInfo = await _paymentCalculationHeaderService.List();
                entity.paymentInformation = paymentInfo.entity is null ? new List<PaymentCalculationHeaderVM>() : paymentInfo.entity.Take(5).ToList();
                var meetingInfo =  _meetingService.List().Result.entity;
                entity.MetingInformation = meetingInfo is null ? new List<Model.EntityViewModels.Meetings.MeetingVM>() : meetingInfo.Take(5).ToList();
                return PartialView("~/Views/Home/_SystemAdminDashboardPartialView.cshtml", entity);
            }
            else if(UserDetails.entity.UserType == UserType.ClientAdmin)
            {
                var currentUser = _UserService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
                var ClientId = currentUser.entity?.ClientId ?? 0;
                entity.TotalEmployees  =  _UserService.ClientWiseUserList(ClientId).Result.entity?.Count() ?? 0;
                var totaltestRequest =  _projectRequestService.GetProjectListByClientId(ClientId).Result.entity;
                entity.totaltestRequest = totaltestRequest is null?0 : totaltestRequest.Count();
                entity.PendingProjectRequests = _projectRequestService.GetProjectListByClientId(ClientId).Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending).Count() ?? 0;
                entity.RejectedProjectRequests = _projectRequestService.GetProjectListByClientId(ClientId).Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Reject).Count() ?? 0;
                entity.TotalCompletedProjects = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Completed).Count() ?? 0;
                var meetingInfo = _meetingService.MeetingListByClientId(ClientId).Result.entity;
                entity.MetingInformation = meetingInfo is null ? new List<Model.EntityViewModels.Meetings.MeetingVM>() : meetingInfo.Take(5).ToList();
                var paymentInfo =  await _paymentCalculationHeaderService.ListByClientId(ClientId);
                entity.paymentInformation = paymentInfo.entity is null ? new List<PaymentCalculationHeaderVM>() : paymentInfo.entity.Take(5).ToList();
                entity.TotalAgreements = _allTypesDocumentService.ListByClientId(ClientId).Result.entity?.Where(x => x.DocumentType == DocumentType.Agreement).Count() ?? 0;
                return PartialView("~/Views/Home/_ClientAdminDashboardPartialView.cshtml",entity);
            }
            else if(UserDetails.entity.UserType == UserType.Client_User)
            {
                var currentUser = _UserService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
                var ClientId = currentUser.entity?.ClientId ?? 0;
                //entity.TotalEmployees = _UserService.ClientWiseUserList(ClientId).Result.entity?.Count() ?? 0;
                //entity.TotalEmployees = _UserService.List().entity?.Where(x => x.ClientId == ClientId && x.UserType == UserType.ClientAdmin || x.UserType == UserType.Client_User).Count() ?? 0;
                var totaltestRequest = _projectRequestService.GetProjectListByClientId(ClientId).Result.entity;
                entity.totaltestRequest = totaltestRequest is null ? 0 : totaltestRequest.Count();
                entity.PendingProjectRequests = _projectRequestService.GetProjectListByClientId(ClientId).Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending).Count() ?? 0;
                entity.RejectedProjectRequests = _projectRequestService.GetProjectListByClientId(ClientId).Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Reject).Count() ?? 0;
                entity.TotalCompletedProjects = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Completed).Count() ?? 0;
                var meetingInfo = _meetingService.MeetingListByClientId(ClientId).Result.entity;
                entity.MetingInformation = meetingInfo is null ? new List<Model.EntityViewModels.Meetings.MeetingVM>() : meetingInfo.Take(5).ToList();
                var paymentInfo = await _paymentCalculationHeaderService.ListByClientId(ClientId);
                entity.paymentInformation = paymentInfo.entity is null ? new List<PaymentCalculationHeaderVM>() : paymentInfo.entity.Take(5).ToList();
                entity.TotalAgreements = _allTypesDocumentService.ListByClientId(ClientId).Result.entity?.Where(x => x.DocumentType == DocumentType.Agreement).Count() ?? 0;
                return PartialView("~/Views/Home/_ClientUserDashboardPartialView.cshtml", entity);
            }

            else if(UserDetails.entity.UserType == UserType.SQTC_User)
            {
                entity.TotalRegistredClients = _clientService.List().Result.entity?.Where(x => x.ClientApprovalStatus == ClientApprovalStatus.Accept).Count() ?? 0;
                entity.TotalCompletedProjects = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Completed).Count() ?? 0;
                entity.TotalEmployees = _UserService.List().entity?.Where(x => x.UserType == UserType.SQTC_Admin || x.UserType == UserType.SQTC_User).Count() ?? 0;
                entity.TotalAgreements = _allTypesDocumentService.List().Result.entity?.Where(x => x.DocumentType == DocumentType.Agreement).Count() ?? 0;
                entity.PendingProjectRequests = _projectRequestService.List().Result.entity?.Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending).Count() ?? 0;
                entity.totaltestRequest = _projectRequestService.List().Result.entity?.Count() ?? 0;
                var taskofPr = _taskService.List().Result.entity;
                entity.TaskOfProjects = taskofPr;
                var paymentInfo = await _paymentCalculationHeaderService.List();
                entity.paymentInformation = paymentInfo.entity is null ? new List<PaymentCalculationHeaderVM>() : paymentInfo.entity.Take(5).ToList();
                var meetingInfo = _meetingService.List().Result.entity.Where(x=>x.IsInitiatedBySqtc == true);
                entity.MetingInformation = meetingInfo is null ? new List<Model.EntityViewModels.Meetings.MeetingVM>() : meetingInfo.Take(5).ToList();
                return PartialView("~/Views/Home/_SQTCUserDashboardPartialView.cshtml", entity);
            }



            else
            {
                return PartialView("~/Views/Home/_DefaultDashboardView.cshtml");
            }

            //return View(entity);
        }

        public JsonResult RootMenue()
        {
            var result = _userRoleService.CurrentUserRootMenu();

            return Json(result.entity, SerializerOption.Default);
        }

       

        public Menu PmsRootMenueList()
        {
            Menu menu = new Menu();
            try
            {

                List<CustomerAccessList> AllAccesslist = new List<CustomerAccessList>();

                List<MenueViewModel> menueList = new List<MenueViewModel>();

                List<int> AccesList = new List<int>();
                //var aspNetUser = UserManager.Users.ToList().Where(x => !x.IsRemoved && x.Id == User.Identity.GetUserId()).FirstOrDefault();
                long GroupID = 1;// Convert.ToInt32(aspNetUser.PmsGroupID);


                long.TryParse(HttpContext.Session.GetString("UserId"), out var LoggedInUser); // User.Identity.GetUserId();

                var loginUser = _UserService.GetById(LoggedInUser);
                var UserName = HttpContext.Session.GetString("UserEmail");// User.Identity.GetUserName();
                // var PmsGroupId = _PmsGroupService.GetById(GroupID);
                if (loginUser.entity != null)
                {
                    GroupID = loginUser.entity.PmsGroupID;
                }
                (ExecutionState executionState, PmsGroupVM entity, string message) returnPmsGroupResponse = _PmsGroupService.GetById(GroupID);

                //string GroupName = returnPmsGroupResponse.entity.GroupName;


                if (GroupID != 0)
                {
                    (ExecutionState executionState, AccessMapperVM entity, string message) returnAccessMapperVMResponse = _AccessMapperService.GetById(GroupID);

                    //var accessMapper = _AccessMapperService.GetById(GroupID);
                    string s = string.Empty;
                    if (returnAccessMapperVMResponse.entity != null && returnAccessMapperVMResponse.entity.AccessList != null)
                    {
                        s = returnAccessMapperVMResponse.entity.AccessList;
                    }
                    string[] values = s.Split(',');
                    foreach (var value in values)
                    {
                        AccesList.Add(Convert.ToInt32(value));
                    }
                    foreach (var item in AccesList)
                    {
                        try
                        {
                            //var access = _AccesslistService.GetById(item);
                            (ExecutionState executionState, AccesslistVM entity, string message) returnAccesslistResponse = _AccesslistService.GetById(item);

                            CustomerAccessList custom = new CustomerAccessList();

                            if (returnAccesslistResponse.entity != null && returnAccesslistResponse.entity.IsVisible == 0)
                            {
                                custom.AccessID = returnAccesslistResponse.entity.Id;
                                custom.AccessStatus = returnAccesslistResponse.entity.AccessStatus;
                                custom.ActionName = returnAccesslistResponse.entity.ActionName;
                                //custom.BaseModule = returnAccesslistResponse.entity.BaseModule;
                                custom.ControllerName = returnAccesslistResponse.entity.ControllerName;
                                custom.IconClass = returnAccesslistResponse.entity.IconClass;
                                custom.Mask = returnAccesslistResponse.entity.Mask;
                                custom.BaseModuleIndex = returnAccesslistResponse.entity.BaseModuleIndex;
                                AllAccesslist.Add(custom);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    (ExecutionState executionState, List<ModuleVM> entity, string message) returnModuleVMResponse = _ModuleService.List();

                    var ModuleList = returnModuleVMResponse.entity.OrderBy(a => a.MenueOrder).ToList();
                    foreach (var item in ModuleList)
                    {
                        MenueViewModel menue = new MenueViewModel();
                        menue.ModuleID = item.Id;
                        menue.ModuleName = item.ModuleName;
                        menue.ModuleIcon = item.ModuleIcon;
                        var module = _ModuleService.GetById(item.Id);

                        if (module.entity.IsVisible == 0)
                        {
                            var menulist = AllAccesslist.Where(a => a.BaseModule == item.Id).OrderBy(a => a.BaseModuleIndex).ToList();
                            if (menulist.Count > 0)
                            {
                                menue.AccessList = menulist;
                                menueList.Add(menue);
                            }
                        }
                    }
                }
                menu.MenuList = menueList;
                menu.UserName = UserName;
                //menu.GroupName = GroupName;

                return menu;

            }
            catch (Exception ex)
            {
                //var formatter = RequestFormat.JsonFormaterString();
                //return Request.CreateResponse(HttpStatusCode.OK, new PayloadResponse { Success = false, Message = "200", Payload = null }, formatter);
                return menu;
            }
        }





    }
}
