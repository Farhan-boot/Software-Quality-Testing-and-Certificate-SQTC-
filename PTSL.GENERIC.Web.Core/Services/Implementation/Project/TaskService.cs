using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class TaskService : ITaskService
    {
        private readonly HttpHelper _httpHelper;
        public TaskService(HttpHelper httpHelper)
        {
            this._httpHelper = httpHelper;
        }
        public async Task<(ExecutionState executionState, List<TaskVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TaskVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TaskVM>>>(json)!;
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data.OrderByDescending(s => s.Id).ToList();
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
        public async Task<(ExecutionState executionState, TaskVM entity, string message)> Create(TaskVM model)
        {
            (ExecutionState executionState, TaskVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Task));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<TaskVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskVM>>(json)!;
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

        public (ExecutionState executionState, TaskTimeTrackingVM entity, string message) CreateTimeTracking(TaskTimeTrackingVM model)
        {
            (ExecutionState executionState, TaskTimeTrackingVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskTimeTrackingCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                var responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTimeTrackingVM>>(json);
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
        public async Task<(ExecutionState executionState, TaskVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, TaskVM entity, string message) returnResponse;
            try
            {
                TaskVM model = new TaskVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Task + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<TaskVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskVM>>(json)!;
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
                TaskVM model = new TaskVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskDoesExist + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<TaskVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskVM>>(json)!;
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
        public async Task<(ExecutionState executionState, TaskVM entity, string message)> Update(TaskVM model)
        {
            (ExecutionState executionState, TaskVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Task));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<TaskVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskVM>>(json)!;
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
        public async Task<(ExecutionState executionState, TaskVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, TaskVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TaskVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskSoftDelete + "/"+ id));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<TaskVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskVM>>(json)!;
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This task is not exist.";
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

        public (ExecutionState executionState, TaskCascadingDDLVM entity, string message) GetProjectsAndTaskTypes(long projectTypeId)
        {
            (ExecutionState executionState, TaskCascadingDDLVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"{URLHelper.TaskCascadeDDL}?projectTypeId={projectTypeId}"));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TaskCascadingDDLVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskCascadingDDLVM>>(json);
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

        public async Task<(ExecutionState executionState, List<TaskVM> entity, string message)> GetTaskListByUserId(long? id)
        {
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse;
            try
            {
                TaskVM model = new TaskVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetTaskListByUserId + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TaskVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TaskVM>>>(json)!;
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
        public (ExecutionState executionState, List<TaskVM> entity, string message) GetTaskListByProjectId(long? id)
        {
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse;
            try
            {
                TaskVM model = new TaskVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskListByProjectId + "?projectId=" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TaskVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TaskVM>>>(json)!;
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
        public (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) GetTaskTimeTrackList()
        {
            (ExecutionState executionState, List<TaskTimeTrackingVM> entity, string message) returnResponse;
            try
            {
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskTimeTrackingList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TaskTimeTrackingVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TaskTimeTrackingVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<TaskVM> entity, string message)> Search(long? ProjectRequestId, string? TaskId, long? AssigneeId, DateTime? CreateDate, DateTime? Deadline)
        {
            (ExecutionState executionState, List<TaskVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"Task/Search?ProjectRequestId={ProjectRequestId}&TaskId={TaskId}&AssigneeId={AssigneeId}&CreateDate={CreateDate}&Deadline={Deadline}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<IList<TaskVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<IList<TaskVM>>>(json)!;
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data.OrderByDescending(s => s.Id).ToList();
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
