using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;

namespace PTSL.GENERIC.Api.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentAmendmentController : BaseController<IDocumentAmendmentService, DocumentAmendmentVM, DocumentAmendment>
    {
        private readonly IDocumentAmendmentService _documentAmendmentService;
        private readonly IUserService _userService;
        public DocumentAmendmentController(IDocumentAmendmentService documentAmendmentService, IUserService userService)
            : base(documentAmendmentService)
        {
            _documentAmendmentService = documentAmendmentService;
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateDocAmendmentAsync")]
        public async Task<ActionResult<WebApiResponse<DocumentAmendmentVM>>> CreateDocAmendmentAsync([FromBody] DocumentAmendmentVM model)
        {
            var result = await _documentAmendmentService.CreateDocAmendment(model);
            //if(result.executionState == ExecutionState.Created)
            //{
            //    ProjectRequestLogVM ProjectRequestLog = new ProjectRequestLogVM();

            //    ProjectRequestLog.ProjectRequestId = result.entity.ProjectID;
            //    ProjectRequestLog.Description = "Project forwarded for permission";
            //    ProjectRequestLog.CreatedBy = result.entity.SenderId??0;
            //    ProjectRequestLog.CreatedAt = result.entity.CreatedAt;
            //    var logResult = await _ProjectRequestLogService.CreateAsync(ProjectRequestLog);
            //}
            var apiResponse = new WebApiResponse<DocumentAmendmentVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("GetDocumentAmendmentById")]
        public ActionResult<WebApiResponse<DocumentAmendmentVM>> GetDocumentAmendmentById(long documentId)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) responseResult;
            try
            {
                var result = _documentAmendmentService.GetAmendmentById(documentId).Result.entity;
                
                responseResult.entity = result;
                responseResult.executionState = ExecutionState.Retrieved;
                responseResult.message = "Log Found";
            }
            catch (Exception ex)
            {
                responseResult.entity = new DocumentAmendmentVM();
                responseResult.executionState = ExecutionState.Failure;
                responseResult.message = "Error Occured";
            }
            WebApiResponse<DocumentAmendmentVM> apiResponse = new WebApiResponse<DocumentAmendmentVM>(responseResult);
            return Ok(apiResponse);
        }
    }
}
