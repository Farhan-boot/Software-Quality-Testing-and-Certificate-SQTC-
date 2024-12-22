using AutoMapper;
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
    public class ProjectStateLogService : BaseService<ProjectStateLogVM, ProjectStateLog>, IProjectStateLogService
    {
        public readonly IProjectStateLogBusiness _ProjectStateLogBusiness;
        public IMapper _mapper;
        public ProjectStateLogService(IProjectStateLogBusiness ProjectStateLogBusiness, IMapper mapper) : base(ProjectStateLogBusiness)
        {
            _ProjectStateLogBusiness = ProjectStateLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ProjectStateLog CastModelToEntity(ProjectStateLogVM model)
        {
            try
            {
                return _mapper.Map<ProjectStateLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ProjectStateLogVM CastEntityToModel(ProjectStateLog entity)
        {
            try
            {
                ProjectStateLogVM model = _mapper.Map<ProjectStateLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ProjectStateLogVM> CastEntityToModel(IQueryable<ProjectStateLog> entity)
        {
            try
            {
                IList<ProjectStateLogVM> colorList = _mapper.Map<IList<ProjectStateLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, ProjectStateLogVM entity, string message)> GetLogData(long projectRequestId, long projectStateEnumId)
        {
            var result = await _ProjectStateLogBusiness.GetLogData(projectRequestId, projectStateEnumId);
            return (ExecutionState.Success, _mapper.Map<ProjectStateLogVM>(result.entity), "Data Found");
        }
    }
}
