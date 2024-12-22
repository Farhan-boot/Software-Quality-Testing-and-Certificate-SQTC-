using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Api.Controllers.SystemUser
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PmsGroupController : BaseController<IPmsGroupService, PmsGroupVM, PmsGroup>
    {
        public PmsGroupController(IPmsGroupService PmsGroupService) :
            base(PmsGroupService)
        { }
    }
}
