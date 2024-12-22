using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public interface IProjectPackageService : IBaseService<ProjectPackageVM, ProjectPackage>
    {
      Task<(ExecutionState executionState, IList<ProjectPackageVM> entity, string message)> GetProjectPackageByProjectModuleNameId(long ProjectModuleNameId);
    }
}