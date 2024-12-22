using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup
{
    public interface IDocumentCategoriesService
    {
        (ExecutionState executionState, List<DocumentCategoriesVM> entity, string message) List();
        (ExecutionState executionState, DocumentCategoriesVM entity, string message) Create(DocumentCategoriesVM model);
        (ExecutionState executionState, DocumentCategoriesVM entity, string message) GetById(long? id);
        (ExecutionState executionState, DocumentCategoriesVM entity, string message) Update(DocumentCategoriesVM model);
        (ExecutionState executionState, DocumentCategoriesVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
