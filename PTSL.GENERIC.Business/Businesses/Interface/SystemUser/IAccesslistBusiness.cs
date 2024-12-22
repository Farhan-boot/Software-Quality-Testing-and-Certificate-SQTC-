using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface
{
    public interface IAccesslistBusiness : IBaseBusiness<Accesslist>
    {
        Task<(ExecutionState executionState, long data, string message)> GetAccessListId(string accessUrl);
        Task<(ExecutionState executionState, IQueryable<Accesslist> entity, string message)> ListForApproval();
    }
}
