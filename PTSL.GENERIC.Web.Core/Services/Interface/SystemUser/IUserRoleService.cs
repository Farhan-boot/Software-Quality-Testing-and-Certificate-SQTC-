using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Web.Core.Services.Interface.SystemUser
{
    public interface IUserRoleService
	{
		(ExecutionState executionState, List<UserRoleVM> entity, string message) List();
		(ExecutionState executionState, UserRoleVM entity, string message) Create(UserRoleVM model);
		(ExecutionState executionState, UserRoleVM entity, string message) GetById(long? id);
		(ExecutionState executionState, UserRoleVM entity, string message) Update(UserRoleVM model);
		(ExecutionState executionState, UserRoleVM entity, string message) Delete(long? id);
        (ExecutionState executionState, UserRoleVM entity, string message) SetAccessLists(UserRoleSetAccessListsVM model);
        (ExecutionState executionState, UserRoleVM entity, string message) SetPermissions(UserRoleSetPermissionsVM model);
        (ExecutionState executionState, UserRoleVM entity, string message) AddRoleToUser(long userId, long roleId);
        (ExecutionState executionState, List<PermissionGroup> entity, string message) PermissionList();
        Dictionary<string, bool> CurrentUserHasPermissions(params string[] permissions);
        (ExecutionState executionState, List<string> entity, string message) ListByRoleId(long roleId);
        (ExecutionState executionState, RootMenuVM entity, string message) CurrentUserRootMenu();
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        (ExecutionState executionState, List<UserRoleVM> entity, string message) GetRolesByIds(IEnumerable<long> roleIds);
    }
}
