using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Documents
{
    public interface IDocumentsBusiness : IBaseBusiness<DocumentsByType>
    {
        Task<(ExecutionState executionState, DocumentsByType entity, string message)> CreateProjectDocumentListAsync(List<DocumentsByType> entityList);
        Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle);
        Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> DocumentsListByClientId(long clientId);
    }
}
