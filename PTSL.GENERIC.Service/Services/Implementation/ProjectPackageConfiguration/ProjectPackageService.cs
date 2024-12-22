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
    public class ProjectPackageService : BaseService<ProjectPackageVM, ProjectPackage>, IProjectPackageService
    {
        public IMapper _mapper;
        public readonly IProjectPackageBusiness _ProjectPackageBusiness;
   
        public ProjectPackageService(IProjectPackageBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
            _ProjectPackageBusiness = business;
        }

        public override ProjectPackage CastModelToEntity(ProjectPackageVM model)
        {
            return _mapper.Map<ProjectPackage>(model);
        }

        public override ProjectPackageVM CastEntityToModel(ProjectPackage entity)
        {
            return _mapper.Map<ProjectPackageVM>(entity);
        }

        public override IList<ProjectPackageVM> CastEntityToModel(IQueryable<ProjectPackage> entity)
        {
            return _mapper.Map<IList<ProjectPackageVM>>(entity).ToList();
        }


        public async Task<(ExecutionState executionState, IList<ProjectPackageVM> entity, string message)> GetProjectPackageByProjectModuleNameId(long ProjectModuleNameId)
        {
            (ExecutionState executionState, IList<ProjectPackageVM> entity, string message) returnResponse;

            try
            {
                (ExecutionState executionState, IQueryable<ProjectPackage> entity, string message) response = await _ProjectPackageBusiness.GetProjectPackageByProjectModuleNameId(ProjectModuleNameId);

                if (response.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (response.executionState, entity: CastEntityToModel(response.entity), response.message);
                }
                else
                {
                    returnResponse = (response.executionState, entity: null, response.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }


    }
}