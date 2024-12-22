using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class TaskBusiness : BaseBusiness<TaskOfProject>, ITaskBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly ITaskRepository _taskRepository;
        public TaskBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,ITaskRepository taskRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _taskRepository = taskRepository;
        }

        public override async Task<(ExecutionState executionState, TaskOfProject entity, string message)> CreateAsync(TaskOfProject entity)
        {
            (ExecutionState executionState, TaskOfProject entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<TaskOfProject> filterOptions = new FilterOptions<TaskOfProject>();
                    filterOptions.FilterExpression = x => x.TaskTitle.Trim() == entity.TaskTitle.Trim();
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success" || entity.TaskTitle.Trim() == "")
                    {
                        createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TaskOfProject).Name} name already exists with type.");
                        return createResponse;
                    }

                    //Generate Project Request Code By Type
                    var query = _readOnlyContext.Set<TaskOfProject>()
                    .Where(x => x.IsActive && !x.IsDeleted && x.ProjectType == entity.ProjectType)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
                    var tasks = await query.ToListAsync();
                    string taskCode = "";
                    var taskByType = tasks.Select(x => x.ProjectType == entity.ProjectType).Count();
                    if (taskByType != 0)
                    {
                        var totalCount = taskByType + 1;
                        if (entity.ProjectType == ProjectType.SoftwareTesting)
                            taskCode = "TSTP" + totalCount.ToString().PadLeft(4, '0');
                        else if (entity.ProjectType == ProjectType.HardwareTesting)
                            taskCode = "THTP" + totalCount.ToString().PadLeft(4, '0');
                        else if (entity.ProjectType == ProjectType.SecurityTesting)
                            taskCode = "TSSTP" + totalCount.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        if (entity.ProjectType == ProjectType.SoftwareTesting)
                            taskCode = "TSTP0001";
                        else if (entity.ProjectType == ProjectType.HardwareTesting)
                            taskCode = "THTP0001";
                        else if (entity.ProjectType == ProjectType.SecurityTesting)
                            taskCode = "TSSTP0001";
                    }
                    entity.TaskId = taskCode;



                    (ExecutionState executionState, TaskOfProject entity, string message) createdResponse = await UoW.CreateAsync<TaskOfProject>(entity);

                    if (createdResponse.executionState == ExecutionState.Failure)
                    {
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
                        {
                            UoW.Complete(transaction, CompletionState.Failure);
                        }

                        createResponse = createdResponse;
                    }
                    else
                    {
                        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                        bool success = (saveResponse.executionState == ExecutionState.Success);

                        #region Post validation
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                        {
                            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                            createResponse = success ? createdResponse :
                                        (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        }
                        else
                        {
                            createResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ProjectRequest).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }

        public override async Task<(ExecutionState executionState, TaskOfProject entity, string message)> UpdateAsync(TaskOfProject entity)
        {
            (ExecutionState executionState, TaskOfProject entity, string message) updateResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<TaskOfProject> filterOptions = new FilterOptions<TaskOfProject>();
                    filterOptions.FilterExpression = x => x.TaskTitle.Trim() == entity.TaskTitle.Trim() && x.Id != entity.Id;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TaskOfProject).Name} name already exists.");
                        return updateResponse;
                    }
                    var task = await _unitOfWork.GetAsync(new FilterOptions<TaskOfProject>()
                    {
                        FilterExpression = x => x.Id == entity.Id,
                    });
                    if (task.entity == null)
                    {
                        updateResponse = (ExecutionState.Failure, entity, "Invalid id");
                        return updateResponse;
                    }

                    var updateEntity = task.entity;
                    updateEntity.TaskTitle = entity.TaskTitle;
                    updateEntity.TaskOfProjectStatus = entity.TaskOfProjectStatus;
                    //updateEntity.ProjectType = entity.ProjectType;
                    updateEntity.TaskDeadline = updateEntity.TaskDeadline;
                    updateEntity.TaskEstimationHour = updateEntity.TaskEstimationHour;
                    updateEntity.TaskPriority = updateEntity.TaskPriority;
                    updateEntity.ModifiedBy = entity.ModifiedBy;
                    updateEntity.UpdatedAt = entity.UpdatedAt;
                    updateEntity.TaskDescription = entity.TaskDescription;
                    updateEntity.TaskFileName = String.IsNullOrEmpty(entity.TaskFileName) ? updateEntity.TaskFileName : entity.TaskFileName;
                    updateEntity.TaskFilePath = String.IsNullOrEmpty(entity.TaskFilePath) ? updateEntity.TaskFilePath : entity.TaskFilePath;
                    (ExecutionState executionState, TaskOfProject entity, string message) updatedEntity = await UoW.UpdateAsync<TaskOfProject>(updateEntity);
                    if (updatedEntity.executionState == ExecutionState.Updated)
                    {
                        TaskLog taskLog = new TaskLog();
                        taskLog.TaskOfProjectId = updateEntity.Id;
                        taskLog.Description = "task updated";
                        taskLog.CreatedBy = updateEntity.ModifiedBy.Value;
                        taskLog.CreatedAt = updateEntity.UpdatedAt.Value;
                        taskLog.IsActive = true;

                        (ExecutionState executionState, TaskLog entity, string message) projectLog = await UoW.CreateAsync<TaskLog>(taskLog);

                    }
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

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(TaskOfProject).Name} update.");
                }
            }

            return updateResponse;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TaskOfProject> filterOptions = new FilterOptions<TaskOfProject>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TaskOfProject> entity, string message)> List(QueryOptions<TaskOfProject> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TaskOfProject> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TaskOfProject>();
            queryOption.IncludeExpression = x => x.Include(y => y.ProjectRequest!)
            .Include(y => y.TaskType!)
            .Include(y => y.User!);
            (ExecutionState executionState, IQueryable<TaskOfProject> entity, string message) entityObject = await _unitOfWork.List<TaskOfProject>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
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

        public async Task<(ExecutionState executionState, List<TaskOfProject> entity, string message)> GetTaskListByUserId(long userId)
        {
            var result = await _readOnlyContext.Set<TaskOfProject>().Where(x => x.UserId == userId)
                .Include(x=>x.ProjectRequest)
                .Include(x=>x.TaskType).Include(x=>x.User)
                .ToListAsync();

            return (ExecutionState.Success, result, "Ok");
        }
        public async Task<(ExecutionState executionState, List<TaskOfProject> entity, string message)> GetTasksByProject(long projectId)
        {
            try
            {
                var query = _readOnlyContext.Set<TaskOfProject>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();

                //Extra Filter
                if (query != null)
                {
                    query = query.Where(x => x.ProjectRequestId == projectId);
                }

                query = query?.OrderByDescending(x => x.Id);

                var result = await query
                    .ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<TaskOfProject>()!, "Unexpected error occurred.");
            }
        }

        public async override Task<(ExecutionState executionState, TaskOfProject entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<TaskOfProject>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y=>y.ProjectRequest!)
            .Include(y => y.TaskType!)
            .Include(y => y.User!);
            return await base.GetAsync(filterOptions);
        }

        public async  Task<(ExecutionState executionState, IList<TaskOfProject> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline)
        {
            var result = await _taskRepository.Search(ProjectRequestId,TaskId,AssigneeId,CreateDate,Deadline);
            return result;
        }
    }
}
