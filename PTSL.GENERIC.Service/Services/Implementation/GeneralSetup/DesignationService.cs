using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class DesignationService : BaseService<DesignationVM, Designation>, IDesignationService
    {
        public readonly IDesignationBusiness _DesignationBusiness;
        public IMapper _mapper;
        public DesignationService(IDesignationBusiness DesignationBusiness, IMapper mapper) : base(DesignationBusiness)
        {
            _DesignationBusiness = DesignationBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override Designation CastModelToEntity(DesignationVM model)
        {
            try
            {
                return _mapper.Map<Designation>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override DesignationVM CastEntityToModel(Designation entity)
        {
            try
            {
                DesignationVM model = _mapper.Map<DesignationVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<DesignationVM> CastEntityToModel(IQueryable<Designation> entity)
        {
            try
            {
                IList<DesignationVM> colorList = _mapper.Map<IList<DesignationVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
