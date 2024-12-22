using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;

namespace PTSL.GENERIC.Business.Businesses.Implementation.PermissionSettings
{
    public class PermissionRowSettingsBusiness : BaseBusiness<PermissionRowSettings>, IPermissionRowSettingsBusiness
    {
        private readonly GENERICReadOnlyCtx _readOnlyCtx;

        public PermissionRowSettingsBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyCtx)
            : base(unitOfWork)
        {
            _readOnlyCtx = readOnlyCtx;
        }

        public override Task<(ExecutionState executionState, IQueryable<PermissionRowSettings> entity, string message)> List(QueryOptions<PermissionRowSettings> queryOptions = null)
        {
            return base.List(new QueryOptions<PermissionRowSettings>()
            {
                IncludeExpression = e => e.Include(x => x.User!)
                .Include(x => x.UserRole!),
                SortingExpression = x => x.OrderBy(y => y.OrderId)
            });
        }

        public async Task<(ExecutionState executionState, List<PermissionRowSettings> data, string message)> GetPermissionRowsByControllerName(string controller)
        {
            try
            {
                controller = HttpUtility.UrlDecode(controller).Trim('/').ToUpper();

                var result = await _readOnlyCtx.Set<PermissionHeaderSettings>()
                    //.Where(x => x.Accesslist!.ControllerName.ToUpper().Equals(controller))
                    .Select(x => x.PermissionRowSettings)
                    .FirstOrDefaultAsync();

                if (result is null || result.Count == 0)
                {
                    return (ExecutionState.Failure, new List<PermissionRowSettings>(), "No data found");
                }

                return (ExecutionState.Success, result!, "Success");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<PermissionRowSettings>(), "Failed");
            }
        }

        public async Task<(ExecutionState executionState, List<PermissionRowSettings> entity, string message)> GetRowListByHeaderAscending(long permissionHeaderSettingsId)
        {
            var result = await _readOnlyCtx.Set<PermissionRowSettings>()
                .Where(x => x.PermissionHeaderSettingsId == permissionHeaderSettingsId)
                .OrderBy(x => x.OrderId)
                .ToListAsync();
            if (result.Count == 0)
            {
                return (ExecutionState.Failure, result, "No data found");
            }

            return (ExecutionState.Success, result, "Success");
        }
    }
}