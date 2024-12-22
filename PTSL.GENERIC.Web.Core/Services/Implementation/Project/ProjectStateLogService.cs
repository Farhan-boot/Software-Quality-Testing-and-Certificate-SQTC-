using Newtonsoft.Json;
using PTSL.eCommerce.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Helper;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Project
{
    public class ProjectStateLogService : IProjectStateLogService
    {
        private readonly HttpHelper _httpHelper;

        public ProjectStateLogService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<ProjectStateLogVM> entity, string message) List()
        {
            (ExecutionState executionState, List<ProjectStateLogVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLogList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<ProjectStateLogVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectStateLogVM>>>(json);
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
        public (ExecutionState executionState, ProjectStateLogVM entity, string message) Create(ProjectStateLogVM model)
        {
            (ExecutionState executionState, ProjectStateLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLog));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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
        public (ExecutionState executionState, ProjectStateLogVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, ProjectStateLogVM entity, string message) returnResponse;
            try
            {
                ProjectStateLogVM model = new ProjectStateLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLog + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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
                ProjectStateLogVM model = new ProjectStateLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLogDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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
        public (ExecutionState executionState, ProjectStateLogVM entity, string message) Update(ProjectStateLogVM model)
        {
            (ExecutionState executionState, ProjectStateLogVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLog));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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
        public (ExecutionState executionState, ProjectStateLogVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, ProjectStateLogVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ProjectStateLogVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectStateLog));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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

        public (ExecutionState executionState, ProjectStateLogVM entity, string message) GetLogData(long projectRequestId, long projectStateEnumId)
        {
            (ExecutionState executionState, ProjectStateLogVM entity, string message) returnResponse;
            try
            {
                ProjectStateLogVM model = new ProjectStateLogVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"ProjectStateLog/GetLogData?projectRequestId={projectRequestId}&projectStateEnumId={projectStateEnumId}"));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ProjectStateLogVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectStateLogVM>>(json);
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