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
    public class ReconciliationService : BaseService<ReconciliationVM, Reconciliation>, IReconciliationService
    {
        public IMapper _mapper;

        public ReconciliationService(IReconciliationBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override Reconciliation CastModelToEntity(ReconciliationVM model)
        {
            return _mapper.Map<Reconciliation>(model);
        }

        public override ReconciliationVM CastEntityToModel(Reconciliation entity)
        {
            return _mapper.Map<ReconciliationVM>(entity);
        }

        public override IList<ReconciliationVM> CastEntityToModel(IQueryable<Reconciliation> entity)
        {
            return _mapper.Map<IList<ReconciliationVM>>(entity).ToList();
        }
    }
}