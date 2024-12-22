using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client.ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForRegisteredClientLogVM;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Sqtc_Client.ApprovalForRegisteredClientLog
{
    public class ApprovalForRegisteredClientLogService : IApprovalForRegisteredClientLogService
    {
        private readonly HttpHelper _httpHelper;

        public ApprovalForRegisteredClientLogService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) List()
        {
            (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLogList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<ApprovalForRegisteredClientLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForRegisteredClientLogVM>>>(json);
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
        public async Task<(ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message)> Create(ApprovalForRegisteredClientLogVM model)
        {
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLog));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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

        public async Task<(ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message)> CreateBackwardProcess(ApprovalForRegisteredClientLogVM model)
        {
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BackwardForRegisteredClientLog));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse;
            try
            {
                ApprovalForRegisteredClientLogVM model = new ApprovalForRegisteredClientLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLog + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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
                ApprovalForRegisteredClientLogVM model = new ApprovalForRegisteredClientLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLogDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) Update(ApprovalForRegisteredClientLogVM model)
        {
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLog));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ApprovalForRegisteredClientLogVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForRegisteredClientLog));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<ApprovalForRegisteredClientLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForRegisteredClientLogVM>>(json);
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
        public async Task<(ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message)> ClientCommentHistoryById(long id)
        {
            (ExecutionState executionState, List<ApprovalForRegisteredClientLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ClientCommentHistory + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ApprovalForRegisteredClientLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForRegisteredClientLogVM>>>(json)!;
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