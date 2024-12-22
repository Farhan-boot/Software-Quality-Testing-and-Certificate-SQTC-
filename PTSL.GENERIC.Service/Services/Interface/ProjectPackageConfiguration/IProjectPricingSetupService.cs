using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public interface IProjectPricingSetupService : IBaseService<ProjectPricingSetupVM, ProjectPricingSetup>
    {
        Task<(ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message)> GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId);
    }
}