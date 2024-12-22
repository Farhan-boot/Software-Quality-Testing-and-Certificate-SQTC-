﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public class TaskTypeController : BaseController<ITaskTypeService, TaskTypeVM, TaskType>
    {
        public TaskTypeController(ITaskTypeService TaskTypeervice) :
            base(TaskTypeervice)
        { }

        //Implement here new api, if we want.
    }
}
