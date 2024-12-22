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
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        private readonly DbSet<Module> WriteOnlySet;
        private readonly DbSet<Module> ReadOnlySet;
        private GENERICWriteOnlyCtx _ecommarceWriteOnlyCtx { get; }
        private GENERICReadOnlyCtx _ecommarceReadOnlyCtx { get; }

        public ModuleRepository(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx
            )
            : base(ecommarceWriteOnlyCtx, ecommarceReadOnlyCtx)
        {
            this._ecommarceWriteOnlyCtx = ecommarceWriteOnlyCtx;
            this._ecommarceReadOnlyCtx = ecommarceReadOnlyCtx;

            this.WriteOnlySet = this._ecommarceWriteOnlyCtx.Set<Module>();
            this.ReadOnlySet = this._ecommarceReadOnlyCtx.Set<Module>();
        }

        //public async Task<(ExecutionState executionState, Module entity, string message)> ModuleLogin(LoginVM model)
        //{
        //    (ExecutionState executionState, Module entity, string message) getResponse;

        //    try
        //    {
        //        Module Module1 = ReadOnlySet.FirstOrDefault(x => x.ModuleEmail.Trim() == model.ModuleEmail.Trim() 
        //        && x.ModulePassword == model.ModulePassword);


        //        Module Module = _ecommarceReadOnlyCtx.Modules.FirstOrDefault
        //            (
        //             x => x.ModuleEmail.Trim() == model.ModuleEmail.Trim() &&
        //            x.ModulePassword == model.ModulePassword);

        //        if (Module != null)
        //        {
        //            getResponse = (executionState: ExecutionState.Retrieved, Module, message: $"{typeof(Module).Name} item found.");
        //        }
        //        else
        //        {
        //            getResponse = (executionState: ExecutionState.Failure, null, $"{typeof(Module).Name} item not found.");
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
