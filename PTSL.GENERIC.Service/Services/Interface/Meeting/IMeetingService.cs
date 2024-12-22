using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Meetings
{
    public interface IMeetingService : IBaseService<MeetingVM, Meeting>
	{
        Task<(ExecutionState executionState, IList<UserVM> entity, string message)> GetSqtcUser();
        Task<(ExecutionState executionState, IList<UserVM> entity, string message)> GetClientUser(long ProjectId);
        Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> pendingMeetingList();
        Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> MeetingListByClientId(long ClientId);
        Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate);


    }
}
