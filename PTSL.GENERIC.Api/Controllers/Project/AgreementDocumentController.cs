using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementDocumentController : BaseController<IAgreementDocumentService, AgreementDocumentsVM, AgreementDocuments>
    {
        private readonly IAgreementDocumentService _AgreementDocumentService;
        public AgreementDocumentController(IAgreementDocumentService AgreementDocumentService) :
            base(AgreementDocumentService)
        {
            _AgreementDocumentService = AgreementDocumentService;
        }

        //Implement here new api, if we want.
       
    }
}
