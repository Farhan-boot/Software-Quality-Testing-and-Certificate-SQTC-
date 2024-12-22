using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.UserEntitys;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.Services.Interface.SystemUser;

namespace PTSL.GENERIC.Api.Controllers.SystemUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolePermissionMapController : BaseController<IUserRolePermissionMapService, UserRolePermissionMapVM, UserRolePermissionMap>
    {
        public UserRolePermissionMapController(IUserRolePermissionMapService service) :
            base(service)
        {
        }
    }
}