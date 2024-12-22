using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration
{
    public interface IProjectPackageBusiness : IBaseBusiness<ProjectPackage>
    {
        Task<(ExecutionState executionState, IQueryable<ProjectPackage> entity, string message)> GetProjectPackageByProjectModuleNameId(long ProjectModuleNameId);
    }
}