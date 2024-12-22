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
    public class TaskTimeTrackingService : BaseService<TaskTimeTrackingVM, TaskTimeTracking>, ITaskTimeTrackingService
    {
        public readonly ITaskTimeTrackingBusiness _taskTimeTrackingBusiness;
        public IMapper _mapper;
        public TaskTimeTrackingService(ITaskTimeTrackingBusiness taskTimeTrackingBusiness, IMapper mapper) : base(taskTimeTrackingBusiness)
        {
            _taskTimeTrackingBusiness = taskTimeTrackingBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TaskTimeTracking CastModelToEntity(TaskTimeTrackingVM model)
        {
            try
            {
                return _mapper.Map<TaskTimeTracking>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TaskTimeTrackingVM CastEntityToModel(TaskTimeTracking entity)
        {
            try
            {
                TaskTimeTrackingVM model = _mapper.Map<TaskTimeTrackingVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TaskTimeTrackingVM> CastEntityToModel(IQueryable<TaskTimeTracking> entity)
        {
            try
            {
                IList<TaskTimeTrackingVM> colorList = _mapper.Map<IList<TaskTimeTrackingVM>>(entity).ToList();
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
    }
}
