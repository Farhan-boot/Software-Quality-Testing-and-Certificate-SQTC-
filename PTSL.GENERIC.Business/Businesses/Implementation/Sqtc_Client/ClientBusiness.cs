using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class ClientBusiness : BaseBusiness<Client>, IClientBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public readonly IClientRepository _clientRepository;
        public ClientBusiness(GENERICUnitOfWork unitOfWork, IClientRepository clientRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository; 
        }
        public async override Task<(ExecutionState executionState, IQueryable<Client> entity, string message)> List(QueryOptions<Client> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<Client> entity, string message) returnResponse;
            var queryOption = new QueryOptions<Client>();
            queryOption.IncludeExpression = x => x.Include(y => y.Designation!)
            .Include(y => y.User!);
            queryOption.SortingExpression = x => x.OrderByDescending(x=>x.Id);
            (ExecutionState executionState, IQueryable<Client> entity, string message) entityObject = await _unitOfWork.List<Client>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, Client entity, string message)> CreateAsync(Client entity)
        {
            (ExecutionState executionState, Client entity, string message) createResponse;

            FilterOptions<Client> filterOptions = new FilterOptions<Client>();
            filterOptions.FilterExpression = x => x.WorkingEmail.Trim() == entity.WorkingEmail.Trim();
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            if (entityObject.executionState.ToString() == "Success" || entity.WorkingEmail.Trim() == "")
            {
                createResponse = (executionState: ExecutionState.Success, entity: null, message: $"{typeof(Client).Name} Email already exists.")!;
                return createResponse;
            }
            (ExecutionState executionState, Client entity, string message) createdResponse = await base.CreateAsync(entity);

            return createdResponse;
        }

        public async Task<(ExecutionState executionState, IList<Client> entity, string message)> Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt)
        {
            var result = await _clientRepository.Search(organizationName, ClientStatus, mobileNo, CreatedAt);
            return result;
        }

        public async Task<(ExecutionState executionState, IList<User> entity, string message)> ClientWiseUserList(long ClientId)
        {
            var result = await _clientRepository.ClientWiseUserList(ClientId);
            return result;
        }

        public async override Task<(ExecutionState executionState, Client entity, string message)> GetAsync(long key)
        {
            var filterOption = new FilterOptions<Client>();
            filterOption.FilterExpression = x=>x.Id== key;
            filterOption.IncludeExpression = x => x
            .Include(y => y.Designation)
            .Include(y => y.User!);
            return await base.GetAsync(filterOption);
        }
    }
}
