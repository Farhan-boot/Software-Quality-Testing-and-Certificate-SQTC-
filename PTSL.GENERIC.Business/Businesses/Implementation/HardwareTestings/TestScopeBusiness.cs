using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.HardwareTestings
{
    public class TestScopeBusiness : BaseBusiness<TestScope>, ITestScopeBusiness
    {
        private readonly ITestScopeRepository _repository;
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public TestScopeBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,ITestScopeRepository TestScopeRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _repository = TestScopeRepository;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TestScope> filterOptions = new FilterOptions<TestScope>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TestScope> entity, string message)> List(QueryOptions<TestScope> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TestScope> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TestScope>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!);
            
            (ExecutionState executionState, IQueryable<TestScope> entity, string message) entityObject = await _unitOfWork.List<TestScope>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, TestScope entity, string message)> CreateListOfTestScope(List<TestScope> entityList)
        {
            (ExecutionState executionState,TestScope entity, string message) createResponse = (executionState: ExecutionState.Created, entity: null, message: $"List Of Item Created");
            var query = _readOnlyContext.Set<TestScope>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
            var tasks = await query.ToListAsync();
            var totalCase = tasks.Count();
            totalCase++; ;
            foreach (TestScope TestScope in entityList)
            {

                string TestScopeNo = "";
                if (totalCase != 0)
                {
                    TestScopeNo = "TC-" + totalCase.ToString().PadLeft(4, '0');

                }
                else

                    TestScopeNo = "TC-0001";

                TestScope.SerialNo = TestScopeNo;


                (ExecutionState executionState, TestScope entity, string message) createdResponse = await base.CreateAsync(TestScope);
                totalCase++;
            }
            return createResponse;

        }

        public async Task<(ExecutionState executionState, IList<TestScope> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? TestItem,string? TenderID,string? SerialNo)
        {
            var result = await _repository.Search(ProjectRequestId, TaskOfProjectId, TestItem,TenderID,SerialNo);
            return result;
        }

        public async override Task<(ExecutionState executionState, TestScope entity, string message)> GetAsync(long key)
        {
            FilterOptions<TestScope> filterOptions = new FilterOptions<TestScope>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!);
            var entityObject = await base.GetAsync(filterOptions);
            return entityObject;
        }
    }
}
