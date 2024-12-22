using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.HardwareTestings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class TestScopeService : BaseService<TestScopeVM, TestScope>, ITestScopeService
    {
        public readonly ITestScopeBusiness _TestScopeBusiness;
        public IMapper _mapper;
        public TestScopeService(ITestScopeBusiness TestScopeBusiness, IMapper mapper) : base(TestScopeBusiness)
        {
            _TestScopeBusiness = TestScopeBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TestScope CastModelToEntity(TestScopeVM model)
        {
            try
            {
                return _mapper.Map<TestScope>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TestScopeVM CastEntityToModel(TestScope entity)
        {
            try
            {
                TestScopeVM model = _mapper.Map<TestScopeVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TestScopeVM> CastEntityToModel(IQueryable<TestScope> entity)
        {
            try
            {
                IList<TestScopeVM> colorList = _mapper.Map<IList<TestScopeVM>>(entity).ToList();
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
        public async Task<(ExecutionState executionState, TestScopeVM entity, string message)> CreateListOfTestScope(List<TestScopeVM> model)
        {
            var businessModel = _mapper.Map<List<TestScope>>(model);
            var result = await _TestScopeBusiness.CreateListOfTestScope(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new TestScopeVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<TestScopeVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? TestItem, string? TenderID,string? SerialNo)
        {
            var result = await _TestScopeBusiness.Search(ProjectRequestId, TaskOfProjectId, TestItem,TenderID,SerialNo);
            return (ExecutionState.Success, _mapper.Map<IList<TestScopeVM>>(result.entity), "Success");
        }
    }
}
