using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : BaseController<IDocumentsService, DocumentsByTypeVM, DocumentsByType>
    {
        private readonly IDocumentsService _documentService;
        private readonly IProjectRequestService _projectRequestService;
        private readonly IUserService _userService;
        public DocumentsController(IDocumentsService documentsService, IProjectRequestService projectRequestService, IUserService userService)
            : base(documentsService)
        {
            _documentService = documentsService;
            _projectRequestService = projectRequestService;
            _userService = userService;
        }

        [HttpPost("CreateListOfProjectDocument")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<DocumentsByTypeVM>>> CreateTestScenarioAsync([FromBody] List<DocumentsByTypeVM> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            foreach (var item in model)
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedBy = currentUserId;
            }
            //model.UserId = currentUserId;
            //model.UserType = UserType.Client_User;
            var result = await _documentService.CreateProjectDocumentsList(model);
            //if (result.executionState == ExecutionState.Created)
            //{
            //    TaskLogVM prLog = new TaskLogVM();
            //    prLog.TaskOfProjectId = result.entity.Id;
            //    //prLog.ProjectRequest = result.entity;
            //    prLog.Description = "Task created";
            //    prLog.CreatedBy = currentUserId;
            //    prLog.CreatedAt = result.entity.CreatedAt;
            //    var logResult = await _taskLogService.CreateAsync(prLog);
            //}
            var apiResponse = new WebApiResponse<DocumentsByTypeVM>(result);

            return Ok(apiResponse);

        }


        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<DocumentsByTypeVM>>>> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle)
        {
            try
            {
                var (executionState, entity, message) = await _documentService.Search(ProjectRequestId, DocumentCategoriesId,DocumentTitle);

                return Ok(new WebApiResponse<IList<DocumentsByTypeVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<DocumentsByTypeVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("DocumentsListByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<DocumentsByTypeVM>>>> DocumentsListByClientId(long clientId)
        {
            try
            {
                var (executionState, entity, message) = await _documentService.DocumentsListByClientId(clientId);

                return Ok(new WebApiResponse<IList<DocumentsByTypeVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<DocumentsByTypeVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
