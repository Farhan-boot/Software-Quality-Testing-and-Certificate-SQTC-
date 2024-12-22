using System.Collections.Generic;
using System.Threading.Tasks;

using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Service.BaseServices;

namespace PTSL.GENERIC.Service.Services.PermissionSettings
{
    public interface IPermissionRowSettingsService : IBaseService<PermissionRowSettingsVM, PermissionRowSettings>
    {
        Task<(ExecutionState executionState, List<PermissionRowSettingsVM> data, string message)> GetPermissionRowsByControllerName(string controller);
    }
}