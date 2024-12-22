using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class TestCaseService : ITestCaseService
    {
        private readonly HttpHelper _httpHelper;

        public TestCaseService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<TestCaseVM> entity, string message) List()
        {
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCaseList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestCaseVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestCaseVM>>>(json);
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
        public (ExecutionState executionState, TestCaseVM entity, string message) Create(TestCaseVM model)
        {
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCase));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json);
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
        public (ExecutionState executionState, TestCaseVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse;
            try
            {
                TestCaseVM model = new TestCaseVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCase + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json);
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
                TestCaseVM model = new TestCaseVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCaseDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json);
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
        public (ExecutionState executionState, TestCaseVM entity, string message) Update(TestCaseVM model)
        {
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCase));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json);
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
        public (ExecutionState executionState, TestCaseVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TestCaseVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    IsExist.entity.IsActive = false;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCase));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json);
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

        public (ExecutionState executionState, TestCaseVM entity, string message) CreateOfList(List<TestCaseVM> model)
        {
            (ExecutionState executionState, TestCaseVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCaseCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestCaseVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCaseVM>>(json)!;
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


        public async Task<(ExecutionState executionState, List<TestCaseVM> entity, string message)> Search(string? TestCaseNo, long? ProjectRequestId, long? TestScenarioId, long? TestCategoryId, DateTime? ActualExecutionDate, DateTime? PlannedExecutionDate)
        {
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"TestCase/Search?TestCaseNo={TestCaseNo}&ProjectRequestId={ProjectRequestId}&TestScenarioId={TestScenarioId}&TestCategoryId={TestCategoryId}&ActualExecutionDate={ActualExecutionDate}&PlannedExecutionDate={PlannedExecutionDate}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestCaseVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestCaseVM>>>(json)!;
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

        public  (ExecutionState executionState, List<TestCaseVM> entity, string message) GetTestCasesByTaskofProjectId(long? id)
        {
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse;
            try
            {
                TestCaseVM model = new TestCaseVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetTestCasesByTaskofProjectId + "?taskOfProjectId=" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestCaseVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestCaseVM>>>(json)!;
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

        public async Task<(ExecutionState executionState, List<TestCaseVM> entity, string message)> GetTestCaseListByProjectRequestId(long projectRequestId)
        {
            (ExecutionState executionState, List<TestCaseVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"TestCase/GetTestCaseListByProjectRequestId?projectRequestId={projectRequestId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestCaseVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestCaseVM>>>(json)!;
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