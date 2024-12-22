using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services
{
    public class ClientService : BaseService<ClientVM, Client>, IClientService
    {
        public readonly IClientBusiness _ClientBusiness;
        public IMapper _mapper;
        public ClientService(IClientBusiness ClientBusiness, IMapper mapper) : base(ClientBusiness)
        {
            _ClientBusiness = ClientBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override Client CastModelToEntity(ClientVM model)
        {
            try
            {
                return _mapper.Map<Client>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ClientVM CastEntityToModel(Client entity)
        {
            try
            {
                ClientVM model = _mapper.Map<ClientVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ClientVM> CastEntityToModel(IQueryable<Client> entity)
        {
            try
            {
                IList<ClientVM> colorList = _mapper.Map<IList<ClientVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, IList<ClientVM> entity, string message)> Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt)
        {
            var result = await _ClientBusiness.Search(organizationName,ClientStatus,mobileNo,CreatedAt);

            return (ExecutionState.Success, _mapper.Map<List<ClientVM>>(result.entity), "Success");
        }

        public async Task<(ExecutionState executionState, IList<UserVM> entity, string message)> ClientWiseUserList(long ClientId)
        {
            var result = await _ClientBusiness.ClientWiseUserList(ClientId);
            return (ExecutionState.Success,_mapper.Map<IList<UserVM>>(result.entity),"Success");
        }
    }
}
