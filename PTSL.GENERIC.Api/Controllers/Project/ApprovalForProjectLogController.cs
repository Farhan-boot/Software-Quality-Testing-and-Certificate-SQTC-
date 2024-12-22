using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalForProjectLogController : BaseController<IApprovalForProjectLogService, ApprovalForProjectLogVM, ApprovalForProjectLog>
    {
        private readonly IApprovalForProjectLogService _ApprovalForProjectLogService;
        private readonly IProjectRequestLogService _ProjectRequestLogService;
        private readonly IUserService _UserService;
        public ApprovalForProjectLogController(IApprovalForProjectLogService ApprovalForProjectLogService, IProjectRequestLogService ProjectRequestLogService, IUserService userService) :
            base(ApprovalForProjectLogService)
        {
            _ApprovalForProjectLogService = ApprovalForProjectLogService;
            _ProjectRequestLogService = ProjectRequestLogService;
            _UserService = userService;
        }

        //Implement here new api, if we want.
        public async override Task<ActionResult<WebApiResponse<ApprovalForProjectLogVM>>> CreateAsync([FromBody] ApprovalForProjectLogVM model)
        {
            var result = await _ApprovalForProjectLogService.CreateAsync(model);
            if(result.executionState == ExecutionState.Created)
            {
                ProjectRequestLogVM ProjectRequestLog = new ProjectRequestLogVM();

                ProjectRequestLog.ProjectRequestId = result.entity.ProjectID;
                ProjectRequestLog.Description = "Project forwarded for permission";
                ProjectRequestLog.CreatedBy = result.entity.SenderId??0;
                ProjectRequestLog.CreatedAt = result.entity.CreatedAt;
                var logResult = await _ProjectRequestLogService.CreateAsync(ProjectRequestLog);
            }
            var apiResponse = new WebApiResponse<ApprovalForProjectLogVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("GetProjectCommentHistory/{projectId}")]
        public ActionResult<WebApiResponse<List<ApprovalForProjectLogVM>>> GetProjectCommentHistory(long projectId)
        {
            (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) responseResult;
            try
            {
                var result = _ApprovalForProjectLogService.List().Result.entity.Where(s => s.ProjectID == projectId).ToList();
                var users = _UserService.List().Result.entity.ToList();
                List<ApprovalForProjectLogVM> list = new List<ApprovalForProjectLogVM>();
                list = result.Select(s => new ApprovalForProjectLogVM()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Remark = s.Remark,
                    SenderName = users.Where(x => x.Id == s.SenderId).FirstOrDefault()?.UserName,
                    SenderId = s.SenderId,
                    ReceiverName = users.Where(x => x.Id == s.ReceiverId).FirstOrDefault()?.UserName,
                    CreatedAt = s.CreatedAt,
                    //ClientID = s.ClientID
                }).ToList();
                responseResult.entity = list;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new List<ApprovalForProjectLogVM>();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<List<ApprovalForProjectLogVM>> apiResponse = new WebApiResponse<List<ApprovalForProjectLogVM>>(responseResult);
            return Ok(apiResponse);
        }
    }
}
