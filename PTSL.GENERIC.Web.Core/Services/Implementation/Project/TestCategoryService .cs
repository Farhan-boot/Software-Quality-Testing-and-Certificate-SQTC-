using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class TestCategoryService : ITestCategoryService
    {
        private readonly HttpHelper _httpHelper;

        public TestCategoryService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<TestCategoryVM> entity, string message) List()
        {
            (ExecutionState executionState, List<TestCategoryVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategoryList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestCategoryVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestCategoryVM>>>(json);
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
        public (ExecutionState executionState, TestCategoryVM entity, string message) Create(TestCategoryVM model)
        {
            (ExecutionState executionState, TestCategoryVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategory));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestCategoryVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCategoryVM>>(json);
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
        public (ExecutionState executionState, TestCategoryVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TestCategoryVM entity, string message) returnResponse;
            try
            {
                TestCategoryVM model = new TestCategoryVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategory + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestCategoryVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCategoryVM>>(json);
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
                TestCategoryVM model = new TestCategoryVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategoryDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestCategoryVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCategoryVM>>(json);
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
        public (ExecutionState executionState, TestCategoryVM entity, string message) Update(TestCategoryVM model)
        {
            (ExecutionState executionState, TestCategoryVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategory));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<TestCategoryVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCategoryVM>>(json);
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
        public (ExecutionState executionState, TestCategoryVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, TestCategoryVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TestCategoryVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestCategory));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<TestCategoryVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestCategoryVM>>(json);
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