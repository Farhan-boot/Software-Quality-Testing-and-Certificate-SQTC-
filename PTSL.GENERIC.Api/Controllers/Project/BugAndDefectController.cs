using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Api.Helpers.BugZilla;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Bugzilla;
using PTSL.GENERIC.Common.Model.EntityViewModels.BugZilla;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BugAndDefectController : BaseController<IBugAndDefectService, BugAndDefectVM, BugAndDefect>
    {
        private readonly IBugAndDefectService _BugAndDefectService;
        private readonly IProjectRequestService _ProjectRequestService;
        private readonly IConfiguration _configuration;

        public BugAndDefectController(IBugAndDefectService BugAndDefectService, IConfiguration configuration, IProjectRequestService projectRequestService) :
            base(BugAndDefectService)
        {
            _BugAndDefectService = BugAndDefectService;
            _configuration = configuration;
            _ProjectRequestService = projectRequestService;
        }

        //Implement here new api, if we want.

        [HttpPost("CreateListOfBugAndDefect")]
        [Authorize]
        public async Task<ActionResult<WebApiResponse<BugAndDefectVM>>> CreateListOfBugAndDefect([FromBody] List<BugAndDefectVM> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.GetCurrentUserId();
            foreach (var item in model)
            {
                item.ReportedBy = currentUserId;
            }
            var result = await _BugAndDefectService.CreateListOfBugAndDefect(model);

            var apiResponse = new WebApiResponse<BugAndDefectVM>(result);

            return Ok(apiResponse);

        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<IList<BugAndDefectVM>>>> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            try
            {
                var (executionState, entity, message) = await _BugAndDefectService.Search(ProjectRequestId, TaskOfProjectId, TestCaseId,bugzillaId,defectId,bugAndDefectStatus,bugAndDefectSeverity);

                return Ok(new WebApiResponse<IList<BugAndDefectVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch (Exception)
            {
                return StatusCode(500, new WebApiResponse<List<BugAndDefectVM>>(
                        (ExecutionState.Failure, null, "Unexpected error occurred")
                    ));
            }
        }

        public async override Task<ActionResult<WebApiResponse<BugAndDefectVM>>> CreateAsync([FromBody] BugAndDefectVM model)
        {
            var currentUserId = HttpContext.GetCurrentUserId();
            model.ReportedBy = currentUserId;
            var result = await _BugAndDefectService.CreateAsync(model);
            var apiResponse = new WebApiResponse<BugAndDefectVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet("GetViewListForBugzillaSync")]
        public async Task<ActionResult<WebApiResponse<BugzillaSyncVM>>> GetViewListForBugzillaSync(long? projectId)
        {
            var response = new WebApiResponse<BugzillaSyncVM>();
            var getProjectInfo = await _ProjectRequestService.GetAsync(projectId.Value);
            var localBugList = await _BugAndDefectService.List();
            var getLocalBugsByProject = localBugList.entity is not null ? localBugList.entity.Where(s => s.ProjectRequestId == projectId && s.BugAndDefectStatus == BugAndDefectStatus.NotResolved).ToList() : new List<BugAndDefectVM>();
            var projectName = getProjectInfo.entity.ProjectName;
            var bugzillaInfo = GetBugZillaApiInfo();
            var bugzillaBugs = await BugzillaApiHelper.GetBugsApiCall(projectName, bugzillaInfo.BugzillaURL, bugzillaInfo.BugzillaApiKey);
            var bugzillaResolvedBugs = bugzillaBugs.bugs.Where(s => s.product.ToLower().Trim() == projectName.ToLower().Trim() && s.status == "RESOLVED").ToList();
            BugzillaSyncVM responeModel = new BugzillaSyncVM();
            responeModel.BugzillaResolvedBugs.AddRange(bugzillaResolvedBugs);
            //List<BugAndDefectVM> matchedBugs = new List<BugAndDefectVM>();
            foreach (var bug in getLocalBugsByProject)
            {
                var getBugFromBZ = bugzillaResolvedBugs.Where(s => s.summary.ToString() == bug.BugzillaId.ToString()).FirstOrDefault();
                if (getBugFromBZ != null)
                {
                    BugAndDefectVM bugAndDefectVM = new BugAndDefectVM();
                    bugAndDefectVM.Id = bug.Id;
                    bugAndDefectVM.BugzillaId = bug.BugzillaId;
                    bugAndDefectVM.BugAndDefectStatus = bug.BugAndDefectStatus;
                    bugAndDefectVM.BugAndDefectStatusName = Enum.GetName(typeof(BugAndDefectStatus), bug.BugAndDefectStatus);
                    var isConvert = Enum.TryParse(getBugFromBZ.status.ToString(), out BugzillaBugStatus bugstatus);
                    bugAndDefectVM.BugzillaBugStatus = isConvert ? bugstatus : BugzillaBugStatus.UNCONFIRMED;
                    bugAndDefectVM.BugzillaBugStatusName = isConvert ? Enum.GetName(typeof(BugzillaBugStatus), bugstatus) : "";
                    responeModel.MatchedBugsToUpdate.Add(bugAndDefectVM);
                }

            }
            var viewResponse = (ExecutionState.Success, responeModel, "Data Found");
            response = new WebApiResponse<BugzillaSyncVM>(viewResponse);

            return Ok(response);
        }


        [HttpGet("SyncBugsFromBugzillaByProject")]
        public async Task<ActionResult<WebApiResponse<BugAndDefectVM>>> SyncBugsFromBugzillaByProject(long? projectId)
        {
            var apiResponse = new WebApiResponse<BugAndDefectVM>();
            var getProjectInfo = _ProjectRequestService.GetAsync(projectId.Value).Result;
            var getLocalBugsByProject = _BugAndDefectService.List().Result.entity.Where(s => s.ProjectRequestId == projectId && s.BugAndDefectStatus == BugAndDefectStatus.NotResolved).ToList();
            var projectName = getProjectInfo.entity.ProjectName;
            var bugzillaInfo = GetBugZillaApiInfo();
            var bugs = await BugzillaApiHelper.GetBugsApiCall(projectName, bugzillaInfo.BugzillaURL, bugzillaInfo.BugzillaApiKey);
            var resolvedBugs = bugs.bugs.Where(s => s.product.ToLower().Trim() == projectName.ToLower().Trim() && s.status == "RESOLVED").ToList();
            List<BugAndDefectVM> updatedList = new List<BugAndDefectVM>();
            foreach (var bug in getLocalBugsByProject)
            {
                var getBugFromBZ = resolvedBugs.Where(s => s.summary.ToLower().Trim() == bug.BugzillaId.ToLower().Trim()).FirstOrDefault();
                if (getBugFromBZ != null)
                {
                    BugAndDefectVM bugAndDefectVM = new BugAndDefectVM();
                    bugAndDefectVM.Id = bug.Id;
                    bugAndDefectVM.BugzillaId = bug.BugzillaId;
                    bugAndDefectVM.BugAndDefectStatus = getBugFromBZ.status == "RESOLVED" ? BugAndDefectStatus.Resolved : BugAndDefectStatus.NotResolved;
                    bugAndDefectVM.ModifiedBy = HttpContext.GetCurrentUserId();
                    bugAndDefectVM.UpdatedAt = DateTime.UtcNow;
                    updatedList.Add(bugAndDefectVM);
                }

            }
            var response = await _BugAndDefectService.UpdateBugListOnBugzilla(updatedList);
            apiResponse = new WebApiResponse<BugAndDefectVM>(response);
            return Ok(apiResponse);
        }

        #region BugZilla Info
        private BugZillaApiInfo GetBugZillaApiInfo()
        {
            var bugzillaUrl = _configuration.GetValue<string>("BugzillaURL") ?? string.Empty;
            var bugzilla_Api_Key = _configuration.GetValue<string>("BugzillaApiKey") ?? string.Empty;

            return new BugZillaApiInfo
            {
                BugzillaApiKey = bugzilla_Api_Key,
                BugzillaURL = bugzillaUrl
            };
        }

        #endregion
    }
}
