using Microsoft.AspNetCore.Mvc;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.SqtcClient
{
    [SessionAuthorize]
    public class ClientEmployeeController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IClientService _ClientService;
        private readonly IUserRoleService _UserRoleService;


        public ClientEmployeeController(HttpHelper httpHelper)
        {
            _UserService = new UserService(httpHelper);
            _ClientService = new ClientService(httpHelper);
            _UserRoleService = new UserRoleService(httpHelper);
        }
        // GET: ClientEmployee
        public async Task<ActionResult> Index()
        {
            var ClientUserData = _UserService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
            var ClientId = ClientUserData.entity?.ClientId ?? 0;
            (ExecutionState executionState, IList<UserVM> entity, string message) returnResponse = await _UserService.ClientWiseUserList(ClientId);
            return View(returnResponse.entity);
        }

        // GET: ClientEmployee/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, UserVM entity, string message) returnResponse =  _UserService.GetById(id);
            return View(returnResponse.entity);
        }

        // GET: ClientEmployee/Create
        public ActionResult Create()
        {
            UserVM entity = new UserVM();
            return View(entity);
        }

        // POST: ClientEmployee/Create
        [HttpPost]
        public ActionResult Create(UserVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get User Roles
                    var allRoles = _UserRoleService.List();
                    long? clientUserRoleId = null;
                    if (allRoles.entity is not null)
                    {
                        clientUserRoleId = allRoles.entity.Where(s => s.RoleName.ToLower().Trim() == "Client User".ToLower().Trim()).FirstOrDefault()?.Id;
                    }
                    var ClientUserData = _UserService.GetById(Convert.ToInt64(HttpContext.Session.GetString(SessionKey.UserId)));
                    var ClientId = ClientUserData.entity.ClientId;
                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.UserType = UserType.Client_User;
                    entity.UserStatus = true;
                    entity.CreatedBy = ClientUserData.entity.ClientId.Value;
                    entity.UserName = entity.UserEmail;
                    entity.UserRoleId = clientUserRoleId;
                    // TODO: Add insert logic here
                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Create(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;

                    if (returnResponse.executionState.ToString() != "Created")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
//                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }


        // GET: ClientEmployee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.GetById(id);

            return View(returnResponse.entity);
        }

        // POST: ClientEmployee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserVM entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(ClientEmployeeController.Index), "ClientEmployee");
                    }
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;
                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Update(entity);
//                    Session["Message"] = returnResponse.message;
//                    Session["executionState"] = returnResponse.executionState;
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

//                Session["Message"] = _ModelStateValidation.ModelStateErrorMessage(ModelState);
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
            catch
            {
//                Session["Message"] = "Form Data Not Valid.";
//                Session["executionState"] = ExecutionState.Failure;
                return View(entity);
            }
        }

        //GET: ClientEmployee/Delete/5
        //public JsonResult Delete(int id)
        //{
        //    (ExecutionState executionState, string message) CheckDataExistOrNot = _UserService.De(id);
        //    string message = "Faild, You can't delete this item.";
        //    if (CheckDataExistOrNot.executionState.ToString() != "Success")
        //    {
        //        return Json(new { Message = message, executionState = CheckDataExistOrNot.executionState }, SerializerOption.Default);

        //    }
        //    (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Delete(id);
        //    if (returnResponse.executionState.ToString() == "Updated")
        //    {
        //        returnResponse.message = "ClientEmployee deleted successfully.";
        //    }
        //    else
        //    {
        //        returnResponse.message = "Failed to delete this item.";
        //    }
        //    return Json(new { Message = returnResponse.message, executionState = returnResponse.executionState }, SerializerOption.Default);
        //    //return View();
        //}

        // POST: ClientEmployee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserVM entity)
        {
            try
            {
                // TODO: Add update logic here
                if (id != entity.Id)
                {
                    return RedirectToAction(nameof(ClientEmployeeController.Index), "ClientEmployee");
                }
                //entity.IsActive = true;
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Update(entity);
//                Session["Message"] = returnResponse.message;
//                Session["executionState"] = returnResponse.executionState;
                //return View(returnResponse.entity);
                // return RedirectToAction("Edit?id="+id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
