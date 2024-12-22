using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IFeedbackService
    {
        (ExecutionState executionState, List<FeedbackVM> entity, string message) List();
        (ExecutionState executionState, FeedbackVM entity, string message) Create(FeedbackVM model);
        (ExecutionState executionState, FeedbackVM entity, string message) GetById(long? id);
        (ExecutionState executionState, FeedbackVM entity, string message) Update(FeedbackVM model);
        (ExecutionState executionState, FeedbackVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}