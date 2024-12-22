using Newtonsoft.Json;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup
{
    public class MeetingTypeService : IMeetingTypeService
    {
        private readonly HttpHelper _httpHelper;

        public MeetingTypeService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<MeetingTypeVM> entity, string message) List()
        {
            (ExecutionState executionState, List<MeetingTypeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingTypeList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<MeetingTypeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<MeetingTypeVM>>>(json);
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
        public (ExecutionState executionState, MeetingTypeVM entity, string message) Create(MeetingTypeVM model)
        {
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingType));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<MeetingTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingTypeVM>>(json);
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
        public (ExecutionState executionState, MeetingTypeVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse;
            try
            {
                MeetingTypeVM model = new MeetingTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingType + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<MeetingTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingTypeVM>>(json);
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
                MeetingTypeVM model = new MeetingTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingTypeDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<MeetingTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingTypeVM>>(json);
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
        public (ExecutionState executionState, MeetingTypeVM entity, string message) Update(MeetingTypeVM model)
        {
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingType));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<MeetingTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingTypeVM>>(json);
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
        public (ExecutionState executionState, MeetingTypeVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, MeetingTypeVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, MeetingTypeVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingType));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<MeetingTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingTypeVM>>(json);
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