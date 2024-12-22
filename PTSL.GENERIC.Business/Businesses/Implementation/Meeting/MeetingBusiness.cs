using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Meetings;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Meetings
{
    public class MeetingBusiness : BaseBusiness<Meeting>, IMeetingBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public readonly GENERICReadOnlyCtx _gENERICReadOnlyCtx;
        public readonly IMeetingRepository _MeetingRepository;
        public MeetingBusiness(GENERICUnitOfWork unitOfWork, IMeetingRepository MeetingRepository, GENERICReadOnlyCtx gENERICReadOnlyCtx)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _MeetingRepository = MeetingRepository;
            _gENERICReadOnlyCtx = gENERICReadOnlyCtx;
        }

        public async override Task<(ExecutionState executionState, IQueryable<Meeting> entity, string message)> List(QueryOptions<Meeting> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<Meeting> entity, string message) returnResponse;
            var queryOption = new QueryOptions<Meeting>();
            queryOption.IncludeExpression = x => x.Include(y => y.ProjectRequest!).Include(y=>y.MeetingFiles)
            .Include(y => y.MeetingType!).Include(x=>x.AttendedUsers!).ThenInclude(x=>x.AttendUser!);
            queryOption.SortingExpression = x => x.OrderByDescending(x => x.Id);
            (ExecutionState executionState, IQueryable<Meeting> entity, string message) entityObject = await _unitOfWork.List<Meeting>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, IList<User> entity, string message)> GetClientUser(long ProjectId)
        {
            var ProjectData =  _gENERICReadOnlyCtx.Set<ProjectRequest>()
                .Where(x => x.Id == ProjectId);
            var ClientId = ProjectData.Select(x => x.ClientId).FirstOrDefault();
            var ClientUserId = _gENERICReadOnlyCtx.Set<User>().
                Where(x=>x.ClientId == ClientId).Select(x=>x.Id).FirstOrDefault();
            var result = await _gENERICReadOnlyCtx.Set<User>()
                .Where(x=>x.CreatedBy == ClientUserId || x.ClientId == ClientId)
                .ToListAsync();
            return (ExecutionState.Success, result, "Data Found");
        }

        public async Task<(ExecutionState executionState, IList<User> entity, string message)> GetSqtcUser()
        {
            var result = await _gENERICReadOnlyCtx.Set<User>()
                .Where(x=>x.UserType == UserType.SQTC_User || x.UserType == UserType.SQTC_Admin)
                .ToListAsync();
            return (ExecutionState.Success,result,"Data Found");
        }

        public override Task<(ExecutionState executionState, Meeting entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<Meeting>();
            filterOptions.FilterExpression = x=>x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.ProjectRequest!)
            .Include(y => y.MeetingType!).Include(x => x.AttendedUsers!).ThenInclude(x => x.AttendUser!);
            
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<Meeting> entity, string message)> pendingMeetingList()
        {
            var result = await _gENERICReadOnlyCtx.Set<Meeting>()
                .Where(x => x.MeetingStatus == MeetingStatus.Pending)
                .Include(y => y.ProjectRequest!)
                .Include(y => y.MeetingType!).Include(x => x.AttendedUsers!).ThenInclude(x => x.AttendUser!)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return (ExecutionState.Success, result, "Pending Meeting List Item Found");
        }

        public async Task<(ExecutionState executionState, IList<Meeting> entity, string message)> MeetingListByClientId(long clientID)
        {
            var projects = await _gENERICReadOnlyCtx.Set<ProjectRequest>()
                .Where(x=>x.ClientId == clientID).Select(x=>x.Id).ToListAsync();
            var result = await _gENERICReadOnlyCtx.Set<Meeting>()
                .Where(x=> projects.Contains(x.ProjectRequestId)).Include(y => y.ProjectRequest!)
                .Include(y => y.MeetingFiles)
                .Include(y => y.MeetingType!).Include(x => x.AttendedUsers!).ThenInclude(x => x.AttendUser!)
                .OrderByDescending(x => x.Id)
                .ToListAsync();


            return (ExecutionState.Success, result, "Data Found");
        }


        public async Task<(ExecutionState executionState, IList<Meeting> entity, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate)
        {
            var result = new List<Meeting>();
            if (firstDate.Date == lastDate.Date)
            {
                result = await _gENERICReadOnlyCtx.Set<Meeting>()
                               .Where(x => x.MeetingStartTime!.Value.Date == firstDate.Date && x.IsInitiatedBySqtc == true).Include(y => y.ProjectRequest!)
                               .Include(y => y.MeetingFiles)
                               .Include(y => y.MeetingType!).Include(x => x.AttendedUsers!).ThenInclude(x => x.AttendUser!)
                               .OrderByDescending(x => x.Id)
                               .ToListAsync();
            }
            else
            {
                result = await _gENERICReadOnlyCtx.Set<Meeting>()
                               .Where(x => x.MeetingStartTime!.Value.Date >= firstDate.Date && x.MeetingStartTime <= lastDate.Date && x.IsInitiatedBySqtc == true).Include(y => y.ProjectRequest!)
                               .Include(y => y.MeetingFiles)
                               .Include(y => y.MeetingType!).Include(x => x.AttendedUsers!).ThenInclude(x => x.AttendUser!)
                               .OrderByDescending(x => x.Id)
                               .ToListAsync();
            }

            return (ExecutionState.Success, result, "Data Found");
        }
    }
}
