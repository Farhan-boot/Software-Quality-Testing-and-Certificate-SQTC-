using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Services.Interface.SystemUser;
using PTSL.GENERIC.Web.Helper;
using System.Text.Json;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.SystemUser
{
    public class UserService : IUserService
    {
        private readonly HttpHelper httpHelper;

        public UserService(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<UserVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserList));
                var json = httpHelper.Get(URL);
                var responseJson = System.Text.Json.JsonSerializer.Deserialize<WebApiResponse<List<UserVM>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
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
        public (ExecutionState executionState, UserVM entity, string message) Create(UserVM model)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
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

        

        public (ExecutionState executionState, UserVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                UserVM model = new UserVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User + "/" + id));
                var json = httpHelper.Get(URL);
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
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
        public (ExecutionState executionState, UserVM entity, string message) Update(UserVM model)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User));
                var json = httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
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
        public (ExecutionState executionState, UserVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UserVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.IsActive = false;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserSoftDelete +"/"+ id));
                    var json = httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This color is not exist.";
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

        public (ExecutionState executionState, LoginResultVM entity, string message) UserLogin(LoginVM model)
        {
            (ExecutionState executionState, LoginResultVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserLogin));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<LoginResultVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<LoginResultVM>>(json);
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

        public (ExecutionState executionState, List<UserVM> entity, string message) GetUserInfoByUserRoleId(long userRoleId)
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse;
            try
            {
                UserVM model = new UserVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetUserInfoByUserRoleId + "/" + userRoleId));
                var json = httpHelper.Get(URL);
                WebApiResponse<List<UserVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserVM>>>(json);
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

        public async Task<(ExecutionState executionState, IList<UserVM> entity, string message)> ClientWiseUserList(long ClienId)
        {
            (ExecutionState executionState, IList<UserVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format($"Client/ClientWiseUserList?ClienId={ClienId}&"));
                var json = await httpHelper.GetAsync(URL);
                var responseJson = System.Text.Json.JsonSerializer.Deserialize<WebApiResponse<IList<UserVM>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
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

        public async Task<(ExecutionState executionState, IList<UserVM> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone)
        {
            (ExecutionState executionState, IList<UserVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format($"Account/Search?userRoleId={userRoleId}&UserName={userName}&FirstName={firstName}&UserEmail={email}&UserPhone={userPhone}"));
                var json = await httpHelper.GetAsync(URL);
                var responseJson = System.Text.Json.JsonSerializer.Deserialize<WebApiResponse<IList<UserVM>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
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
    }
}