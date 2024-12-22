using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Documents
{
    public interface IDocumentsRepository: IBaseRepository<DocumentsByType>
    {
        Task<(ExecutionState executionState, IList<DocumentsByType> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle);
    }
}
