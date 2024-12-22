using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class ProjectRequestService : IProjectRequestService
    {
        private readonly HttpHelper _httpHelper;
        public ProjectRequestService(HttpHelper httpHelper)
        {
            this._httpHelper = httpHelper;
        }
        public async Task<(ExecutionState executionState, List<ProjectRequestVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequestList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectRequestVM>>>(json)!;
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

        public (ExecutionState executionState, List<ProjectRequestLogVM> entity, string message) LogHistoryById(long id)
        {
            (ExecutionState executionState, List<ProjectRequestLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequestLogHistory +"/"+id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<ProjectRequestLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectRequestLogVM>>>(json)!;
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
        public async Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Create(ProjectRequestVM model)
        {
            (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequest));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ProjectRequestVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectRequestVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse;
            try
            {
                ProjectRequestVM model = new ProjectRequestVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequest + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<ProjectRequestVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectRequestVM>>(json)!;
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
                ProjectRequestVM model = new ProjectRequestVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequestDoesExist + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<ProjectRequestVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectRequestVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Update(ProjectRequestVM model)
        {
            (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequest));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<ProjectRequestVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectRequestVM>>(json)!;
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
        public async Task<(ExecutionState executionState, ProjectRequestVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, ProjectRequestVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ProjectRequestVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequest));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<ProjectRequestVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectRequestVM>>(json)!;
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This project request is not exist.";
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

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> Search(string? ProjectName, ProjectType? ProjectType, string? ProjectCode, long? ClientId, DateTime? RequestDate)
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectRequest/Search?ProjectName={ProjectName}&ProjectType={ProjectType}&ProjectCode={ProjectCode}&ClientId={ClientId}&RequestDate={RequestDate}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectRequestVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectPendingList()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectRequest/GetProjectPendingList"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<IList<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<IList<ProjectRequestVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectRejectedList()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectRequest/GetProjectRejectedList"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<IList<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<IList<ProjectRequestVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectListByClientId(long clientId)
        {
            (ExecutionState executionState, List<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectRequest/GetProjectListByClientId?clientId={clientId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectRequestVM>>>(json)!;
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

        public (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id)
        {
            (ExecutionState executionState, bool isDeleted, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectRequestSoftDelete), "/", id);
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

        public async Task<(ExecutionState executionState, IList<ProjectRequestVM> entity, string message)> GetProjectAcceptedList()
        {
            (ExecutionState executionState, IList<ProjectRequestVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectRequest/GetProjectAcceptedList"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<IList<ProjectRequestVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<IList<ProjectRequestVM>>>(json)!;
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
