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
    public class FeedbackService : BaseService<FeedbackVM, Feedback>, IFeedbackService
    {
        public IMapper _mapper;

        public FeedbackService(IFeedbackBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override Feedback CastModelToEntity(FeedbackVM model)
        {
            return _mapper.Map<Feedback>(model);
        }

        public override FeedbackVM CastEntityToModel(Feedback entity)
        {
            return _mapper.Map<FeedbackVM>(entity);
        }

        public override IList<FeedbackVM> CastEntityToModel(IQueryable<Feedback> entity)
        {
            return _mapper.Map<IList<FeedbackVM>>(entity).ToList();
        }
    }
}