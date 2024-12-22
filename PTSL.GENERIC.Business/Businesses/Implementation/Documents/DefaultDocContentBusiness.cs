using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Documents
{
    public class DefaultDocContentBusiness : BaseBusiness<DefaultDocumentContent>, IDefaultDocContentBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public DefaultDocContentBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<(ExecutionState executionState, DefaultDocumentContent entity, string message)> GetDefaultDocByDocType(DocumentType documentType)
        {
            var result = await _readOnlyContext.Set<DefaultDocumentContent>()
                .Where(x => x.DocumentType == documentType).FirstOrDefaultAsync() ?? new DefaultDocumentContent();

            return (ExecutionState.Retrieved, result, "Data Found");
        }
    }
}
