using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLogController : BaseController<IClientLogService, ClientLogVM, ClientLog>
    {
        private readonly IClientLogService _ClientLogService;
        private readonly IUserService _userService;
        public ClientLogController(IClientLogService ClientLogService, IUserService userService) :
            base(ClientLogService)
        {
            _ClientLogService = ClientLogService;
            _userService = userService;
        }

        //Implement here new api, if we want.

        [HttpGet]
        [Route("GetClientLogHistory/{clientId}")]
        public ActionResult<WebApiResponse<List<ClientLogVM>>> GetClientLogHistory(long clientId)
        {
            (ExecutionState executionState, List<ClientLogVM> entity, string message) responseResult;
            try
            {
                var result = _ClientLogService.List().Result.entity.Where(s => s.ClientID == clientId).ToList();
                var users = _userService.List().Result.entity.ToList();
                List<ClientLogVM> list = new List<ClientLogVM>();
                list = result.Select(s => new ClientLogVM()
                {
                    Id = s.Id,
                    Description = s.Description,
                    CreatedUserName = users.Where(x => x.Id == s.CreatedBy).FirstOrDefault()?.UserName,
                    CreatedAt = s.CreatedAt,
                    ClientID = s.ClientID
                }).ToList();
                responseResult.entity = list;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new List<ClientLogVM>();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<List<ClientLogVM>> apiResponse = new WebApiResponse<List<ClientLogVM>>(responseResult);
            return Ok(apiResponse);
        }
    }
}
