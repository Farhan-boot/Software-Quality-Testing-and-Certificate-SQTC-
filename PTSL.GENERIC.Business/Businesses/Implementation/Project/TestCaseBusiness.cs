using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Project
{
    public class TestCaseBusiness : BaseBusiness<TestCase>, ITestCaseBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        private readonly ITestCaseRepository _TestCaseRepository;
        public TestCaseBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,ITestCaseRepository testCaseRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _TestCaseRepository = testCaseRepository;
        }

        
        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<TestCase> filterOptions = new FilterOptions<TestCase>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<TestCase> entity, string message)> List(QueryOptions<TestCase> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<TestCase> entity, string message) returnResponse;
            var queryOption = new QueryOptions<TestCase>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!)
            .Include(x=>x.TestScenario!)
            .Include(x => x.User!)
            .Include(x => x.TestCategory!);

            (ExecutionState executionState, IQueryable<TestCase> entity, string message) entityObject = await _unitOfWork.List<TestCase>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, TestCase entity, string message)> CreateListOfTestCase(List<TestCase> entityList)
        {
            (ExecutionState executionState, TestCase entity, string message) createResponse = (executionState: ExecutionState.Activated, entity: null, message: $"");


                var query = _readOnlyContext.Set<TestCase>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();
                var tasks = await query.ToListAsync();
               var totalCase = tasks.Count();
                  totalCase++; ;
                foreach (TestCase testCase in entityList)
                {

                   string TestCaseNo = "";
                   if (totalCase != 0)
                   {
                        TestCaseNo = "TC-" + totalCase.ToString().PadLeft(4, '0');

                   }
                    else
                    
                        TestCaseNo = "TC-0001";
                    
                    testCase.TestCaseNo = TestCaseNo;


                    (ExecutionState executionState, TestCase entity, string message) createdResponse = await base.CreateAsync(testCase);
                    totalCase++;
                }
                return createResponse;
            
        }

        public override Task<(ExecutionState executionState, TestCase entity, string message)> GetAsync(long key)
        {
            var filterOptions = new FilterOptions<TestCase>();
            filterOptions.FilterExpression = x=>x.Id == key;
            filterOptions.IncludeExpression = x=>x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!)
            .Include(x => x.TestScenario!)
            .Include(x => x.User!)
            .Include(x => x.TestCategory!);
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<TestCase> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
        {
            var result = await _TestCaseRepository.Search( TestCaseNo, ProjectRequestId,TestScenarioId,TestCategoryId, ActualExecutionDate, PlannedExecutionDate);
            return result;
        }

        public async Task<(ExecutionState executionState, List<TestCase> entity, string message)> GetTestCasesByTaskofProjectId(long taskOfProjectId)
        {
            try
            {
                var result = await _readOnlyContext.Set<TestCase>()
                    .Where(x => x.TaskOfProjectId == taskOfProjectId)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable().ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<TestCase>()!, "Unexpected error occurred.");
            }
        }

        public async Task<(ExecutionState executionState, IList<TestCase> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId)
        {
            var result = await _TestCaseRepository.GetTestCaseListByProjectRequestId(projectRequestId);
            return result;
        }
    }
}
