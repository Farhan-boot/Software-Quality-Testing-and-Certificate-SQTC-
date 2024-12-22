using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.HardwareTestings;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.HardwareTestings
{
    public class TestScopeService : ITestScopeService
    {
        private readonly HttpHelper _httpHelper;

        public TestScopeService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<TestScopeVM> entity, string message) List()
        {
            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScopeList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<TestScopeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestScopeVM>>>(json);
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
        public (ExecutionState executionState, TestScopeVM entity, string message) Create(TestScopeVM model)
        {
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScope));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json);
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
        public (ExecutionState executionState, TestScopeVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse;
            try
            {
                TestScopeVM model = new TestScopeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScope + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json);
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
                TestScopeVM model = new TestScopeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScopeDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json);
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
        public (ExecutionState executionState, TestScopeVM entity, string message) Update(TestScopeVM model)
        {
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScope));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json);
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
        public (ExecutionState executionState, TestScopeVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TestScopeVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScope));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json);
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

        public (ExecutionState executionState, TestScopeVM entity, string message) CreateOfList(List<TestScopeVM> model)
        {
            (ExecutionState executionState, TestScopeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TestScopeCreate));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TestScopeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TestScopeVM>>(json)!;
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

        public async Task<(ExecutionState executionState, IList<TestScopeVM> entity, string message)> Search(long? ProjectRequestId, long? TaskOfProjectId, string? TestItem,string? TenderID, string? SerialNo)
        {
            (ExecutionState executionState, List<TestScopeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"TestScope/Search?ProjectRequestId={ProjectRequestId}&TaskOfProjectId={TaskOfProjectId}&TestItem={TestItem}&TenderID={TenderID}&SerialNo={SerialNo}"));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TestScopeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TestScopeVM>>>(json)!;
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