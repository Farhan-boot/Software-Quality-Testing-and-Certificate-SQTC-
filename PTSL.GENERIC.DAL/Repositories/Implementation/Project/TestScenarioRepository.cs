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
    public class TestScenarioRepository : BaseRepository<TestScenario>, ITestScenarioRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public TestScenarioRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, List<TestScenario> entity, string message)> CreateListOfTestScenarioAsync(List<TestScenario> entityList)
        {
            if (entityList.Count is not 0) { 
                foreach (var entity in entityList) {
                    if (entity != null)
                    {
                        try
                        {
                            await base.CreateAsync(entity);

                        }
                        catch (Exception ex)
                        {
                            return (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
                        }
                    }
                    else
                    {
                        return (executionState: ExecutionState.Failure, entity: null, message: null);
                    }
                }
                return (ExecutionState.Success, entityList, $"New test scenarios added.");

            }
            else
            {
                return (executionState: ExecutionState.Failure, entity: null, message: null);
            }
        }

        public async Task<(ExecutionState executionState, IList<TestScenario> entity, string message)> Search(long? ProjectRequestId, string? TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
        {
            IQueryable<TestScenario> query = _readOnlyCtx.Set<TestScenario>()
                .Include(x => x.ProjectRequest).Include(x=>x.TaskOfProject);

            query = query.WhereIf(!string.IsNullOrEmpty(TestScenarioNo),x=> TestScenarioNo.Contains(x.TestScenarioNo));
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(TaskPriority is not null, x => x.TaskPriority == TaskPriority);
            query = query.WhereIf(ActualExecutionDate is not null, x => x.ActualExecutionDate == ActualExecutionDate);
            query = query.WhereIf(PlannedExecutionDate is not null, x => x.ActualExecutionDate == PlannedExecutionDate);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
