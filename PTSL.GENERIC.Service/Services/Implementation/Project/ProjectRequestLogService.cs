using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Interface.GeneralSetup;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
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
    public class ProjectRequestLogService : BaseService<ProjectRequestLogVM, ProjectRquestLog>, IProjectRequestLogService
    {
        public readonly IProjectRequestLogBusiness _projectRequestLogBusiness;
        public IMapper _mapper;
        public ProjectRequestLogService(IProjectRequestLogBusiness projectRequestLogBusiness, IMapper mapper) : base(projectRequestLogBusiness)
        {
            _projectRequestLogBusiness = projectRequestLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ProjectRquestLog CastModelToEntity(ProjectRequestLogVM model)
        {
            try
            {
                return _mapper.Map<ProjectRquestLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ProjectRequestLogVM CastEntityToModel(ProjectRquestLog entity)
        {
            try
            {
                ProjectRequestLogVM model = _mapper.Map<ProjectRequestLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ProjectRequestLogVM> CastEntityToModel(IQueryable<ProjectRquestLog> entity)
        {
            try
            {
                IList<ProjectRequestLogVM> colorList = _mapper.Map<IList<ProjectRequestLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
