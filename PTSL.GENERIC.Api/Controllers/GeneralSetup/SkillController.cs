using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.Services.Interface.GeneralSetup;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : BaseController<ISkillService, SkillVM, Skill>
    {
        public SkillController(ISkillService skillService) :
            base(skillService)
        { }
    }
}
