using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.TokenHelper;
//using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Api.Controllers.SystemUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<IUserService, UserVM, User>
    {
        private readonly IUserService _userService;
        private readonly IUserBusiness _business;
        private IConfiguration _config;
        private readonly IGenerateJSONWebToken _generateJSONWebToken;

        public AccountController(IUserService userService, IUserBusiness business, IConfiguration config, IGenerateJSONWebToken generateJSONWebToken) :
            base(userService)
        {
            _config = config;
            _generateJSONWebToken = generateJSONWebToken;
            _userService = userService;
            _business = business;
        }

        //Implement here new api, if we want.

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<LoginResultVM>>> Login([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var (executionState, entity, refreshToken, message) = await _userService.UserLogin(model);

            (ExecutionState executionState, LoginResultVM entity, string message) responseResult;

            if (executionState == ExecutionState.Retrieved)
            {
                var accessToken = _generateJSONWebToken.GetToken(entity, model.RememberMe);
                var loginResult = new LoginResultVM
                {
                    UserId = entity.Id,
                    UserEmail = entity.UserEmail,
                    UserName = entity.UserName,
                    RoleName = entity.RoleName,
                   

                    Token = accessToken,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,

                    UserRoleId = entity.UserRoleId
                };
                //HttpContext.Session.SetString("UserId", loginResult.UserId.ToString());
                responseResult.entity = loginResult;
                responseResult.message = message;
                responseResult.executionState = executionState;
                WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);
                return Ok(apiResponse);
            }
            else
            {
                responseResult.entity = null;
                responseResult.message = message;
                responseResult.executionState = executionState;

                WebApiResponse<LoginResultVM> apiResponse = new WebApiResponse<LoginResultVM>(responseResult);
                return NotFound(apiResponse);
            }
        }

        [HttpGet("UserLists")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<UserDropdownVM>>> UserLists()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            (ExecutionState executionState, IList<UserVM> entity, string message) result = await _userService.List();

            (ExecutionState executionState, List<UserDropdownVM> entity, string message) responseResult;

            if (result.executionState == ExecutionState.Retrieved)
            {
                List<UserDropdownVM> users = new List<UserDropdownVM>();
                foreach (var data in result.entity)
                {
                    UserDropdownVM userDropdownVM = new UserDropdownVM();
                    userDropdownVM.Id = data.Id;
                    userDropdownVM.UserEmail = data.UserEmail;
                    userDropdownVM.UserName = data.UserName;
                    users.Add(userDropdownVM);

                }
                responseResult.entity = users;
                responseResult.message = result.message;
                responseResult.executionState = result.executionState;
                WebApiResponse<List<UserDropdownVM>> apiResponse = new WebApiResponse<List<UserDropdownVM>>(responseResult);
                return Ok(apiResponse);
            }
            else
            {
                responseResult.entity = null;
                responseResult.message = result.message;
                responseResult.executionState = result.executionState;

                WebApiResponse<List<UserDropdownVM>> apiResponse = new WebApiResponse<List<UserDropdownVM>>(responseResult);
                return NotFound(apiResponse);
            }
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WebApiResponse<LoginResultVM>>> Register([FromBody] UserRegisterModel model)
        {
            //TODO: Admin Registration will need separate api for security
            
                (ExecutionState executionState, UserRegisterModel entity, string message) responseResult;

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

            if (!string.IsNullOrEmpty(model.Email))
            {
                (ExecutionState executionState, UserVM entity, string message) userresult = await _userService.Getuser(model);
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                long rndm = _rdm.Next(_min, _max);

                if (userresult.executionState == ExecutionState.Failure)
                {
                    UserVM userVM = new UserVM();
                    userVM.UserName = model.UserName;
                    userVM.UserPassword = model.Password;
                    userVM.UserStatus = true;
                    userVM.UserType = UserType.SQTC_Admin;
                    userVM.UserEmail = model.Email;

                    (ExecutionState executionState, UserVM entity, string message) createUser = await _userService.CreateAsync(userVM);
                    if(createUser.executionState == ExecutionState.Created)
                    {
                        responseResult.message = "User Registered Successfully";
                        responseResult.entity = model;
                        responseResult.executionState = ExecutionState.Success;
                        WebApiResponse<UserRegisterModel> apiResponse = new WebApiResponse<UserRegisterModel>(responseResult);

                        return Ok(apiResponse);
                    }
                }
                else
                {
                    responseResult.message = "User already exists";
                    responseResult.entity = null;
                    responseResult.executionState = ExecutionState.Failure;
                    WebApiResponse<UserRegisterModel> apiResponse = new WebApiResponse<UserRegisterModel>(responseResult);

                    return Ok(apiResponse);
                }

            }
            return NotFound();

        }

        public override async Task<ActionResult<WebApiResponse<UserVM>>> UpdateAsync([FromBody] UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userIdJwt = HttpContext.User.FindFirst("UserId")?.Value;
            _ = long.TryParse(userIdJwt, out var userId);

            model.ModifiedBy = userId;
            model.UpdatedAt = DateTime.Now;

            var result = await _userService.UpdateAsync(model);
            var apiResponse = new WebApiResponse<UserVM>(result);

            return Ok(apiResponse);
        }

        [HttpGet("GetUserInfoByUserRoleId/{userRoleId}")]
        public async Task<ActionResult<WebApiResponse<List<UserVM>>>> GetUserInfoByUserRoleId(long userRoleId)
        {
            try
            {
                var (executionState, entity, message) = await _userService.GetUserInfoByUserRoleId(userRoleId);

                return Ok(new WebApiResponse<List<UserVM>>(
                        (executionState, entity, message)
                    ));
            }
            catch
            {
                return StatusCode(500, new WebApiResponse<List<UserVM>>(
                        (ExecutionState.Failure, "Unexpected error occurred")
                    ));
            }
        }


        public async override Task<ActionResult<WebApiResponse<UserVM>>> CreateAsync([FromBody] UserVM model)
        {
            try
            {
                var (executionState, entity, message) = await _userService.CreateAsync(model);

                return Ok(new WebApiResponse<UserVM>(
                        (executionState, entity, message)
                    ));
            }
            catch
            {
                return StatusCode(500, new WebApiResponse<UserVM>(
                        (ExecutionState.Failure, "Unexpected error occurred")
                    ));
            }
        }

        //[HttpGet("Search")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<WebApiResponse<IList<UserVM>>>> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone)
        //{
        //    try
        //    {
        //        var (executionState, entity, message) = await _userService.Search(userRoleId, userName, firstName, email, userPhone);

        //        return Ok(new WebApiResponse<IList<UserVM>>(
        //                (executionState, entity, message)
        //            ));
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, new WebApiResponse<List<UserVM>>(
        //                (ExecutionState.Failure, null, "Unexpected error occurred")
        //            ));
        //    }
        //}
    }
}
