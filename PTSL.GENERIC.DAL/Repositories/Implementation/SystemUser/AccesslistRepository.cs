using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{
    public class AccesslistRepository : BaseRepository<Accesslist>, IAccesslistRepository
    {
        private readonly DbSet<Accesslist> WriteOnlySet;
        private readonly DbSet<Accesslist> ReadOnlySet;
        private GENERICWriteOnlyCtx _ecommarceWriteOnlyCtx { get; }
        private GENERICReadOnlyCtx _ecommarceReadOnlyCtx { get; }

        public AccesslistRepository(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx
            )
            : base(ecommarceWriteOnlyCtx, ecommarceReadOnlyCtx)
        {
            this._ecommarceWriteOnlyCtx = ecommarceWriteOnlyCtx;
            this._ecommarceReadOnlyCtx = ecommarceReadOnlyCtx;

            this.WriteOnlySet = this._ecommarceWriteOnlyCtx.Set<Accesslist>();
            this.ReadOnlySet = this._ecommarceReadOnlyCtx.Set<Accesslist>();
        }

        //public async Task<(ExecutionState executionState, Accesslist entity, string message)> AccesslistLogin(LoginVM model)
        //{
        //    (ExecutionState executionState, Accesslist entity, string message) getResponse;

        //    try
        //    {
        //        Accesslist Accesslist1 = ReadOnlySet.FirstOrDefault(x => x.AccesslistEmail.Trim() == model.AccesslistEmail.Trim() 
        //        && x.AccesslistPassword == model.AccesslistPassword);


        //        Accesslist Accesslist = _ecommarceReadOnlyCtx.Accesslists.FirstOrDefault
        //            (
        //             x => x.AccesslistEmail.Trim() == model.AccesslistEmail.Trim() &&
        //            x.AccesslistPassword == model.AccesslistPassword);

        //        if (Accesslist != null)
        //        {
        //            getResponse = (executionState: ExecutionState.Retrieved, Accesslist, message: $"{typeof(Accesslist).Name} item found.");
        //        }
        //        else
        //        {
        //            getResponse = (executionState: ExecutionState.Failure, null, $"{typeof(Accesslist).Name} item not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        getResponse = (executionState: ExecutionState.Failure, null, message: ex.Message.ToString());
        //    }

        //    return getResponse;
        //}

    }
}
