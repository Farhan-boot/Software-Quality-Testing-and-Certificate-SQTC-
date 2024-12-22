using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;
using System;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using Microsoft.EntityFrameworkCore;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class ApprovalForAllDocumentBusiness : BaseBusiness<ApprovalForAllDocument>, IApprovalForAllDocumentBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public ApprovalForAllDocumentBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async override Task<(ExecutionState executionState, IQueryable<ApprovalForAllDocument> entity, string message)> List(QueryOptions<ApprovalForAllDocument> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<ApprovalForAllDocument> entity, string message) returnResponse;
            var queryOption = new QueryOptions<ApprovalForAllDocument>();
            queryOption.IncludeExpression = x => x.Include(x => x.AllTypesOfDocument!);


            (ExecutionState executionState, IQueryable<ApprovalForAllDocument> entity, string message) entityObject = await _unitOfWork.List<ApprovalForAllDocument>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
        public override async Task<(ExecutionState executionState, ApprovalForAllDocument entity, string message)> CreateAsync(ApprovalForAllDocument entity)
        {
            (ExecutionState executionState, ApprovalForAllDocument entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, IQueryable<ApprovalForAllDocument> entity, string message) entityObject = await base.List();
                    var anyApprovalItem = entityObject.entity is null ? false : entityObject.entity.Where(x => x.AllTypesOfDocumentId == entity.AllTypesOfDocumentId).Any();

                    var getDocItem = await _unitOfWork.GetAsync(new FilterOptions<AllTypesOfDocument>()
                    {
                        FilterExpression = x => x.Id == entity.AllTypesOfDocumentId,
                    });

                    if(getDocItem.entity.DocumentAmendmentState == Common.Enum.Documents.DocumentAmendmentState.Amendmented)
                        entity.IsAmmendment = true;

                    if (!anyApprovalItem)
                        entity.StatusForPdf = Common.Enum.Documents.ApprovalStatusForPdf.Generated;

                    
                    

                    (ExecutionState executionState, ApprovalForAllDocument entity, string message) createdResponse = await UoW.CreateAsync<ApprovalForAllDocument>(entity);

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

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ApprovalForAllDocument).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }

    }
}
