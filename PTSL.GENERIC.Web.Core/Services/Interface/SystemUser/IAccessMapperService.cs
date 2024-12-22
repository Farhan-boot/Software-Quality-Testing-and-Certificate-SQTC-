using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;

namespace PTSL.GENERIC.Web.Core.Services.Interface.SystemUser
{
	public interface IAccessMapperService
	{
		(ExecutionState executionState, List<AccessMapperVM> entity, string message) List();
		(ExecutionState executionState, AccessMapperVM entity, string message) Create(AccessMapperVM model);
		(ExecutionState executionState, AccessMapperVM entity, string message) GetById(long? id);
		(ExecutionState executionState, AccessMapperVM entity, string message) Update(AccessMapperVM model);
		(ExecutionState executionState, AccessMapperVM entity, string message) Delete(long? id);
	}
}
