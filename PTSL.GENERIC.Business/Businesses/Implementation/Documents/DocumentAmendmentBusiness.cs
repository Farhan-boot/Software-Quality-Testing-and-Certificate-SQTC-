using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Documents
{
    public class DocumentAmendmentBusiness : BaseBusiness<DocumentAmendment>, IDocumentAmendmentBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly IProjectCertificationBusiness _projectCertificationBusiness;
        public DocumentAmendmentBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, IProjectCertificationBusiness projectCertificationBusiness)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _projectCertificationBusiness = projectCertificationBusiness;
        }

        public override async Task<(ExecutionState executionState, DocumentAmendment entity, string message)> CreateAsync(DocumentAmendment entity)
        {
            (ExecutionState executionState, DocumentAmendment entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, IQueryable<ProjectCertification> entity, string message) entityObject = await _projectCertificationBusiness.List();
                    var getCertificateByProject = entityObject.entity.Where(s => s.AllTypesOfDocumentId == entity.AllTypesOfDocumentId).FirstOrDefault();
                    if (getCertificateByProject is not null)
                    {
                        getCertificateByProject.CertificationStatus = CertificationStatus.AmendmentFromClient;
                        var executionState = await UoW.UpdateAsync<ProjectCertification>(getCertificateByProject);
                    }

                    (ExecutionState executionState, DocumentAmendment entity, string message) createdResponse = await UoW.CreateAsync<DocumentAmendment>(entity);

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

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(AllTypesOfDocument).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }
        public async Task<(ExecutionState executionState, DocumentAmendment entity, string message)> CreateDocAmendment(DocumentAmendment entity)
        {
            (ExecutionState executionState, DocumentAmendment entity, string message) createResponse = new(ExecutionState.Activated, new DocumentAmendment(), "");

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    var allTypeDocument = await _unitOfWork.GetAsync(new FilterOptions<AllTypesOfDocument>()
                    {
                        FilterExpression = x => x.Id == entity.AllTypesOfDocumentId,
                    });

                    if(allTypeDocument.entity is not null)
                    {
                        allTypeDocument.entity.DocumentApprovalStatus = Common.Enum.Documents.DocumentApprovalStatus.Pending;
                        allTypeDocument.entity.DocumentAmendmentState = Common.Enum.Documents.DocumentAmendmentState.Amendmented;
                        (ExecutionState executionState, AllTypesOfDocument entity, string message) updateDocResponse = await UoW.UpdateAsync<AllTypesOfDocument>(allTypeDocument.entity);
                        if(updateDocResponse.executionState == ExecutionState.Updated)
                        {
                            (ExecutionState executionState, DocumentAmendment entity, string message) createdResponse = await UoW.CreateAsync<DocumentAmendment>(entity);

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
                    }


                    
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(AllTypesOfDocument).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }
        public async Task<(ExecutionState executionState, DocumentAmendment entity, string message)> GetDocAmendmentByDocId(long documentId)
        {
            var result = await _readOnlyContext.Set<DocumentAmendment>()
                .Where(x => x.AllTypesOfDocumentId == documentId)
                .Include(x => x.AllTypesOfDocument!)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            return (ExecutionState.Success, result ?? new DocumentAmendment(), "Amendment Item Found");
        }

    }
}
