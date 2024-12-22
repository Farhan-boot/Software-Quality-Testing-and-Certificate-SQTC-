using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IReviewCommentService
    {
        (ExecutionState executionState, List<ReviewCommentVM> entity, string message) List();
        (ExecutionState executionState, ReviewCommentVM entity, string message) Create(ReviewCommentVM model);
        (ExecutionState executionState, ReviewCommentVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ReviewCommentVM entity, string message) Update(ReviewCommentVM model);
        (ExecutionState executionState, ReviewCommentVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}