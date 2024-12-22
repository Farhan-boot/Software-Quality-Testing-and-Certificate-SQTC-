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
    public class DepositSlipFileService : BaseService<DepositSlipFileVM, DepositSlipFile>, IDepositSlipFileService
    {
        public IMapper _mapper;

        public DepositSlipFileService(IDepositSlipFileBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override DepositSlipFile CastModelToEntity(DepositSlipFileVM model)
        {
            return _mapper.Map<DepositSlipFile>(model);
        }

        public override DepositSlipFileVM CastEntityToModel(DepositSlipFile entity)
        {
            return _mapper.Map<DepositSlipFileVM>(entity);
        }

        public override IList<DepositSlipFileVM> CastEntityToModel(IQueryable<DepositSlipFile> entity)
        {
            return _mapper.Map<IList<DepositSlipFileVM>>(entity).ToList();
        }
    }
}