using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class TaskRepository : BaseRepository<TaskOfProject>, ITaskRepository
    {
        private readonly GENERICReadOnlyCtx _ReadOnlyCtx;
        public TaskRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _ReadOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<TaskOfProject> entity, string message)> Search(long? ProjectRequestId, string TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline)
        {
            IQueryable<TaskOfProject> query = _ReadOnlyCtx.Set<TaskOfProject>()
                 .Include(x => x.ProjectRequest)
                 .Include(x=>x.TaskType)
                 .Include(x => x.User);

            query = query.WhereIf(!string.IsNullOrEmpty(TaskId), x => x.TaskId == TaskId);
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(AssigneeId is not null, x => x.UserId == AssigneeId);
            query = query.WhereIf(CreateDate is not null, x => x.CreatedAt == CreateDate);
            query = query.WhereIf(Deadline is not null, x => x.TaskDeadline == Deadline);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
