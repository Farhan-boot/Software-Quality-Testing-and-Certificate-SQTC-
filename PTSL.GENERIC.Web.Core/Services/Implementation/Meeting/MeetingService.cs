using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class MeetingService : IMeetingService
    {
        private readonly HttpHelper _httpHelper;

        public MeetingService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<MeetingVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<MeetingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<MeetingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<MeetingVM>>>(json);
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
        public async Task<(ExecutionState executionState, MeetingVM entity, string message)> Create(MeetingVM model)
        {
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Meeting));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<MeetingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingVM>>(json);
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
        public async Task<(ExecutionState executionState, MeetingVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse;
            try
            {
                MeetingVM model = new MeetingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Meeting + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<MeetingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingVM>>(json);
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
                MeetingVM model = new MeetingVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingDoesExist + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<MeetingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingVM>>(json);
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
        public async Task<(ExecutionState executionState, MeetingVM entity, string message)> Update(MeetingVM model)
        {
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Meeting));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<MeetingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingVM>>(json);
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

        public async Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetClientUser(long ProjectId)
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Meeting/GetClientUser?ProjectId={ProjectId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<UserVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserVM>>>(json);
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

        public async Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetSqtcUser()
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Meeting/GetSqtcUser"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<UserVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserVM>>>(json);
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

        public async Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> pendingMeetingList()
        {
            (ExecutionState executionState, List<MeetingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Meeting/PendingMeetingList"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<MeetingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<MeetingVM>>>(json);
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

        public async Task<(ExecutionState executionState, IList<MeetingVM> entity, string message)> MeetingListByClientId(long ClientId)
        {
            (ExecutionState executionState, List<MeetingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Meeting/MeetingListByClientId?clientID={ClientId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<MeetingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<MeetingVM>>>(json);
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
        public async Task<(ExecutionState executionState, MeetingVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, MeetingVM entity, string message) returnResponse;
            try
            {
                Task<(ExecutionState executionState, MeetingVM entity, string message)> IsExist = GetById(id);
                if (IsExist.Result.entity != null)
                {
                    IsExist.Result.entity.IsDeleted = true;
                    IsExist.Result.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.Result.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingSoftDelete));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<MeetingVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<MeetingVM>>(json);
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

        public (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id)
        {
            (ExecutionState executionState, bool isDeleted, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.MeetingSoftDelete), "/", id);
                var json = _httpHelper.Put(URL, respJson, "application/json");
                var result = JsonConvert.DeserializeObject<WebApiResponse<bool>>(json);
                returnResponse.executionState = result.ExecutionState;
                returnResponse.isDeleted = result.Data;
                returnResponse.message = result.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.isDeleted = false;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public async Task<(ExecutionState executionState, IList<MeetingVM>, string message)> MeetingListByDate(DateTime firstDate, DateTime lastDate)
        {
            firstDate = Convert.ToDateTime(firstDate);
            lastDate = Convert.ToDateTime(lastDate);

            (ExecutionState executionState, List<MeetingVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Meeting/MeetingListByDate?firstDate={firstDate.ToString("dd-MMM-yyyy")}&lastDate={lastDate.ToString("dd-MMM-yyyy")}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<MeetingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<MeetingVM>>>(json);
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