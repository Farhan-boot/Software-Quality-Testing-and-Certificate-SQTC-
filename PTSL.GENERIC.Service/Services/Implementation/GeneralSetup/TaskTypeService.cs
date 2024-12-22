using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class TaskTypeService : BaseService<TaskTypeVM, TaskType>, ITaskTypeService
    {
        public readonly ITaskTypeBusiness _TaskTypeBusiness;
        public IMapper _mapper;
        public TaskTypeService(ITaskTypeBusiness TaskTypeBusiness, IMapper mapper) : base(TaskTypeBusiness)
        {
            _TaskTypeBusiness = TaskTypeBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TaskType CastModelToEntity(TaskTypeVM model)
        {
            try
            {
                return _mapper.Map<TaskType>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TaskTypeVM CastEntityToModel(TaskType entity)
        {
            try
            {
                TaskTypeVM model = _mapper.Map<TaskTypeVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TaskTypeVM> CastEntityToModel(IQueryable<TaskType> entity)
        {
            try
            {
                IList<TaskTypeVM> colorList = _mapper.Map<IList<TaskTypeVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
