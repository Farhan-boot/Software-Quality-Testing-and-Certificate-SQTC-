using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers.SystemUser;

[SessionAuthorize]
public class UserRoleController : Controller
{
    private readonly IUserRoleService _userRoleService;
    private readonly IAccesslistService _accessListService;
    private readonly IModuleService _moduleService;
    private readonly IUserService _UserService;
    public UserRoleController(HttpHelper httpHelper)
    {
        _userRoleService = new UserRoleService(httpHelper);
        _accessListService = new AccesslistService(httpHelper);
        _moduleService = new ModuleService(httpHelper);
        _UserService = new UserService(httpHelper);
    }

    public ActionResult Index()
    {
        var (_, entity, _) = _userRoleService.List();

        return View(entity ?? new List<UserRoleVM>());
    }

    public ActionResult Create()
    {
        return View(new UserRoleVM());
    }

    [HttpPost]
    public ActionResult Create(UserRoleVM entity)
    {
        try
        {
            //if (ModelState.IsValid == false)
            //{
            //    HttpContext.Session.SetString("Message", ModelState.FirstErrorMessage());
            //    HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            //    return View(entity);
            //}

            entity.IsActive = true;

            var returnResponse = _userRoleService.Create(entity);

            if (returnResponse.executionState != ExecutionState.Created)
            {
                HttpContext.Session.SetString("Message", returnResponse.message);
                HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

                return View(entity);
            }
            else
            {
                HttpContext.Session.SetString("Message", "New Role has been created successfully");
                HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                return RedirectToAction("Index");
            }
        }
        catch
        {
            HttpContext.Session.SetString("Message", "Unexpected error occurred");
            HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            return View(entity);
        }
    }

    public ActionResult Edit(long? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var (_, entity, _) = _userRoleService.GetById(id);
        if (entity is null)
        {
            HttpContext.Session.SetString("Message", "Role not found");
            HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            return RedirectToAction("Index");
        }

        return View(entity);
    }

    [HttpPost]
    public ActionResult Edit(UserRoleVM entity)
    {
        try
        {
            //if (ModelState.IsValid == false)
            //{
            //    HttpContext.Session.SetString("Message", ModelState.FirstErrorMessage());
            //    HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            //    return View(entity);
            //}

            entity.IsActive = true;

            var returnResponse = _userRoleService.Update(entity);

            if (returnResponse.executionState != ExecutionState.Updated)
            {
                HttpContext.Session.SetString("Message", returnResponse.message);
                HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

                return View(entity);
            }
            else
            {
                HttpContext.Session.SetString("Message", "Role has been updated successfully");
                HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                return RedirectToAction("Index");
            }
        }
        catch
        {
            HttpContext.Session.SetString("Message", "Unexpected error occurred");
            HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            return View(entity);
        }
    }

    public ActionResult SetPermissions(long roleId)
    {
        if (roleId == default)
        {
            return NotFound();
        }

        var (_, entity, _) = _userRoleService.GetById(roleId);
        if (entity is null)
        {
            HttpContext.Session.SetString("Message", "Role not found");
            HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            return RedirectToAction("Index");
        }

        ViewBag.PermissionList = _userRoleService.PermissionList().entity;
        ViewBag.RolePermissionList = _userRoleService.ListByRoleId(roleId).entity;

        return View(entity);
    }

    [HttpPost]
    public ActionResult SetPermissions(UserRoleSetPermissionsVM model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        var (executionState, _, message) = _userRoleService.SetPermissions(model);

        return executionState == ExecutionState.Success
            ? Json(new { Success = true, Message = "Permissions set successfully" }, SerializerOption.Default)
            : Json(new { Success = false, Message = message }, SerializerOption.Default);
    }

    public ActionResult SetPagePermissions(long roleId)
    {
        var userId = Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId));
        var userType = _UserService.GetById(userId).entity?.UserType;

        if (roleId == default)
        {
            return NotFound();
        }

        var (_, entity, _) = _userRoleService.GetById(roleId);
        if (entity is null)
        {
            HttpContext.Session.SetString("Message", "Role not found");
            HttpContext.Session.SetString("executionState", ExecutionState.Failure.ToString());

            return RedirectToAction("Index");
        }

        //ViewBag.Modules = _moduleService.List().entity;
        var modules = _moduleService.List().entity;

        //ViewBag.RoleAccessList = entity.AccessList
        //    .Split(',')
        //    .Select(x => long.TryParse(x, out var result) ? result : 0)
        //    .ToList(); 
        
        var accesslists = entity.AccessList
            .Split(',')
            .Select(x => long.TryParse(x, out var result) ? result : 0)
            .ToList();

        var accessListObjects = _accessListService.List().entity;


        entity.RoleAccessList = accesslists;
        List<long?> filterdAccessList = new List<long?>();
        
        var moduleList = new List<ModuleVM>();

        foreach (var module in modules)
        {
            ModuleVM moduleVM = new ModuleVM();
            var allAccessListByType = new List<AccesslistVM>();
            if (module.AccessList is not null)
            {
                if (userType == UserType.ClientAdmin || userType == UserType.Client_User)
                    allAccessListByType.AddRange(module.AccessList.Where(s=>s.IsForClient == true || s.IsBothForSqtcAndClient == true));
                else if(userType == UserType.SQTC_Admin || userType == UserType.SQTC_User)
                    allAccessListByType.AddRange(module.AccessList.Where(s => s.IsForSqtc == true || s.IsBothForSqtcAndClient == true));

            }
            moduleVM = module;
            moduleVM.AccessList = new List<AccesslistVM>();
            moduleVM.AccessList.AddRange(allAccessListByType);
            moduleList.Add(moduleVM);

        }
        //var accessListIds = allAccessListByType.Select(x => x.Id).ToList();
        //var filterItems = accessListObjects.Where(x => accessListIds.Contains(x.Id)).ToList();

        //entity.Modules = modules;
        entity.Modules = moduleList;

        return View(entity);
    }

    [HttpPost]
    public ActionResult SetPagePermissions(UserRoleSetAccessListsVM model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        var (executionState, _, message) = _userRoleService.SetAccessLists(model);

        return executionState == ExecutionState.Success
            ? Json(new { Success = true, Message = "Permissions set successfully" }, SerializerOption.Default)
            : Json(new { Success = false, Message = message }, SerializerOption.Default);
    }

    public JsonResult Delete(int id)
    {
        var result = _userRoleService.SoftDelete(id);
        if (result.isDeleted)
        {
            result.message = "Item deleted successfully.";
        }
        else
        {
            result.message = "Failed to delete this item.";
        }

        return Json(new { Message = result.message, result.executionState }, SerializerOption.Default);
    }
}
