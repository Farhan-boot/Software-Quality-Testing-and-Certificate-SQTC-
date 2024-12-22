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
    public class BugAndDefectService : BaseService<BugAndDefectVM, BugAndDefect>, IBugAndDefectService
    {
        public readonly IBugAndDefectBusiness _BugAndDefectBusiness;
        public IMapper _mapper;
        public BugAndDefectService(IBugAndDefectBusiness BugAndDefectBusiness, IMapper mapper) : base(BugAndDefectBusiness)
        {
            _BugAndDefectBusiness = BugAndDefectBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override BugAndDefect CastModelToEntity(BugAndDefectVM model)
        {
            try
            {
                return _mapper.Map<BugAndDefect>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override BugAndDefectVM CastEntityToModel(BugAndDefect entity)
        {
            try
            {
                BugAndDefectVM model = _mapper.Map<BugAndDefectVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<BugAndDefectVM> CastEntityToModel(IQueryable<BugAndDefect> entity)
        {
            try
            {
                IList<BugAndDefectVM> colorList = _mapper.Map<IList<BugAndDefectVM>>(entity).ToList();
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
        public async Task<(ExecutionState executionState, BugAndDefectVM entity, string message)> CreateListOfBugAndDefect(List<BugAndDefectVM> model)
        {
            var businessModel = _mapper.Map<List<BugAndDefect>>(model);
            var result = await _BugAndDefectBusiness.CreateListOfBugAndDefect(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new BugAndDefectVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<BugAndDefectVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            var result = await _BugAndDefectBusiness.Search(ProjectRequestId, TaskOfProjectId, TestCaseId,bugzillaId,defectId,bugAndDefectStatus,bugAndDefectSeverity);
            return (ExecutionState.Success, _mapper.Map<IList<BugAndDefectVM>>(result.entity), "Success");
        }

        public async Task<(ExecutionState executionState, BugAndDefectVM entity, string message)>UpdateBugListOnBugzilla(List<BugAndDefectVM> model)
        {
            var businessModel = _mapper.Map<List<BugAndDefect>>(model);
            var result = await _BugAndDefectBusiness.UpdateBugListOnBugZilla(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new BugAndDefectVM(), result.message);
        }
    }
}
