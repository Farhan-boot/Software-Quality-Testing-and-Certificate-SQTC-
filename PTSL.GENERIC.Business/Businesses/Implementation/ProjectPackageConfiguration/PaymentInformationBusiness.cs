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
    public class PaymentInformationBusiness : BaseBusiness<PaymentInformation>, IPaymentInformationBusiness
    {
        public PaymentInformationBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Task<(ExecutionState executionState, IQueryable<PaymentInformation> entity, string message)> List(QueryOptions<PaymentInformation> queryOptions = null)
        {
            return base.List(new QueryOptions<PaymentInformation>()
            {
                IncludeExpression = e => e.Include(x => x.PaymentCalculationHeader!).ThenInclude(x=>x.ProjectRequest!).ThenInclude(x=>x.Client!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

        public override Task<(ExecutionState executionState, PaymentInformation entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<PaymentInformation>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.DepositSlipFiles!)
                    .Include(x => x.PaymentCalculationHeader!)
                    .ThenInclude(x => x.ProjectRequest!)
            };
            return base.GetAsync(filterOptions);
        }
    }
}