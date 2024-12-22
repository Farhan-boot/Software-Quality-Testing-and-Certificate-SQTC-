using Microsoft.AspNetCore.Mvc;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.Services.Interface.Project;

namespace PTSL.GENERIC.Api.Controllers.GeneralSetup
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryController : BaseController<ITestCategoryService, TestCategoryVM, TestCategory>
    {
        private readonly ITestCategoryService _TestCategoryService;
        public TestCategoryController(ITestCategoryService TestCategoryService) :
            base(TestCategoryService)
        {
            _TestCategoryService = TestCategoryService;
        }

        //Implement here new api, if we want.
       
    }
}
