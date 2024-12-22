using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.ProjectPackageConfiguration
{
    public class ProjectPricingSetupBusiness : BaseBusiness<ProjectPricingSetup>, IProjectPricingSetupBusiness
    {

        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyCtx;

        public ProjectPricingSetupBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx context)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyCtx = context;
        }

        public override Task<(ExecutionState executionState, IQueryable<ProjectPricingSetup> entity, string message)> List(QueryOptions<ProjectPricingSetup> queryOptions = null)
        {
            return base.List(new QueryOptions<ProjectPricingSetup>()
            {
            IncludeExpression = e => e.Include(x => x.ProjectModuleName!).Include(x=>x.ProjectPackage),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

        public override Task<(ExecutionState executionState, ProjectPricingSetup entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<ProjectPricingSetup>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.ProjectModuleName!)
                    .Include(x=>x.ProjectPackage!)
            };
            return base.GetAsync(filterOptions);
        }


        public Task<(ExecutionState executionState, ProjectPricingSetup entity, string message)> GetProjectPricingSetupByProjectModuleNameIdAndProjectPackageId(long ProjectModuleNameId, long ProjectPackageId)
        {
            var filterOptions = new FilterOptions<ProjectPricingSetup>
            {
                FilterExpression = x => x.ProjectModuleNameId == ProjectModuleNameId && x.ProjectPackageId ==ProjectPackageId,
                IncludeExpression = x => x
                    .Include(x => x.ProjectModuleName!)
                    .Include(x => x.ProjectPackage!)
            };
            
            return base.GetAsync(filterOptions);
        }


    }
}