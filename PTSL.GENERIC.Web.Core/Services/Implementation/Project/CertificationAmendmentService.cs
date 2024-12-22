using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Documents
{
    public class CertificationAmendmentService : ICertificationAmendmentService
    {
        private readonly HttpHelper _httpHelper;

        public CertificationAmendmentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<DocumentAmendmentVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<DocumentAmendmentVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendmentList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<DocumentAmendmentVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DocumentAmendmentVM>>>(json);
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
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Create(DocumentAmendmentVM model)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendment));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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

        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> CreateDocAmendment(DocumentAmendmentVM model)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.CreateDocAmendment));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                DocumentAmendmentVM model = new DocumentAmendmentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendment + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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
                DocumentAmendmentVM model = new DocumentAmendmentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendmentDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Update(DocumentAmendmentVM model)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendment));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, DocumentAmendmentVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocAmendment));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json);
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
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> DocumentAmendmentByDocId(long id)
        {
            (ExecutionState executionState, DocumentAmendmentVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentAmendmentByDocId + "?documentId=" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<DocumentAmendmentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentAmendmentVM>>(json)!;
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
