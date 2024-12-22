using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class ApprovalForAllDocumentService : BaseService<ApprovalForAllDocumentVM, ApprovalForAllDocument>, IApprovalForAllDocumentService
    {
        public readonly IApprovalForAllDocumentBusiness _ApprovalForAllDocumentBusiness;
        public IMapper _mapper;
        public ApprovalForAllDocumentService(IApprovalForAllDocumentBusiness ApprovalForAllDocumentBusiness, IMapper mapper) : base(ApprovalForAllDocumentBusiness)
        {
            _ApprovalForAllDocumentBusiness = ApprovalForAllDocumentBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ApprovalForAllDocument CastModelToEntity(ApprovalForAllDocumentVM model)
        {
            try
            {
                return _mapper.Map<ApprovalForAllDocument>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ApprovalForAllDocumentVM CastEntityToModel(ApprovalForAllDocument entity)
        {
            try
            {
                ApprovalForAllDocumentVM model = _mapper.Map<ApprovalForAllDocumentVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ApprovalForAllDocumentVM> CastEntityToModel(IQueryable<ApprovalForAllDocument> entity)
        {
            try
            {
                IList<ApprovalForAllDocumentVM> colorList = _mapper.Map<IList<ApprovalForAllDocumentVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
