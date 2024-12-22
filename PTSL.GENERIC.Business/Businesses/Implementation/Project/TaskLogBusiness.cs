using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class TaskLogBusiness : BaseBusiness<TaskLog>, ITaskLogBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public TaskLogBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
        }

        //public override async Task<(ExecutionState executionState, TaskLog entity, string message)> CreateAsync(TaskLog entity)
        //{
        //    (ExecutionState executionState, TaskLog entity, string message) createResponse;

        //    await using (IDbContextTransaction transaction = UoW.Begin())
        //    {
        //        try
        //        {
        //            FilterOptions<TaskLog> filterOptions = new FilterOptions<TaskLog>();
        //            filterOptions.FilterExpression = x => x.TaskTitle.Trim() == entity.TaskTitle.Trim();
        //            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
        //            if (entityObject.executionState.ToString() == "Success" || entity.TaskTitle.Trim() == "")
        //            {
        //                createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TaskLog).Name} name already exists with type.");
        //                return createResponse;
        //            }

        //            //Generate Project Request Code By Type
        //            var query = _readOnlyContext.Set<TaskLog>()
        //            .Where(x => x.IsActive && !x.IsDeleted && x.ProjectType == entity.ProjectType)
        //            .OrderByDescending(x => x.Id)
        //            .AsQueryable();
        //            var tasks = await query.ToListAsync();
        //            string taskCode = "";
        //            var taskByType = tasks.Select(x => x.ProjectType == entity.ProjectType).Count();
        //            if (taskByType != 0)
        //            {
        //                var totalCount = taskByType + 1;
        //                if (entity.ProjectType == ProjectType.SoftwareTesting)
        //                    taskCode = "STP" + totalCount.ToString().PadLeft(4, '0');
        //                else if (entity.ProjectType == ProjectType.HardwareTesting)
        //                    taskCode = "HTP" + totalCount.ToString().PadLeft(4, '0');
        //                else if (entity.ProjectType == ProjectType.SecurityTesting)
        //                    taskCode = "SSTP" + totalCount.ToString().PadLeft(4, '0');
        //            }
        //            else
        //            {
        //                if (entity.ProjectType == ProjectType.SoftwareTesting)
        //                    taskCode = "STP0001";
        //                else if (entity.ProjectType == ProjectType.HardwareTesting)
        //                    taskCode = "HTP0001";
        //                else if (entity.ProjectType == ProjectType.SecurityTesting)
        //                    taskCode = "SSTP0001";
        //            }
        //            entity.TaskId = taskCode;



        //            (ExecutionState executionState, TaskLog entity, string message) createdResponse = await UoW.CreateAsync<TaskLog>(entity);

        //            if (createdResponse.executionState == ExecutionState.Failure)
        //            {
        //                if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
        //                {
        //                    UoW.Complete(transaction, CompletionState.Failure);
        //                }

        //                createResponse = createdResponse;
        //            }
        //            else
        //            {
        //                (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

        //                bool success = (saveResponse.executionState == ExecutionState.Success);

        //                #region Post validation
        //                if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
        //                {
        //                    UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

        //                    createResponse = success ? createdResponse :
        //                                (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

        //                }
        //                else
        //                {
        //                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
        //                }
        //                #endregion
        //            }
        //        }
        //        catch
        //        {
        //            if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
        //            {
        //                UoW.Complete(transaction, CompletionState.Failure);
        //            }

        //            createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ProjectRequest).ToString()} creation.");
        //        }
        //    }
        //    //}

        //    return createResponse;
        //}

        //public override async Task<(ExecutionState executionState, TaskLog entity, string message)> UpdateAsync(TaskLog entity)
        //{
        //    (ExecutionState executionState, TaskLog entity, string message) updateResponse;

        //    await using (IDbContextTransaction transaction = UoW.Begin())
        //    {
        //        try
        //        {
        //            FilterOptions<TaskLog> filterOptions = new FilterOptions<TaskLog>();
        //            filterOptions.FilterExpression = x => x.TaskTitle.Trim() == entity.TaskTitle.Trim() && x.Id != entity.Id;
        //            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
        //            if (entityObject.executionState.ToString() == "Success")
        //            {
        //                updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TaskLog).Name} name already exists.");
        //                return updateResponse;
        //            }

        //            (ExecutionState executionState, TaskLog entity, string message) updatedEntity = await UoW.UpdateAsync<TaskLog>(entity);

        //            (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

        //            bool success = saveResponse.executionState == ExecutionState.Success;

        //            if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
        //            {
        //                UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

        //                updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

        //            }
        //            else
        //            {
        //                updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
        //            }
        //        }
        //        catch
        //        {
        //            if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
        //            {
        //                UoW.Complete(transaction, CompletionState.Failure);
        //            }

        //            updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(TaskLog).Name} update.");
        //        }
        //    }

        //    return updateResponse;
        //}


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TaskLog> filterOptions = new FilterOptions<TaskLog>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TaskLog> entity, string message)> List(QueryOptions<TaskLog> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TaskLog> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TaskLog>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!);
            
            (ExecutionState executionState, IQueryable<TaskLog> entity, string message) entityObject = await _unitOfWork.List<TaskLog>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
    }
}
