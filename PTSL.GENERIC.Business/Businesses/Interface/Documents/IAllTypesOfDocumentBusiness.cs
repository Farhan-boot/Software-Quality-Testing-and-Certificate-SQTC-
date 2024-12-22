using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Documents
{
    public interface IAllTypesOfDocumentBusiness : IBaseBusiness<AllTypesOfDocument>
    {
        Task<(ExecutionState executionState, IList<AllTypesOfDocument> entity, string message)> ListByClientId(long clientId);
    }
}
