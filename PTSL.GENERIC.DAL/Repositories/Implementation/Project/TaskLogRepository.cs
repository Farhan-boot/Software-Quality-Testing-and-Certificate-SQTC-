﻿using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class TaskLogRepository : BaseRepository<TaskLog>, ITaskLogRepository
    {
        public TaskLogRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
        }
    }
}
