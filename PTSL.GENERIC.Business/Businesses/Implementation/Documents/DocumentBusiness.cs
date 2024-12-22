using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using PTSL.GENERIC.Common.Enum;
using Microsoft.EntityFrameworkCore;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Documents
{
    public class DocumentBusiness : BaseBusiness<DocumentsByType>, IDocumentsBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly IDocumentsRepository _documentRepository;
        public DocumentBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, IDocumentsRepository documentRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _documentRepository = documentRepository;
        }

        public override async Task<(ExecutionState executionState, DocumentsByType entity, string message)> CreateAsync(DocumentsByType entity)
        {
            (ExecutionState executionState, DocumentsByType entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    //FilterOptions<DocumentsByType> filterOptions = new FilterOptions<DocumentsByType>();
                    //filterOptions.FilterExpression = x => x.ProjectName.Trim() == entity.ProjectName.Trim();
                    //(ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    //if (entityObject.executionState.ToString() == "Success" || entity.ProjectName.Trim() == "")
                    //{
                    //    createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(DocumentsByType).Name} name already exists with type.");
                    //    return createResponse;
                    //}

                    //Generate Project Request Code By Type
                    


                    (ExecutionState executionState, DocumentsByType entity, string message) createdResponse = await UoW.CreateAsync<DocumentsByType>(entity);

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

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(DocumentsByType).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }
        public async Task<(ExecutionState executionState, DocumentsByType entity, string message)> CreateProjectDocumentListAsync(List<DocumentsByType> entityList)
        {
            (ExecutionState executionState, DocumentsByType entity, string message) createResponse = (executionState: ExecutionState.Activated, entity: null, message: $"");

            //await using (IDbContextTransaction transaction = UoW.Begin())
            //{
            try
            {
                //var query = _readOnlyContext.Set<DocumentsByType>()
                //    .Where(x => x.IsActive && !x.IsDeleted)
                //    .OrderByDescending(x => x.Id)
                //    .AsQueryable();
                //var tasks = await query.ToListAsync();
                //var totalScenario = tasks.Count();
                //totalScenario++;
                foreach (DocumentsByType document in entityList)
                {
                    //FilterOptions<DocumentsByType> filterOptions = new FilterOptions<DocumentsByType>();
                    //filterOptions.FilterExpression = x => x.Module.Trim() == testScenario.Module.Trim() && x.UserType.Trim() == testScenario.UserType.Trim() && x.ProjectRequestId == testScenario.ProjectRequestId && x.TaskOfProjectId == testScenario.TaskOfProjectId;
                    //(ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    //if (entityObject.executionState.ToString() == "Success")
                    //{
                    //    createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(TestScenario).Name} already exists.");
                    //    return createResponse;
                    //}
                    //string scenarioCode = "";
                    //if (totalScenario != 0)
                    //{
                    //    scenarioCode = "TS-" + totalScenario.ToString().PadLeft(4, '0');
                    //}
                    //else
                    //    scenarioCode = "TS-0001";

                    //testScenario.TestScenarioNo = scenarioCode;

                    (ExecutionState executionState, DocumentsByType entity, string message) createdResponse = await base.CreateAsync(document);
                    createResponse = (createdResponse.executionState, createdResponse.entity, createdResponse.message);

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
        public async override Task<(ExecutionState executionState, IQueryable<DocumentsByType> entity, string message)> List(QueryOptions<DocumentsByType> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<DocumentsByType> entity, string message) returnResponse;
            var queryOption = new QueryOptions<DocumentsByType>();
            queryOption.IncludeExpression = x => x.Include(y => y.ProjectRequest!)
            .Include(y => y.DocumentCategories!);
            (ExecutionState executionState, IQueryable<DocumentsByType> entity, string message) entityObject = await _unitOfWork.List<DocumentsByType>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async override Task<(ExecutionState executionState, DocumentsByType entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<DocumentsByType>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.ProjectRequest!)
            .Include(y => y.DocumentCategories!);
            return await base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle)
        {
            var result = await _documentRepository.Search(ProjectRequestId,DocumentCategoriesId,DocumentTitle);
            return result;
        }

        public async Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> DocumentsListByClientId(long clientId)
        {
            var result = await _readOnlyContext.Set<DocumentsByType>()
                .Where(x => x.ClientId == clientId).Include(y => y.ProjectRequest!)
                .Include(y => y.DocumentCategories!)
                .ToListAsync();

            return (ExecutionState.Retrieved, result, "Data Found");
        }
    }
}
