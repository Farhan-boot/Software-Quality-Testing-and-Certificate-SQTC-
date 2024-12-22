using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Documents
{
    public class DocumentAmendmentService : BaseService<DocumentAmendmentVM, DocumentAmendment>, IDocumentAmendmentService
    {
        public readonly IDocumentAmendmentBusiness _documentAmendmentBusiness;
        public IMapper _mapper;
        public DocumentAmendmentService(IDocumentAmendmentBusiness documentAmendmentBusiness, IMapper mapper) : base(documentAmendmentBusiness)
        {
            _documentAmendmentBusiness = documentAmendmentBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override DocumentAmendment CastModelToEntity(DocumentAmendmentVM model)
        {
            try
            {
                return _mapper.Map<DocumentAmendment>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override DocumentAmendmentVM CastEntityToModel(DocumentAmendment entity)
        {
            try
            {
                DocumentAmendmentVM model = _mapper.Map<DocumentAmendmentVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<DocumentAmendmentVM> CastEntityToModel(IQueryable<DocumentAmendment> entity)
        {
            try
            {
                IList<DocumentAmendmentVM> colorList = _mapper.Map<IList<DocumentAmendmentVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> CreateDocAmendment(DocumentAmendmentVM model)
        {

            try
            {
                var dbDoc = CastModelToEntity(model);
                var result = await _documentAmendmentBusiness.CreateDocAmendment(dbDoc);
                return (ExecutionState.Success, _mapper.Map<DocumentAmendmentVM>(result.entity), result.message);
            }
            catch (Exception ex)
            {
                return (ExecutionState.Success, new DocumentAmendmentVM(), "");
            }

        }
        public async Task<(ExecutionState executionState, DocumentAmendmentVM entity, string message)> GetAmendmentById(long docId)
        {

            try
            {
                //var dbDoc = CastModelToEntity(model);
                var result = await _documentAmendmentBusiness.GetDocAmendmentByDocId(docId);
                return (ExecutionState.Success, _mapper.Map<DocumentAmendmentVM>(result.entity), result.message);
            }
            catch (Exception ex)
            {
                return (ExecutionState.Success, new DocumentAmendmentVM(), "");
            }

        }
    }
}
