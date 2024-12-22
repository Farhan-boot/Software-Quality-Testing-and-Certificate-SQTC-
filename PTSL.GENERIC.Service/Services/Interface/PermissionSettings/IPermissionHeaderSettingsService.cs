using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Service.BaseServices;

namespace PTSL.GENERIC.Service.Services.PermissionSettings
{
    public interface IPermissionHeaderSettingsService : IBaseService<PermissionHeaderSettingsVM, PermissionHeaderSettings>
    {
        Task<(ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message)> GetPermissionHeaderSettingsByModuleEnumId(long moduleEnumId);
        //Task<(ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message)> GetByFilter(ExecutiveCommitteeFilterVM filter);
       // Task<(ExecutionState executionState, long data, string message)> GetPermissionHeaderIdByControllerName(string controller);
    }
}