using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IProjectPackageService
    {
        (ExecutionState executionState, List<ProjectPackageVM> entity, string message) List();
        (ExecutionState executionState, ProjectPackageVM entity, string message) Create(ProjectPackageVM model);
        (ExecutionState executionState, ProjectPackageVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ProjectPackageVM entity, string message) Update(ProjectPackageVM model);
        (ExecutionState executionState, ProjectPackageVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        (ExecutionState executionState, List<ProjectPackageVM> entity, string message) GetProjectPackageByProjectModuleNameId(long? ProjectModuleNameId);
    }
}