using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;

namespace PTSL.GENERIC.Web.Core.Services.Interface.SystemUser
{
    public interface IUserService
	{
		(ExecutionState executionState, List<UserVM> entity, string message) List();
		(ExecutionState executionState, UserVM entity, string message) Create(UserVM model);
		(ExecutionState executionState, UserVM entity, string message) GetById(long? id);
		(ExecutionState executionState, UserVM entity, string message) Update(UserVM model);
		(ExecutionState executionState, UserVM entity, string message) Delete(long? id);
		(ExecutionState executionState, LoginResultVM entity, string message) UserLogin(LoginVM model);
        (ExecutionState executionState, List<UserVM> entity, string message) GetUserInfoByUserRoleId(long userRoleId);
		Task<(ExecutionState executionState, IList<UserVM> entity, string message)> ClientWiseUserList(long ClienId);
		Task<(ExecutionState executionState, IList<UserVM> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone);

        //(ExecutionState executionState, IList<UserVM> entity, string message) ListSurveyAccounts(int takeLatest);
    }
}
