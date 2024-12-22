using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Documents
{
    public class AllTypesOfDocumentRepository : BaseRepository<AllTypesOfDocument>, IAllTypesOfDocumentRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public AllTypesOfDocumentRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
        }
    }
}
