using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Documents
{
    public class DefaultDocService : BaseService<DefaultDocumentContentVM, DefaultDocumentContent>, IDefaultDocContentService
    {
        public readonly IDefaultDocContentBusiness _defaultdocBusiness;
        public IMapper _mapper;
        public DefaultDocService(IDefaultDocContentBusiness defaultdocBusiness, IMapper mapper) : base(defaultdocBusiness)
        {
            _defaultdocBusiness = defaultdocBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override DefaultDocumentContent CastModelToEntity(DefaultDocumentContentVM model)
        {
            try
            {
                return _mapper.Map<DefaultDocumentContent>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override DefaultDocumentContentVM CastEntityToModel(DefaultDocumentContent entity)
        {
            try
            {
                DefaultDocumentContentVM model = _mapper.Map<DefaultDocumentContentVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<DefaultDocumentContentVM> CastEntityToModel(IQueryable<DefaultDocumentContent> entity)
        {
            try
            {
                IList<DefaultDocumentContentVM> colorList = _mapper.Map<IList<DefaultDocumentContentVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<DefaultDocumentContent> CastProjectDocumentsModelToEntity(List<DefaultDocumentContentVM> entity)
        {
            return _mapper.Map<List<DefaultDocumentContent>>(entity);
        }

        public List<DefaultDocumentContentVM> CastProjectDocumentsEntityListToModel(List<DefaultDocumentContent> entity)
        {
            return _mapper.Map<List<DefaultDocumentContentVM>>(entity);
        }
        public async Task<(ExecutionState executionState, DefaultDocumentContentVM entity, string message)> GetDefaultDocByDocType(DocumentType documentType)
        {
            var result = await _defaultdocBusiness.GetDefaultDocByDocType(documentType);
            return (ExecutionState.Retrieved, _mapper.Map<DefaultDocumentContentVM>(result.entity), "Data Found");
        }
    }
}
