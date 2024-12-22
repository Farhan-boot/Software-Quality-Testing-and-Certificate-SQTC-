using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class DocumentCategoriesService : BaseService<DocumentCategoriesVM, DocumentCategories>, IDocumentCategoriesService
    {
        public readonly IDocumentCategoriesBusiness _DocumentCategoriesBusiness;
        public IMapper _mapper;
        public DocumentCategoriesService(IDocumentCategoriesBusiness DocumentCategoriesBusiness, IMapper mapper) : base(DocumentCategoriesBusiness)
        {
            _DocumentCategoriesBusiness = DocumentCategoriesBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override DocumentCategories CastModelToEntity(DocumentCategoriesVM model)
        {
            try
            {
                return _mapper.Map<DocumentCategories>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override DocumentCategoriesVM CastEntityToModel(DocumentCategories entity)
        {
            try
            {
                DocumentCategoriesVM model = _mapper.Map<DocumentCategoriesVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<DocumentCategoriesVM> CastEntityToModel(IQueryable<DocumentCategories> entity)
        {
            try
            {
                IList<DocumentCategoriesVM> colorList = _mapper.Map<IList<DocumentCategoriesVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
