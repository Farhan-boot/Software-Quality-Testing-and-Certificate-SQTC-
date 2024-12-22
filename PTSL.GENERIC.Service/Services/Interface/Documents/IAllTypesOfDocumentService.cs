using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Documents
{
    public interface IAllTypesOfDocumentService : IBaseService<AllTypesOfDocumentVM, AllTypesOfDocument>
    {
        Task<(ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message)> ListByClientId(long clientId);

    }
}
