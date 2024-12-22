using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Enum;
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
    public class DocumentsService : BaseService<DocumentsByTypeVM, DocumentsByType>, IDocumentsService
    {
        public readonly IDocumentsBusiness _documentBusiness;
        public IMapper _mapper;
        public DocumentsService(IDocumentsBusiness documentBusiness, IMapper mapper) : base(documentBusiness)
        {
            _documentBusiness = documentBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override DocumentsByType CastModelToEntity(DocumentsByTypeVM model)
        {
            try
            {
                return _mapper.Map<DocumentsByType>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override DocumentsByTypeVM CastEntityToModel(DocumentsByType entity)
        {
            try
            {
                DocumentsByTypeVM model = _mapper.Map<DocumentsByTypeVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<DocumentsByTypeVM> CastEntityToModel(IQueryable<DocumentsByType> entity)
        {
            try
            {
                IList<DocumentsByTypeVM> colorList = _mapper.Map<IList<DocumentsByTypeVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<DocumentsByType> CastProjectDocumentsModelToEntity(List<DocumentsByTypeVM> entity)
        {
            return _mapper.Map<List<DocumentsByType>>(entity);
        }

        public List<DocumentsByTypeVM> CastProjectDocumentsEntityListToModel(List<DocumentsByType> entity)
        {
            return _mapper.Map<List<DocumentsByTypeVM>>(entity);
        }
        public async Task<(ExecutionState executionState, DocumentsByTypeVM entity, string message)> CreateProjectDocumentsList(List<DocumentsByTypeVM> model)
        {
            var businessModel = CastProjectDocumentsModelToEntity(model);
            var result = await _documentBusiness.CreateProjectDocumentListAsync(businessModel);

            if (result.entity is not null)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, new DocumentsByTypeVM(), result.message);
        }

        public async Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> Search(long? ProjectRequestId, long? DocumentCategoriesId, string DocumentTitle)
        {
            var result = await _documentBusiness.Search(ProjectRequestId,DocumentCategoriesId,DocumentTitle);
            return (ExecutionState.Success, _mapper.Map<IList<DocumentsByTypeVM>>(result.entity), "Success");
        }

        public async Task<(ExecutionState executionState, IList<DocumentsByTypeVM> entity, string message)> DocumentsListByClientId(long clientId)
        {
            var result = await _documentBusiness.DocumentsListByClientId(clientId);
            return (ExecutionState.Retrieved, _mapper.Map<IList<DocumentsByTypeVM>>(result.entity), "Data Found");
        }
    }
}
