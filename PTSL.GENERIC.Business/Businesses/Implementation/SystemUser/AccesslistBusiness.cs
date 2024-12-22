using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class AccesslistBusiness : BaseBusiness<Accesslist>, IAccesslistBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyCtx;

        public AccesslistBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyCtx)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyCtx = readOnlyCtx;
        }

        public Task<(ExecutionState executionState, IQueryable<Accesslist> entity, string message)> ListForApproval()
        {
            return base.List(new QueryOptions<Accesslist>
            {
                SortingExpression = e => e.OrderByDescending(x => x.Id),
                FilterExpression = e => e.IsAvailableForApproval == true
            });
        }

        public async Task<(ExecutionState executionState, long data, string message)> GetAccessListId(string accessUrl)
        {
            accessUrl = HttpUtility.UrlDecode(accessUrl).Trim('/');
            var split = accessUrl.Split("/");
            if (split.Length != 2)
            {
                return (ExecutionState.Failure, 0, "Invalid Access URL");
            }

            var controllerName = split.First().ToUpper();
            var actionName = split.Last().ToUpper();
            var accessId = await _readOnlyCtx.Set<Accesslist>()
                .Where(x => x.ControllerName.ToUpper().Equals(controllerName) && x.ActionName.ToUpper().Equals(actionName))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return (accessId == default ? ExecutionState.Failure : ExecutionState.Success, accessId, "Success");
        }

        public async override Task<(ExecutionState executionState, IQueryable<Accesslist> entity, string message)> List(QueryOptions<Accesslist> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<Accesslist> entity, string message) returnResponse;

            queryOptions = new QueryOptions<Accesslist>();
            queryOptions.IncludeExpression = x => x.Include(i => i.Module!);
            queryOptions.SortingExpression = x => x.OrderByDescending(x => x.Id);
            (ExecutionState executionState, IQueryable<Accesslist> entity, string message) entityObject = await _unitOfWork.List<Accesslist>(queryOptions);
            returnResponse = entityObject;

            return returnResponse;
        }
    }
}
