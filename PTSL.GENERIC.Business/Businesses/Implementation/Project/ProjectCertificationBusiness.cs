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
    public class ProjectCertificationBusiness : BaseBusiness<ProjectCertification>, IProjectCertificationBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public ProjectCertificationBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
        }

        public async override Task<(ExecutionState executionState, IQueryable<ProjectCertification> entity, string message)> List(QueryOptions<ProjectCertification> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<ProjectCertification> entity, string message) returnResponse;
            var queryOption = new QueryOptions<ProjectCertification>();
            queryOption.IncludeExpression = x => x.Include(x => x.AllTypesOfDocument!).ThenInclude(x=>x.ProjectRequest!);
            

            (ExecutionState executionState, IQueryable<ProjectCertification> entity, string message) entityObject = await _unitOfWork.List<ProjectCertification>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
        public override async Task<(ExecutionState executionState, ProjectCertification entity, string message)> CreateAsync(ProjectCertification entity)
        {
            (ExecutionState executionState, ProjectCertification entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    //(ExecutionState executionState, IQueryable<ProjectCertification> entity, string message) entityObject = await base.List();
                    //var lastDocument = entityObject.entity?.Where(x => x.ProjectRequestId == entity.ProjectRequestId && x.DocumentType == entity.DocumentType && x.TestingType == entity.TestingType).OrderBy(s=>s.Id).LastOrDefault() ?? new ProjectCertification();
                    //string versionNo = "Version 1";
                    //if (lastDocument is not null)
                    //{
                    //    versionNo = String.IsNullOrEmpty(lastDocument.VersionNo) ? "Version 1" : "Version " + (Convert.ToInt32(lastDocument.VersionNo.Split(' ')[1]) + 1).ToString();
                    //}
                    //entity.VersionNo = versionNo;
                    //Generate Project Request Code By Type



                    (ExecutionState executionState, ProjectCertification entity, string message) createdResponse = await UoW.CreateAsync<ProjectCertification>(entity);

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

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ProjectCertification).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }
        public override async Task<(ExecutionState executionState, ProjectCertification entity, string message)> UpdateAsync(ProjectCertification entity)
        {
            (ExecutionState executionState, ProjectCertification entity, string message) updateResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    
                    var dbDoc = await _unitOfWork.GetAsync(new FilterOptions<ProjectCertification>()
                    {
                        FilterExpression = x => x.Id == entity.Id,
                    });
                    if (dbDoc.entity == null)
                    {
                        updateResponse = (ExecutionState.Failure, entity, "Invalid id");
                        return updateResponse;
                    }

                    var updateEntity = dbDoc.entity;
                    if (updateEntity != null) { 
                        updateEntity.CertificationStatus = entity.CertificationStatus;
                        updateEntity.CertificateContent = entity.CertificateContent;
                    }
                    if(entity.CertificationStatus == CertificationStatus.ApprovedByClient || entity.CertificationStatus == CertificationStatus.ApprovedByAdmin)
                    {
                        var project = await _unitOfWork.GetAsync(new FilterOptions<ProjectRequest>()
                        {
                            FilterExpression = x => x.Id == entity.AllTypesOfDocument.ProjectRequestId,
                        });

                        if(project.entity != null)
                        {
                            project.entity.ProjectApprovalStatus = ProjectApprovalStatus.Completed;
                            project.entity.UpdatedAt = entity.UpdatedAt;
                            project.entity.ModifiedBy = entity.ModifiedBy;

                            var updateProject = await UoW.UpdateAsync<ProjectRequest>(project.entity);
                        }
                    }

                    (ExecutionState executionState, ProjectCertification entity, string message) updatedEntity = await UoW.UpdateAsync<ProjectCertification>(updateEntity);

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

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ProjectCertification).Name} update.");
                }
            }

            return updateResponse;
        }

    }
}
