using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Bugzilla;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class BugAndDefectService : IBugAndDefectService
    {
        private readonly HttpHelper _httpHelper;

        public BugAndDefectService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<BugAndDefectVM> entity, string message) List()
        {
            (ExecutionState executionState, List<BugAndDefectVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefectList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<BugAndDefectVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<BugAndDefectVM>>>(json);
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
        public (ExecutionState executionState, BugAndDefectVM entity, string message) Create(BugAndDefectVM model)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefect));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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
        public (ExecutionState executionState, BugAndDefectVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                BugAndDefectVM model = new BugAndDefectVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefect + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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
                BugAndDefectVM model = new BugAndDefectVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefectExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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
        public (ExecutionState executionState, BugAndDefectVM entity, string message) Update(BugAndDefectVM model)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefect));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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
        public (ExecutionState executionState, BugAndDefectVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, BugAndDefectVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.IsActive = false;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefectSoftDelete +"/"+id));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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

        public (ExecutionState executionState, BugAndDefectVM entity, string message) CreateOfList(List<BugAndDefectVM> model)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.BugAndDefectCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<BugAndDefectVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId, string? bugzillaId, string? defectId, BugAndDefectStatus? bugAndDefectStatus, BugAndDefectSeverity? bugAndDefectSeverity)
        {
            (ExecutionState executionState, List<BugAndDefectVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"BugAndDefect/Search?ProjectRequestId={ProjectRequestId}&TaskOfProjectId={TaskOfProjectId}&TestCaseId={TestCaseId}&bugzillaId={bugzillaId}&defectId={defectId}&BugAndDefectStatus={bugAndDefectStatus}&BugAndDefectSeverity={bugAndDefectSeverity}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<BugAndDefectVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<BugAndDefectVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, BugAndDefectVM entity, string message)> SyncBugsFromBugzillaByProjId(long id)
        {
            (ExecutionState executionState, BugAndDefectVM entity, string message) returnResponse;
            try
            {
                BugAndDefectVM model = new BugAndDefectVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.SyncBugsWithBugzilla + "?projectId=" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<BugAndDefectVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugAndDefectVM>>(json);
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


        public async Task<(ExecutionState executionState, BugzillaSyncVM entity, string message)> SyncBugsDataViewByProjId(long id)
        {
            (ExecutionState executionState, BugzillaSyncVM entity, string message) returnResponse;
            try
            {
                BugzillaSyncVM model = new BugzillaSyncVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetViewListForBugzillaSync + "?projectId=" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<BugzillaSyncVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<BugzillaSyncVM>>(json);
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