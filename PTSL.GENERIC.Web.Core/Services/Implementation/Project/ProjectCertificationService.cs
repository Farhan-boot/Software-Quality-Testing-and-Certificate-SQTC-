using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Project;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Documents
{
    public class ProjectCertificationService : IProjectCertificationService
    {
        private readonly HttpHelper _httpHelper;

        public ProjectCertificationService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<ProjectCertificationVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<ProjectCertificationVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertificationList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<ProjectCertificationVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<ProjectCertificationVM>>>(json);
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
        public async Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Create(ProjectCertificationVM model)
        {
            (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertification));
                var json = await _httpHelper.PostAsync(URL, respJson, "application/json");
                WebApiResponse<ProjectCertificationVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectCertificationVM>>(json);
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
         public async Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> GetById(long? id)
        {
            (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse;
            try
            {
                ProjectCertificationVM model = new ProjectCertificationVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertification + "/" + id));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<ProjectCertificationVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectCertificationVM>>(json);
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
                ProjectCertificationVM model = new ProjectCertificationVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertificationDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<ProjectCertificationVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectCertificationVM>>(json);
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
        public async Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Update(ProjectCertificationVM model)
        {
            (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertification));
                var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                WebApiResponse<ProjectCertificationVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectCertificationVM>>(json);
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
        public async Task<(ExecutionState executionState, ProjectCertificationVM entity, string message)> Delete(long? id)
        {
            (ExecutionState executionState, ProjectCertificationVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, ProjectCertificationVM entity, string message) IsExist = await GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.ProjectCertification));
                    var json = await _httpHelper.PutAsync(URL, respJson, "application/json");
                    WebApiResponse<ProjectCertificationVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<ProjectCertificationVM>>(json);
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
