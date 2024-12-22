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
    public class ReviewCommentService : BaseService<ReviewCommentVM, ReviewComment>, IReviewCommentService
    {
        public IMapper _mapper;

        public ReviewCommentService(IReviewCommentBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override ReviewComment CastModelToEntity(ReviewCommentVM model)
        {
            return _mapper.Map<ReviewComment>(model);
        }

        public override ReviewCommentVM CastEntityToModel(ReviewComment entity)
        {
            return _mapper.Map<ReviewCommentVM>(entity);
        }

        public override IList<ReviewCommentVM> CastEntityToModel(IQueryable<ReviewComment> entity)
        {
            return _mapper.Map<IList<ReviewCommentVM>>(entity).ToList();
        }
    }
}