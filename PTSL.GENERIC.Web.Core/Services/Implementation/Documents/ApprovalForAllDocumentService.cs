using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class ApprovalForAllDocumentService : IApprovalForAllDocumentService
    {
        private readonly HttpHelper _httpHelper;

        public ApprovalForAllDocumentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocumentList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ApprovalForAllDocumentVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForAllDocumentVM>>>(json);
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
        public (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Create(ApprovalForAllDocumentVM model)
        {
            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocument));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<ApprovalForAllDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForAllDocumentVM>>(json);
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
        public (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse;
            try
            {
                ApprovalForAllDocumentVM model = new ApprovalForAllDocumentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocument + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForAllDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForAllDocumentVM>>(json);
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
                ApprovalForAllDocumentVM model = new ApprovalForAllDocumentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocumentDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ApprovalForAllDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForAllDocumentVM>>(json);
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
        public (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Update(ApprovalForAllDocumentVM model)
        {
            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocument));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<ApprovalForAllDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForAllDocumentVM>>(json);
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
        public (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ApprovalForAllDocumentVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocument));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<ApprovalForAllDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ApprovalForAllDocumentVM>>(json);
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
        public async Task<(ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message)> DocumentCommentHistoryById(long id)
        {
            (ExecutionState executionState, List<ApprovalForAllDocumentVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ApprovalForAllDocumentComments + "?documentId=" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ApprovalForAllDocumentVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ApprovalForAllDocumentVM>>>(json)!;
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