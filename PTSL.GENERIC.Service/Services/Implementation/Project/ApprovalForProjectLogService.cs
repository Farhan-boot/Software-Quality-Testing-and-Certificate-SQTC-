using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class ApprovalForProjectLogService : BaseService<ApprovalForProjectLogVM, ApprovalForProjectLog>, IApprovalForProjectLogService
    {
        public readonly IApprovalForProjectLogBusiness _ApprovalForProjectLogBusiness;
        public IMapper _mapper;
        public ApprovalForProjectLogService(IApprovalForProjectLogBusiness ApprovalForProjectLogBusiness, IMapper mapper) : base(ApprovalForProjectLogBusiness)
        {
            _ApprovalForProjectLogBusiness = ApprovalForProjectLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ApprovalForProjectLog CastModelToEntity(ApprovalForProjectLogVM model)
        {
            try
            {
                return _mapper.Map<ApprovalForProjectLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ApprovalForProjectLogVM CastEntityToModel(ApprovalForProjectLog entity)
        {
            try
            {
                ApprovalForProjectLogVM model = _mapper.Map<ApprovalForProjectLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ApprovalForProjectLogVM> CastEntityToModel(IQueryable<ApprovalForProjectLog> entity)
        {
            try
            {
                IList<ApprovalForProjectLogVM> colorList = _mapper.Map<IList<ApprovalForProjectLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
