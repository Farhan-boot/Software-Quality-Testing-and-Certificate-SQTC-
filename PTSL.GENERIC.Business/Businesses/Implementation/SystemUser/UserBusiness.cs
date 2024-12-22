using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.UnitOfWork;
using PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        private readonly GENERICWriteOnlyCtx _writeOnlyCtx;
        private readonly IUserRepository _userRepository;

        public UserBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx readOnlyCtx, GENERICWriteOnlyCtx writeOnlyCtx,IUserRepository userRepository)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyCtx = readOnlyCtx;
            _writeOnlyCtx = writeOnlyCtx;
            _userRepository = userRepository;
        }

        //Implement System Busniess Logic here
        public async Task<(ExecutionState executionState, User entity, string message)> UserLogin(LoginVM model)
        {
            (ExecutionState executionState, User entity, string message) returnResponse;

            //(ExecutionState executionState, User entity, string message) entityObject = await _unitOfWork.users.UserLogin(model);

            FilterOptions<User> filterOptions = new FilterOptions<User>();
            filterOptions.FilterExpression = x => x.UserEmail.ToLower() == model.UserEmail.ToLower() && x.UserPassword == model.UserPassword;

            (ExecutionState executionState, User entity, string message) entityObject = await _unitOfWork.users.GetAsync(filterOptions, RetrievalPurpose.Consumption);

            if (entityObject.entity != null)
            {
                returnResponse = entityObject;
            }
            else
            {
                returnResponse = entityObject;
            }

            return returnResponse;
        }

        //public async Task<(ExecutionState executionState, User entity, string message)> UserLists()
        //{
        //    (ExecutionState executionState, User entity, string message) returnResponse;

        //    //(ExecutionState executionState, User entity, string message) entityObject = await _unitOfWork.users.UserLogin(model);

        //    FilterOptions<User> filterOptions = new FilterOptions<User>();
        //    filterOptions.FilterExpression = x => x.IsActive == true && x.IsDeleted == false;

        //    (ExecutionState executionState, User entity, string message) entityObject = await _unitOfWork.users.GetAsync(filterOptions, RetrievalPurpose.Consumption);

        //    if (entityObject.entity != null)
        //    {
        //        returnResponse = entityObject;
        //    }
        //    else
        //    {
        //        returnResponse = entityObject;
        //    }

        //    return returnResponse;
        //}

        public async override Task<(ExecutionState executionState, IQueryable<User> entity, string message)> List(QueryOptions<User> queryOptions = null)
        {

            (ExecutionState executionState, IQueryable<User> entity, string message) returnResponse;

            queryOptions = new QueryOptions<User>();
            queryOptions.IncludeExpression = x => x.Include(i => i.UserRole!);
            queryOptions.SortingExpression = x => x.OrderByDescending(x=>x.Id);
            (ExecutionState executionState, IQueryable<User> entity, string message) entityObject = await _unitOfWork.List<User>(queryOptions);
            returnResponse = entityObject;

            return returnResponse;
        }

        public async  override Task<(ExecutionState executionState, User entity, string message)> CreateAsync(User entity)
        {
            (ExecutionState executionState, User entity, string message) createResponse;
            FilterOptions<User> filterOptions = new FilterOptions<User>();
            filterOptions.FilterExpression = x => x.UserEmail.Trim() == entity.UserEmail.Trim();
            (ExecutionState executionState, string message) entityObject = await _unitOfWork.DoesExistAsync(filterOptions);
            if (entityObject.executionState.ToString() == "Success" || entity.UserEmail.Trim() == "")
            {
                createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Email already exists.")!;
                return createResponse;
            }
            (ExecutionState executionState, User entity, string message) createdResponse = await base.CreateAsync(entity);
            createdResponse.message = " New user added successfully.";
            return createdResponse;
        }

        public async Task<(ExecutionState executionState, User entity, string message)> Getuser(UserRegisterModel registration)
        {
            (ExecutionState executionState, User entity, string message) createResponse;

            try
            {
                createResponse.message = "No user found";
                createResponse.entity = null;
                createResponse.executionState = ExecutionState.Failure;
                if (!string.IsNullOrEmpty(registration.Email))
                {
                    FilterOptions<User> filterOptions = new FilterOptions<User>();
                    filterOptions.FilterExpression = x => x.UserEmail.ToLower() == registration.Email.ToLower();
                    (ExecutionState executionState, User entity, string message) entityObject = await _unitOfWork.users.GetAsync(filterOptions);
                    createResponse = entityObject;


                }
                else
                {
                    return createResponse;
                }
                return createResponse;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //GetUserNameByUserRoleId
        public async Task<(ExecutionState executionState, List<User> entity, string message)> GetUserNameByUserRoleId(long userRoleId)
        {
            try
            {
                var query = _readOnlyCtx.Set<User>()
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsQueryable();

                //Extra Filter
                if (query != null)
                {
                    query = query.Where(x => x.UserRoleId == (long)userRoleId);
                }

                query = query?.OrderByDescending(x => x.Id);

                var result = await query
                    .ToListAsync();

                return (ExecutionState.Retrieved, result, "Data returned successfully.");
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, new List<User>()!, "Unexpected error occurred.");
            }
        }



        /*
        private static readonly Random random = new();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        */

        public async Task<(ExecutionState executionState, List<User> entity, string message)> GetUserInfoByUserRoleId(long userRoleId)
        {
            var result = await _readOnlyCtx.Set<User>().Where(x => x.UserRoleId == userRoleId)
                .Include(x=>x.UserRole)
                .ToListAsync();

            return (ExecutionState.Success, result, "Ok");
        }

        public async Task<(ExecutionState execution, IList<User> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone)
        {
            var result =await _userRepository.Search(userRoleId, userName, firstName, email, userPhone);
            return result;
        }
    }
}
