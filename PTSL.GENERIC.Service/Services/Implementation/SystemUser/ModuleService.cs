using AutoMapper;
using PTSL.GENERIC.Business;
using PTSL.GENERIC.Business.Businesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation
{
    public class ModuleService : BaseService<ModuleVM, Module>, IModuleService
    {
        public readonly IModuleBusiness _ModuleBusiness;
        public IMapper _mapper;
        public ModuleService(IModuleBusiness ModuleBusiness, IMapper mapper) : base(ModuleBusiness)
        {
            _ModuleBusiness = ModuleBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here


        public override Module CastModelToEntity(ModuleVM model)
        {
            try
            {
                return _mapper.Map<Module>(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override ModuleVM CastEntityToModel(Module entity)
        {
            try
            {
                ModuleVM model = _mapper.Map<ModuleVM>(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override IList<ModuleVM> CastEntityToModel(IQueryable<Module> entity)
        {
            try
            {
                IList<ModuleVM> colorList = _mapper.Map<IList<ModuleVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
