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
    public class AccessMapperService : BaseService<AccessMapperVM, AccessMapper>, IAccessMapperService
    {
        public readonly IAccessMapperBusiness _AccessMapperBusiness;
        public IMapper _mapper;
        public AccessMapperService(IAccessMapperBusiness AccessMapperBusiness, IMapper mapper) : base(AccessMapperBusiness)
        {
            _AccessMapperBusiness = AccessMapperBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here


        public override AccessMapper CastModelToEntity(AccessMapperVM model)
        {
            try
            {
                return _mapper.Map<AccessMapper>(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override AccessMapperVM CastEntityToModel(AccessMapper entity)
        {
            try
            {
                AccessMapperVM model = _mapper.Map<AccessMapperVM>(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override IList<AccessMapperVM> CastEntityToModel(IQueryable<AccessMapper> entity)
        {
            try
            {
                IList<AccessMapperVM> colorList = _mapper.Map<IList<AccessMapperVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
