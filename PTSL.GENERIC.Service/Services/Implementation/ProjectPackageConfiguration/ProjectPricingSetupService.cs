using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public class ProjectPricingSetupService : BaseService<ProjectPricingSetupVM, ProjectPricingSetup>, IProjectPricingSetupService
    {
        public IMapper _mapper;
        private readonly IProjectPricingSetupBusiness _business;

        public ProjectPricingSetupService(IProjectPricingSetupBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
            _business = business;
        }

        public override ProjectPricingSetup CastModelToEntity(ProjectPricingSetupVM model)
        {
            return _mapper.Map<ProjectPricingSetup>(model);
        }

        public override ProjectPricingSetupVM CastEntityToModel(ProjectPricingSetup entity)
        {
            return _mapper.Map<ProjectPricingSetupVM>(entity);
        }

        public override IList<ProjectPricingSetupVM> CastEntityToModel(IQueryable<ProjectPricingSetup> entity)
        {
            return _mapper.Map<IList<ProjectPricingSetupVM>>(entity).ToList();
        }


        public async Task<(ExecutionState executionState, List<ProjectPricingSetupVM> entity, string message)> GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId)
        {
            var result = await _business.GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(ProjectModuleNameId, ProjectPackageId);

            if (result.entity is not null)
            {
                return (result.executionState, _mapper.Map<List<ProjectPricingSetupVM>>(result.entity), result.message);
                //return (result.executionState, CastEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<ProjectPricingSetupVM>(), result.message);
        }


    }
}