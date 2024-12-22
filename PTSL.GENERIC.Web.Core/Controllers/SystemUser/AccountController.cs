using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Controllers.SystemUser
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IPmsGroupService _PmsGroupService;
        private readonly IUserService _UserService;
        private readonly IUserRoleService _userRoleService;
        private readonly FileHelper _fileHelper;
        private IDesignationService _DesignationService;


        public AccountController(HttpHelper httpHelper, FileHelper fileHelper)
        {
            _PmsGroupService = new PmsGroupService(httpHelper);
            _UserService = new UserService(httpHelper);
            _userRoleService = new UserRoleService(httpHelper);
            _fileHelper = fileHelper;
            _DesignationService = new DesignationService(httpHelper);
            // Map To Account into User
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginVM model = new LoginVM();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Dashboard(string returnUrl)
        {

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            (ExecutionState executionState, LoginResultVM entity, string message) result = _UserService.UserLogin(model);

            if (result.entity != null)
            {

                HttpContext.Session.SetString(SessionKey.UserRoleId, result.entity?.UserRoleId?.ToString() ?? string.Empty);
                HttpContext.Session.SetString("Token", result.entity?.Token ?? string.Empty);
                HttpContext.Session.SetString(SessionKey.UserEmail, result.entity?.UserEmail ?? string.Empty);
                HttpContext.Session.SetString(SessionKey.RoleName, result.entity?.RoleName ?? string.Empty);
                HttpContext.Session.SetString(SessionKey.UserId, result.entity?.UserId.ToString() ?? string.Empty);

                var token = HttpContext.Session.GetString("Token");
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }

            ViewBag.ErrorMsg = "Username or Password Invalid !";

            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //	// Require that the user has already logged in via username/password or external login
        //	if (!await SignInManager.HasBeenVerifiedAsync())
        //	{
        //		return View("Error");
        //	}
        //	return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}

        //	// The following code protects for brute force attacks against the two factor codes.
        //	// If a user enters incorrect codes for a specified amount of time then the user account
        //	// will be locked out for a specified amount of time.
        //	// You can configure the account lockout settings in IdentityConfig
        //	var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        //	switch (result)
        //	{
        //		case SignInStatus.Success:
        //			return RedirectToLocal(model.ReturnUrl);
        //		case SignInStatus.LockedOut:
        //			return View("Lockout");
        //		case SignInStatus.Failure:
        //		default:
        //			ModelState.AddModelError("", "Invalid code.");
        //			return View(model);
        //	}
        //}

        // GET: UrerList
        [AllowAnonymous]
        public ActionResult UserLists()
        {
            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName");
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse = _UserService.List();
            return View(returnResponse.entity.Where(x=>x.UserType == UserType.SQTC_User || x.UserType == UserType.SQTC_Admin));
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var designation = _DesignationService.List();
            ViewBag.DesignationId = new SelectList(designation.entity?? new List<DesignationVM>(), "Id", "Name");

            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName");

            return View(new UserVM());
        }

        [AllowAnonymous]
        public ActionResult ExternalClientRegister()
        {
            var designation = _DesignationService.List();
            ViewBag.UserType = new SelectList(EnumHelper.GetEnumDropdowns<UserType>(), "Id", "Name");
            ViewBag.ClientStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientStatus>(), "Id", "Name");
            ViewBag.ClientApprovalStatus = new SelectList(EnumHelper.GetEnumDropdowns<ClientApprovalStatus>(), "Id", "Name");
            ViewBag.DesignationId = new SelectList(designation.entity, "Id", "Name");

            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserVM model)
        {
            try
            {
                if (model?.file != null)
                {
                    // Save files
                    if (SaveFiles(model.file, ref model, FileType.Image, out var imageFileError) == false)
                    {
                        return Json(
                            new { Success = false, Message = imageFileError },
                            SerializerOption.Default);
                    }
                }


                model.UserName = model.FirstName + " " + model.LastName;
                model.UserStatus = true;
                model.UserType = UserType.SQTC_User;
                var (executionState, entity, message) = _UserService.Create(model);

                HttpContext.Session.SetString("Message", message);
                HttpContext.Session.SetString("executionState", executionState.ToString());

                if(executionState == ExecutionState.Failure)
                {
                    return RedirectToAction("Register");
                }
                else
                {
                   return RedirectToAction("UserLists");
                }
                     
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (ExecutionState executionState, UserVM entity, string message) result = _UserService.GetById(id);

            

            

            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName", result.entity.UserRoleId);
            return View(result.entity);
        }

        // POST: Account/UserEdit/5
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(int id, UserVM entity)
        {
            try
            {
                //List<PmsGroupViewModel> group = new List<PmsGroupViewModel>();
                //group = GroupList();
                //ViewBag.GroupList = new SelectList(GroupList(), "Id", "GroupName", entity.PmsGroupID);

                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (id != entity.Id)
                    {
                        return RedirectToAction(nameof(AccountController.UserLists), "Account");
                    }

                    if (entity?.file != null)
                    {
                        // Save files
                        if (SaveFiles(entity.file, ref entity, FileType.Image, out var imageFileError) == false)
                        {
                            return Json(
                                new { Success = false, Message = imageFileError },
                                SerializerOption.Default);
                        }
                    }


                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;

                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Update(entity);
                    if (returnResponse.executionState.ToString() != "Updated")
                    {
                        return View(entity);
                    }
                    else
                    {
                        return RedirectToAction("UserLists");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: Account/UserDelete/5
        [AllowAnonymous]
        public JsonResult UserDelete(int id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Delete(id);
            if (returnResponse.executionState.ToString() == "Updated")
            {
                returnResponse.message = "User deleted successfully.";
            }
            return Json(new { Message = returnResponse.message, returnResponse.executionState }, SerializerOption.Default);
            //return View();
        }


        public List<PmsGroupViewModel> GroupList()
        {


            (ExecutionState executionState, List<PmsGroupVM> entity, string message) returnPmsGroupResponse = _PmsGroupService.List();

            List<PmsGroupViewModel> result = returnPmsGroupResponse.entity.Where(c => c.IsActive == true && c.IsVisible != 1).Select(c => new PmsGroupViewModel
            {
                Id = c.Id,
                GroupName = c.GroupName
            }).ToList();
            //var accessList = _PmsGroupService.GetAll()
            //.Select(g => new PmsGroupViewModel
            //{
            //    GroupID = g.GroupID,
            //    GroupName = g.GroupName

            //}).ToList();

            return result;
        }

        //
        // GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //	if (userId == null || code == null)
        //	{
        //		return View("Error");
        //	}
        //	var result = await UserManager.ConfirmEmailAsync(userId, code);
        //	return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var user = await UserManager.FindByNameAsync(model.Email);
        //		if (user == null || !await UserManager.IsEmailConfirmedAsync(user.Id))
        //		{
        //			// Don't reveal that the user does not exist or is not confirmed
        //			return View("ForgotPasswordConfirmation");
        //		}

        //		// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //		// Send an email with this link
        //		// string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //		// var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //		// await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //		// return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //	}

        //	// If we got this far, something failed, redisplay form
        //	return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}
        //	var user = await UserManager.FindByNameAsync(model.Email);
        //	if (user == null)
        //	{
        //		// Don't reveal that the user does not exist
        //		return RedirectToAction("ResetPasswordConfirmation", "Account");
        //	}
        //	var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //	if (result.Succeeded)
        //	{
        //		return RedirectToAction("ResetPasswordConfirmation", "Account");
        //	}
        //	AddErrors(result);
        //	return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //	// Request a redirect to the external login provider
        //	return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //	var userId = await SignInManager.GetVerifiedUserIdAsync();
        //	if (userId == null)
        //	{
        //		return View("Error");
        //	}
        //	var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //	var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //	return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View();
        //	}

        //	// Generate the token and send it
        //	if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //	{
        //		return View("Error");
        //	}
        //	return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //	var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //	if (loginInfo == null)
        //	{
        //		return RedirectToAction("Login");
        //	}

        //	// Sign in the user with this external login provider if the user already has a login
        //	var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //	switch (result)
        //	{
        //		case SignInStatus.Success:
        //			return RedirectToLocal(returnUrl);
        //		case SignInStatus.LockedOut:
        //			return View("Lockout");
        //		case SignInStatus.RequiresVerification:
        //			return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //		case SignInStatus.Failure:
        //		default:
        //			// If the user does not have an account, then prompt the user to create an account
        //			ViewBag.ReturnUrl = returnUrl;
        //			ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //			return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //	}
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //	if (User.Identity.IsAuthenticated)
        //	{
        //		return RedirectToAction("Index", "Manage");
        //	}

        //	if (ModelState.IsValid)
        //	{
        //		// Get the information about the user from the external login provider
        //		var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //		if (info == null)
        //		{
        //			return View("ExternalLoginFailure");
        //		}
        //		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //		var result = await UserManager.CreateAsync(user);
        //		if (result.Succeeded)
        //		{
        //			result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //			if (result.Succeeded)
        //			{
        //				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //				return RedirectToLocal(returnUrl);
        //			}
        //		}
        //		AddErrors(result);
        //	}

        //	ViewBag.ReturnUrl = returnUrl;
        //	return View(model);
        //}

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            //Session["UserEmail"] = string.Empty;
            //MySession.Current.Token = string.Empty;
            //Session.Abandon();
            //Session.Clear();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            ////return Json(true,SerializerOption.Default);
            //return RedirectToAction("Login", "Account");

            //Session["UserEmail"] = string.Empty;
            HttpContext.Session.Clear();

            //MySession.Current.Token = string.Empty;
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        ////
        //// POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    //Session["UserEmail"] = string.Empty;
        //	HttpContext.Session.Clear();

        //    //MySession.Current.Token = string.Empty;
        //    //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Login", "Account");
        //}

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //protected override void Dispose(bool disposing)
        //{
        //	if (disposing)
        //	{
        //		if (_userManager != null)
        //		{
        //			_userManager.Dispose();
        //			_userManager = null;
        //		}

        //		if (_signInManager != null)
        //		{
        //			_signInManager.Dispose();
        //			_signInManager = null;
        //		}
        //	}

        //	base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //	get
        //	{
        //		return HttpContext.GetOwinContext().Authentication;
        //	}
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //	foreach (var error in result.Errors)
        //	{
        //		ModelState.AddModelError("", error);
        //	}
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //	if (Url.IsLocalUrl(returnUrl))
        //	{
        //		return Redirect(returnUrl);
        //	}
        //	return RedirectToAction("Index", "Home");
        //}

        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //	public ChallengeResult(string provider, string redirectUri)
        //		: this(provider, redirectUri, null)
        //	{
        //	}

        //	public ChallengeResult(string provider, string redirectUri, string userId)
        //	{
        //		LoginProvider = provider;
        //		RedirectUri = redirectUri;
        //		UserId = userId;
        //	}

        //	public string LoginProvider { get; set; }
        //	public string RedirectUri { get; set; }
        //	public string UserId { get; set; }

        //	public override void ExecuteResult(ControllerContext context)
        //	{
        //		var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //		if (UserId != null)
        //		{
        //			properties.Dictionary[XsrfKey] = UserId;
        //		}
        //		context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //	}
        //}
        #endregion


        

        // Map To Account into User
        public ActionResult UserMapToAccount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (ExecutionState executionState, UserVM entity, string message) result = _UserService.GetById(id);

            

            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName", result.entity.UserRoleId);

            //New Info
        

            
            //if (returnResponseAccount.entity != null)
            //{
            //    ViewBag.AccountsId = new SelectList(returnResponseAccount.entity ?? new List<AccountVM>(), "Id", "AccountFullInformation", result.entity.AccountsId ?? 0);
            //}

            return View(result.entity);
        }



        // POST: Account/UserMapToAccount/5
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult UserMapToAccount(UserVM entity)
        {
            try
            {
                List<PmsGroupViewModel> group = new List<PmsGroupViewModel>();
                group = GroupList();
                ViewBag.GroupList = new SelectList(GroupList(), "Id", "GroupName", entity.PmsGroupID);

                if (ModelState.IsValid)
                {
                    entity.IsActive = true;
                    entity.IsDeleted = false;
                    entity.UpdatedAt = DateTime.Now;


                    (ExecutionState executionState, UserVM entity, string message) returnResponse = _UserService.Update(entity);

                   
                    returnResponse.entity.Id = entity.Id;
                    returnResponse.entity.CreatedAt = DateTime.Now;

                    if (returnResponse.executionState == ExecutionState.Updated)
                    {
                    }

                    return Json(new
                    {
                        Success = returnResponse.executionState == ExecutionState.Updated,
                        Data = returnResponse.entity,
                        Message = returnResponse.message
                    }, SerializerOption.Default);
                }

                return Json(new
                {
                    Success = ExecutionState.Failure,
                    Data = new {},
                    Message = ModelState.FirstErrorMessage()
                }, SerializerOption.Default);
            }
            catch
            {
                return Json(new
                {
                    Success = ExecutionState.Failure,
                    Data = new {},
                    Message = "Unexpected error occurred"
                }, SerializerOption.Default);
            }
        }

        


        

        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (ExecutionState executionState, UserVM entity, string message) result = _UserService.GetById(id);

            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName", result.entity.UserRoleId);
            return View(result.entity);
        }

        [HttpGet]
        public async Task<ActionResult> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone)
        {
            ViewBag.UserRoleId = new SelectList(_userRoleService.List().entity ?? new List<UserRoleVM>(), "Id", "RoleName");
            ViewBag.UserName = userName;
            ViewBag.FirstName = firstName;
            ViewBag.UserEmail = email;
            ViewBag.UserPhone = userPhone;
            (ExecutionState executionState, IList<UserVM> entity, string message) returnResponse = await _UserService.Search(userRoleId,userName,firstName,email,userPhone);

            return View("UserLists", returnResponse.entity);
        }


        private bool SaveFiles(IFormFile files, ref UserVM entity, FileType fileType, out string error)
        {
            //foreach (var file in files)
            //{
                var (isSaved, fileName, _) = _fileHelper.SaveFile(files, fileType, "SignatureUploadForUser", out var errorMessage);
                if (isSaved == false)
                {
                    error = errorMessage;
                    return false;
                }

            entity.SignatureUrl = fileName;

            //var entityFile = new UserVM
            //    {
            //        IsActive = true,
            //        CreatedAt = DateTime.Now,
            //        //FileName = file.FileName,
            //        //FileType = fileType,
            //        SignatureUrl = fileName,
            //    };
            // }

            error = string.Empty;
            return true;
        }



    }
}
