using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.HardwareTestings;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.HardwareTestings
{
    public class HardwareTestingService : IHardwareTestingService
    {
        private readonly HttpHelper _httpHelper;

        public HardwareTestingService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<HardwareTestingVM> entity, string message) List()
        {
            (ExecutionState executionState, List<HardwareTestingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTestingList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<HardwareTestingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<HardwareTestingVM>>>(json);
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
        public (ExecutionState executionState, HardwareTestingVM entity, string message) Create(HardwareTestingVM model)
        {
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTesting));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json);
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
        public (ExecutionState executionState, HardwareTestingVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse;
            try
            {
                HardwareTestingVM model = new HardwareTestingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTesting + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json);
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
                HardwareTestingVM model = new HardwareTestingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTestingDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json);
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
        public (ExecutionState executionState, HardwareTestingVM entity, string message) Update(HardwareTestingVM model)
        {
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTesting));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json);
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
        public (ExecutionState executionState, HardwareTestingVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, HardwareTestingVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTesting));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json);
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

        public (ExecutionState executionState, HardwareTestingVM entity, string message) CreateOfList(List<HardwareTestingVM> model)
        {
            (ExecutionState executionState, HardwareTestingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.HardwareTestingCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<HardwareTestingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<HardwareTestingVM>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<HardwareTestingVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestScopeId, string? SubItem)
        {
            (ExecutionState executionState, List<HardwareTestingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"HardwareTesting/Search?ProjectRequestId={ProjectRequestId}&TaskOfProjectId={TaskOfProjectId}&TestScopeId={TestScopeId}&SubItem={SubItem}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<HardwareTestingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<HardwareTestingVM>>>(json)!;
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