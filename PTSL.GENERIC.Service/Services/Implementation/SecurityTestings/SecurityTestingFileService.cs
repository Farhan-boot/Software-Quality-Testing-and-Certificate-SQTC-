using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class SecurityTestingFileService : BaseService<SecurityTestingFileVM, SecurityTestingFile>, ISecurityTestingFileService
    {
        public readonly ISecurityTestingFileBusiness _SecurityTestingFileBusiness;
        public IMapper _mapper;
        public SecurityTestingFileService(ISecurityTestingFileBusiness SecurityTestingFileBusiness, IMapper mapper) : base(SecurityTestingFileBusiness)
        {
            _SecurityTestingFileBusiness = SecurityTestingFileBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override SecurityTestingFile CastModelToEntity(SecurityTestingFileVM model)
        {
            try
            {
                return _mapper.Map<SecurityTestingFile>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override SecurityTestingFileVM CastEntityToModel(SecurityTestingFile entity)
        {
            try
            {
                SecurityTestingFileVM model = _mapper.Map<SecurityTestingFileVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<SecurityTestingFileVM> CastEntityToModel(IQueryable<SecurityTestingFile> entity)
        {
            try
            {
                IList<SecurityTestingFileVM> colorList = _mapper.Map<IList<SecurityTestingFileVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
