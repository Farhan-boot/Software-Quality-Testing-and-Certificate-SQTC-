using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client.ApprovalForProjectLogVM;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class ApprovalForProjectLogService : IApprovalForProjectLogService
    {
        private readonly HttpHelper _httpHelper;

        public ApprovalForProjectLogService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) List()
        {
            (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLogList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<ApprovalForProjectLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForProjectLogVM>>>(json);
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
        public (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Create(ApprovalForProjectLogVM model)
        {
            (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLog));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<ApprovalForProjectLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForProjectLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponse;
            try
            {
                ApprovalForProjectLogVM model = new ApprovalForProjectLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLog + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForProjectLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForProjectLogVM>>(json);
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
                ApprovalForProjectLogVM model = new ApprovalForProjectLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLogDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForProjectLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForProjectLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Update(ApprovalForProjectLogVM model)
        {
            (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLog));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<ApprovalForProjectLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForProjectLogVM>>(json);
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
        public (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ApprovalForProjectLogVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForProjectLog));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<ApprovalForProjectLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForProjectLogVM>>(json);
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
        public async Task<(ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message)> ProjectCommentHistoryById(long id)
        {
            (ExecutionState executionState, List<ApprovalForProjectLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCommentHistory + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ApprovalForProjectLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForProjectLogVM>>>(json)!;
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