using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PTSL.GENERIC.Service.BaseServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">This is a Entity View Model</typeparam>
    /// <typeparam name="TEntity">This is a Entity</typeparam>
    public abstract class BaseService<T, TEntity> : IBaseService<T, TEntity>
    where T : BaseModel, new() where TEntity : BaseEntity
    {
        public readonly IBaseBusiness<TEntity> Business;
        public BaseService(IBaseBusiness<TEntity> business)
        {
            Business = business;
        }

        public virtual async Task<(ExecutionState executionState, IList<T> entity, string message)> List(QueryOptions<T> queryOptions = null)
        {
            (ExecutionState executionState, IList<T> entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, IQueryable<TEntity> entity, string message) Getentity = await Business.List();

                if (Getentity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: Getentity.executionState, entity: CastEntityToModel(Getentity.entity), message: Getentity.message);
                }
                else
                {
                    returnResponse = (executionState: Getentity.executionState, entity: null, message: Getentity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T model)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;


            //var re =  Request;
            //var xyz = Context..HttpContext.Request?.Headers["Basic"];
            //var headers = re.Headers;

            //if (headers.Contains("Custom"))
            //{
            //    string token = headers.GetValues("Custom").First();
            //}

            try
            {
                TEntity entity = CastModelToEntity(model);
                //entity.CreatedBy = 1;
                (ExecutionState executionState, TEntity entity, string message) createEntity = await Business.CreateAsync(entity);

                if (createEntity.executionState == ExecutionState.Created)
                {
                    returnResponse = (executionState: createEntity.executionState, entity: CastEntityToModel(createEntity.entity), message: createEntity.message);
                }
                else
                {
                    returnResponse = (executionState: createEntity.executionState, entity: model, message: createEntity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: model, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TEntity entity, string message) entity = await Business.GetAsync(key);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: null, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(FilterOptions<T> filterOptions)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TEntity entity, string message) entity = await Business.GetAsync(null);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: null, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(long key)
        {
            (ExecutionState executionState, string message) returnResponse;
            try
            {
                (ExecutionState executionState, string message) entity = await Business.DoesExistAsync(key);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(FilterOptions<T> filterOptions)
        {
            (ExecutionState executionState, string message) returnResponse;
            try
            {
                (ExecutionState executionState, string message) entity = await Business.DoesExistAsync(null);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T model)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                TEntity modelToEntity = CastModelToEntity(model);

                (ExecutionState executionState, TEntity entity, string message) entity = await Business.UpdateAsync(modelToEntity);

                if (entity.executionState == ExecutionState.Updated)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: model, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: model, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TEntity entity, string message) entity = await Business.RemoveAsync(key);

                if (entity.executionState == ExecutionState.Success)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: null, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(CountOptions<T> countOptions = null)
        {
            (ExecutionState executionState, long entityCount, string message) returnResponse;
            try
            {
                (ExecutionState executionState, long entityCount, string message) entity = await Business.CountAsync(null);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, entity.entityCount, message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity.entityCount, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, 0, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TEntity entity, string message) entity = await Business.MarkAsActiveAsync(key);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: null, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TEntity entity, string message) entity = await Business.MarkAsInactiveAsync(key);

                if (entity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: entity.executionState, entity: CastEntityToModel(entity.entity), message: entity.message);
                }
                else
                {
                    returnResponse = (executionState: entity.executionState, entity: null, message: entity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
            // throw new NotImplementedException();
        }

        public abstract TEntity CastModelToEntity(T model);
        public abstract T CastEntityToModel(TEntity entity);
        public abstract IList<T> CastEntityToModel(IQueryable<TEntity> entity);
        // public abstract IList<T> CastEntityToModelList(IQueryable<TEntity> entity);

        public virtual Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, long userId)
        {
            return Business.SoftDeleteAsync(key, userId);
        }
    }
}
