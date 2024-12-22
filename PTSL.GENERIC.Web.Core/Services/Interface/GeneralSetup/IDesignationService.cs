using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.eCommerce.Web.Core.Services.Interface.GeneralSetup
{
    public interface IDesignationService
    {
        (ExecutionState executionState, List<DesignationVM> entity, string message) List();
        (ExecutionState executionState, DesignationVM entity, string message) Create(DesignationVM model);
        (ExecutionState executionState, DesignationVM entity, string message) GetById(long? id);
        (ExecutionState executionState, DesignationVM entity, string message) Update(DesignationVM model);
        (ExecutionState executionState, DesignationVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}
