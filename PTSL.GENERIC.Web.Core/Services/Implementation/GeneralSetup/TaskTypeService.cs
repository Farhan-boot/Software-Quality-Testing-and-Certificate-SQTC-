﻿using Newtonsoft.Json;

using PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.GeneralSetup
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly HttpHelper _httpHelper;

        public TaskTypeService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<(ExecutionState executionState, List<TaskTypeVM> entity, string message)> List()
        {
            (ExecutionState executionState, List<TaskTypeVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskTypeList));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<List<TaskTypeVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<TaskTypeVM>>>(json)!;
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
        public (ExecutionState executionState, TaskTypeVM entity, string message) Create(TaskTypeVM model)
        {
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskType));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<TaskTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTypeVM>>(json);
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
        public (ExecutionState executionState, TaskTypeVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse;
            try
            {
                TaskTypeVM model = new TaskTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskType + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TaskTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTypeVM>>(json);
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
                TaskTypeVM model = new TaskTypeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskTypeDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<TaskTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTypeVM>>(json);
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
        public (ExecutionState executionState, TaskTypeVM entity, string message) Update(TaskTypeVM model)
        {
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskType));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<TaskTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTypeVM>>(json);
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
        public (ExecutionState executionState, TaskTypeVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, TaskTypeVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, TaskTypeVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.TaskType));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<TaskTypeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<TaskTypeVM>>(json);
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