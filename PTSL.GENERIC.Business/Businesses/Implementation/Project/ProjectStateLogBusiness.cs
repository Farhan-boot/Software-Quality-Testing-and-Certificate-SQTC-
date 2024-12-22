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

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class ProjectStateLogBusiness : BaseBusiness<ProjectStateLog>, IProjectStateLogBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly IProjectStateLogRepository _projectStateLogRepository;
        public ProjectStateLogBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext, IProjectStateLogRepository projectStateLogRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _projectStateLogRepository = projectStateLogRepository;
        }

        public async Task<(ExecutionState executionState, ProjectStateLog entity, string message)> GetLogData(long projectRequestId, long projectStateEnumId)
        {
            var result = await _projectStateLogRepository.GetLogData(projectRequestId, projectStateEnumId);
            return result;
        }
    }
}
