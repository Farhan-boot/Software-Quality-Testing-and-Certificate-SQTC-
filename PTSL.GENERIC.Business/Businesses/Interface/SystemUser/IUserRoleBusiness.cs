using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface
{
    public interface IUserRoleBusiness : IBaseBusiness<UserRole>
    {
        Task<(ExecutionState executionState, UserRole entity, string message)> SetPermissions(UserRoleSetPermissionsVM model);
        Task<(ExecutionState executionState, UserRole entity, string message)> SetAccessLists(UserRoleSetAccessListsVM model);
        Task<(ExecutionState executionState, UserRole entity, string message)> AddRoleToUser(long userId, long roleId);
        Task<bool> UserHasPermission(long userId, List<string> permissionNames);
        Task<Dictionary<string, bool>> UserHasPermission(UserHasPermissionsVM model);
        Task<(ExecutionState executionState, List<string> entity, string message)> ListByRoleId(long roleId);
        Task<(ExecutionState executionState, RootMenuVM entity, string message)> CurrentUserRootMenu(long userId);
        Task<(ExecutionState executionState, long roleId, string message)> GetRoleIdByUserId(long userId);
        Task<(ExecutionState executionState, List<UserRole> data, string message)> GetRolesByIds(long[] roleIds);
    }
}
