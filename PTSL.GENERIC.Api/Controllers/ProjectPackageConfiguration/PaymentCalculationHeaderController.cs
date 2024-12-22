using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Api.Controllers.ProjectPackageConfiguration
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCalculationHeaderController : BaseController<IPaymentCalculationHeaderService, PaymentCalculationHeaderVM, PaymentCalculationHeader>
    {
        private readonly IPaymentCalculationHeaderService _paymentCalculationHeaderService;
        public PaymentCalculationHeaderController(IPaymentCalculationHeaderService service) :
            base(service)
        {
            _paymentCalculationHeaderService= service;
        }

        [HttpGet("ListByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<PaymentCalculationHeaderVM>>>> ListByClientId(long ClientId)
        {
            try
            {
                var (executionState, entity, message) = await _paymentCalculationHeaderService.ListByClientId(ClientId);

                return Ok(new WebApiResponse<IList<PaymentCalculationHeaderVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<PaymentCalculationHeaderVM>>(
                        (Common.Enum.ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }
    }
}