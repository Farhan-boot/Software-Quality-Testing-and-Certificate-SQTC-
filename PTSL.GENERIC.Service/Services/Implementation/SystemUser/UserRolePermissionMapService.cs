using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.SystemUser;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.SystemUser;

using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services.Implementation.SystemUser
{
    public class UserRolePermissionMapService : BaseService<UserRolePermissionMapVM, UserRolePermissionMap>, IUserRolePermissionMapService
    {
        public IMapper _mapper;

        public UserRolePermissionMapService(IUserRolePermissionMapBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override UserRolePermissionMap CastModelToEntity(UserRolePermissionMapVM model)
        {
            return _mapper.Map<UserRolePermissionMap>(model);
        }

        public override UserRolePermissionMapVM CastEntityToModel(UserRolePermissionMap entity)
        {
            return _mapper.Map<UserRolePermissionMapVM>(entity);
        }

        public override IList<UserRolePermissionMapVM> CastEntityToModel(IQueryable<UserRolePermissionMap> entity)
        {
            return _mapper.Map<IList<UserRolePermissionMapVM>>(entity).ToList();
        }
    }
}