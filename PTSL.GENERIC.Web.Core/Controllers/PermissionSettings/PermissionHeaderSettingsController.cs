using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using PTSL.GENERIC.Web.Controllers.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers.PermissionSettings;


[SessionAuthorize]
public class PermissionHeaderSettingsController : Controller
{
    

    
    private readonly IPermissionHeaderSettingsService _PermissionHeaderSettingsService;
    private readonly IPermissionRowSettingsService _PermissionRowSettingsService;
    private readonly IAccesslistService _AccesslistService;


   

    //For New
    private readonly IUserRoleService _UserRoleService;
    private readonly IUserService _UserService;


    public PermissionHeaderSettingsController(HttpHelper httpHelper)
    {
        
        _PermissionHeaderSettingsService = new PermissionHeaderSettingsService(httpHelper);
        _PermissionRowSettingsService = new PermissionRowSettingsService(httpHelper);
        _AccesslistService = new AccesslistService(httpHelper);
        

        //For New
        _UserRoleService = new UserRoleService(httpHelper);
        _UserService = new UserService(httpHelper);

    }

    public ActionResult Index()
    {
        (ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message) returnResponseIsActiveData = _PermissionHeaderSettingsService.List();

        if (returnResponseIsActiveData.entity == null)
        {
            return View(new List<PermissionHeaderSettingsVM>());
        }

        return View(returnResponseIsActiveData.entity.OrderByDescending(x=>x.Id));
    }

    [HttpPost]
   

