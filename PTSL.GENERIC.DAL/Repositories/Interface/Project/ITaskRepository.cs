using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
    public interface ITaskRepository : IBaseRepository<TaskOfProject>
    {
        Task<(ExecutionState executionState, IList<TaskOfProject> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline);
    }
}
