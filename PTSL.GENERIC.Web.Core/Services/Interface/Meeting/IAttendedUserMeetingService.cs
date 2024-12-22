using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IAttendedUserMeetingService
    {
        (ExecutionState executionState, List<AttendedUserMeetingVM> entity, string message) List();
        (ExecutionState executionState, AttendedUserMeetingVM entity, string message) Create(AttendedUserMeetingVM model);
        (ExecutionState executionState, AttendedUserMeetingVM entity, string message) GetById(long? id);
        (ExecutionState executionState, AttendedUserMeetingVM entity, string message) Update(AttendedUserMeetingVM model);
        (ExecutionState executionState, AttendedUserMeetingVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
