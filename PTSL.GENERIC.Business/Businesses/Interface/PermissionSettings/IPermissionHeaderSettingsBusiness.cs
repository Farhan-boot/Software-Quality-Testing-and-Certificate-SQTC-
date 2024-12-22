using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings
{
    public interface IPermissionHeaderSettingsBusiness : IBaseBusiness<PermissionHeaderSettings>
    {
        Task<(ExecutionState executionState, List<PermissionHeaderSettings> entity, string message)> GetPermissionHeaderSettingsByModuleEnumId(long moduleEnumId);
        //Task<(ExecutionState executionState, long data, string message)> GetPermissionHeaderIdByControllerName(string controller);
    }
}