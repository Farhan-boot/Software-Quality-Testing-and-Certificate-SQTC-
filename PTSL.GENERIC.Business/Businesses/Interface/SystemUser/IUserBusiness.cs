using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface
{
    public interface IUserBusiness : IBaseBusiness<User>
    {
        
        Task<(ExecutionState executionState, User entity, string message)> UserLogin(LoginVM model);
        //GetUserNameByUserRoleId
        Task<(ExecutionState executionState, List<User> entity, string message)> GetUserNameByUserRoleId(long userRoleId);
        Task<(ExecutionState executionState, List<User> entity, string message)> GetUserInfoByUserRoleId(long userRoleId);
        //Task<(ExecutionState executionState, User? entity, string message)> Register(UserRegisterModel model);
        Task<(ExecutionState executionState, User entity, string message)> Getuser(UserRegisterModel registration);
        Task<(ExecutionState execution, IList<User> entity, string message)> Search(long? userRoleId, string? userName, string? firstName, string? email, string? userPhone);


    }
}
