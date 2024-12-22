using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
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
    public class ApprovalForAllDocumentController : BaseController<IApprovalForAllDocumentService, ApprovalForAllDocumentVM, ApprovalForAllDocument>
    {
        private readonly IApprovalForAllDocumentService _ApprovalForAllDocumentService;
        private readonly IUserService _UserService;
        public ApprovalForAllDocumentController(IApprovalForAllDocumentService ApprovalForAllDocumentService, IUserService userService) :
            base(ApprovalForAllDocumentService)
        {
            _ApprovalForAllDocumentService = ApprovalForAllDocumentService;
            _UserService = userService;
        }

        //Implement here new api, if we want.
        public async override Task<ActionResult<WebApiResponse<ApprovalForAllDocumentVM>>> CreateAsync([FromBody] ApprovalForAllDocumentVM model)
        {
            var result = await _ApprovalForAllDocumentService.CreateAsync(model);
            //if(result.executionState == ExecutionState.Created)
            //{
            //    ProjectRequestLogVM ProjectRequestLog = new ProjectRequestLogVM();

            //    ProjectRequestLog.ProjectRequestId = result.entity.ProjectID;
            //    ProjectRequestLog.Description = "Project forwarded for permission";
            //    ProjectRequestLog.CreatedBy = result.entity.SenderId??0;
            //    ProjectRequestLog.CreatedAt = result.entity.CreatedAt;
            //    var logResult = await _ProjectRequestLogService.CreateAsync(ProjectRequestLog);
            //}
            var apiResponse = new WebApiResponse<ApprovalForAllDocumentVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("GetDocumentCommentHistory")]
        public ActionResult<WebApiResponse<List<ApprovalForAllDocumentVM>>> GetDocumentCommentHistory(long documentId)
        {
            (ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message) responseResult;
            try
            {
                var result = _ApprovalForAllDocumentService.List().Result.entity.Where(s => s.AllTypesOfDocumentId == documentId).ToList();
                var users = _UserService.List().Result.entity.ToList();
                List<ApprovalForAllDocumentVM> list = new List<ApprovalForAllDocumentVM>();
                list = result.Select(s => new ApprovalForAllDocumentVM()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Remarks = s.Remarks,
                    SenderName = users.Where(x => x.Id == s.SenderId).FirstOrDefault()?.UserName,
                    SenderId = s.SenderId,
                    ReceiverName = users.Where(x => x.Id == s.ReceiverId).FirstOrDefault()?.UserName,
                    ReceiverId = s.ReceiverId,
                    CreatedAt = s.CreatedAt,
                    SenderRoleId = s.SenderRoleId,
                    ProcessFlowType = s.ProcessFlowType,
                    StatusForPdf = s.StatusForPdf
                }).ToList();
                responseResult.entity = list;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new List<ApprovalForAllDocumentVM>();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<List<ApprovalForAllDocumentVM>> apiResponse = new WebApiResponse<List<ApprovalForAllDocumentVM>>(responseResult);
            return Ok(apiResponse);
        }
    }
}
