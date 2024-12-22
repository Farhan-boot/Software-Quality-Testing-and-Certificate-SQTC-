using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IProjectPricingSetupService
    {
        (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) List();
        (ExecutionState executionState, ProjectPricingSetupVM entity, string message) Create(ProjectPricingSetupVM model);
        (ExecutionState executionState, ProjectPricingSetupVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ProjectPricingSetupVM entity, string message) Update(ProjectPricingSetupVM model);
        (ExecutionState executionState, ProjectPricingSetupVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        (ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message) GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId);
    }

}