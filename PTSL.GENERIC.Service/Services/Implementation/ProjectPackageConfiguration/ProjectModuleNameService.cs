using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public class ProjectModuleNameService : BaseService<ProjectModuleNameVM, ProjectModuleName>, IProjectModuleNameService
    {
        public IMapper _mapper;

        public ProjectModuleNameService(IProjectModuleNameBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override ProjectModuleName CastModelToEntity(ProjectModuleNameVM model)
        {
            return _mapper.Map<ProjectModuleName>(model);
        }

        public override ProjectModuleNameVM CastEntityToModel(ProjectModuleName entity)
        {
            return _mapper.Map<ProjectModuleNameVM>(entity);
        }

        public override IList<ProjectModuleNameVM> CastEntityToModel(IQueryable<ProjectModuleName> entity)
        {
            return _mapper.Map<IList<ProjectModuleNameVM>>(entity).ToList();
        }
    }
}