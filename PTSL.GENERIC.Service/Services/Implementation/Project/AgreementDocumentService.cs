using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class AgreementDocumentService : BaseService<AgreementDocumentsVM, AgreementDocuments>, IAgreementDocumentService
    {
        public readonly IAgreementDocumentBusiness _AgreementDocumentsBusiness;
        public IMapper _mapper;
        public AgreementDocumentService(IAgreementDocumentBusiness AgreementDocumentsBusiness, IMapper mapper) : base(AgreementDocumentsBusiness)
        {
            _AgreementDocumentsBusiness = AgreementDocumentsBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override AgreementDocuments CastModelToEntity(AgreementDocumentsVM model)
        {
            try
            {
                return _mapper.Map<AgreementDocuments>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override AgreementDocumentsVM CastEntityToModel(AgreementDocuments entity)
        {
            try
            {
                AgreementDocumentsVM model = _mapper.Map<AgreementDocumentsVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<AgreementDocumentsVM> CastEntityToModel(IQueryable<AgreementDocuments> entity)
        {
            try
            {
                IList<AgreementDocumentsVM> colorList = _mapper.Map<IList<AgreementDocumentsVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
