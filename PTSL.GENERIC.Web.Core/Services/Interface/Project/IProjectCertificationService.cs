using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Project
{
    public interface IProjectCertificationService
    {
        Task<(ExecutionState executionState, List<ProjectCertificationVM> entity, string message)> List();
        Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Create(ProjectCertificationVM model);
        Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> GetById(long? id);
        Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Update(ProjectCertificationVM model);
        Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
