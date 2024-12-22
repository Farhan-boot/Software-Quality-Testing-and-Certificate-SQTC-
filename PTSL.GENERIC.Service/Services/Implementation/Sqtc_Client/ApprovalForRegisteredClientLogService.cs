using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ApprovalForRegisteredClientLog;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class ApprovalForRegisteredClientLogService : BaseService<ApprovalForRegisteredClientLogVM, ApprovalForRegisteredClientLog>, IApprovalForRegisteredClientLogService
    {
        public readonly IApprovalForRegisteredClientLogBusiness _ApprovalForRegisteredClientLogBusiness;
        public IMapper _mapper;
        public ApprovalForRegisteredClientLogService(IApprovalForRegisteredClientLogBusiness ApprovalForRegisteredClientLogBusiness, IMapper mapper) : base(ApprovalForRegisteredClientLogBusiness)
        {
            _ApprovalForRegisteredClientLogBusiness = ApprovalForRegisteredClientLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ApprovalForRegisteredClientLog CastModelToEntity(ApprovalForRegisteredClientLogVM model)
        {
            try
            {
                return _mapper.Map<ApprovalForRegisteredClientLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ApprovalForRegisteredClientLogVM CastEntityToModel(ApprovalForRegisteredClientLog entity)
        {
            try
            {
                ApprovalForRegisteredClientLogVM model = _mapper.Map<ApprovalForRegisteredClientLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ApprovalForRegisteredClientLogVM> CastEntityToModel(IQueryable<ApprovalForRegisteredClientLog> entity)
        {
            try
            {
                IList<ApprovalForRegisteredClientLogVM> colorList = _mapper.Map<IList<ApprovalForRegisteredClientLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
