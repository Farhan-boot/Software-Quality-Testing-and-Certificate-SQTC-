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
    public class AttendedUserMeetingService : BaseService<AttendedUserMeetingVM, AttendedUserMeeting>, IAttendedUserMeetingService
    {
        public readonly IAttendedUserMeetingBusiness _AttendedUserMeetingsBusiness;
        public IMapper _mapper;
        public AttendedUserMeetingService(IAttendedUserMeetingBusiness AttendedUserMeetingsBusiness, IMapper mapper) : base(AttendedUserMeetingsBusiness)
        {
            _AttendedUserMeetingsBusiness = AttendedUserMeetingsBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override AttendedUserMeeting CastModelToEntity(AttendedUserMeetingVM model)
        {
            try
            {
                return _mapper.Map<AttendedUserMeeting>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override AttendedUserMeetingVM CastEntityToModel(AttendedUserMeeting entity)
        {
            try
            {
                AttendedUserMeetingVM model = _mapper.Map<AttendedUserMeetingVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<AttendedUserMeetingVM> CastEntityToModel(IQueryable<AttendedUserMeeting> entity)
        {
            try
            {
                IList<AttendedUserMeetingVM> colorList = _mapper.Map<IList<AttendedUserMeetingVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
