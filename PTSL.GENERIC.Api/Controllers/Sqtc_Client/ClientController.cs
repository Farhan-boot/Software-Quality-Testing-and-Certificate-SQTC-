using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController<IClientService, ClientVM, Client>
    {
        private readonly IClientService _clientService;
        private readonly IClientLogService _clientLogService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        public ClientController(IClientService ClientService, IClientLogService ClientLogService, IUserService userService, IUserRoleService userRoleService) :
            base(ClientService)
        {
            _clientService = ClientService;
            _clientLogService = ClientLogService;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        //Implement here new api, if we want.

        public async override Task<ActionResult<WebApiResponse<ClientVM>>> CreateAsync([FromBody] ClientVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            model.UserId = currentUserId;
            model.UserType= UserType.ClientAdmin;
            model.ClientStatus = ClientStatus.Active;
            model.ClientApprovalStatus = ClientApprovalStatus.Accept;
            var result = await _clientService.CreateAsync(model);
            if(result.executionState == ExecutionState.Created)
            {
                ClientLogVM clientLog = new ClientLogVM();
                clientLog.ClientID = result.entity.Id;
                clientLog.Description = "Client approved by SHQTC";
                clientLog.CreatedBy = result.entity.UserId??0;
                clientLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _clientLogService.CreateAsync(clientLog);
                if(logResult.executionState == ExecutionState.Created)
                {
                    //Get User Roles
                    var allRoles = await _userRoleService.List();
                    long? clientAdminRoleId = null;
                    if (allRoles.entity is not null)
                    {
                        clientAdminRoleId = allRoles.entity.Where(s => s.RoleName.ToLower().Trim() == "Client Admin".ToLower().Trim()).FirstOrDefault()?.Id;
                    }
                    
                    UserVM ClientAddToUser = new UserVM();
                    
                    ClientAddToUser.ClientId = result.entity.Id;
                    ClientAddToUser.UserName = result.entity.UserName;
                    ClientAddToUser.UserEmail = result.entity.WorkingEmail;
                    ClientAddToUser.UserPassword = result.entity.Password;
                    ClientAddToUser.UserPhone =  result.entity.MobileNumber;
                    ClientAddToUser.CreatedAt = DateTime.UtcNow;
                    ClientAddToUser.UserType = UserType.ClientAdmin;
                    ClientAddToUser.UserRoleId = 5;
                    ClientAddToUser.UserStatus = true;
                    ClientAddToUser.IsActive = true;
                    ClientAddToUser.IsDeleted = false;
                    ClientAddToUser.UserRoleId = clientAdminRoleId;
                    var ClientAddedtoUserResult = await _userService.CreateAsync(ClientAddToUser); 
                }
            }
            var apiResponse = new WebApiResponse<ClientVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<ClientVM>>>> Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt)
        {
            try
            {
                var (executionState, entity, message) = await _clientService.Search(organizationName, ClientStatus, mobileNo, CreatedAt);

                return Ok(new WebApiResponse<IList<ClientVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<ClientVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
        [HttpPost("ClientRegistration")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<ClientVM>>> Register([FromBody] ClientVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            model.UserType = UserType.ClientAdmin;

            model.ClientStatus = ClientStatus.Inactive;
            model.ClientApprovalStatus = ClientApprovalStatus.Pending;
            var result = await _clientService.CreateAsync(model);
            if (result.executionState == ExecutionState.Created)
            {
                ClientLogVM clientLog = new ClientLogVM();
                clientLog.ClientID = result.entity.Id;
                clientLog.Description = "Client Requested For Registration";
                //clientLog.CreatedBy = result.entity.UserId ?? 0;
                clientLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _clientLogService.CreateAsync(clientLog);
                //if(logResult.executionState == ExecutionState.Created)
                //{
                //    var res = _emailService.SendEmailAsync(model.WorkingEmail,"Registration","Welcome to sqtc");
                //}
            }
            var apiResponse = new WebApiResponse<ClientVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("ClientWiseUserList")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<UserVM>>>> ClientWiseUserList(long ClienId)
        { 
            try
            {
                var (executionState, entity, message) = await _clientService.ClientWiseUserList(ClienId);

                return Ok(new WebApiResponse<IList<UserVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<UserVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}
