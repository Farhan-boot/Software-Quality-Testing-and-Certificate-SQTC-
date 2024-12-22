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
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class ProjectRequestLogBusiness : BaseBusiness<ProjectRquestLog>, IProjectRequestLogBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public ProjectRequestLogBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
        }

        public async override Task<(ExecutionState executionState, IQueryable<ProjectRquestLog> entity, string message)> List(QueryOptions<ProjectRquestLog> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<ProjectRquestLog> entity, string message) returnResponse;
            var queryOption = new QueryOptions<ProjectRquestLog>();
            queryOption.IncludeExpression = x => x.Include(y => y.ProjectRequest!);
            (ExecutionState executionState, IQueryable<ProjectRquestLog> entity, string message) entityObject = await _unitOfWork.List<ProjectRquestLog>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
    }
}
