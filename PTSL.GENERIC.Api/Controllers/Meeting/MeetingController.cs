using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.Services.Interface.Meetings;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : BaseController<IMeetingService, MeetingVM, Meeting>
    {
        private readonly IMeetingService _meetService;
        public MeetingController(IMeetingService MeetingService) :
            base(MeetingService)
        { 
            _meetService = MeetingService;
        }

        //Implement here new api, if we want.
        [HttpGet("GetSqtcUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<UserVM>>>> GetSqtcUser()
        {
            try
            {
                var (executionState, entity, message) = await _meetService.GetSqtcUser();

                return Ok(new WebApiResponse<IList<UserVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<UserVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("GetClientUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<UserVM>>>> GetClientUser(long ProjectId)
        {
            try
            {
                var (executionState, entity, message) = await _meetService.GetClientUser(ProjectId);

                return Ok(new WebApiResponse<IList<UserVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<UserVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("pendingMeetingList")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<MeetingVM>>>> pendingMeetingList()
        {
            try
            {
                var (executionState, entity, message) = await _meetService.pendingMeetingList();

                return Ok(new WebApiResponse<IList<MeetingVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<MeetingVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        [HttpGet("MeetingListByClientId")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<MeetingVM>>>> MeetingListByClientId(long ClientId)
        {
            try
            {
                var (executionState, entity, message) = await _meetService.MeetingListByClientId(ClientId);

                return Ok(new WebApiResponse<IList<MeetingVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<MeetingVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }


        [HttpGet("MeetingListByDate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<MeetingVM>>>> MeetingListByDate(DateTime firstDate, DateTime lastDate)
        {
            try
            {
                var (executionState, entity, message) = await _meetService.MeetingListByDate(firstDate, lastDate);

                return Ok(new WebApiResponse<IList<MeetingVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<IList<MeetingVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

    }
}
