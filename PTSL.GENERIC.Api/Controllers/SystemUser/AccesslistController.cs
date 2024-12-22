using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.SystemUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccesslistController : BaseController<IAccesslistService, AccesslistVM, Accesslist>
    {
        private readonly IAccesslistService _accesslistService;

        public AccesslistController(IAccesslistService AccesslistService) :
            base(AccesslistService)
        {
            _accesslistService = AccesslistService;
        }

        [HttpGet("GetAccessListId/{accessUrl}")]
        public async Task<ActionResult<WebApiResponse<long>>> GetAccessListId(string accessUrl)
        {
            try
            {
                var (executionState, entity, message) = await _accesslistService.GetAccessListId(accessUrl);

                return Ok(new WebApiResponse<long>(
                        (executionState, entity, message)
                    ));
            }
            catch
            {
                return StatusCode(500, new WebApiResponse<long>(
                        (ExecutionState.Failure, 0, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("ListForApproval")]
        public async Task<ActionResult<WebApiResponse<IList<AccesslistVM>>>> ListForApproval()
        {
            try
            {
                var (executionState, entity, message) = await _accesslistService.ListForApproval();

                return Ok(new WebApiResponse<IList<AccesslistVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch
            {
                return StatusCode(500, new WebApiResponse<IList<AccesslistVM>>(
                        (ExecutionState.Failure, null!, "Unexpected error occurred")
                    ));
            }
        }
    }
}
