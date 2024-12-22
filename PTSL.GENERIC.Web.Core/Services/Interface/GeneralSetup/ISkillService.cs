using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Services.Interface.GeneralSetup
{
    public interface ISkillService
    {
        (ExecutionState executionState, List<SkillVM> entity, string message) List();
        (ExecutionState executionState, SkillVM entity, string message) Create(SkillVM model);
        (ExecutionState executionState, SkillVM entity, string message) GetById(long? id);
        (ExecutionState executionState, SkillVM entity, string message) Update(SkillVM model);
        (ExecutionState executionState, SkillVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
