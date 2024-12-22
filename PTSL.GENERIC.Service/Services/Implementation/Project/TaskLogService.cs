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
    public class TaskLogService : BaseService<TaskLogVM, TaskLog>, ITaskLogService
    {
        public readonly ITaskLogBusiness _taskLogBusiness;
        public IMapper _mapper;
        public TaskLogService(ITaskLogBusiness taskLogBusiness, IMapper mapper) : base(taskLogBusiness)
        {
            _taskLogBusiness = taskLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TaskLog CastModelToEntity(TaskLogVM model)
        {
            try
            {
                return _mapper.Map<TaskLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TaskLogVM CastEntityToModel(TaskLog entity)
        {
            try
            {
                TaskLogVM model = _mapper.Map<TaskLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TaskLogVM> CastEntityToModel(IQueryable<TaskLog> entity)
        {
            try
            {
                IList<TaskLogVM> colorList = _mapper.Map<IList<TaskLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public List<ProjectRequestVM> CastProjectEntityListToModel(List<ProjectRequest> entity)
        //{
        //    return _mapper.Map<List<ProjectRequestVM>>(entity);
        //}

        public List<TaskTypeVM> CastTaskTypeEntityListToModel(List<TaskType> entity)
        {
            return _mapper.Map<List<TaskTypeVM>>(entity);
        }
    }
}
