using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{

    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        
        public ClientRepository(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx)
            : base(ecommarceWriteOnlyCtx, ecommarceReadOnlyCtx)
        {
            EcommarceReadOnlyCtx = ecommarceReadOnlyCtx;
        }

        public GENERICReadOnlyCtx EcommarceReadOnlyCtx { get; }

        public async Task<(ExecutionState executionState, IList<User> entity, string message)> ClientWiseUserList(long ClientId)
        {
            var result =await EcommarceReadOnlyCtx.Set<User>()
                .Where(x => x.CreatedBy == ClientId)
                .ToListAsync();
            return (ExecutionState.Retrieved,result,"Data Found");
        }

        public async Task<(ExecutionState executionState, IList<Client> entity, string message)>Search(string? organizationName, ClientStatus? ClientStatus, string? mobileNo, DateTime? CreatedAt)
        {
            IQueryable<Client> query = EcommarceReadOnlyCtx.Set<Client>()
                .Include(x=>x.Designation).Include(x=>x.User);
            query = query.WhereIf(!string.IsNullOrEmpty(organizationName), x => x.OrganizationName == organizationName);
            query = query.WhereIf(ClientStatus is not null, x => x.ClientStatus == ClientStatus);
            query = query.WhereIf(!string.IsNullOrEmpty(mobileNo), x => x.MobileNumber == mobileNo);
            query = query.WhereIf(CreatedAt is not null , x=>x.CreatedAt == CreatedAt);
            var result = await query.ToListAsync();

            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }

}
