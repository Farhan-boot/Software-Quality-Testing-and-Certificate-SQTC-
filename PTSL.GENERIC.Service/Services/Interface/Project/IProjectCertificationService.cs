using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface.Documents
{
    public interface IProjectCertificationService : IBaseService<ProjectCertificationVM, ProjectCertification>
    {
        //Task<(ExecutionState executionState, AllTypesOfDocument entity, string message)> CreateProjectDocumentsList(AllTypesOfDocument model);
    }
}
