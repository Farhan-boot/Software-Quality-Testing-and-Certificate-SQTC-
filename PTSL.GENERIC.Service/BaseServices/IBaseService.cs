using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.BaseServices
{
    public interface IBaseService<T, TEntity> where T : BaseModel, new() where TEntity : BaseEntity
    {
        Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T entity);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(long key);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(FilterOptions<T> filterOptions);
        Task<(ExecutionState executionState, IList<T> entity, string message)> List(QueryOptions<T> queryOptions = null);
        Task<(ExecutionState executionState, string message)> DoesExistAsync(long key);
        Task<(ExecutionState executionState, string message)> DoesExistAsync(FilterOptions<T> filterOptions);
        Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T entity);
        Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long key);
        Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(CountOptions<T> countOptions = null);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long key);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long key);
        Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, long userId);

        TEntity CastModelToEntity(T model);
        T CastEntityToModel(TEntity entity);
        IList<T> CastEntityToModel(IQueryable<TEntity> entity);
        //IList<T> CastEntityToModelList(IQueryable<TEntity> entity);
    }
}
