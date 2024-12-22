using PTSL.GENERIC.Web.Core.Enum.Documents;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Documents
{
    public interface IDefaultDocContentService
    {
        //Task<(ExecutionState executionState, List<DefaultDocumentContentVM> entity, string message)> List();
        //Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> Create(DefaultDocumentContentVM model);
        //Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> GetById(long? id);
        //Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> Update(DefaultDocumentContentVM model);
        //Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> Delete(long? id);
        //(ExecutionState executionState, string message) DoesExist(long? id);
        Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> GetDefaultDocByDocType(DocumentType documentType);
    }
}
