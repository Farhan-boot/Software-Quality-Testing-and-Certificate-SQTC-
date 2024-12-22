using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.PermissionSettings
{
    public class PermissionRowSettingsService : BaseService<PermissionRowSettingsVM, PermissionRowSettings>, IPermissionRowSettingsService
    {
        private readonly IPermissionRowSettingsBusiness _business;
        public IMapper _mapper;

        public PermissionRowSettingsService(IPermissionRowSettingsBusiness business, IMapper mapper) : base(business)
        {
            _business = business;
            _mapper = mapper;
        }

        public override PermissionRowSettings CastModelToEntity(PermissionRowSettingsVM model)
        {
            return _mapper.Map<PermissionRowSettings>(model);
        }

        public override PermissionRowSettingsVM CastEntityToModel(PermissionRowSettings entity)
        {
            return _mapper.Map<PermissionRowSettingsVM>(entity);
        }

        public override IList<PermissionRowSettingsVM> CastEntityToModel(IQueryable<PermissionRowSettings> entity)
        {
            return _mapper.Map<IList<PermissionRowSettingsVM>>(entity).ToList();
        }

        public async Task<(ExecutionState executionState, List<PermissionRowSettingsVM> data, string message)> GetPermissionRowsByControllerName(string controller)
        {
            var result = await _business.GetPermissionRowsByControllerName(controller);
            return (result.executionState, _mapper.Map<List<PermissionRowSettingsVM>>(result.data), result.message);
        }
    }
}