    public ActionResult IndexFilter()
    {
        return RedirectToAction("Index");
    }

    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) returnResponse = _PermissionHeaderSettingsService.GetById(id);

        //ViewBag.CommitteeTypeId = new SelectList(EnumHelper.GetEnumDropdowns<CommitteeType>(), "Id", "Name", returnResponse.entity.CommitteeTypeId);
        //ViewBag.ExDesignatinId = new SelectList(EnumHelper.GetEnumDropdowns<ExecutiveDesignationType>(), "Id", "Name", returnResponse.entity.ExDesignatinId);
        //ViewBag.SubExecutiveDesignationId = new SelectList(EnumHelper.GetEnumDropdowns<SubCommitteeType>(), "Id", "Name", returnResponse.entity.SubCommitteeDesignationId);


        //ViewBag.SubDesignationId = new SelectList(EnumHelper.GetEnumDropdowns<SubCommitteeType>(), "Id", "Name", returnResponse.entity.SubCommitteeDesignationId);
        //ViewBag.ExecutiveMemberTypeId = new SelectList(EnumHelper.GetEnumDropdowns<SubCommitteeType>(), "Id", "Name", returnResponse.entity.DesignatinId);

        return View(returnResponse.entity);
    }


    public ActionResult Create()
    {
        PermissionHeaderSettingsVM entity = new PermissionHeaderSettingsVM();

        ViewBag.ModuleEnumId = new SelectList(EnumHelper.GetEnumDropdowns<ModuleEnum>(), "Id", "Name");
        //ViewBag.OtherMemberId = new SelectList("");

        (ExecutionState executionState, List<UserRoleVM> entity, string message) returnResponseUserRole = _UserRoleService.List();
        if (returnResponseUserRole.entity != null)
        {
            List<UserRoleVM> actualRoles = new List<UserRoleVM>();
            foreach (UserRoleVM role in returnResponseUserRole.entity)
            {
                if(role.RoleName != "Client User" && role.RoleName != "Client Admin")
                    actualRoles.Add(role);
            }
            ViewBag.UserRoleId = new SelectList(actualRoles ?? new List<UserRoleVM>(), "Id", "RoleName");
        }
        (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
        if (returnResponseUser.entity != null)
        {
            ViewBag.UserId = new SelectList(returnResponseUser.entity, "Id", "UserName");
        }

        //Authority
        (ExecutionState executionState, List<UserRoleVM> entity, string message) returnResponseUserRoleAuthority = _UserRoleService.List();
        if (returnResponseUserRoleAuthority.entity != null)
        {
            ViewBag.AuthorityUserRoleId = new SelectList(returnResponseUserRoleAuthority.entity ?? new List<UserRoleVM>(), "Id", "RoleName");
        }
        (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUserAuthority = _UserService.List();
        if (returnResponseUserAuthority.entity != null)
        {
            ViewBag.AuthorityUserId = new SelectList(returnResponseUserAuthority.entity, "Id", "UserName");
        }

        //Access list
        (ExecutionState executionState, List<AccesslistVM> entity, string message) returnResponseAccesslist = _AccesslistService.List();
        if (returnResponseAccesslist.entity != null)
        {
            ViewBag.AccesslistId = new SelectList(returnResponseAccesslist.entity.Where(x=>x.IsAvailableForApproval == true), "Id", "Mask");
        }


        return View(entity);
    }

    [HttpPost]
    public ActionResult Create(PermissionHeaderSettingsVM entity)
    {
        try
        {
            
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;

            entity.PermissionRowSettings = JsonConvert.DeserializeObject<List<PermissionRowSettingsVM>>(HttpContext.Request.Form["PermissionRowSettings"]!);

            (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) returnResponse = _PermissionHeaderSettingsService.Create(entity);
            //                    Session["Message"] = returnResponse.message;
            //                    Session["executionState"] = returnResponse.executionState;

            if (returnResponse.executionState.ToString() != "Created")
            {
                //HttpContext.Session.SetString("Message", returnResponse.message);
                //HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());
                //return RedirectToAction("Index");

                return Json(
                 new
                 {
                     Success = false,
                     Data = returnResponse.entity,
                     Message = returnResponse.message
                 },
                 SerializerOption.Default);

            }
            else
            {
                //HttpContext.Session.SetString("Message", "Permission Settings has been created successfully");
                //HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                return Json(
                new
                {
                    Success = true,
                    Data = returnResponse.entity,
                    Message = returnResponse.message
                },
                SerializerOption.Default);

            }
            
        }
        catch
        {
            return View(entity);
        }
    }


    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) returnResponse = _PermissionHeaderSettingsService.GetById(id);

        (ExecutionState executionState, List<UserRoleVM> entity, string message) returnResponseUserRole = _UserRoleService.List();
        if (returnResponseUserRole.entity != null)
        {
            List<UserRoleVM> actualRoles = new List<UserRoleVM>();
            foreach (UserRoleVM role in returnResponseUserRole.entity)
            {
                if (role.RoleName != "Client User" && role.RoleName != "Client Admin")
                    actualRoles.Add(role);
            }
            ViewBag.UserRoleId = new SelectList(actualRoles ?? new List<UserRoleVM>(), "Id", "RoleName");
        }

        (ExecutionState executionState, List<UserVM> entity, string message) returnResponseUser = _UserService.List();
        if (returnResponseUser.entity != null)
        {
            ViewBag.UserId = new SelectList(returnResponseUser.entity ?? new List<UserVM>(), "Id", "UserName", returnResponse.entity.UserId);
        }

        

        ViewBag.ModuleEnumId = new SelectList(EnumHelper.GetEnumDropdowns<ModuleEnum>(), "Id", "Name", (int)returnResponse.entity.ModuleEnumId!);


        return View(returnResponse.entity);
    }


    [HttpPost]
    public ActionResult Edit(int id, PermissionHeaderSettingsVM entity)
    {
        try
        {
            if (id != entity.Id)
            {
                return RedirectToAction(nameof(CategoryController.IndexAsync), "Category");
            }

            entity.PermissionRowSettings = JsonConvert.DeserializeObject<List<PermissionRowSettingsVM>>(HttpContext.Request.Form["PermissionRowSettings"]!);
            foreach (var item in entity.PermissionRowSettings!)
            {
                item.IsActive = true;
                item.IsDeleted = false;
            }
           
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.UpdatedAt = DateTime.Now;

           
            (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) returnResponse = _PermissionHeaderSettingsService.Update(entity);
            //                    Session["Message"] = returnResponse.message;
            //                    Session["executionState"] = returnResponse.executionState;
            if (returnResponse.executionState.ToString() != "Updated")
            {
                return View(entity);
            }
            else
            {
                HttpContext.Session.SetString("Message", "Permission Settings has been Updated successfully");
                HttpContext.Session.SetString("executionState", returnResponse.executionState.ToString());

                return Json(
          new
          {
              Success = returnResponse.executionState == ExecutionState.Retrieved,
              Data = returnResponse.entity,
              Message = returnResponse.message
          },
          SerializerOption.Default);

                // return RedirectToAction("Index", "PermissionHeaderSettings");
            }
            //}

            //                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
            //                Session["executionState"] = ExecutionState.Failure;
            return Json(
           new
           {
               Success = returnResponse.executionState == ExecutionState.Retrieved,
               Data = returnResponse.entity,
               Message = returnResponse.message
           },
           SerializerOption.Default);

        }
        catch
        {
            //                Session["Message"] = "Form Data Not Valid.";
            //                Session["executionState"] = ExecutionState.Failure;
            return View(entity);
        }
    }



    [HttpGet]
    public ActionResult DeleteMemberById(long id)
    {
        (ExecutionState executionState, string message) CheckDataExistOrNot = _PermissionRowSettingsService.DoesExist(id);
        string message = "Failed, You can't delete this item.";

        if (CheckDataExistOrNot.executionState.ToString() != "Success")
        {
            return Json(new { Message = message, CheckDataExistOrNot.executionState }, SerializerOption.Default);
        }

        (ExecutionState executionState, PermissionRowSettingsVM entity, string message) returnResponse = _PermissionRowSettingsService.Delete(id);
        if (returnResponse.executionState.ToString() == "Updated")
        {
            returnResponse.message = "Item deleted successfully.";
        }
        else
        {
            returnResponse.message = "Failed to delete this item.";
        }

        return Json(new { Data = returnResponse.entity, Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
    }





    //public ActionResult ActiveInactive(long Id)
    //{
    //    var result = _PermissionHeaderSettingsService.GetById(Id);
    //    result.entity.PermissionHeaderSettingsMembers = new List<PermissionHeaderSettingsMemberVM>();
    //    if (result.entity != null)
    //    {
    //        if (result.entity.CommitteeApprovalStatus == 1)
    //        {
    //            result.entity.IsInActiveCommittee = true;
    //            result.entity.CommitteeApprovalStatus = (int)CommitteeApprovalStatus.InActive;
    //            result.entity.PermissionHeaderSettingsMembers = null;

    //            var returnResponse = _PermissionHeaderSettingsService.Update(result.entity);
    //            return RedirectToAction("Index", "PermissionHeaderSettings");
    //        }
    //        else
    //        {
    //            result.entity.IsInActiveCommittee = true;
    //            var returnResponse = _PermissionHeaderSettingsService.Update(result.entity);
    //            return RedirectToAction("Index", "PermissionHeaderSettings");
    //        }
    //    }
    //    return RedirectToAction("Index", "PermissionHeaderSettings");
    //}


    //[HttpPost]
    //public JsonResult GetDesignationByDesigOrSubDesigId(CommitteeDesignationFilterVM filter)
    //{

    //    var designationLists = _SubCommitteeDesignationService.ListByFilter(filter);

    //    return Json(new { Entity = designationLists.entity, Message = designationLists.message, designationLists.executionState }, SerializerOption.Default);
    //}


    //public ActionResult SendRequest(int id)
    //{
    //    var result = _PermissionHeaderSettingsService.GetById(id);

    //    result.entity.CommitteeApprovalDate = DateTime.Now;
    //    result.entity.CommitteeApprovalBy = 0;
    //    result.entity.IsInActiveCommittee = true;
    //    result.entity.PermissionHeaderSettingsMembers = null;

    //    result.entity.CommitteeApprovalStatus = (int) CommitteeApprovalStatus.SentRequest;


    //    var returnResponse = _PermissionHeaderSettingsService.Update(result.entity);
    //    return RedirectToAction("Index", "PermissionHeaderSettings");
    //}

    //public ActionResult RequestList()
    //{
    //    AuthLocationHelper.GenerateViewBagForForestAndCivil(
    //        HttpContext,
    //        ViewBag,
    //        _ForestCircleService,
    //        _ForestDivisionService,
    //        _ForestRangeService,
    //        _ForestBeatService,
    //        _ForestFcvVcfService,
    //        _DivisionService,
    //        _DistrictService,
    //        _UpazillaService,
    //        _UnionService
    //    );

    //    (ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message) returnResponseIsActiveData = _PermissionHeaderSettingsService.List();

    //    if (returnResponseIsActiveData.entity == null)
    //    {
    //        return View(new List<PermissionHeaderSettingsVM>());
    //    }

    //    foreach (var isActiveObj in returnResponseIsActiveData.entity)
    //    {
    //        if (isActiveObj != null)
    //        {
    //            if (isActiveObj.CommitteeEndDate.AddDays(1) < DateTime.Now)
    //            {
    //                isActiveObj.IsInActiveCommittee = true;
    //            }

    //            var returnResponseIsActiveUpdate = _PermissionHeaderSettingsService.Update(isActiveObj);
    //        }
    //    }

    //    (ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message) returnResponse = _PermissionHeaderSettingsService.List();

    //    foreach (var item in returnResponse.entity)
    //    {
    //        item.CommitteeTypeName = item.CommitteeTypeId.GetEnumDisplayName();
    //    }



    //    return View(returnResponse.entity);
    //}


    public ActionResult GetUserNameByUserRoleId(long userRoleId)
    {
        var result = _PermissionHeaderSettingsService.GetUserNameByUserRoleId(userRoleId);
       
        return Json(result.entity, SerializerOption.Default);
    }


    

}
