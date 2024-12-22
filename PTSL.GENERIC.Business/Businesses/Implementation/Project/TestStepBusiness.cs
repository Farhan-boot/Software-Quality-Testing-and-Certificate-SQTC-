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
    public class TestStepBusiness : BaseBusiness<TestStep>, ITestStepBusiness
    {
        private readonly ITestStepRepository _repository;
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public TestStepBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,ITestStepRepository testStepRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _repository = testStepRepository;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TestStep> filterOptions = new FilterOptions<TestStep>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TestStep> entity, string message)> List(QueryOptions<TestStep> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TestStep> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TestStep>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x=>x.ProjectRequest!)
            .Include(x=>x.TestCase!);
            
            (ExecutionState executionState, IQueryable<TestStep> entity, string message) entityObject = await _unitOfWork.List<TestStep>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, TestStep entity, string message)> CreateListOfTestStep(List<TestStep> entityList)
        {
            (ExecutionState executionState,TestStep entity, string message) createResponse = (executionState: ExecutionState.Created, entity: null, message: $"List Of Item Created");
            long TestStepId = 1;
            foreach (TestStep testStep in entityList)
            {
                testStep.TestStepId = TestStepId;
                (ExecutionState executionState, TestStep entity, string message) createdResponse = await base.CreateAsync(testStep);
                TestStepId++;
            }
            return createResponse;

        }

        public override Task<(ExecutionState executionState, TestStep entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<TestStep>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!)
            .Include(x => x.TestCase!);
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<TestStep> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId)
        {
            var result = await _repository.Search(ProjectRequestId, TaskOfProjectId, TestCaseId);
            return result;
        }
    }
}
