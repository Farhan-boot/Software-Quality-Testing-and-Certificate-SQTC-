using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services
{
    public class MeetingService : BaseService<MeetingVM, Meeting>, IMeetingService
    {
        public readonly IMeetingBusiness _MeetingsBusiness;
        public IMapper _mapper;
        public MeetingService(IMeetingBusiness MeetingsBusiness, IMapper mapper) : base(MeetingsBusiness)
        {
            _MeetingsBusiness = MeetingsBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override Meeting CastModelToEntity(MeetingVM model)
        {
            try
            {
                return _mapper.Map<Meeting>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override MeetingVM CastEntityToModel(Meeting entity)
        {
            try
            {
                MeetingVM model = _mapper.Map<MeetingVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<MeetingVM> CastEntityToModel(IQueryable<Meeting> entity)
        {
            try
            {
                IList<MeetingVM> colorList = _mapper.Map<IList<MeetingVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, IList<UserVM> entity, string message)> GetSqtcUser()
        {
            var result = await _MeetingsBusiness.GetSqtcUser();
            return(ExecutionState.Success,_mapper.Map<IList<UserVM>>(result.entity),"Data Found");
        }

        public async Task<(ExecutionState executionState, IList<UserVM> entity, string message)> GetClientUser(long ProjectId)
        {
            var result = await _MeetingsBusiness.GetClientUser(ProjectId);
            return (ExecutionState.Success, _mapper.Map<IList<UserVM>>(result.entity), "Data Found");
        }

        public async Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> pendingMeetingList()
        {
            var result  = await _MeetingsBusiness.pendingMeetingList();
            return (ExecutionState.Success, _mapper.Map<IList<MeetingVM>>(result.entity), "Data Found");
        }

        public async Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> MeetingListByClientId(long ClientId)
        {
            var result = await _MeetingsBusiness.MeetingListByClientId(ClientId);
            return (ExecutionState.Success, _mapper.Map<IList<MeetingVM>>(result.entity), "Data Found");
        }

        public async Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate)
        {
            var result = await _MeetingsBusiness.MeetingListByDate(firstDate, lastDate);
            return (ExecutionState.Success, _mapper.Map<IList<MeetingVM>>(result.entity), "Data Found");
        }
    }
}
