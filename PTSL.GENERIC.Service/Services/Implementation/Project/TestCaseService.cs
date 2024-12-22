using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class TestCaseService : BaseService<TestCaseVM, TestCase>, ITestCaseService
    {
        public readonly ITestCaseBusiness _TestCaseBusiness;
        public IMapper _mapper;
        public TestCaseService(ITestCaseBusiness TestCaseBusiness, IMapper mapper) : base(TestCaseBusiness)
        {
            _TestCaseBusiness = TestCaseBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TestCase CastModelToEntity(TestCaseVM model)
        {
            try
            {
                return _mapper.Map<TestCase>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TestCaseVM CastEntityToModel(TestCase entity)
        {
            try
            {
                TestCaseVM model = _mapper.Map<TestCaseVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TestCaseVM> CastEntityToModel(IQueryable<TestCase> entity)
        {
            try
            {
                IList<TestCaseVM> colorList = _mapper.Map<IList<TestCaseVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProjectRequestVM> CastProjectEntityListToModel(List<ProjectRequest> entity)
        {
            return _mapper.Map<List<ProjectRequestVM>>(entity);
        }

        public List<TaskTypeVM> CastTaskTypeEntityListToModel(List<TaskType> entity)
        {
            return _mapper.Map<List<TaskTypeVM>>(entity);
        }

        public async Task<(ExecutionState executionState, TestCaseVM entity, string message)> CreateListOfTestCase(List<TestCaseVM> model)
        {
            var businessModel = _mapper.Map<List<TestCase>>(model);
            var result = await _TestCaseBusiness.CreateListOfTestCase(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new TestCaseVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<TestCaseVM> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
        {
            var result = await _TestCaseBusiness.Search(TestCaseNo, ProjectRequestId, TestScenarioId, TestCategoryId, ActualExecutionDate, PlannedExecutionDate);

            return (ExecutionState.Success, _mapper.Map<List<TestCaseVM>>(result.entity), "Success");
        }

        public async Task<(ExecutionState executionState, IList<TestCaseVM> entity, string message)> GetTestCasesByTaskofProjectId(long taskOfProjectId)
        {
            var result = await _TestCaseBusiness.GetTestCasesByTaskofProjectId(taskOfProjectId);
            return (ExecutionState.Success, _mapper.Map<List<TestCaseVM>>(result.entity), "Success");
        }

        public async Task<(ExecutionState executionState, IList<TestCaseVM> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId)
        {
            var result = await _TestCaseBusiness.GetTestCaseListByProjectRequestId(projectRequestId);
            return(ExecutionState.Success,_mapper.Map<IList<TestCaseVM>>(result.entity),"Success");
        }
    }
}
