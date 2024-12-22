using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.ProjectPackageConfiguration
{
    public class PaymentCalculationHeaderBusiness : BaseBusiness<PaymentCalculationHeader>, IPaymentCalculationHeaderBusiness
    {
        private readonly GENERICReadOnlyCtx _gENERICReadOnlyCtx;
        public PaymentCalculationHeaderBusiness(GENERICUnitOfWork unitOfWork,GENERICReadOnlyCtx gENERICReadOnlyCtx)
            : base(unitOfWork)
        {
            _gENERICReadOnlyCtx = gENERICReadOnlyCtx;
        }

        public async override Task<(ExecutionState executionState, IQueryable<PaymentCalculationHeader> entity, string message)> List(QueryOptions<PaymentCalculationHeader> queryOptions = null)
        {
            return await base.List(new QueryOptions<PaymentCalculationHeader>()
            {
                IncludeExpression = e => e.Include(x => x.ProjectRequest!).ThenInclude(y=>y.Client!).Include(x => x.TaskOfProject!)
                .Include(x=>x.PaymentInformation!).ThenInclude(y=>y.Reconciliations!),
                SortingExpression = x => x.OrderByDescending(y => y.Id)
            });
        }

        public override Task<(ExecutionState executionState, PaymentCalculationHeader entity, string message)> GetAsync(long id)
        {
            var filterOptions = new FilterOptions<PaymentCalculationHeader>
            {
                FilterExpression = x => x.Id == id,
                IncludeExpression = x => x
                    .Include(x => x.ProjectRequest!)
                    .Include(x => x.TaskOfProject!)
                    .Include(x => x.PaymentCalculationRows!)
                    .ThenInclude(x=>x.ProjectModuleName!)
                    .Include(x => x.PaymentCalculationRows!)
                    .ThenInclude(x => x.ProjectPackage!)
            };
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, IList<PaymentCalculationHeader> entity, string message)> ListByClientId(long clientId)
        {
            var projects = await _gENERICReadOnlyCtx.Set<ProjectRequest>()
                .Where(x => x.ClientId == clientId).Select(x => x.Id).ToListAsync();
            var result =await _gENERICReadOnlyCtx.Set<PaymentCalculationHeader>().
                Where(x=> projects.Contains(x.ProjectRequestId.Value))
                .Include(x => x.ProjectRequest!)
                .ThenInclude(y => y.Client!).Include(x => x.TaskOfProject!)
                .Include(x => x.PaymentInformation!).ThenInclude(y => y.Reconciliations!).OrderByDescending(x=>x.Id).ToListAsync();
            return (ExecutionState.Success, result, "Data Found");
        }
    }
}