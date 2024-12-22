using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Documents
{
    public class DocumentService : IDocumentsService
    {
        private readonly HttpHelper _httpHelper;

        public DocumentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<DocumentsByTypeVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<DocumentsByTypeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocumentsList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<DocumentsByTypeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DocumentsByTypeVM>>>(json);
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
        public (ExecutionState executionState, DocumentsByTypeVM entity, string message) Create(DocumentsByTypeVM model)
        {
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocuments));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json);
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
        public (ExecutionState executionState, DocumentsByTypeVM entity, string message) CreateOfList(List<DocumentsByTypeVM> model)
        {
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocumentsCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json)!;
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
        public (ExecutionState executionState, DocumentsByTypeVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse;
            try
            {
                DocumentsByTypeVM model = new DocumentsByTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocuments + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json);
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
                DocumentsByTypeVM model = new DocumentsByTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocumentsDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json);
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
        public (ExecutionState executionState, DocumentsByTypeVM entity, string message) Update(DocumentsByTypeVM model)
        {
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocuments));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json);
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
        public (ExecutionState executionState, DocumentsByTypeVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, DocumentsByTypeVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, DocumentsByTypeVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectDocuments));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<DocumentsByTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentsByTypeVM>>(json);
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

        public async Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string? DocumentTitle)
        {
            (ExecutionState executionState, List<DocumentsByTypeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Documents/Search?ProjectRequestId={ProjectRequestId}&DocumentCategoriesId={DocumentCategoriesId}&DocumentTitle={DocumentTitle}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<DocumentsByTypeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DocumentsByTypeVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> DocumentsListByClientId(long clientId)
        {
            (ExecutionState executionState, List<DocumentsByTypeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Documents/DocumentsListByClientId?clientId={clientId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<DocumentsByTypeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DocumentsByTypeVM>>>(json)!;
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
