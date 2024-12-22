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

namespace PTSL.GENERIC.Service.Services.Implementation.HardwareTestings
{
    public class HardwareTestingService : BaseService<HardwareTestingVM, HardwareTesting>, IHardwareTestingService
    {
        public readonly IHardwareTestingBusiness _HardwareTestingBusiness;
        public IMapper _mapper;
        public HardwareTestingService(IHardwareTestingBusiness HardwareTestingBusiness, IMapper mapper) : base(HardwareTestingBusiness)
        {
            _HardwareTestingBusiness = HardwareTestingBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override HardwareTesting CastModelToEntity(HardwareTestingVM model)
        {
            try
            {
                return _mapper.Map<HardwareTesting>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override HardwareTestingVM CastEntityToModel(HardwareTesting entity)
        {
            try
            {
                HardwareTestingVM model = _mapper.Map<HardwareTestingVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<HardwareTestingVM> CastEntityToModel(IQueryable<HardwareTesting> entity)
        {
            try
            {
                IList<HardwareTestingVM> colorList = _mapper.Map<IList<HardwareTestingVM>>(entity).ToList();
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
        public async Task<(ExecutionState executionState, HardwareTestingVM entity, string message)> CreateListOfHardwareTesting(List<HardwareTestingVM> model)
        {
            var businessModel = _mapper.Map<List<HardwareTesting>>(model);
            var result = await _HardwareTestingBusiness.CreateListOfHardwareTesting(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new HardwareTestingVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<HardwareTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,long? TestScopeId,string? SubItem)
        {
            var result = await _HardwareTestingBusiness.Search(ProjectRequestId, TaskOfProjectId, TestScopeId,SubItem);
            return (ExecutionState.Success, _mapper.Map<IList<HardwareTestingVM>>(result.entity), "Success");
        }
    }
}
