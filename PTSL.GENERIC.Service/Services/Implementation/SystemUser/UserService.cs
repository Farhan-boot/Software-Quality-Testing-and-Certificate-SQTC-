using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.TokenHelper;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.SystemUser;
using PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation
{
    public class UserService : BaseService<UserVM, User>, IUserService
    {
        public readonly IUserBusiness _userBusiness;
        public IMapper _mapper;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IGenerateJSONWebToken _generateJSONWebToken;

        public UserService(IUserBusiness userBusiness, IMapper mapper, IRefreshTokenService refreshTokenService, IGenerateJSONWebToken generateJSONWebToken) : base(userBusiness)
        {
            _userBusiness = userBusiness;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
            _generateJSONWebToken = generateJSONWebToken;
        }

        //Implement System Busniess Logic here

        public async Task<(ExecutionState executionState, UserVM entity, string refreshToken, string message)> UserLogin(LoginVM model)
        {
            var (executionState, entity, message) = await _userBusiness.UserLogin(model);
            if (executionState == ExecutionState.Retrieved)
            {
                var tokenResult = await _refreshTokenService.CreateAsync(entity!.Id, model.RememberMe, _generateJSONWebToken.GenerateRefreshToken());
                return (executionState, CastEntityToModel(entity), tokenResult.entity.Token, message);
            }

            return (executionState, null, null, message);
        }

        //public async Task<(ExecutionState executionState, List<UserVM> entity, string message)> UserLists()
        //{
        //    (ExecutionState executionState, IList<UserVM> entity, string message) returnResponse;
        //    try
        //    {
        //        (ExecutionState executionState, List<User> entity, string message) recieveEntity = await _userBusiness.UserLists();

        //        if (recieveEntity.executionState == ExecutionState.Retrieved)
        //        {
        //            returnResponse = (executionState: recieveEntity.executionState, entity: CastEntityToModel(IQueryable<recieveEntity.entity>), message: recieveEntity.message);
        //        }
        //        else
        //        {
        //            returnResponse = (executionState: recieveEntity.executionState, entity: null, message: recieveEntity.message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
        //    }

        //    return returnResponse;
        //}

        public override User CastModelToEntity(UserVM model)
        {
            try
            {
                return _mapper.Map<User>(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override UserVM CastEntityToModel(User entity)
        {
            try
            {
                UserVM model = _mapper.Map<UserVM>(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override IList<UserVM> CastEntityToModel(IQueryable<User> entity)
        {
            try
            {
                IList<UserVM> colorList = _mapper.Map<IList<UserVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<(ExecutionState executionState, UserVM? entity, string message)> Register(UserRegisterModel model)
        //{
        //    (ExecutionState executionState, User? entity, string message) result = await _userBusiness.Register(model);
        //    if (result.executionState == ExecutionState.Success)
        //    {
        //        return (result.executionState, _mapper.Map<UserVM>(result.entity), result.message);
        //    }

        //    return (result.executionState, null, result.message);
        //}


        public List<UserVM> CastEntityListToModel(List<User> entity)
        {
            return _mapper.Map<List<UserVM>>(entity);
        }

        public async Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetUserNameByUserRoleId(long userRoleId)
        {
            var result = await _userBusiness.GetUserNameByUserRoleId(userRoleId);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<UserVM>(), result.message);
        }

        public async Task<(ExecutionState executionState, UserVM entity, string message)> Getuser(UserRegisterModel model)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, User entity, string message) recieveEntity = await _userBusiness.Getuser(model);

                if (recieveEntity.executionState == ExecutionState.Retrieved)
                {
                    returnResponse = (executionState: recieveEntity.executionState, entity: CastEntityToModel(recieveEntity.entity), message: recieveEntity.message);
                }
                else
                {
                    returnResponse = (executionState: recieveEntity.executionState, entity: null, message: recieveEntity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return returnResponse;
        }

        public async Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetUserInfoByUserRoleId(long userRoleId)
        {
            var result = await _userBusiness.GetUserInfoByUserRoleId(userRoleId);

            return (result.executionState, _mapper.Map<List<UserVM>>(result.entity), result.message);
        }

        public async Task<(ExecutionState execution, IList<UserVM> entity, string message)> Search(long? userRoleId, string userName, string firstName, string email, string userPhone)
        {
            var result= await _userBusiness.Search(userRoleId, userName, firstName, email, userPhone);
            return (ExecutionState.Success,_mapper.Map<IList<UserVM>>(result.entity),"Success");
        }
    }
}
