using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Service.BaseServices;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.PermissionSettings
{
    public class PermissionHeaderSettingsService : BaseService<PermissionHeaderSettingsVM, PermissionHeaderSettings>, IPermissionHeaderSettingsService
    {
        public IMapper _mapper;
        private readonly IPermissionHeaderSettingsBusiness _business;
        public PermissionHeaderSettingsService(IPermissionHeaderSettingsBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
            _business = business;
        }

        public override PermissionHeaderSettings CastModelToEntity(PermissionHeaderSettingsVM model)
        {
            return _mapper.Map<PermissionHeaderSettings>(model);
        }

        public override PermissionHeaderSettingsVM CastEntityToModel(PermissionHeaderSettings entity)
        {
            return _mapper.Map<PermissionHeaderSettingsVM>(entity);
        }

        public override IList<PermissionHeaderSettingsVM> CastEntityToModel(IQueryable<PermissionHeaderSettings> entity)
        {
            return _mapper.Map<IList<PermissionHeaderSettingsVM>>(entity).ToList();
        }


        public List<PermissionHeaderSettingsVM> CastEntityListToModel(List<PermissionHeaderSettings> entity)
        {
            return _mapper.Map<List<PermissionHeaderSettingsVM>>(entity);
        }

        public async Task<(ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message)> GetPermissionHeaderSettingsByModuleEnumId(long moduleEnumId)
        {
            var result = await _business.GetPermissionHeaderSettingsByModuleEnumId(moduleEnumId);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityListToModel(result.entity), result.message);
            }

            return (result.executionState, new List<PermissionHeaderSettingsVM>(), result.message);
        }


       

        //public Task<(ExecutionState executionState, long data, string message)> GetPermissionHeaderIdByControllerName(string controller)
        //{
        //    return _business.GetPermissionHeaderIdByControllerName(controller);
        //}
    }
}