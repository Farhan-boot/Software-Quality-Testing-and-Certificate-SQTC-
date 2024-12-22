using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class BugAndDefectBusiness : BaseBusiness<BugAndDefect>, IBugAndDefectBusiness
    {
        private readonly IBugAndDefectRepository _repository;
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly IBugAndDefectLogBusiness _bugAndDefectLogBusiness;
        public BugAndDefectBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, IBugAndDefectRepository BugAndDefectRepository, IBugAndDefectLogBusiness bugAndDefectLogBusiness)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _repository = BugAndDefectRepository;
            _bugAndDefectLogBusiness = bugAndDefectLogBusiness;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<BugAndDefect> filterOptions = new FilterOptions<BugAndDefect>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<BugAndDefect> entity, string message)> List(QueryOptions<BugAndDefect> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<BugAndDefect> entity, string message) returnResponse;
            var queryOption = new QueryOptions<BugAndDefect>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!)
            .Include(x => x.TestCase!)
            .Include(x => x.User!);

            (ExecutionState executionState, IQueryable<BugAndDefect> entity, string message) entityObject = await _unitOfWork.List<BugAndDefect>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public override Task<(ExecutionState executionState, BugAndDefect entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<BugAndDefect>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!)
            .Include(x => x.TestCase!)
            .Include(x => x.BugAndDefectFiles!);
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, BugAndDefect entity, string message)> CreateListOfBugAndDefect(List<BugAndDefect> entityList)
        {
            (ExecutionState executionState, BugAndDefect entity, string message) createResponse = (executionState: ExecutionState.Activated, entity: null, message: $"");
            var query = _readOnlyContext.Set<BugAndDefect>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
            var tasks = await query.ToListAsync();
            var totalCase = tasks.Count();
            totalCase++;
            foreach (BugAndDefect testCase in entityList)
            {

                string TestCaseNo = "";
                if (totalCase != 0)
                {
                    TestCaseNo = "D-" + totalCase.ToString().PadLeft(4, '0');

                }
                else

                    TestCaseNo = "D-0001";

                testCase.DefectId = TestCaseNo;


                (ExecutionState executionState, BugAndDefect entity, string message) createdResponse = await base.CreateAsync(testCase);
                totalCase++;
            }
            return createResponse;

        }

        public async Task<(ExecutionState executionState, IList<BugAndDefect> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            var result = await _repository.Search(ProjectRequestId, TaskOfProjectId, TestCaseId, bugzillaId, defectId, bugAndDefectStatus, bugAndDefectSeverity);
            return result;
        }

        public async override Task<(ExecutionState executionState, BugAndDefect entity, string message)> CreateAsync(BugAndDefect entity)
        {
            (ExecutionState executionState, BugAndDefect entity, string message) createResponse;
            var query = _readOnlyContext.Set<BugAndDefect>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
            var tasks = await query.ToListAsync();
            var totalCase = tasks.Count();
            totalCase++;
            string DefectID = "";
            if (totalCase != 0)
            {
                DefectID = "D-" + totalCase.ToString().PadLeft(4, '0');

            }
            else
            {
                DefectID = "D-0001";

            }

            entity.DefectId = DefectID;
            (ExecutionState executionState, BugAndDefect entity, string message) createdResponse = await base.CreateAsync(entity);

            return createdResponse;
        }

        public async Task<(ExecutionState executionState, BugAndDefect entity, string message)> UpdateBugListOnBugZilla(List<BugAndDefect> entityList)
        {
            (ExecutionState executionState, BugAndDefect entity, string message) updateResponse = (ExecutionState.Activated, null, ""); ;

            try
            {
                List<BugAndDefect> bugAndDefects = new List<BugAndDefect>();
                foreach (var entity in entityList)
                {
                    FilterOptions<BugAndDefect> filterOptions = new FilterOptions<BugAndDefect>();
                    filterOptions.FilterExpression = x => x.BugzillaId.Trim() == entity.BugzillaId.Trim() && x.Id != entity.Id;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(BugAndDefect).Name} name already exists.");
                        return updateResponse;
                    }

                    var project = await _unitOfWork.GetAsync(new FilterOptions<BugAndDefect>()
                    {
                        FilterExpression = x => x.Id == entity.Id,
                    });
                    if (project.entity == null)
                    {
                        updateResponse = (ExecutionState.Failure, entity, "Invalid id");
                        return updateResponse;
                    }

                    var updateEntity = project.entity;
                    //updateEntity.Id = entity.Id;
                    updateEntity.ModifiedBy = entity.ModifiedBy;
                    updateEntity.UpdatedAt = entity.UpdatedAt;
                    updateEntity.BugAndDefectStatus = entity.BugAndDefectStatus;
                    bugAndDefects.Add(updateEntity);
                    (ExecutionState executionState, BugAndDefect entity, string message) updatedEntity = await base.UpdateAsync(updateEntity);

                    //(ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync();
                    if (updatedEntity.executionState == ExecutionState.Updated)
                        updateResponse = updatedEntity;

                    if (updatedEntity.executionState == ExecutionState.Updated)
                    {
                        BugAndDefectLog bugLog = new BugAndDefectLog();
                        bugLog.BugAndDefectId = updateEntity.Id;
                        bugLog.LogRemarks = updateEntity.BugAndDefectStatus == BugAndDefectStatus.Resolved ? "Bug Resolved" : "Bug Resolved failed.";
                        bugLog.CreatedBy = updateEntity.ModifiedBy.Value;
                        bugLog.CreatedAt = updateEntity.UpdatedAt.Value;
                        bugLog.IsActive = true;

                        (ExecutionState executionState, BugAndDefectLog entity, string message) projectLog = await _bugAndDefectLogBusiness.CreateAsync(bugLog);

                    }
                }


                //(ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                //bool success = saveResponse.executionState == ExecutionState.Success;
                //if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                //{
                //    UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                //    updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                //}
                //else
                //{
                //    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                //}
            }
            catch
            {
                //if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                //{
                //    UoW.Complete(transaction, CompletionState.Failure);
                //}

                //updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(BugAndDefectLog).Name} update.");
            }

            return updateResponse;
        }

        public override async Task<(ExecutionState executionState, BugAndDefect entity, string message)> UpdateAsync(BugAndDefect entity)
        {
            (ExecutionState executionState, BugAndDefect entity, string message) updateResponse;


            var newentity = await base.GetAsync(entity.Id);

            var dbEntityToUpdate = newentity.entity;
            //updateEntity.Id = entity.Id;
            dbEntityToUpdate.ModifiedBy = entity.ModifiedBy;
            dbEntityToUpdate.UpdatedAt = entity.UpdatedAt;
            dbEntityToUpdate.BugAndDefectStatus = entity.BugAndDefectStatus;
            dbEntityToUpdate.BugzillaId = entity.BugzillaId;

            (ExecutionState executionState, BugAndDefect entity, string message) updatedEntity = await base.UpdateAsync(dbEntityToUpdate);

            return updatedEntity;


            //await using (IDbContextTransaction transaction = UoW.Begin())
            //{
            //    try
            //    {
            //        //FilterOptions<BugAndDefect> filterOptions = new FilterOptions<BugAndDefect>();
            //        //filterOptions.FilterExpression = x => x.BugzillaId.Trim() == entity.BugzillaId.Trim() && x.Id != entity.Id;
            //        //(ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            //        //if (entityObject.executionState.ToString() == "Success")
            //        //{
            //        //    updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(BugAndDefect).Name} name already exists.");
            //        //    return updateResponse;
            //        //}

            //        var bugItem = await _unitOfWork.GetAsync(new FilterOptions<BugAndDefect>()
            //        {
            //            FilterExpression = x => x.Id == entity.Id,
            //        });
            //        if (bugItem.entity == null)
            //        {
            //            updateResponse = (ExecutionState.Failure, entity, "Invalid id");
            //            return updateResponse;
            //        }



            //        (ExecutionState executionState, BugAndDefect entity, string message) updatedEntity = await UoW.UpdateAsync<BugAndDefect>(dbEntityToUpdate);

            //        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

            //        bool success = saveResponse.executionState == ExecutionState.Success;

            //        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
            //        {
            //            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

            //            updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

            //        }
            //        else
            //        {
            //            updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
            //        }
            //    }
            //    catch
            //    {
            //        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
            //        {
            //            UoW.Complete(transaction, CompletionState.Failure);
            //        }

            //        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(BugAndDefect).Name} update.");
            //    }
            //}


        }
    }
}
