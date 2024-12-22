using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalForRegisteredClientLogController : BaseController<IApprovalForRegisteredClientLogService, ApprovalForRegisteredClientLogVM, ApprovalForRegisteredClientLog>
    {
        private readonly IApprovalForRegisteredClientLogService _ApprovalForRegisteredClientLogService;
        private readonly IClientLogService _ClientLogService;
        private readonly IUserService _UserService;
        public ApprovalForRegisteredClientLogController(IApprovalForRegisteredClientLogService ApprovalForRegisteredClientLogService,IClientLogService ClientLogService, IUserService userService) :
            base(ApprovalForRegisteredClientLogService)
        {
            _ApprovalForRegisteredClientLogService = ApprovalForRegisteredClientLogService;
            _ClientLogService = ClientLogService;
            _UserService = userService;
        }

        //Implement here new api, if we want.
        public async override Task<ActionResult<WebApiResponse<ApprovalForRegisteredClientLogVM>>> CreateAsync([FromBody] ApprovalForRegisteredClientLogVM model)
        {
            //model.CreatedBy = HttpContext.GetCurrentUserId();
            var result = await _ApprovalForRegisteredClientLogService.CreateAsync(model);
            if(result.executionState == ExecutionState.Created)
            {
                ClientLogVM clientLog = new ClientLogVM();
                
                clientLog.ClientID = result.entity.ClientID;
                clientLog.Description = "Client forwarded for approval";
                clientLog.CreatedBy = result.entity.SenderId??0;
                clientLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _ClientLogService.CreateAsync(clientLog);
            }
            var apiResponse = new WebApiResponse<ApprovalForRegisteredClientLogVM>(result);

            return Ok(apiResponse);
        }

        [HttpPost("CreateBackwardProcessForClient")]
        
        public async Task<ActionResult<WebApiResponse<ApprovalForRegisteredClientLogVM>>> CreateBackwardProcess([FromBody] ApprovalForRegisteredClientLogVM model)
        {
            //model.CreatedBy = HttpContext.GetCurrentUserId();
            var result = await _ApprovalForRegisteredClientLogService.CreateAsync(model);
            if (result.executionState == ExecutionState.Created)
            {
                ClientLogVM clientLog = new ClientLogVM();

                clientLog.ClientID = result.entity.ClientID;
                clientLog.Description = "Client backwarded for recheck";
                clientLog.CreatedBy = result.entity.SenderId ?? 0;
                clientLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _ClientLogService.CreateAsync(clientLog);
            }
            var apiResponse = new WebApiResponse<ApprovalForRegisteredClientLogVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("GetClientCommentHistory/{clientId}")]
        public ActionResult<WebApiResponse<List<ApprovalForRegisteredClientLogVM>>> GetClientCommentHistory(long clientId)
        {
            (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) responseResult;
            try
            {
                var result = _ApprovalForRegisteredClientLogService.List().Result.entity.Where(s => s.ClientID == clientId).ToList();
                var users = _UserService.List().Result.entity.ToList();
                List<ApprovalForRegisteredClientLogVM> list = new List<ApprovalForRegisteredClientLogVM>();
                list = result.Select(s => new ApprovalForRegisteredClientLogVM()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Remark = s.Remark,
                    SenderName = users.Where(x => x.Id == s.SenderId).FirstOrDefault()?.UserName,
                    SenderId = s.SenderId,
                    ReceiverName = users.Where(x => x.Id == s.ReceiverId).FirstOrDefault()?.UserName,
                    CreatedAt = s.CreatedAt,
                    ClientID = s.ClientID
                }).ToList();
                responseResult.entity = list;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new List<ApprovalForRegisteredClientLogVM>();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<List<ApprovalForRegisteredClientLogVM>> apiResponse = new WebApiResponse<List<ApprovalForRegisteredClientLogVM>>(responseResult);
            return Ok(apiResponse);
        }
    }
}
