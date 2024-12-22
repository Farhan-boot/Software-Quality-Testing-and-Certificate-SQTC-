using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration
{
    public interface IProjectPricingSetupBusiness : IBaseBusiness<ProjectPricingSetup>
    {
        Task<(ExecutionState executionState, ProjectPricingSetup entity, string message)> GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId);
    }
}