using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.ProjectPackageConfiguration
{
    public class ProjectPackageBusiness : BaseBusiness<ProjectPackage>, IProjectPackageBusiness
    {
        public ProjectPackageBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Task<(ExecutionState executionState, IQueryable<ProjectPackage> entity, string message)> List(QueryOptions<ProjectPackage> queryOptions = null)
        {
            return base.List(new QueryOptions<ProjectPackage>()
            {
                IncludeExpression = e => e.Include(x => x.ProjectModuleName!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

        public override Task<(ExecutionState executionState, ProjectPackage entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<ProjectPackage>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.ProjectModuleName!)
            };
            return base.GetAsync(filterOptions);
        }


        public async Task<(ExecutionState executionState, IQueryable<ProjectPackage> entity, string message)> GetProjectPackageByProjectModuleNameId(long ProjectModuleNameId)
        {
            var queryOptions = new QueryOptions<ProjectPackage>();
            queryOptions.FilterExpression = e => e.ProjectModuleNameId == ProjectModuleNameId;
            return await base.List(queryOptions);
        }

    }
}