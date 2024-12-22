using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Documents
{
    public class DocumentsRepository : BaseRepository<DocumentsByType>, IDocumentsRepository
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public DocumentsRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx) 
            : base(writeOnlyCtx, readOnlyCtx)
        {
            _readOnlyCtx = readOnlyCtx;
        }

        public async Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle)
        {
            IQueryable<DocumentsByType> query = _readOnlyCtx.Set<DocumentsByType>()
                .Include(x => x.ProjectRequest)
                .Include(x=>x.DocumentCategories);

            query = query.WhereIf(!string.IsNullOrEmpty(DocumentTitle), x => x.DocumentTitle == DocumentTitle);
            query = query.WhereIf(ProjectRequestId is not null, x => x.ProjectRequestId == ProjectRequestId);
            query = query.WhereIf(DocumentCategoriesId is not null, x => x.DocumentCategoriesId == DocumentCategoriesId);
            var result = await query.ToListAsync();
            return (ExecutionState.Retrieved, result, "Data returned successfully.");
        }
    }
}
