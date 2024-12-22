using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.SecurityTestings;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.SecurityTestings
{
    public class SecurityTestingService : ISecurityTestingService
    {
        private readonly HttpHelper _httpHelper;

        public SecurityTestingService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<SecurityTestingVM> entity, string message) List()
        {
            (ExecutionState executionState, List<SecurityTestingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTestingList));
                var json =  _httpHelper.Get(URL);
                WebApiResponse<List<SecurityTestingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<SecurityTestingVM>>>(json);
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
        public (ExecutionState executionState, SecurityTestingVM entity, string message) Create(SecurityTestingVM model)
        {
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTesting));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<SecurityTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<SecurityTestingVM>>(json);
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
        public (ExecutionState executionState, SecurityTestingVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse;
            try
            {
                SecurityTestingVM model = new SecurityTestingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTesting + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<SecurityTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<SecurityTestingVM>>(json);
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
                SecurityTestingVM model = new SecurityTestingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTestingDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<SecurityTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<SecurityTestingVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                //returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                //returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
        public (ExecutionState executionState, SecurityTestingVM entity, string message) Update(SecurityTestingVM model)
        {
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTesting));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<SecurityTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<SecurityTestingVM>>(json);
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
        public (ExecutionState executionState, SecurityTestingVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, SecurityTestingVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, SecurityTestingVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SecurityTesting));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<SecurityTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<SecurityTestingVM>>(json);
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

        public async Task<(ExecutionState executionState, IList<SecurityTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId,string? Vulnerability, SeverityLevel? SeverityLevel, EaseOfExploitation? EaseOfExploitation)
        {
            (ExecutionState executionState, List<SecurityTestingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"SecurityTesting/Search?ProjectRequestId={ProjectRequestId}&TaskOfProjectId={TaskOfProjectId}&Vulnerability={Vulnerability}&SeverityLevel={SeverityLevel}&EaseOfExploitation={EaseOfExploitation}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<SecurityTestingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<SecurityTestingVM>>>(json)!;
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