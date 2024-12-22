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
    public interface IDocumentAmendmentService : IBaseService<DocumentAmendmentVM, DocumentAmendment>
    {
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> CreateDocAmendment(DocumentAmendmentVM model);
        Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> GetAmendmentById(long docId);
    }
}
