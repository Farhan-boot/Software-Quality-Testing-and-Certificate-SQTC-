using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class ClientLogService : BaseService<ClientLogVM, ClientLog>, IClientLogService
    {
        public readonly IClientLogBusiness _ClientLogBusiness;
        public IMapper _mapper;
        public ClientLogService(IClientLogBusiness ClientLogBusiness, IMapper mapper) : base(ClientLogBusiness)
        {
            _ClientLogBusiness = ClientLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ClientLog CastModelToEntity(ClientLogVM model)
        {
            try
            {
                return _mapper.Map<ClientLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ClientLogVM CastEntityToModel(ClientLog entity)
        {
            try
            {
                ClientLogVM model = _mapper.Map<ClientLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ClientLogVM> CastEntityToModel(IQueryable<ClientLog> entity)
        {
            try
            {
                IList<ClientLogVM> colorList = _mapper.Map<IList<ClientLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
