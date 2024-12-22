using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Interface;

public interface IUserService : IBaseService<UserVM, User>
{
    //Task<(ExecutionState executionState, IList<UserVM> entity, string message)> GetBeneficiaryByFilter(BeneficiaryUserFilterVM filter);
    //Task<(ExecutionState executionState, IList<UserVM> entity, string message)> ListSurveyAccounts(int takeLatest = 50);
    Task<(ExecutionState executionState, UserVM entity, string refreshToken, string message)> UserLogin(LoginVM model);
    Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetUserNameByUserRoleId(long userRoleId);
    //Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetFilterByForestId(ExecutiveCommitteeFilterVM filterData);
    Task<(ExecutionState executionState, List<UserVM> entity, string message)> GetUserInfoByUserRoleId(long userRoleId);
   // Task<(ExecutionState executionState, UserVM? entity, string message)> Register(UserRegisterModel model);
    Task<(ExecutionState executionState, UserVM entity, string message)> Getuser(UserRegisterModel model);
    Task<(ExecutionState execution, IList<UserVM> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone);



}
