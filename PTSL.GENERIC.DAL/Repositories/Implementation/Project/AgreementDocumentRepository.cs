using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    
    public class AgreementDocumentRepository
	: BaseRepository<AgreementDocuments>, IAgreementDocumentRepository
	{
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public AgreementDocumentRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
			: base(writeOnlyCtx, readOnlyCtx)
		{
            _readOnlyCtx = readOnlyCtx;

        }

        public async Task<(ExecutionState executionState, IQueryable<AgreementDocuments> entity, string message)> GetByIsDefault(bool isDefault)
        {
            var result =  _readOnlyCtx.Set<AgreementDocuments>()
                .Where(x=>x.IsDefault == isDefault)
                .AsQueryable();
            return(ExecutionState.Success, result,"Data Found");
        }
    }
}
