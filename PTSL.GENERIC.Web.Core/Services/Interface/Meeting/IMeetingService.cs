using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IMeetingService
    {
        Task<(ExecutionState executionState, List<MeetingVM> entity, string message)> List();
        Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetClientUser(long ProjectId);
        Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetSqtcUser();
        Task<(ExecutionState executionState, MeetingVM entity, string message)> Create(MeetingVM model);
        Task<(ExecutionState executionState, MeetingVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, MeetingVM entity, string message)> Update(MeetingVM model);
        Task<(ExecutionState executionState, MeetingVM entity, string message)> Delete(long? id);
        Task<(ExecutionState executionState, string message)> DoesExist(long? id);
        Task<(ExecutionState executionState,IList<MeetingVM> entity,string message)> pendingMeetingList();
        Task<(ExecutionState executionState,IList<MeetingVM> entity,string message)> MeetingListByClientId(long clientId);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        Task<(ExecutionState executionState, IList<MeetingVM>, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate);
    }
}
