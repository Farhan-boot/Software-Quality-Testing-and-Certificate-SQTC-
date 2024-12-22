using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;

namespace PTSL.GENERIC.Api.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllTypesDocumentController : BaseController<IAllTypesOfDocumentService, AllTypesOfDocumentVM, AllTypesOfDocument>
    {
        private readonly IAllTypesOfDocumentService _allTypesDocumentService;
        private readonly IUserService _userService;
        public AllTypesDocumentController(IAllTypesOfDocumentService allTypesDocumentService, IUserService userService)
            : base(allTypesDocumentService)
        {
            _allTypesDocumentService = allTypesDocumentService;
            _userService = userService;
        }

        [HttpGet("ListByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<AllTypesOfDocumentVM>>>> ListByClientId(long ClientId)
        {
            try
            {
                var (executionState, entity, message) = await _allTypesDocumentService.ListByClientId(ClientId);

                return Ok(new WebApiResponse<IList<AllTypesOfDocumentVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<AllTypesOfDocumentVM>>(
                        (Common.Enum.ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}
