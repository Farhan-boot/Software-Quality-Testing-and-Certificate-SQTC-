using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings
{
    public interface IPermissionRowSettingsBusiness : IBaseBusiness<PermissionRowSettings>
    {
        Task<(ExecutionState executionState, List<PermissionRowSettings> data, string message)> GetPermissionRowsByControllerName(string controller);
        Task<(ExecutionState executionState, List<PermissionRowSettings> entity, string message)> GetRowListByHeaderAscending(long permissionHeaderSettingsId);
    }
}