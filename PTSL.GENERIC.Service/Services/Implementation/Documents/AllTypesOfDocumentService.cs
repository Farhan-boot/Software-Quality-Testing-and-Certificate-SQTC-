using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Common.Entity.Documents;
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
    public class AllTypesOfDocumentService : BaseService<AllTypesOfDocumentVM, AllTypesOfDocument>, IAllTypesOfDocumentService
    {
        public readonly IAllTypesOfDocumentBusiness _typesOfDocumentBusiness;
        public IMapper _mapper;
        public AllTypesOfDocumentService(IAllTypesOfDocumentBusiness typesOfDocumentBusiness, IMapper mapper) : base(typesOfDocumentBusiness)
        {
            _typesOfDocumentBusiness = typesOfDocumentBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override AllTypesOfDocument CastModelToEntity(AllTypesOfDocumentVM model)
        {
            try
            {
                return _mapper.Map<AllTypesOfDocument>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override AllTypesOfDocumentVM CastEntityToModel(AllTypesOfDocument entity)
        {
            try
            {
                AllTypesOfDocumentVM model = _mapper.Map<AllTypesOfDocumentVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<AllTypesOfDocumentVM> CastEntityToModel(IQueryable<AllTypesOfDocument> entity)
        {
            try
            {
                IList<AllTypesOfDocumentVM> colorList = _mapper.Map<IList<AllTypesOfDocumentVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(ExecutionState executionState, IList<AllTypesOfDocumentVM> entity, string message)> ListByClientId(long clientId)
        {
            var result =await _typesOfDocumentBusiness.ListByClientId(clientId);
            return (ExecutionState.Success ,_mapper.Map<IList<AllTypesOfDocumentVM>>(result.entity),"Data Dound");
        }
    }
}
