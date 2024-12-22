using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;

namespace PTSL.GENERIC.Api.Controllers.Documents
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCertificationController : BaseController<IProjectCertificationService, ProjectCertificationVM, ProjectCertification>
    {
        private readonly IProjectCertificationService _projectCertificationService;
        private readonly IUserService _userService;
        public ProjectCertificationController(IProjectCertificationService projectCertificationService, IUserService userService)
            : base(projectCertificationService)
        {
            _projectCertificationService = projectCertificationService;
            _userService = userService;
        }
    }
}
