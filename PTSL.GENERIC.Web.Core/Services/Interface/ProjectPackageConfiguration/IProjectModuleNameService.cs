using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IProjectModuleNameService
    {
        (ExecutionState executionState, List<ProjectModuleNameVM> entity, string message) List();
        (ExecutionState executionState, ProjectModuleNameVM entity, string message) Create(ProjectModuleNameVM model);
        (ExecutionState executionState, ProjectModuleNameVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ProjectModuleNameVM entity, string message) Update(ProjectModuleNameVM model);
        (ExecutionState executionState, ProjectModuleNameVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}