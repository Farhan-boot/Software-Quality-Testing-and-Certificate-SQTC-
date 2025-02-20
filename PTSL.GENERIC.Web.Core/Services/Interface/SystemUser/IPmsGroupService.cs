﻿using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;

namespace PTSL.GENERIC.Web.Core.Services.Interface.SystemUser
{
	public interface IPmsGroupService
	{
		(ExecutionState executionState, List<PmsGroupVM> entity, string message) List();
		(ExecutionState executionState, PmsGroupVM entity, string message) Create(PmsGroupVM model);
		(ExecutionState executionState, PmsGroupVM entity, string message) GetById(long? id);
		(ExecutionState executionState, PmsGroupVM entity, string message) Update(PmsGroupVM model);
		(ExecutionState executionState, PmsGroupVM entity, string message) Delete(long? id);
	}
}
