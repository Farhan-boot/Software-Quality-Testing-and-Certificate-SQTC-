using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class MeetingFilesService : BaseService<MeetingFilesVM, MeetingFiles>, IMeetingFilesService
    {
        public readonly IMeetingFilesBusiness _MeetingFilessBusiness;
        public IMapper _mapper;
        public MeetingFilesService(IMeetingFilesBusiness MeetingFilessBusiness, IMapper mapper) : base(MeetingFilessBusiness)
        {
            _MeetingFilessBusiness = MeetingFilessBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override MeetingFiles CastModelToEntity(MeetingFilesVM model)
        {
            try
            {
                return _mapper.Map<MeetingFiles>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override MeetingFilesVM CastEntityToModel(MeetingFiles entity)
        {
            try
            {
                MeetingFilesVM model = _mapper.Map<MeetingFilesVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<MeetingFilesVM> CastEntityToModel(IQueryable<MeetingFiles> entity)
        {
            try
            {
                IList<MeetingFilesVM> colorList = _mapper.Map<IList<MeetingFilesVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
