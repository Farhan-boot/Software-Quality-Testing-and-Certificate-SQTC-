using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using PTSL.GENERIC.Common.Entity;
using System.Web;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Business.Businesses.Implementation.PermissionSettings
{
    public class PermissionHeaderSettingsBusiness : BaseBusiness<PermissionHeaderSettings>, IPermissionHeaderSettingsBusiness
    {
        private readonly GENERICReadOnlyCtx _readOnlyContext;
        public readonly GENERICUnitOfWork _unitOfWork;
        public PermissionHeaderSettingsBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyContext)
            : base(unitOfWork)
        {
            _readOnlyContext = readOnlyContext;
            _unitOfWork = unitOfWork;
        }

        public async override Task<(ExecutionState executionState, IQueryable<PermissionHeaderSettings> entity, string message)> List(QueryOptions<PermissionHeaderSettings> queryOptions = null)
        {
            //return base.List(new QueryOptions<PermissionHeaderSettings>()
            //{
            //    IncludeExpression = e => e.Include(x => x.UserRole!)
            //    .Include(x=>x.PermissionRowSettings!)
            //    .Include(x => x.User!),
            //    SortingExpression = x => x.OrderByDescending(y => y.Id)
            //});
            (ExecutionState executionState, IQueryable<PermissionHeaderSettings> entity, string message) returnResponse;

            queryOptions = new QueryOptions<PermissionHeaderSettings>();
            queryOptions.IncludeExpression = x => x.Include(i => i.UserRole)
            .Include(i=>i.PermissionRowSettings!)
            .Include(i => i.User!);

            (ExecutionState executionState, IQueryable<PermissionHeaderSettings> entity, string message) entityObject = await _unitOfWork.List<PermissionHeaderSettings>(queryOptions);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, List<PermissionHeaderSettings> entity, string message)> GetPermissionHeaderSettingsByModuleEnumId(long moduleEnumId)
        {
            try
            {
                var query = _readOnlyContext.Set<PermissionHeaderSettings>()
                    .Where(x =>((long)x.ModuleEnumId!) == moduleEnumId && x.IsDeleted==false && x.IsActive==true)
                    .Include(x=>x.PermissionRowSettings!.Where(x=>x.IsActive == true && x.IsDeleted==false))
                    .ThenInclude(x=>x.UserRole).ThenInclude(y=>y!.Users)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();

                    //if (query != null)
                    //{
                    //    query = query.Where(x => (long)x.AccesslistId  == moduleEnumId);
                    //}

                  //query = query.Include(y => y.Ngo).OrderByDescending(x => x.Id);

                    var result = await query
                        .ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<PermissionHeaderSettings>()!, "Unexpected error occurred.");
            }
        }

        public async override Task<(ExecutionState executionState, PermissionHeaderSettings entity, string message)> GetAsync(long key)
        {
            (ExecutionState executionState, PermissionHeaderSettings entity, string message) returnResponse;
            var filterOptions = new FilterOptions< PermissionHeaderSettings >();
            filterOptions.FilterExpression = x=>x.Id == key;
            filterOptions.IncludeExpression = x => x.Include(i => i.PermissionRowSettings!)
            .ThenInclude(y=>y.UserRole!);
            (ExecutionState executionState, PermissionHeaderSettings entity, string message) entityObject = await _unitOfWork.GetAsync(filterOptions);
            returnResponse = entityObject;
            return returnResponse;
        }

        //public async Task<(ExecutionState executionState, long data, string message)> GetPermissionHeaderIdByControllerName(string controller)
        //{
        //    try
        //    {
        //        controller = HttpUtility.UrlDecode(controller).Trim('/').ToUpper();
        //        var accessId = await _readOnlyContext.Set<PermissionHeaderSettings>()
        //            .Where(x => x.Accesslist!.ControllerName.ToUpper().Equals(controller))
        //            .Select(x => x.Id)
        //            .FirstOrDefaultAsync();

        //        return (accessId == default ? ExecutionState.Failure : ExecutionState.Success, accessId, "Success");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (ExecutionState.Failure, 0, "Failed");
        //    }
        //}

        public override async Task<(ExecutionState executionState, PermissionHeaderSettings entity, string message)> CreateAsync(PermissionHeaderSettings entity)
        {
            (ExecutionState executionState, PermissionHeaderSettings entity, string message) createResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    FilterOptions<PermissionHeaderSettings> filterOptions = new FilterOptions<PermissionHeaderSettings>();
                    filterOptions.FilterExpression = x => x.ModuleEnumId == entity.ModuleEnumId;
                    (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
                    if (entityObject.executionState.ToString() == "Success")
                    {
                        createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Permission already exists for this module.");
                        return createResponse;
                    }


                    (ExecutionState executionState, PermissionHeaderSettings entity, string message) createdResponse = await UoW.CreateAsync<PermissionHeaderSettings>(entity);

                    if (createdResponse.executionState == ExecutionState.Failure)
                    {
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
                        {
                            UoW.Complete(transaction, CompletionState.Failure);
                        }

                        createResponse = createdResponse;
                    }
                    else
                    {
                        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                        bool success = (saveResponse.executionState == ExecutionState.Success);

                        #region Post validation
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                        {
                            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                            createResponse = success ? createdResponse :
                                        (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        }
                        else
                        {
                            createResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on module permission creation.");
                }
            }
            //}

            return createResponse;
        }
    }
}