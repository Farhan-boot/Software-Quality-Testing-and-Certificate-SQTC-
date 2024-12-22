using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public class PaymentInformationService : BaseService<PaymentInformationVM, PaymentInformation>, IPaymentInformationService
    {
        public IMapper _mapper;

        public PaymentInformationService(IPaymentInformationBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override PaymentInformation CastModelToEntity(PaymentInformationVM model)
        {
            return _mapper.Map<PaymentInformation>(model);
        }

        public override PaymentInformationVM CastEntityToModel(PaymentInformation entity)
        {
            return _mapper.Map<PaymentInformationVM>(entity);
        }

        public override IList<PaymentInformationVM> CastEntityToModel(IQueryable<PaymentInformation> entity)
        {
            return _mapper.Map<IList<PaymentInformationVM>>(entity).ToList();
        }
    }
}