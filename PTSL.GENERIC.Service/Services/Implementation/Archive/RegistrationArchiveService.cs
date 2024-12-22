using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Archive;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.Archive;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Archive
{
    public class RegistrationArchiveService : BaseService<RegistrationArchiveVM, RegistrationArchive>, IRegistrationArchiveService
    {
        public IMapper _mapper;
        private readonly IRegistrationArchiveBusiness _business;
        public RegistrationArchiveService(IRegistrationArchiveBusiness business, IMapper mapper) : base(business)
        {
            _business = business;
            _mapper = mapper;
        }

        public override RegistrationArchive CastModelToEntity(RegistrationArchiveVM model)
        {
            return _mapper.Map<RegistrationArchive>(model);
        }

        public override RegistrationArchiveVM CastEntityToModel(RegistrationArchive entity)
        {
            return _mapper.Map<RegistrationArchiveVM>(entity);
        }

        public override IList<RegistrationArchiveVM> CastEntityToModel(IQueryable<RegistrationArchive> entity)
        {
            return _mapper.Map<IList<RegistrationArchiveVM>>(entity).ToList();
        }


        public List<RegistrationArchiveVM> CastEntityListToModel(List<RegistrationArchive> entity)
        {
            return _mapper.Map<List<RegistrationArchiveVM>>(entity);
        }

        

    }
}