using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation;

public class UserRoleBusiness : BaseBusiness<UserRole>, IUserRoleBusiness
{
    private readonly IMemoryCache _memoryCache;
    private readonly GENERICWriteOnlyCtx _writeOnlyCtx;
    private readonly GENERICReadOnlyCtx _readOnlyCtx;

    public UserRoleBusiness(
        IMemoryCache memoryCache,
        GENERICUnitOfWork unitOfWork,
        GENERICWriteOnlyCtx writeOnlyCtx,
        GENERICReadOnlyCtx readOnlyCtx)
        : base(unitOfWork)
    {
        _memoryCache = memoryCache;
        _writeOnlyCtx = writeOnlyCtx;
        _readOnlyCtx = readOnlyCtx;
    }

    public override Task<(ExecutionState executionState, IQueryable<UserRole> entity, string message)> List(QueryOptions<UserRole> queryOptions = null)
    {
        return base.List(new QueryOptions<UserRole>
        {
            SortingExpression = e => e.OrderByDescending(x => x.Id)
        });
    }

    public async Task<(ExecutionState executionState, RootMenuVM entity, string message)> CurrentUserRootMenu(long userId)
    {
        var userInfo = await _readOnlyCtx.Set<User>()
            .Where(x => x.Id == userId)
            .Select(x => new { 
                x.UserName,
                x.UserRole!.RoleName,
                x.UserRole!.AccessList
            })
            .FirstOrDefaultAsync();
        var rootMenu = new RootMenuVM
        {
            UserName = userInfo?.UserName ?? string.Empty,
            RoleName = userInfo?.RoleName ?? string.Empty,
        };

        if (string.IsNullOrWhiteSpace(userInfo?.AccessList))
        {
            return (ExecutionState.Success, rootMenu, "Ok");
        }

        var accessLists = userInfo.AccessList.Split(',')
            .Select(s => long.TryParse(s, out long result) ? result : 0)
            .ToArray();

        var modules = await _readOnlyCtx.Set<Module>()
            .Where(x => x.AccessList!.Any(y => accessLists.Contains(y.Id)))
            .Include(x => x.AccessList)
            .ToListAsync();
        var menus = new List<MenuVM>();

        foreach (var module in modules.OrderBy(x => x.MenueOrder))
        {
            if (module is null) continue;
            var newMenu = new MenuVM
            {
                ModuleID = module.Id,
                ModuleName = module.ModuleName,
                ModuleIcon = module.ModuleIcon
            };

            if (module.AccessList is null) continue;
            foreach (var list in module.AccessList.OrderBy(x => x.BaseModuleIndex))
            {
                foreach (var item in accessLists)
                {
                    if (list.Id == item)
                    {
                        newMenu.AccessList.Add(new ModuleAccessList
                        {
                            AccessID = list.Id,
                            ControllerName = list.ControllerName,
                            ActionName = list.ActionName,
                            Mask = list.Mask,
                            AccessStatus = list.AccessStatus,
                            IsVisible = list.IsVisible,
                            IconClass = list.IconClass,
                            BaseModule = list.ModuleId ?? 0,
                            BaseModuleIndex = list.BaseModuleIndex ?? 0,
                        });
                    }
                }

               
            }

            menus.Add(newMenu);
        }

        rootMenu.MenuList = menus;

        return (ExecutionState.Success, rootMenu, "Ok");
    }

    public async Task<(ExecutionState executionState, List<string> entity, string message)> ListByRoleId(long roleId)
    {
        var permissions = await _readOnlyCtx.Set<UserRolePermissionMap>()
            .Where(x => x.UserRoleId == roleId)
            .Select(x => x.PermissionName)
            .ToListAsync();

        return (ExecutionState.Success, permissions, "Ok");
    }

    public async Task<(ExecutionState executionState, UserRole entity, string message)> SetAccessLists(UserRoleSetAccessListsVM model)
    {
        var accessList = string.Join(",", model.AccessLists.Select(x => x.ToString()));

        await _writeOnlyCtx.Set<UserRole>()
            .Where(x => x.Id == model.UserRoleId)
            .ExecuteUpdateAsync(x => x.SetProperty(s => s.AccessList, accessList));

        return (ExecutionState.Success, null!, "Updated");
    }

