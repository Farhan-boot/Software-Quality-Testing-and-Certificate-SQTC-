using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Bugzilla;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.eCommerce.Web.Core.Services.Interface.Project
{
    public interface IBugAndDefectService
    {
        (ExecutionState executionState, List<BugAndDefectVM> entity, string message) List();
        (ExecutionState executionState, BugAndDefectVM entity, string message) Create(BugAndDefectVM model);
        (ExecutionState executionState, BugAndDefectVM entity, string message) GetById(long? id);
        (ExecutionState executionState, BugAndDefectVM entity, string message) Update(BugAndDefectVM model);
        (ExecutionState executionState, BugAndDefectVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, BugAndDefectVM entity, string message) CreateOfList(List<BugAndDefectVM> model);
        Task<(ExecutionState executionState, IList<BugAndDefectVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity);
        Task<(ExecutionState executionState, BugAndDefectVM entity, string message)> SyncBugsFromBugzillaByProjId(long id);
        Task<(ExecutionState executionState, BugzillaSyncVM entity, string message)> SyncBugsDataViewByProjId(long id);
    }
}
