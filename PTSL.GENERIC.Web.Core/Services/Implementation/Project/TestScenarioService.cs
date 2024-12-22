using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class TestScenarioService : ITestScenarioService
    {
        private readonly HttpHelper _httpHelper;
        public TestScenarioService(HttpHelper httpHelper)
        {
            this._httpHelper = httpHelper;
        }
        public async Task<(ExecutionState executionState, List<TestScenarioVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenarioList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestScenarioVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestScenarioVM>>>(json)!;
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data.OrderByDescending(s=>s.Id).ToList();
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
        public async Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Create(TestScenarioVM model)
        {
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenario));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
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

        public (ExecutionState executionState, TestScenarioVM entity, string message) CreateOfList(List<TestScenarioVM> model)
        {
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenarioItemsCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
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

        //public (ExecutionState executionState, TaskTimeTrackingVM entity, string message) CreateTimeTracking(TaskTimeTrackingVM model)
        //{
        //    (ExecutionState executionState, TaskTimeTrackingVM entity, string message) returnResponse;
        //    try
        //    {
        //        var respJson = JsonConvert.SerializeObject(model);
        //        var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskTimeTrackingCreate));
        //        var json = _httpHelper.Post(URL, respJson, "application/json");
        //        var responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTimeTrackingVM>>(json);
        //        returnResponse.executionState = responseJson.ExecutionState;
        //        returnResponse.entity = responseJson.Data;
        //        returnResponse.message = responseJson.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        returnResponse.executionState = ExecutionState.Failure;
        //        returnResponse.entity = null;
        //        returnResponse.message = ex.Message.ToString();
        //    }
        //    return returnResponse;
        //}
        public (ExecutionState executionState, TestScenarioVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse;
            try
            {
                TestScenarioVM model = new TestScenarioVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenario + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
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
                TestScenarioVM model = new TestScenarioVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenarioDoesExist + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
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
        public async Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Update(TestScenarioVM model)
        {
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenario));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
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
        public async Task<(ExecutionState executionState, TestScenarioVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, TestScenarioVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TestScenarioVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    IsExist.entity.IsActive = false;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScenarioSoftDelete +"/"+id));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<TestScenarioVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScenarioVM>>(json)!;
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This scenario is not exist.";
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

        public (ExecutionState executionState, List<TestScenarioVM> entity, string message) GetTestScenarioByTaskId(long? id)
        {
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponse;
            try
            {
                TestScenarioVM model = new TestScenarioVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetTestScenarioByTaskId + "?taskId=" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestScenarioVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestScenarioVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<TestScenarioVM> entity, string message)> Search(long? ProjectRequestId, string TestScenarioNo, TaskPriority? taskPriority, long? CreatedBy, DateTime? PlannedExecutionDate, DateTime? ActualExecutionDate)
        {
            (ExecutionState executionState, List<TestScenarioVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"TestScenario/Search?ProjectRequestId={ProjectRequestId}&TestScenarioNo={TestScenarioNo}&TaskPriority={taskPriority}&CreatedBy={CreatedBy}&PlannedExecutionDate={PlannedExecutionDate}&ActualExecutionDate={ActualExecutionDate}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestScenarioVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestScenarioVM>>>(json)!;
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