    public async Task<(ExecutionState executionState, UserRole entity, string message)> SetPermissions(UserRoleSetPermissionsVM model)
    {
        var currentPermissions = await _readOnlyCtx.Set<UserRolePermissionMap>()
            .Where(x => x.UserRoleId == model.UserRoleId)
            .Select(x => x.PermissionName)
            .ToListAsync();

        var newPermissions = model.Permissions.Except(currentPermissions).ToList();
        var removedItems = currentPermissions.Except(model.Permissions).ToList();

        await _writeOnlyCtx.Set<UserRolePermissionMap>()
            .Where(x => x.UserRoleId == model.UserRoleId)
            .Where(x => removedItems.Contains(x.PermissionName))
            .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.IsDeleted, true)
                .SetProperty(y => y.IsActive, false));

        var lists = new List<UserRolePermissionMap>(newPermissions.Count);
        foreach (var permission in newPermissions)
        {
            lists.Add(new UserRolePermissionMap
            {
                UserRoleId = model.UserRoleId,
                PermissionName = permission,
            });
        }

        await _writeOnlyCtx.Set<UserRolePermissionMap>()
            .AddRangeAsync(lists);
        await _writeOnlyCtx.SaveChangesAsync();

        return (ExecutionState.Success, null!, "Updated");
    }

    public async Task<(ExecutionState executionState, UserRole entity, string message)> AddRoleToUser(long userId, long roleId)
    {
        await _writeOnlyCtx.Set<User>()
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(x => x.SetProperty(s => s.UserRoleId, roleId));

        return (ExecutionState.Success, null!, "Updated");
    }

    public async Task<bool> UserHasPermission(long userId, List<string> permissionNames)
    {
        var userHasPermission = await _readOnlyCtx.Set<User>()
            .Where(x => x.Id == userId)
            .AnyAsync(x => x.UserRole!.UserRolePermissionMaps!.Any(y => permissionNames.Contains(y.PermissionName)));

        return userHasPermission;
    }

    public async Task<Dictionary<string, bool>> UserHasPermission(UserHasPermissionsVM model)
    {
        var userRoleId = await _readOnlyCtx.Set<User>()
            .Where(x => x.Id == model.UserId)
            .Select(x => x.UserRoleId)
            .FirstOrDefaultAsync();

        var userPermissions = await _readOnlyCtx.Set<UserRolePermissionMap>()
            .Where(x => x.UserRoleId == userRoleId)
            .Select(x => x.PermissionName)
            .ToListAsync();

        var permissionDictionary = new Dictionary<string, bool>();
        foreach (var permission in model.Permissions)
        {
            bool hasPermission = userPermissions.Contains(permission);
            permissionDictionary.Add(permission, hasPermission);
        }

        return permissionDictionary;
    }

    public override async Task<(ExecutionState executionState, UserRole entity, string message)> CreateAsync(UserRole entity)
    {
        var roleNameAlreadyExists = await base.CountAsync(new CountOptions<UserRole>()
        {
            FilterExpression = e => e.RoleName == entity.RoleName,
        });

        if (roleNameAlreadyExists.entityCount > 0)
        {
            return (ExecutionState.Success, null, "Same role name already exists");
        }

        return await base.CreateAsync(entity);
    }

    public override async Task<(ExecutionState executionState, UserRole entity, string message)> UpdateAsync(UserRole entity)
    {
        var roleNameAlreadyExists = await base.CountAsync(new CountOptions<UserRole>()
        {
            FilterExpression = e => e.RoleName == entity.RoleName && e.Id != entity.Id,
        });

        if (roleNameAlreadyExists.entityCount > 0)
        {
            return (ExecutionState.Success, null, "Same role name already exists");
        }

        return await base.UpdateAsync(entity);
    }

    public override async Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, long userId)
    {
        var userRoleCount = await _readOnlyCtx.Set<User>().Where(x => x.UserRoleId == key).CountAsync();
        if (userRoleCount > 0)
        {
            return (ExecutionState.Success, false, "User role already added in user");
        }

        return await base.SoftDeleteAsync(key, userId);
    }

    public async Task<(ExecutionState executionState, long roleId, string message)> GetRoleIdByUserId(long userId)
    {
        var userRole = await _readOnlyCtx.Set<User>().Where(x => x.Id == userId).Select(x => x.UserRoleId).FirstOrDefaultAsync();
        if (userRole is null || userRole < 1)
        {
            return (ExecutionState.Failure, default, "Not found");
        }

        return (ExecutionState.Success, (long)userRole, "Ok");
    }

    public async Task<(ExecutionState executionState, List<UserRole> data, string message)> GetRolesByIds(long[] roleIds)
    {
        var userRole = await _readOnlyCtx.Set<UserRole>().Where(x => roleIds.Contains(x.Id)).ToListAsync();

        return (ExecutionState.Success, userRole, "Success");
    }
}
