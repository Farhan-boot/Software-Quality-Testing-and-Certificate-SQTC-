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
    public class ReconciliationBusiness : BaseBusiness<Reconciliation>, IReconciliationBusiness
    {
        public ReconciliationBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Task<(ExecutionState executionState, IQueryable<Reconciliation> entity, string message)> List(QueryOptions<Reconciliation> queryOptions = null)
        {
            return base.List(new QueryOptions<Reconciliation>()
            {
                IncludeExpression = e => e.Include(x => x.PaymentInformation!).ThenInclude(x => x.PaymentCalculationHeader!).ThenInclude(x=>x.ProjectRequest!).ThenInclude(x=>x.Client!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

    }
}