using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Enum.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.Documents
{
    public interface IDefaultDocContentBusiness : IBaseBusiness<DefaultDocumentContent>
    {
        Task<(ExecutionState executionState, DefaultDocumentContent entity, string message)> GetDefaultDocByDocType(DocumentType documentType);

    }
}
