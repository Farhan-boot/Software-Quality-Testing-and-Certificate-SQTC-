using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface
{
    public interface IAccesslistService : IBaseService<AccesslistVM, Accesslist>
    {
        Task<(ExecutionState executionState, long data, string message)> GetAccessListId(string accessUrl);
        Task<(ExecutionState executionState, IList<AccesslistVM> entity, string message)> ListForApproval();
    }
}
