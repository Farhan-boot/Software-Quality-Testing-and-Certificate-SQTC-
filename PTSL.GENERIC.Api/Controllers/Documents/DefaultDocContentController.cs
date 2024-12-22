using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultDocContentController : BaseController<IDefaultDocContentService, DefaultDocumentContentVM, DefaultDocumentContent>
    {
        private readonly IDefaultDocContentService _defaultDocContentService;
        private readonly IUserService _userService;
        public DefaultDocContentController(IDefaultDocContentService defaultDocContentService, IUserService userService)
            : base(defaultDocContentService)
        {
            _defaultDocContentService = defaultDocContentService;
            _userService = userService;
        }
        
        [HttpGet("GetDocContentByDocType")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<DefaultDocumentContentVM>>> DocContentByDocType(long docTypeId)
        {
            try
            {
                var (executionState, entity, message) = await _defaultDocContentService.GetDefaultDocByDocType((DocumentType)docTypeId);

                return Ok (new WebApiResponse<DefaultDocumentContentVM>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<DefaultDocumentContentVM>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
