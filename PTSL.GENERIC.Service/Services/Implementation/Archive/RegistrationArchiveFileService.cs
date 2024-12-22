using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface.Archive;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Model.EntityViewModels.Archive;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services.Archive
{
    public class RegistrationArchiveFileService : BaseService<RegistrationArchiveFileVM, RegistrationArchiveFile>, IRegistrationArchiveFileService
    {
        public IMapper _mapper;

        public RegistrationArchiveFileService(IRegistrationArchiveFileBusiness business, IMapper mapper) : base(business)
        {
            _mapper = mapper;
        }

        public override RegistrationArchiveFile CastModelToEntity(RegistrationArchiveFileVM model)
        {
            return _mapper.Map<RegistrationArchiveFile>(model);
        }

        public override RegistrationArchiveFileVM CastEntityToModel(RegistrationArchiveFile entity)
        {
            return _mapper.Map<RegistrationArchiveFileVM>(entity);
        }

        public override IList<RegistrationArchiveFileVM> CastEntityToModel(IQueryable<RegistrationArchiveFile> entity)
        {
            return _mapper.Map<IList<RegistrationArchiveFileVM>>(entity).ToList();
        }
    }
}