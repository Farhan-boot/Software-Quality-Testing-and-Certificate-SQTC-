using System.Text;

using Newtonsoft.Json;

using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup
{
    public class UserRoleService : IUserRoleService
    {
        private readonly HttpHelper _httpHelper;

        public UserRoleService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, RootMenuVM entity, string message) CurrentUserRootMenu()
        {
            (ExecutionState executionState, RootMenuVM entity, string message) returnResponse;
            try
            {
                var URL = String.Concat(URLHelper.ApiBaseURL, URLHelper.UserRoleCurrentUserRootMenu);
                var json = _httpHelper.Get(URL);
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<RootMenuVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = new RootMenuVM();
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, List<string> entity, string message) ListByRoleId(long roleId)
        {
            (ExecutionState executionState, List<string> entity, string message) returnResponse;
            try
            {
                var URL = String.Concat(URLHelper.ApiBaseURL, $"{URLHelper.UserRoleListByRoleId}/{roleId}");
                var json = _httpHelper.Get(URL);
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<string>>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, List<UserRoleVM> entity, string message) GetRolesByIds(IEnumerable<long> roleIds)
        {
            (ExecutionState executionState, List<UserRoleVM> entity, string message) returnResponse;
            try
            {
                var queryParams = new List<string>();
                foreach (var item in roleIds)
                {
                    queryParams.Add($"roleIds={item}");
                }

                var query = string.Join('&', queryParams);

                var URL = String.Concat(URLHelper.ApiBaseURL, $"{URLHelper.UserRoleGetRolesByIds}?{query}");
                var json = _httpHelper.Get(URL);
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserRoleVM>>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = new List<UserRoleVM>();
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public Dictionary<string, bool> CurrentUserHasPermissions(params string[] permissions)
        {
            try
            {
                var respJson = JsonConvert.SerializeObject(new UserHasPermissionsVM
                {
                    Permissions = permissions.ToList()
                });

                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleCurrentUserHasPermissions));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<Dictionary<string, bool>>>(json);
                return responseJson?.Data ?? new Dictionary<string, bool>();
            }
            catch (Exception ex)
            {
                return new Dictionary<string, bool>();
            }
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) SetAccessLists(UserRoleSetAccessListsVM model)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleSetAccessLists));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) SetPermissions(UserRoleSetPermissionsVM model)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleSetPermissions));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) AddRoleToUser(long userId, long roleId)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                var URL = String.Concat(URLHelper.ApiBaseURL, $"{URLHelper.UserRoleSetAccessLists}?userId={userId}&roleId={roleId}");
                var json = _httpHelper.Get(URL);
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, List<PermissionGroup> entity, string message) PermissionList()
        {
            (ExecutionState executionState, List<PermissionGroup> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRolePermissionList));
                var json = _httpHelper.Get(URL);
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<PermissionGroup>>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = new List<PermissionGroup>();
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, List<UserRoleVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UserRoleVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<UserRoleVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserRoleVM>>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) Create(UserRoleVM model)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRole));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                UserRoleVM model = new UserRoleVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRole + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, string message) DoesExist(long? id)
        {
            (ExecutionState executionState, string message) returnResponse;
            try
            {
                UserRoleVM model = new UserRoleVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) Update(UserRoleVM model)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRole));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserRoleVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UserRoleVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UserRoleVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRole));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UserRoleVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserRoleVM>>(json);
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This item does not exist.";
                }
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id)
        {
            (ExecutionState executionState, bool isDeleted, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserRoleSoftDelete), "/", id);
                var json = _httpHelper.Put(URL, respJson, "application/json");
                var result = JsonConvert.DeserializeObject<WebApiResponse<bool>>(json);
                returnResponse.executionState = result.ExecutionState;
                returnResponse.isDeleted = result.Data;
                returnResponse.message = result.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.isDeleted = false;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
    }
}