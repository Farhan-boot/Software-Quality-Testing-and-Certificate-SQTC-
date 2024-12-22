using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public class PaymentCalculationHeaderService : BaseService<PaymentCalculationHeaderVM, PaymentCalculationHeader>, IPaymentCalculationHeaderService
    {
        public IMapper _mapper;
        public IPaymentCalculationHeaderBusiness _business;

        public PaymentCalculationHeaderService(IPaymentCalculationHeaderBusiness business, IMapper mapper,IPaymentCalculationHeaderBusiness paymentCalculationHeaderBusiness) : base(business)
        {
            _mapper = mapper;
            _business = paymentCalculationHeaderBusiness;
        }

        public override PaymentCalculationHeader CastModelToEntity(PaymentCalculationHeaderVM model)
        {
            return _mapper.Map<PaymentCalculationHeader>(model);
        }

        public override PaymentCalculationHeaderVM CastEntityToModel(PaymentCalculationHeader entity)
        {
            return _mapper.Map<PaymentCalculationHeaderVM>(entity);
        }

        public override IList<PaymentCalculationHeaderVM> CastEntityToModel(IQueryable<PaymentCalculationHeader> entity)
        {
            return _mapper.Map<IList<PaymentCalculationHeaderVM>>(entity).ToList();
        }

        public async Task<(ExecutionState executionState, IList<PaymentCalculationHeaderVM> entity, string message)> ListByClientId(long clientId)
        {
            var result = await _business.ListByClientId(clientId);
            return (ExecutionState.Success, _mapper.Map<IList<PaymentCalculationHeaderVM>>(result.entity),"Data Found");
        }
    }
}