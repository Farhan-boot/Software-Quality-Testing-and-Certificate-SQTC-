using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestings;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.SecurityTestings
{
    public class SecurityTestingBusiness : BaseBusiness<SecurityTesting>, ISecurityTestingBusiness
    {
        private readonly ISecurityTestingRepository _repository;
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public SecurityTestingBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,ISecurityTestingRepository SecurityTestingRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _repository = SecurityTestingRepository;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<SecurityTesting> filterOptions = new FilterOptions<SecurityTesting>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<SecurityTesting> entity, string message)> List(QueryOptions<SecurityTesting> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<SecurityTesting> entity, string message) returnResponse;
            var queryOption = new QueryOptions<SecurityTesting>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!);
            
            (ExecutionState executionState, IQueryable<SecurityTesting> entity, string message) entityObject = await _unitOfWork.List<SecurityTesting>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        

        public async Task<(ExecutionState executionState, IList<SecurityTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
        {
            var result = await _repository.Search(ProjectRequestId, TaskOfProjectId,Vulnerability,SeverityLevel,EaseOfExploitation);
            return result;
        }

        public async override Task<(ExecutionState executionState, SecurityTesting entity, string message)> GetAsync(long key)
        {

            FilterOptions<SecurityTesting> filterOptions = new FilterOptions<SecurityTesting>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!).Include(x=>x.SecurityTestingFiles);
            var entityObject = await base.GetAsync(filterOptions);
            return entityObject;
            
        }
    }
}
