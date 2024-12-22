using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Archive;

namespace PTSL.GENERIC.Web.Core.Services.Interface.Archive
{
    public interface IRegistrationArchiveFileService
    {
        (ExecutionState executionState, List<RegistrationArchiveFileVM> entity, string message) List();
        (ExecutionState executionState, RegistrationArchiveFileVM entity, string message) Create(RegistrationArchiveFileVM model);
        (ExecutionState executionState, RegistrationArchiveFileVM entity, string message) GetById(long? id);
        (ExecutionState executionState, RegistrationArchiveFileVM entity, string message) Update(RegistrationArchiveFileVM model);
        (ExecutionState executionState, RegistrationArchiveFileVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);

    }
}