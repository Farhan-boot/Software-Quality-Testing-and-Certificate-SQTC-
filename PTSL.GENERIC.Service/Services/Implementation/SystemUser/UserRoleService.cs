using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation
{
    public class UserRoleService : BaseService<UserRoleVM, UserRole>, IUserRoleService
    {
        public readonly IUserRoleBusiness _userRolesBusiness;
        public IMapper _mapper;

        public UserRoleService(IUserRoleBusiness userRolesBusiness, IMapper mapper) : base(userRolesBusiness)
        {
            _userRolesBusiness = userRolesBusiness;
            _mapper = mapper;
        }

        public override UserRole CastModelToEntity(UserRoleVM model)
        {
            try
            {
                return _mapper.Map<UserRole>(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override UserRoleVM CastEntityToModel(UserRole entity)
        {
            try
            {
                UserRoleVM model = _mapper.Map<UserRoleVM>(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override IList<UserRoleVM> CastEntityToModel(IQueryable<UserRole> entity)
        {
            try
            {
                IList<UserRoleVM> userRolesList = _mapper.Map<IList<UserRoleVM>>(entity).ToList();
                return userRolesList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, List<string> entity, string message)> ListByRoleId(long roleId)
        {
            var (executionState, entity, message) = await _userRolesBusiness.ListByRoleId(roleId);

            return (executionState, entity, message);
        }

        public async Task<(ExecutionState executionState, UserRoleVM entity, string message)> SetAccessLists(UserRoleSetAccessListsVM model)
        {
            var (executionState, _, message) = await _userRolesBusiness.SetAccessLists(model);

            return (executionState, null!, message);
        }

        public async Task<(ExecutionState executionState, UserRoleVM entity, string message)> SetPermissions(UserRoleSetPermissionsVM model)
        {
            var (executionState, _, message) = await _userRolesBusiness.SetPermissions(model);

            return (executionState, null!, message);
        }

        public async Task<(ExecutionState executionState, UserRoleVM entity, string message)> AddRoleToUser(long userId, long roleId)
        {
            var (executionState, _, message) = await _userRolesBusiness.AddRoleToUser(userId, roleId);

            return (executionState, null!, message);
        }

        public Task<bool> UserHasPermission(long userId, List<string> permissionNames)
        {
            return _userRolesBusiness.UserHasPermission(userId, permissionNames);
        }

        public Task<Dictionary<string, bool>> UserHasPermission(UserHasPermissionsVM model)
        {
            return _userRolesBusiness.UserHasPermission(model);
        }

        public async Task<(ExecutionState executionState, List<UserRoleVM> data, string message)> GetRolesByIds(long[] roleIds)
        {
            var userRole = await _userRolesBusiness.GetRolesByIds(roleIds);

            return (ExecutionState.Success, _mapper.Map<List<UserRoleVM>>(userRole.data), "Success");
        }
    }
}
