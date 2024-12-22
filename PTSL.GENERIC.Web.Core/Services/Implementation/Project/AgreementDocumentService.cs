using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class AgreementDocumentService : IAgreementDocumentService
    {
        private readonly HttpHelper _httpHelper;

        public AgreementDocumentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<AgreementDocumentsVM> entity, string message) List()
        {
            (ExecutionState executionState, List<AgreementDocumentsVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocumentsList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<AgreementDocumentsVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<AgreementDocumentsVM>>>(json);
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
        public (ExecutionState executionState, AgreementDocumentsVM entity, string message) Create(AgreementDocumentsVM model)
        {
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocuments));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<AgreementDocumentsVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AgreementDocumentsVM>>(json);
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
        public (ExecutionState executionState, AgreementDocumentsVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse;
            try
            {
                AgreementDocumentsVM model = new AgreementDocumentsVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocuments + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<AgreementDocumentsVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AgreementDocumentsVM>>(json);
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
                AgreementDocumentsVM model = new AgreementDocumentsVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocumentsDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<AgreementDocumentsVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AgreementDocumentsVM>>(json);
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
        public (ExecutionState executionState, AgreementDocumentsVM entity, string message) Update(AgreementDocumentsVM model)
        {
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocuments));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<AgreementDocumentsVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AgreementDocumentsVM>>(json);
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
        public (ExecutionState executionState, AgreementDocumentsVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, AgreementDocumentsVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, AgreementDocumentsVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AgreementDocuments));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<AgreementDocumentsVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AgreementDocumentsVM>>(json);
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