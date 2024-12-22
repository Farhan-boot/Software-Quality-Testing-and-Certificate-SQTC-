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
    public class MeetingTypeService : BaseService<MeetingTypeVM, MeetingType>, IMeetingTypeService
    {
        public readonly IMeetingTypeBusiness _MeetingTypeBusiness;
        public IMapper _mapper;
        public MeetingTypeService(IMeetingTypeBusiness MeetingTypeBusiness, IMapper mapper) : base(MeetingTypeBusiness)
        {
            _MeetingTypeBusiness = MeetingTypeBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override MeetingType CastModelToEntity(MeetingTypeVM model)
        {
            try
            {
                return _mapper.Map<MeetingType>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override MeetingTypeVM CastEntityToModel(MeetingType entity)
        {
            try
            {
                MeetingTypeVM model = _mapper.Map<MeetingTypeVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<MeetingTypeVM> CastEntityToModel(IQueryable<MeetingType> entity)
        {
            try
            {
                IList<MeetingTypeVM> colorList = _mapper.Map<IList<MeetingTypeVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
