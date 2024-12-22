using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class TestScenarioBusiness : BaseBusiness<TestScenario>, ITestScenarioBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly ITestScenarioRepository _testScenarioRepository;
        public TestScenarioBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, ITestScenarioRepository testScenarioRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _testScenarioRepository = testScenarioRepository;
        }

        public async Task<(ExecutionState executionState, TestScenario entity, string message)> CreateScenarioListAsync(List<TestScenario> entityList)
        {
            (ExecutionState executionState, TestScenario entity, string message) createResponse = (executionState: ExecutionState.Activated, entity: null, message: $"");

            //await using (IDbContextTransaction transaction = UoW.Begin())
            //{
                try
                {
                    var query = _readOnlyContext.Set<TestScenario>()
                        .Where(x => x.IsActive && !x.IsDeleted)
                        .OrderByDescending(x => x.Id)
                        .AsQueryable();
                    var tasks = await query.ToListAsync();
                    var totalScenario = tasks.Count();
                    totalScenario++;
                    foreach (TestScenario testScenario in entityList)
                    {
                    FilterOptions<TestScenario> filterOptions = new FilterOptions<TestScenario>();
                    filterOptions.FilterExpression = x => x.Module.Trim() == testScenario.Module.Trim() && x.UserType.Trim() == testScenario.UserType.Trim() && x.ProjectRequestId == testScenario.ProjectRequestId && x.TaskOfProjectId == testScenario.TaskOfProjectId;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TestScenario).Name} already exists.");
                        return createResponse;
                    }
                    string scenarioCode = "";
                        if (totalScenario != 0)
                        {
                            scenarioCode = "TS-" + totalScenario.ToString().PadLeft(4, '0');
                        }
                        else
                            scenarioCode = "TS-0001";

                        testScenario.TestScenarioNo = scenarioCode;

                        (ExecutionState executionState, TestScenario entity, string message) createdResponse = await base.CreateAsync(testScenario);
                        createResponse = (createdResponse.executionState, createdResponse.entity, createdResponse.message);
                    
                        totalScenario++;
                    }
                    return createResponse;

                }
                catch (Exception ex)
                {
                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on test scenarios creation.");
                    return createResponse;

                }
            //}
           

        }

        public override async Task<(ExecutionState executionState, TestScenario entity, string message)> UpdateAsync(TestScenario entity)
        {
            (ExecutionState executionState, TestScenario entity, string message) updateResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<TestScenario> filterOptions = new FilterOptions<TestScenario>();
                    filterOptions.FilterExpression = x => x.Module.Trim() == entity.Module.Trim() && x.UserType.Trim() == entity.UserType.Trim() && x.Id != entity.Id;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TestScenario).Name} name already exists.");
                        return updateResponse;
                    }

                    var scenario = await _unitOfWork.GetAsync(new FilterOptions<TestScenario>()
                    {
                        FilterExpression = x => x.Id == entity.Id,
                    });
                    if (scenario.entity == null)
                    {
                        updateResponse = (ExecutionState.Failure, entity, "Invalid id");
                        return updateResponse;
                    }

                    var updateEntity = scenario.entity;
                    updateEntity.Module = entity.Module;
                    updateEntity.SubModule = entity.SubModule;
                    updateEntity.UserType = entity.UserType;
                    updateEntity.SubModule1 = entity.SubModule1;
                    updateEntity.SubModule2 = entity.SubModule2;
                    updateEntity.ScenarioDescription = entity.ScenarioDescription;
                    updateEntity.TC = entity.TC;
                    updateEntity.POC = entity.POC;
                    updateEntity.TaskPriority = entity.TaskPriority;
                    updateEntity.PlannedExecutionDate = entity.PlannedExecutionDate;
                    updateEntity.ActualExecutionDate = entity.ActualExecutionDate;
                    updateEntity.ModifiedBy = entity.ModifiedBy;
                    updateEntity.UpdatedAt = entity.UpdatedAt;

                    (ExecutionState executionState, TestScenario entity, string message) updatedEntity = await UoW.UpdateAsync<TestScenario>(updateEntity);

                    (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                    bool success = saveResponse.executionState == ExecutionState.Success;

                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                        updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                    }
                    else
                    {
                        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(TestScenario).Name} update.");
                }
            }

            return updateResponse;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TestScenario> filterOptions = new FilterOptions<TestScenario>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TestScenario> entity, string message)> List(QueryOptions<TestScenario> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TestScenario> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TestScenario>();
            queryOption.IncludeExpression = x => x.Include(y => y.ProjectRequest!)
            .Include(y => y.TaskOfProject!);
            (ExecutionState executionState, IQueryable<TestScenario> entity, string message) entityObject = await _unitOfWork.List<TestScenario>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
        public override Task<(ExecutionState executionState, TestScenario entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<TestScenario>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!);
            return base.GetAsync(filterOptions);
        }
        public async Task<(ExecutionState executionState, List<ProjectRequest> entity, string message)> GetProjectReqsByProjectType(long projectTypeId)
        {
            try
            {
                var query = _readOnlyContext.Set<ProjectRequest>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();

                //Extra Filter
                if (query != null)
                {
                    query = query.Where(x => x.ProjectType == (ProjectType)projectTypeId);
                }

                query = query?.OrderByDescending(x => x.Id);

                var result = await query
                    .ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<ProjectRequest>()!, "Unexpected error occurred.");
            }
        }
        public async Task<(ExecutionState executionState, List<TaskType> entity, string message)> GetTaskTypesByProjectType(long projectTypeId)
        {
            try
            {
                var query = _readOnlyContext.Set<TaskType>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();

                //Extra Filter
                if (query != null)
                {
                    query = query.Where(x => x.ProjectType == (ProjectType)projectTypeId);
                }

                query = query?.OrderByDescending(x => x.Id);

                var result = await query
                    .ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<TaskType>()!, "Unexpected error occurred.");
            }
        }

        public async Task<(ExecutionState executionState, List<TestScenario> entity, string message)> GetTestScenarioByTaskId(long taskId)
        {
            try
            {
                var result = _readOnlyContext.Set<TestScenario>()
                    .Where(x => x.TaskOfProjectId == taskId)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable().ToList();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<TestScenario>()!, "Unexpected error occurred.");
            }
        }

        public async Task<(ExecutionState executionState, IList<TestScenario> entity, string message)> Search(long? ProjectRequestId, string TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
        {
            var result = await _testScenarioRepository.Search(ProjectRequestId,TestScenarioNo,TaskPriority,CreatedBy,PlannedExecutionDate,ActualExecutionDate);
            return result;
        }
    }
}
