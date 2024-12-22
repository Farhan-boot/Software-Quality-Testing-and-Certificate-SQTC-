using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class TestStepService : ITestStepService
    {
        private readonly HttpHelper _httpHelper;

        public TestStepService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<TestStepVM> entity, string message) List()
        {
            (ExecutionState executionState, List<TestStepVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStepList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestStepVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestStepVM>>>(json);
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
        public (ExecutionState executionState, TestStepVM entity, string message) Create(TestStepVM model)
        {
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStep));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json);
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
        public (ExecutionState executionState, TestStepVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse;
            try
            {
                TestStepVM model = new TestStepVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStep + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json);
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
                TestStepVM model = new TestStepVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStepDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json);
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
        public (ExecutionState executionState, TestStepVM entity, string message) Update(TestStepVM model)
        {
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStep));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json);
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
        public (ExecutionState executionState, TestStepVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TestStepVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.IsActive = false;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStep));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json);
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

        public (ExecutionState executionState, TestStepVM entity, string message) CreateOfList(List<TestStepVM> model)
        {
            (ExecutionState executionState, TestStepVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestStepCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestStepVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestStepVM>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<TestStepVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, long? TestCaseId)
        {
            (ExecutionState executionState, List<TestStepVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"TestStep/Search?ProjectRequestId={ProjectRequestId}&TaskOfProjectId={TaskOfProjectId}&TestCaseId={TestCaseId}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestStepVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestStepVM>>>(json)!;
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