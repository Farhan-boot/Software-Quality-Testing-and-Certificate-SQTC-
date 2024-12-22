using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Api.Helpers;
using PTSL.GENERIC.Api.Helpers.Authorize;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Service.Services.Interface;

namespace PTSL.GENERIC.Api.Controllers.SystemUser;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserRoleController : BaseController<IUserRoleService, UserRoleVM, UserRole>
{
    private readonly IUserRoleService _userRolesService;
    private readonly IUserRoleBusiness _userRoleBusiness;
    private readonly IEnumerable<IAPIPermission> _permissions;

    public UserRoleController(IUserRoleService userRolesService, IUserRoleBusiness userRoleBusiness, IEnumerable<IAPIPermission> permissions) :
        base(userRolesService)
    {
        _userRolesService = userRolesService;
        _userRoleBusiness = userRoleBusiness;
        _permissions = permissions;
    }

    [HttpGet("CurrentUserRootMenu")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<RootMenuVM>>> CurrentUserRootMenu()
    {
        try
        {
            var userId = HttpContext.GetCurrentUserId();

            var (executionState, entity, message) = await _userRoleBusiness.CurrentUserRootMenu(userId);

            return Ok(new WebApiResponse<RootMenuVM>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<RootMenuVM>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpGet("ListByRoleId/{roleId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<List<string>>>> ListByRoleId([FromRoute] long roleId)
    {
        try
        {
            var (executionState, entity, message) = await _userRolesService.ListByRoleId(roleId);

            return Ok(new WebApiResponse<List<string>>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<List<string>>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpPost("SetAccessLists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<UserRoleVM>>> SetAccessLists([FromBody] UserRoleSetAccessListsVM model)
    {
        try
        {
            var (executionState, entity, message) = await _userRolesService.SetAccessLists(model);

            return Ok(new WebApiResponse<UserRoleVM>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<UserRoleVM>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpPost("SetPermissions")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<UserRoleVM>>> SetPermissions([FromBody] UserRoleSetPermissionsVM model)
    {
        try
        {
            var (executionState, entity, message) = await _userRolesService.SetPermissions(model);

            return Ok(new WebApiResponse<UserRoleVM>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<UserRoleVM>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpGet("AddRoleToUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<UserRoleVM>>> AddRoleToUser(
        [FromQuery] long userId,
        [FromQuery] long roleId)
    {
        try
        {
            var (executionState, entity, message) = await _userRolesService.AddRoleToUser(userId, roleId);

            return Ok(new WebApiResponse<UserRoleVM>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<UserRoleVM>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpGet("PermissionList")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<WebApiResponse<List<PermissionGroup>>> PermissionList()
    {
        try
        {
            var groupedPermissions = _permissions
                .PermissionListToGroupedList();

            return Ok(new WebApiResponse<List<PermissionGroup>>(
                    (ExecutionState.Success, groupedPermissions, "Ok")
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<List<PermissionGroup>>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpPost("CurrentUserHasPermissions")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<Dictionary<string, bool>>>> CurrentUserHasPermissions([FromBody] UserHasPermissionsVM model)
    {
        try
        {
            model.UserId = HttpContext.GetCurrentUserId();

            var permissionDictionary = await _userRolesService.UserHasPermission(model);

            return Ok(new WebApiResponse<Dictionary<string, bool>>(
                    (ExecutionState.Success, permissionDictionary, "Ok")
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<Dictionary<string, bool>>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }

    [HttpGet("GetRolesByIds")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WebApiResponse<List<string>>>> GetRolesByIds([FromQuery] long[] roleIds)
    {
        try
        {
            var (executionState, entity, message) = await _userRolesService.GetRolesByIds(roleIds);

            return Ok(new WebApiResponse<List<UserRoleVM>>(
                    (executionState, entity, message)
                ));
        }
        catch (Exception)
        {
            return StatusCode(500, new WebApiResponse<List<UserRoleVM>>(
                    (ExecutionState.Failure, null, "Unexpected error occurred")
                ));
        }
    }
}
