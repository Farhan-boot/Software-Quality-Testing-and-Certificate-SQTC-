using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface
{
    public interface IAccesslistRepository : IBaseRepository<Accesslist>
    {
        //Task<(ExecutionState executionState, Accesslist entity, string message)> AccesslistLogin(LoginVM model);
    }
}
