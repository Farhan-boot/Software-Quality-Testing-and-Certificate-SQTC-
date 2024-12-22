using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.ApiResponseModel;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Services.Interface.Documents;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Core.Services.Implementation.Documents
{
    public class DefaultDocContentService : IDefaultDocContentService
    {
        private readonly HttpHelper _httpHelper;

        public DefaultDocContentService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }
        public async Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> GetDefaultDocByDocType(DocumentType documentType)
        {
            (ExecutionState executionState, DefaultDocumentContentVM entity, string message) returnResponse;
            try
            {
                DefaultDocumentContentVM model = new DefaultDocumentContentVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.GetDefaultDocContentByType + "?docTypeId=" + (int)documentType));
                var json = await _httpHelper.GetAsync(URL);
                WebApiResponse<DefaultDocumentContentVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DefaultDocumentContentVM>>(json);
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
