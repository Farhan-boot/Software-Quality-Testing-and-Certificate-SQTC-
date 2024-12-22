using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;

using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class ModuleBusiness : BaseBusiness<Module>, IModuleBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public ModuleBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<(ExecutionState executionState, IQueryable<Module> entity, string message)> List(QueryOptions<Module> queryOptions = null)
        {
            return base.List(new QueryOptions<Module>
            {
                IncludeExpression = e => e.Include(x => x.AccessList!)
            });
        }
    }
}
