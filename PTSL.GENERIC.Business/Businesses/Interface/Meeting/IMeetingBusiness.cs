using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Meetings
{
    public interface IMeetingBusiness : IBaseBusiness<Meeting>
    {
        Task<(ExecutionState executionState, IList<User> entity, string message)> GetSqtcUser();
        Task<(ExecutionState executionState, IList<User> entity, string message)> GetClientUser(long ProjectId);
        Task<(ExecutionState executionState ,IList<Meeting> entity, string message)> pendingMeetingList();
        Task<(ExecutionState executionState ,IList<Meeting> entity, string message)> MeetingListByClientId(long clientID);
        Task<(ExecutionState executionState ,IList<Meeting> entity, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate);
        //Task<(ExecutionState executionState ,IList<Meeting> entity, string message)> GetMeetingListByClientId(long clientId);
    }
}
