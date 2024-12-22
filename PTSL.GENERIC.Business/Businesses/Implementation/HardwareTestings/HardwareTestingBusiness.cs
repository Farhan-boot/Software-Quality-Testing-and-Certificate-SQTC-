using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings;
using PTSL.GENERIC.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation.HardwareTestings
{
    public class HardwareTestingBusiness : BaseBusiness<HardwareTesting>, IHardwareTestingBusiness
    {
        private readonly IHardwareTestingRepository _repository;
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public HardwareTestingBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext,IHardwareTestingRepository HardwareTestingRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyContext = readOnlyContext;
            _repository = HardwareTestingRepository;
        }


        public override async Task<(ExecutionState executionState, string message)> DoesExistAsync(long id)
        {
            (ExecutionState executionState, string message) returnResponse;

            FilterOptions<HardwareTesting> filterOptions = new FilterOptions<HardwareTesting>();
            filterOptions.FilterExpression = x => x.Id == id;
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }
        public async override Task<(ExecutionState executionState, IQueryable<HardwareTesting> entity, string message)> List(QueryOptions<HardwareTesting> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<HardwareTesting> entity, string message) returnResponse;
            var queryOption = new QueryOptions<HardwareTesting>();
            queryOption.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!).Include(x=>x.TestScope!);
            
            (ExecutionState executionState, IQueryable<HardwareTesting> entity, string message) entityObject = await _unitOfWork.List<HardwareTesting>(queryOption);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, HardwareTesting entity, string message)> CreateListOfHardwareTesting(List<HardwareTesting> entityList)
        {
            (ExecutionState executionState,HardwareTesting entity, string message) createResponse = (executionState: ExecutionState.Created, entity: null, message: $"List Of Item Created");
            
            foreach (HardwareTesting HardwareTesting in entityList)
            {

                (ExecutionState executionState, HardwareTesting entity, string message) createdResponse = await base.CreateAsync(HardwareTesting);
                
            }
            return createResponse;

        }

        public async Task<(ExecutionState executionState, IList<HardwareTesting> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestScopeId, string? SubItem)
        {
            var result = await _repository.Search(ProjectRequestId, TaskOfProjectId, TestScopeId, SubItem);
            return result;
        }

        public async override Task<(ExecutionState executionState, HardwareTesting entity, string message)> GetAsync(long key)
        {
            FilterOptions<HardwareTesting> filterOptions = new FilterOptions<HardwareTesting>();
            filterOptions.FilterExpression = x => x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(y => y.TaskOfProject!)
            .Include(x => x.ProjectRequest!).Include(x => x.TestScope!);
            var entityObject = await base.GetAsync(filterOptions);
            return entityObject;
        }
    }
}
