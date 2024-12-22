using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Documents
{
    public class AllTypesDocumentService : IAllTypesDocumentService
    {
        private readonly HttpHelper _httpHelper;

        public AllTypesDocumentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocumentList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<AllTypesOfDocumentVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<AllTypesOfDocumentVM>>>(json);
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
        public async Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Create(AllTypesOfDocumentVM model)
        {
            (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse;
            try
            {
                model.DocumentApprovalStatus = DocumentApprovalStatus.Pending;
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocument));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<AllTypesOfDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AllTypesOfDocumentVM>>(json);
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
         public async Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse;
            try
            {
                AllTypesOfDocumentVM model = new AllTypesOfDocumentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocument + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<AllTypesOfDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AllTypesOfDocumentVM>>(json);
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
                AllTypesOfDocumentVM model = new AllTypesOfDocumentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocumentExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<AllTypesOfDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AllTypesOfDocumentVM>>(json);
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
        public async Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Update(AllTypesOfDocumentVM model)
        {
            (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocument));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<AllTypesOfDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AllTypesOfDocumentVM>>(json);
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
        public async Task<(ExecutionState executionState, AllTypesOfDocumentVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, AllTypesOfDocumentVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AllTypesDocument));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<AllTypesOfDocumentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AllTypesOfDocumentVM>>(json);
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

        public async Task<(ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message)> ListByClientId(long clientId)
        {
            (ExecutionState executionState, List<AllTypesOfDocumentVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"AllTypesOfDocument/ListByClientId?ClientId={clientId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<AllTypesOfDocumentVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<AllTypesOfDocumentVM>>>(json);
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
