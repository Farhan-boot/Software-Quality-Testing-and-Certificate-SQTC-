using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Api.Controllers.ProjectPackageConfiguration
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReconciliationController : BaseController<IReconciliationService, ReconciliationVM, Reconciliation>
    {
        public ReconciliationController(IReconciliationService service) :
            base(service)
        {
        }
    }
}