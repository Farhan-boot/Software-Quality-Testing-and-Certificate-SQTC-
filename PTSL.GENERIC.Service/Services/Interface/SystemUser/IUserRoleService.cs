using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface
{
    public interface IUserRoleService : IBaseService<UserRoleVM, UserRole>
    {
        Task<(ExecutionState executionState, UserRoleVM entity, string message)> SetPermissions(UserRoleSetPermissionsVM model);
        Task<(ExecutionState executionState, UserRoleVM entity, string message)> SetAccessLists(UserRoleSetAccessListsVM model);
        Task<(ExecutionState executionState, UserRoleVM entity, string message)> AddRoleToUser(long userId, long roleId);
        Task<bool> UserHasPermission(long userId, List<string> permissionNames);
        Task<Dictionary<string, bool>> UserHasPermission(UserHasPermissionsVM model);
        Task<(ExecutionState executionState, List<string> entity, string message)> ListByRoleId(long roleId);
        Task<(ExecutionState executionState, List<UserRoleVM> data, string message)> GetRolesByIds(long[] roleIds);
    }
}
