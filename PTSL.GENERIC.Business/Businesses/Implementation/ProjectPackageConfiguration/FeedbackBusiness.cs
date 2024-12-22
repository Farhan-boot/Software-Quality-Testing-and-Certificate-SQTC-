using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.ProjectPackageConfiguration
{
    public class FeedbackBusiness : BaseBusiness<Feedback>, IFeedbackBusiness
    {
        public FeedbackBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Task<(ExecutionState executionState, IQueryable<Feedback> entity, string message)> List(QueryOptions<Feedback> queryOptions = null)
        {
            return base.List(new QueryOptions<Feedback>()
            {
                IncludeExpression = e => e.Include(x => x.ProjectRequest!).ThenInclude(x=>x.Client!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

        public override Task<(ExecutionState executionState, Feedback entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<Feedback>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.ProjectRequest!)
            };
            return base.GetAsync(filterOptions);
        }

    }
}