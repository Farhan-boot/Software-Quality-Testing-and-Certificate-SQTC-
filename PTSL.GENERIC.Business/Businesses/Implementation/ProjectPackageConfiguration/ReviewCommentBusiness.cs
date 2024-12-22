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
    public class ReviewCommentBusiness : BaseBusiness<ReviewComment>, IReviewCommentBusiness
    {
        public ReviewCommentBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Task<(ExecutionState executionState, IQueryable<ReviewComment> entity, string message)> List(QueryOptions<ReviewComment> queryOptions = null)
        {
            return base.List(new QueryOptions<ReviewComment>()
            {
                IncludeExpression = e => e.Include(x => x.ProjectRequest!).Include(x => x.TaskOfProject!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }


        public override Task<(ExecutionState executionState, ReviewComment entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<ReviewComment>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.ProjectRequest!)
                    .Include(x => x.TaskOfProject!)
            };
            return base.GetAsync(filterOptions);
        }


    }
}