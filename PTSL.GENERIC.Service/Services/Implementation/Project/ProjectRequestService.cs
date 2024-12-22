using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.GeneralSetup;
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
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class ProjectRequestService : BaseService<ProjectRequestVM, ProjectRequest>, IProjectRequestService
    {
        public readonly IProjectRequestBusiness _projectRequestBusiness;
        public IMapper _mapper;
        public ProjectRequestService(IProjectRequestBusiness projectRequestBusiness, IMapper mapper) : base(projectRequestBusiness)
        {
            _projectRequestBusiness = projectRequestBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ProjectRequest CastModelToEntity(ProjectRequestVM model)
        {
            try
            {
                return _mapper.Map<ProjectRequest>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ProjectRequestVM CastEntityToModel(ProjectRequest entity)
        {
            try
            {
                ProjectRequestVM model = _mapper.Map<ProjectRequestVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ProjectRequestVM> CastEntityToModel(IQueryable<ProjectRequest> entity)
        {
            try
            {
                IList<ProjectRequestVM> colorList = _mapper.Map<IList<ProjectRequestVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> Search(string ProjectName, ProjectType? ProjectType, string ProjectCode, long? ClientId, DateTime? RequestDate)
        {
            var result = await _projectRequestBusiness.Search(ProjectName, ProjectType, ProjectCode, ClientId, RequestDate);
            return(ExecutionState.Success, _mapper.Map<IList<ProjectRequestVM>>(result.entity),"Success");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectPendingList()
        {
            var result = await _projectRequestBusiness.GetProjectPendingList();
            return (ExecutionState.Success, _mapper.Map<IList<ProjectRequestVM>>(result.entity), "Pending Project Item Found");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectRejectedList()
        {
            var result = await _projectRequestBusiness.GetProjectRejectedList();
            return (ExecutionState.Success, _mapper.Map<IList<ProjectRequestVM>>(result.entity), "Rejected Project Item Found");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectListByClientId(long clientId)
        {
            var result = await _projectRequestBusiness.GetProjectListByClientId(clientId);
            return (ExecutionState.Success, _mapper.Map<IList<ProjectRequestVM>>(result.entity), "Client Project Item Found");
        }

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectAcceptedList()
        {
            var result = await _projectRequestBusiness.GetProjectAcceptedList();
            return (ExecutionState.Success, _mapper.Map<IList<ProjectRequestVM>>(result.entity), "Accepted Project Item Found");
        }
    }
}
