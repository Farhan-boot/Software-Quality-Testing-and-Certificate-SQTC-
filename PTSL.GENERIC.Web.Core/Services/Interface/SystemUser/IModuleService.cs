using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;

namespace PTSL.GENERIC.Web.Core.Services.Interface.SystemUser
{
	public interface IModuleService
	{
		(ExecutionState executionState, List<ModuleVM> entity, string message) List();
		(ExecutionState executionState, ModuleVM entity, string message) Create(ModuleVM model);
		(ExecutionState executionState, ModuleVM entity, string message) GetById(long? id);
		(ExecutionState executionState, ModuleVM entity, string message) Update(ModuleVM model);
		(ExecutionState executionState, ModuleVM entity, string message) Delete(long? id);
	}
}
