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
    public class TaskService : BaseService<TaskOfProjectVM, TaskOfProject>, ITaskService
    {
        public readonly ITaskBusiness _taskBusiness;
        public IMapper _mapper;
        private readonly ITaskTimeTrackingBusiness _timeTrackingBusiness;
        public TaskService(ITaskBusiness taskBusiness, IMapper mapper, ITaskTimeTrackingBusiness timeTrackingBusiness) : base(taskBusiness)
        {
            _taskBusiness = taskBusiness;
            _mapper = mapper;
            _timeTrackingBusiness = timeTrackingBusiness;
        }

        //Implement System Busniess Logic here

        public override TaskOfProject CastModelToEntity(TaskOfProjectVM model)
        {
            try
            {
                return _mapper.Map<TaskOfProject>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TaskOfProjectVM CastEntityToModel(TaskOfProject entity)
        {
            try
            {
                TaskOfProjectVM model = _mapper.Map<TaskOfProjectVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TaskOfProjectVM> CastEntityToModel(IQueryable<TaskOfProject> entity)
        {
            try
            {
                IList<TaskOfProjectVM> colorList = _mapper.Map<IList<TaskOfProjectVM>>(entity).ToList();
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

        public List<TaskOfProjectVM> CastTaskOfProjectEntityListToModel(List<TaskOfProject> entity)
        {
            return _mapper.Map<List<TaskOfProjectVM>>(entity);
        }
        public async Task<(ExecutionState executionState, List<ProjectRequestVM> entity, string message)> GetProjectReqsByProjectType(long projectTypeId)
        {
            var result = await _taskBusiness.GetProjectReqsByProjectType(projectTypeId);

            if (result.entity is not null)
            {
                return (result.executionState, CastProjectEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<ProjectRequestVM>(), result.message);
        }

        public async Task<(ExecutionState executionState, List<TaskTypeVM> entity, string message)> GetTaskTypesByProjectType(long projectTypeId)
        {
            var result = await _taskBusiness.GetTaskTypesByProjectType(projectTypeId);

            if (result.entity is not null)
            {
                return (result.executionState, CastTaskTypeEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<TaskTypeVM>(), result.message);
        }

        public async Task<(ExecutionState executionState, List<TaskOfProjectVM> entity, string message)> GetTaskListByUserId(long userId)
        {
            var result = await _taskBusiness.GetTaskListByUserId(userId);

            return (result.executionState, _mapper.Map<List<TaskOfProjectVM>>(result.entity), result.message);
        }

        public async Task<(ExecutionState executionState, List<TaskOfProjectVM> entity, string message)> GetTaskByProjectId(long projectId)
        {
            var result = await _taskBusiness.GetTasksByProject(projectId);

            if (result.entity is not null)
            {
                return (result.executionState, CastTaskOfProjectEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<TaskOfProjectVM>(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<TaskOfProjectVM> entity, string message)> Search(long? ProjectRequestId, string TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline)
        {
            var result = await _taskBusiness.Search(ProjectRequestId, TaskId, AssigneeId, CreateDate, Deadline);
            return (ExecutionState.Success, _mapper.Map<IList<TaskOfProjectVM>>(result.entity),"Success");
        }

        public async override Task<(ExecutionState executionState, TaskOfProjectVM entity, string message)> GetAsync(long key)
        {
            var result= await base.GetAsync(key);
            var timetrackres = _timeTrackingBusiness.List().Result.entity;
            if (timetrackres != null)
            {
                result.entity.TotalTrackedTime = timetrackres.Where(s => s.TaskOfProjectId == key)?.Sum(x => x.TimeSpentHour);
                result.entity.TotalDueTime = (result.entity.TaskEstimationHour - result.entity.TotalTrackedTime);
            }
          
            return result;
        }
    }
}
