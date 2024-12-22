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
    public interface IDocumentsService : IBaseService<DocumentsByTypeVM, DocumentsByType>
    {
        Task<(ExecutionState executionState, DocumentsByTypeVM entity, string message)> CreateProjectDocumentsList(List<DocumentsByTypeVM> model);
        Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle);
        Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> DocumentsListByClientId(long clientId);


    }
}
