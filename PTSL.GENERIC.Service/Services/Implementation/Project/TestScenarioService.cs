using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity;
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
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class TestScenarioService : BaseService<TestScenarioVM, TestScenario>, ITestScenarioService
    {
        public readonly ITestScenarioBusiness _testScenarioBusiness;
        public IMapper _mapper;
        public TestScenarioService(ITestScenarioBusiness testScenarioBusiness, IMapper mapper) : base(testScenarioBusiness)
        {
            _testScenarioBusiness = testScenarioBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TestScenario CastModelToEntity(TestScenarioVM model)
        {
            try
            {
                return _mapper.Map<TestScenario>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TestScenarioVM CastEntityToModel(TestScenario entity)
        {
            try
            {
                TestScenarioVM model = _mapper.Map<TestScenarioVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TestScenarioVM> CastEntityToModel(IQueryable<TestScenario> entity)
        {
            try
            {
                IList<TestScenarioVM> colorList = _mapper.Map<IList<TestScenarioVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TestScenario> CastTestScenarioModelToEntity(List<TestScenarioVM> entity)
        {
            return _mapper.Map<List<TestScenario>>(entity);
        }

        public List<TestScenarioVM> CastTestScenarioEntityListToModel(List<TestScenario> entity)
        {
            return _mapper.Map<List<TestScenarioVM>>(entity);
        }

        public async Task<(ExecutionState executionState, TestScenarioVM entity, string message)> CreateScenarioList(List<TestScenarioVM> model)
        {
            var businessModel = CastTestScenarioModelToEntity(model);
            var result = await _testScenarioBusiness.CreateScenarioListAsync(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new TestScenarioVM(), result.message);
        }

        public async Task<(ExecutionState executionState, List<TestScenarioVM> entity, string message)> GetTestScenarioByTaskId(long taskId)
        {
            var result = await _testScenarioBusiness.GetTestScenarioByTaskId(taskId);

            if (result.entity is not null)
            {
                return (result.executionState, _mapper.Map<List<TestScenarioVM>>(result.entity), result.message);
            }

            return (result.executionState, new List<TestScenarioVM>(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<TestScenarioVM> entity, string message)> Search(long? ProjectRequestId, string TestScenarioNo, TaskPriority? TaskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
        {
            var result = await _testScenarioBusiness.Search(ProjectRequestId, TestScenarioNo, TaskPriority, CreatedBy, PlannedExecutionDate, ActualExecutionDate);
            return(ExecutionState.Success, _mapper.Map<IList<TestScenarioVM>>(result.entity),"Success");
        }
    }
}
