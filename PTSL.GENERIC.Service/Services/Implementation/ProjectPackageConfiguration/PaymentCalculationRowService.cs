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
    public class PaymentCalculationRowService : BaseService<PaymentCalculationRowVM, PaymentCalculationRow>, IPaymentCalculationRowService
    {
        public IMapper _mapper;

        public PaymentCalculationRowService(IPaymentCalculationRowBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override PaymentCalculationRow CastModelToEntity(PaymentCalculationRowVM model)
        {
            return _mapper.Map<PaymentCalculationRow>(model);
        }

        public override PaymentCalculationRowVM CastEntityToModel(PaymentCalculationRow entity)
        {
            return _mapper.Map<PaymentCalculationRowVM>(entity);
        }

        public override IList<PaymentCalculationRowVM> CastEntityToModel(IQueryable<PaymentCalculationRow> entity)
        {
            return _mapper.Map<IList<PaymentCalculationRowVM>>(entity).ToList();
        }
    }
}