using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.DAL.Repositories.Interface.Project
{
	public interface IAgreementDocumentRepository : IBaseRepository<AgreementDocuments>
	{
		Task<(ExecutionState executionState, IQueryable<AgreementDocuments> entity,string message)>GetByIsDefault(bool isDefault);
	}
}
