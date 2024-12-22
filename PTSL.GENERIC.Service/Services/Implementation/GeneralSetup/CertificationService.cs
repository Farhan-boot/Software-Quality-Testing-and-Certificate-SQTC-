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
    public class CertificationService : BaseService<CertificationVM, Certification>, ICertificationService
    {
        public readonly ICertificationBusiness _CertificationBusiness;
        public IMapper _mapper;
        public CertificationService(ICertificationBusiness CertificationBusiness, IMapper mapper) : base(CertificationBusiness)
        {
            _CertificationBusiness = CertificationBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override Certification CastModelToEntity(CertificationVM model)
        {
            try
            {
                return _mapper.Map<Certification>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override CertificationVM CastEntityToModel(Certification entity)
        {
            try
            {
                CertificationVM model = _mapper.Map<CertificationVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<CertificationVM> CastEntityToModel(IQueryable<Certification> entity)
        {
            try
            {
                IList<CertificationVM> colorList = _mapper.Map<IList<CertificationVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
