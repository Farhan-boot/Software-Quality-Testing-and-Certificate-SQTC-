using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
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
using System.Xml.Schema;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class ProjectRequestBusiness : BaseBusiness<ProjectRequest>, IProjectRequestBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly IProjectRequestRepository _projectRequestRepository;
        public ProjectRequestBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, IProjectRequestRepository projectRequestRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _projectRequestRepository = projectRequestRepository;
        }

        public override async Task<(ExecutionState executionState, ProjectRequest entity, string message)> CreateAsync(ProjectRequest entity)
        {
            (ExecutionState executionState, ProjectRequest entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<ProjectRequest> filterOptions = new FilterOptions<ProjectRequest>();
                    filterOptions.FilterExpression = x => x.ProjectName.Trim() == entity.ProjectName.Trim();
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success" || entity.ProjectName.Trim() == "")
                    {
                        createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(ProjectRequest).Name} name already exists with type.");
                        return createResponse;
                    }

                    //Generate Project Request Code By Type
                    var query = _readOnlyContext.Set<ProjectRequest>()
                    .Where(x => x.IsActive && !x.IsDeleted && x.ProjectType == entity.ProjectType)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
                    var projectRequests = await query.ToListAsync();
                    string projectCode = "";
                    var projectRequestByType = projectRequests.Select(x => x.ProjectType == entity.ProjectType).Count();
                    if(projectRequestByType != 0)
                    {
                        var totalCount = projectRequestByType + 1;
                        if (entity.ProjectType == ProjectType.SoftwareTesting)
                            projectCode = "STP" + totalCount.ToString().PadLeft(4, '0');
                        else if(entity.ProjectType == ProjectType.HardwareTesting)
                            projectCode = "HTP" + totalCount.ToString().PadLeft(4, '0');
                        else if (entity.ProjectType == ProjectType.SecurityTesting)
                            projectCode = "SSTP" + totalCount.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        if (entity.ProjectType == ProjectType.SoftwareTesting)
                            projectCode = "STP0001";
                        else if (entity.ProjectType == ProjectType.HardwareTesting)
                            projectCode = "HTP0001";
                        else if (entity.ProjectType == ProjectType.SecurityTesting)
                            projectCode = "SSTP0001";
                    }
                    entity.ProjectCode = projectCode;

                    

                    (ExecutionState executionState, ProjectRequest entity, string message) createdResponse = await UoW.CreateAsync<ProjectRequest>(entity);

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

        public override async Task<(ExecutionState executionState, ProjectRequest entity, string message)> UpdateAsync(ProjectRequest entity)
        {
            (ExecutionState executionState, ProjectRequest entity, string message) updateResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<ProjectRequest> filterOptions = new FilterOptions<ProjectRequest>();
                    filterOptions.FilterExpression = x => x.ProjectName.Trim() == entity.ProjectName.Trim() && x.Id != entity.Id;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        updateResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(ProjectRequest).Name} name already exists.");
                        return updateResponse;
                    }

                    var project = await _unitOfWork.GetAsync(new FilterOptions<ProjectRequest>()
                    {
                        FilterExpression = x => x.Id == entity.Id,
                    });
                    if (project.entity == null)
                    {
                        updateResponse = (ExecutionState.Failure, entity, "Invalid id");
                        return updateResponse;
                    }

                    var updateEntity = project.entity;
                    updateEntity.ProjectName = entity.ProjectName;
                    updateEntity.ProjectType = entity.ProjectType;
                    updateEntity.RequestDate = updateEntity.CreatedAt;
                    updateEntity.ModifiedBy = entity.ModifiedBy;
                    updateEntity.UpdatedAt = entity.UpdatedAt;
					updateEntity.ProjectApprovalStatus = entity.ProjectApprovalStatus;
					updateEntity.ProjectDescription = entity.ProjectDescription;
                    updateEntity.FileName = String.IsNullOrEmpty(entity.FileName) ? updateEntity.FileName : entity.FileName ;
                    updateEntity.FilePath = String.IsNullOrEmpty(entity.FilePath) ? updateEntity.FilePath : entity.FilePath;
                    updateEntity.RejectionComment = entity.RejectionComment;

                    (ExecutionState executionState, ProjectRequest entity, string message) updatedEntity = await UoW.UpdateAsync<ProjectRequest>(updateEntity);

                    if (updatedEntity.executionState == ExecutionState.Updated)
                    {
                        ProjectRquestLog projectRquestLog = new ProjectRquestLog();
                        projectRquestLog.ProjectRequestId = updateEntity.Id;
                        projectRquestLog.Description = updateEntity.ProjectApprovalStatus == ProjectApprovalStatus.Accept ? "Project Approved" : updateEntity.ProjectApprovalStatus == ProjectApprovalStatus.Pending ? "Project Pending" : "Project Rejected";
                        projectRquestLog.CreatedBy = updateEntity.ModifiedBy.Value;
                        projectRquestLog.CreatedAt = updateEntity.UpdatedAt.Value;
                        projectRquestLog.IsActive = true;

                        (ExecutionState executionState, ProjectRquestLog entity, string message) projectLog = await UoW.CreateAsync<ProjectRquestLog>(projectRquestLog);

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

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(ProjectRequest).Name} update.");
                }
            }

            return updateResponse;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<ProjectRequest> filterOptions = new FilterOptions<ProjectRequest>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<ProjectRequest> entity, string message)> List(QueryOptions<ProjectRequest> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<ProjectRequest> entity, string message) returnResponse;
            var queryOption = new QueryOptions<ProjectRequest>();
            queryOption.IncludeExpression = x => x.Include(y => y.Client!);
            (ExecutionState executionState, IQueryable<ProjectRequest> entity, string message) entityObject = await _unitOfWork.List<ProjectRequest>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate)
        {
            var result = await _projectRequestRepository.Search(ProjectName, ProjectType, ProjectCode, ClientId, RequestDate);
            return result;
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectPendingList()
        {
            var result = await _readOnlyContext.Set<ProjectRequest>()
                .Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Pending)
                .Include(x=>x.Client!)
                .OrderByDescending(x=>x.Id)
                .ToListAsync();
            return (ExecutionState.Success, result,"Pending Project Item Found");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectRejectedList()
        {
            var result = await _readOnlyContext.Set<ProjectRequest>()
                .Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Reject)
                .Include(x => x.Client!)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return (ExecutionState.Success, result, "Rejected Project Item Found");
        }

        public override Task<(ExecutionState executionState, ProjectRequest entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<ProjectRequest>();
            filterOptions.FilterExpression = x=>x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.Client!)
            .Include(x=>x.projectStateLogList!)
            .Include(y=>y.documentsByTypeList)
            .Include(y=>y.MeetingList).ThenInclude(z=>z.MeetingFiles);
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectListByClientId(long clientId)
        {
            var result = await _readOnlyContext.Set<ProjectRequest>()
                .Where(x=>x.ClientId == clientId)
                .Include(x => x.Client!)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return (ExecutionState.Success, result, "Client Project Item Found");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequest> entity, string message)> GetProjectAcceptedList()
        {
            var result = await _readOnlyContext.Set<ProjectRequest>()
                .Where(x => x.ProjectApprovalStatus == ProjectApprovalStatus.Accept)
                .Include(x => x.Client!)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return (ExecutionState.Success, result, "Accepted Project Item Found");
        }
    }
}
