using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.SystemUser;
using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.SystemUser
{
    public class UserRolePermissionMapBusiness : BaseBusiness<UserRolePermissionMap>, IUserRolePermissionMapBusiness
    {
        public UserRolePermissionMapBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}