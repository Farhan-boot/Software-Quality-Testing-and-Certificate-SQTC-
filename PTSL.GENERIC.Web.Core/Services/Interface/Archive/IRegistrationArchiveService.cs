using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Archive;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Archive
{
    public interface IRegistrationArchiveService
    {
        (ExecutionState executionState, List<RegistrationArchiveVM> entity, string message) List();
        (ExecutionState executionState, RegistrationArchiveVM entity, string message) Create(RegistrationArchiveVM model);
        (ExecutionState executionState, RegistrationArchiveVM entity, string message) GetById(long? id);
        (ExecutionState executionState, RegistrationArchiveVM entity, string message) Update(RegistrationArchiveVM model);
        (ExecutionState executionState, RegistrationArchiveVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
    }
}