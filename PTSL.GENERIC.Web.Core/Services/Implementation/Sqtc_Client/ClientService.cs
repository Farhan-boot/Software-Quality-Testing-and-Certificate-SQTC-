using Newtonsoft.Json;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.eCommerce.Web.Core.Services.Interface.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup
{
    public class ClientService : IClientService
    {
        private readonly HttpHelper _httpHelper;

        public ClientService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<ClientVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ClientList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ClientVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ClientVM>>>(json)!;
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
        public async Task<(ExecutionState executionState, ClientVM entity, string message)> Create(ClientVM model)
        {
            (ExecutionState executionState, ClientVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Client));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ClientVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, ClientVM entity, string message) returnResponse;
            try
            {
                ClientVM model = new ClientVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Client + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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
        public async Task<(ExecutionState executionState, string message)> DoesExist(long? id)
        {
            (ExecutionState executionState, string message) returnResponse;
            try
            {
                ClientVM model = new ClientVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ClientDoesExist + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ClientVM entity, string message)> Update(ClientVM model)
        {
            (ExecutionState executionState, ClientVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Client));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ClientVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, ClientVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ClientVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Client));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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

        public async Task<(ExecutionState executionState, List<ClientVM> entity, string message)> Search(string? OrganizationName, ClientStatus? clientStatus, string? MobileNumber, DateTime? CreatedAt)
        {
            (ExecutionState executionState, List<ClientVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Client/Search?organizationName={OrganizationName}&ClientStatus={clientStatus}&mobileNo={MobileNumber}&CreatedAt={CreatedAt}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ClientVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ClientVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, ClientVM entity, string message)> ClientRegistration(ClientVM model)
        {
            (ExecutionState executionState, ClientVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ClientRegistration));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ClientVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ClientVM>>(json)!;
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

        public (ExecutionState executionState, List<ClientLogVM> entity, string message) ClientLogHistoryById(long id)
        {
            (ExecutionState executionState, List<ClientLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ClientLogHistory + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<ClientLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ClientLogVM>>>(json)!;
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