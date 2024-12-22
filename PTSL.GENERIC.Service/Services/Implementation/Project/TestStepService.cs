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
    public class TestStepService : BaseService<TestStepVM, TestStep>, ITestStepService
    {
        public readonly ITestStepBusiness _TestStepBusiness;
        public IMapper _mapper;
        public TestStepService(ITestStepBusiness TestStepBusiness, IMapper mapper) : base(TestStepBusiness)
        {
            _TestStepBusiness = TestStepBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TestStep CastModelToEntity(TestStepVM model)
        {
            try
            {
                return _mapper.Map<TestStep>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TestStepVM CastEntityToModel(TestStep entity)
        {
            try
            {
                TestStepVM model = _mapper.Map<TestStepVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TestStepVM> CastEntityToModel(IQueryable<TestStep> entity)
        {
            try
            {
                IList<TestStepVM> colorList = _mapper.Map<IList<TestStepVM>>(entity).ToList();
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
        public async Task<(ExecutionState executionState, TestStepVM entity, string message)> CreateListOfTestStep(List<TestStepVM> model)
        {
            var businessModel = _mapper.Map<List<TestStep>>(model);
            var result = await _TestStepBusiness.CreateListOfTestStep(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new TestStepVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<TestStepVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId)
        {
            var result = await _TestStepBusiness.Search(ProjectRequestId, TaskOfProjectId, TestCaseId);
            return (ExecutionState.Success, _mapper.Map<IList<TestStepVM>>(result.entity), "Success");
        }
    }
}
