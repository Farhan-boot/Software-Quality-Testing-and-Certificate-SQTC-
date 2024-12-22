using Newtonsoft.Json;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup
{
    public class DocumentCategoriesService : IDocumentCategoriesService
    {
        private readonly HttpHelper _httpHelper;

        public DocumentCategoriesService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) List()
        {
            (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategoriesList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<DocumentCategoriesVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DocumentCategoriesVM>>>(json);
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
        public (ExecutionState executionState, DocumentCategoriesVM entity, string message) Create(DocumentCategoriesVM model)
        {
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategories));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<DocumentCategoriesVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentCategoriesVM>>(json);
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
        public (ExecutionState executionState, DocumentCategoriesVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse;
            try
            {
                DocumentCategoriesVM model = new DocumentCategoriesVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategories + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DocumentCategoriesVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentCategoriesVM>>(json);
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
                DocumentCategoriesVM model = new DocumentCategoriesVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategoriesDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DocumentCategoriesVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentCategoriesVM>>(json);
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
        public (ExecutionState executionState, DocumentCategoriesVM entity, string message) Update(DocumentCategoriesVM model)
        {
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategories));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<DocumentCategoriesVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentCategoriesVM>>(json);
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
        public (ExecutionState executionState, DocumentCategoriesVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, DocumentCategoriesVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, DocumentCategoriesVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DocumentCategories));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<DocumentCategoriesVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DocumentCategoriesVM>>(json);
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
    }
}