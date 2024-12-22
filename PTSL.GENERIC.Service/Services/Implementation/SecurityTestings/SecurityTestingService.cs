using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.SecurityTestings
{
    public class SecurityTestingService : BaseService<SecurityTestingVM, SecurityTesting>, ISecurityTestingService
    {
        public readonly ISecurityTestingBusiness _SecurityTestingBusiness;
        public IMapper _mapper;
        public SecurityTestingService(ISecurityTestingBusiness SecurityTestingBusiness, IMapper mapper) : base(SecurityTestingBusiness)
        {
            _SecurityTestingBusiness = SecurityTestingBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override SecurityTesting CastModelToEntity(SecurityTestingVM model)
        {
            try
            {
                return _mapper.Map<SecurityTesting>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override SecurityTestingVM CastEntityToModel(SecurityTesting entity)
        {
            try
            {
                SecurityTestingVM model = _mapper.Map<SecurityTestingVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<SecurityTestingVM> CastEntityToModel(IQueryable<SecurityTesting> entity)
        {
            try
            {
                IList<SecurityTestingVM> colorList = _mapper.Map<IList<SecurityTestingVM>>(entity).ToList();
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
        

        public async Task<(ExecutionState executionState, IList<SecurityTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
        {
            var result = await _SecurityTestingBusiness.Search(ProjectRequestId, TaskOfProjectId,Vulnerability,SeverityLevel,EaseOfExploitation);
            return (ExecutionState.Success, _mapper.Map<IList<SecurityTestingVM>>(result.entity), "Success");
        }
    }
}
