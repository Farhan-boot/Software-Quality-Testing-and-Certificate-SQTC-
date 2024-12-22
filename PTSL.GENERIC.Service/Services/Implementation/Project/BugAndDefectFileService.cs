using AutoMapper;

using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services
{
    public class BugAndDefectFileService : BaseService<BugAndDefectFileVM, BugAndDefectFile>, IBugAndDefectFileService
    {
        public readonly IBugAndDefectFileBusiness _BugAndDefectFileBusiness;
        public IMapper _mapper;
        public BugAndDefectFileService(IBugAndDefectFileBusiness BugAndDefectFileBusiness, IMapper mapper) : base(BugAndDefectFileBusiness)
        {
            _BugAndDefectFileBusiness = BugAndDefectFileBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override BugAndDefectFile CastModelToEntity(BugAndDefectFileVM model)
        {
            try
            {
                return _mapper.Map<BugAndDefectFile>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override BugAndDefectFileVM CastEntityToModel(BugAndDefectFile entity)
        {
            try
            {
                BugAndDefectFileVM model = _mapper.Map<BugAndDefectFileVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<BugAndDefectFileVM> CastEntityToModel(IQueryable<BugAndDefectFile> entity)
        {
            try
            {
                IList<BugAndDefectFileVM> colorList = _mapper.Map<IList<BugAndDefectFileVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
