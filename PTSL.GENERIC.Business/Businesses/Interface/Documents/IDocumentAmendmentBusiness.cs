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
    public interface IDocumentAmendmentBusiness : IBaseBusiness<DocumentAmendment>
    {
        //Task<(ExecutionState executionState, AllTypesOfDocument entity, string message)> CreateDocumentByTypesAsync(AllTypesOfDocument entity);
        Task<(ExecutionState executionState, DocumentAmendment entity, string message)> CreateDocAmendment(DocumentAmendment entity);
        Task<(ExecutionState executionState, DocumentAmendment entity, string message)> GetDocAmendmentByDocId(long documentId);
    }
}
