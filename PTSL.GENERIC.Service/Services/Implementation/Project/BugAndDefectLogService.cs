using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class BugAndDefectLogService : BaseService<BugAndDefectLogVM, BugAndDefectLog>, IBugAndDefectLogService
    {
        public readonly IBugAndDefectLogBusiness _bugAndDefectLogBusiness;
        public IMapper _mapper;
        public BugAndDefectLogService(IBugAndDefectLogBusiness bugAndDefectLogBusiness, IMapper mapper) : base(bugAndDefectLogBusiness)
        {
            _bugAndDefectLogBusiness = bugAndDefectLogBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override BugAndDefectLog CastModelToEntity(BugAndDefectLogVM model)
        {
            try
            {
                return _mapper.Map<BugAndDefectLog>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override BugAndDefectLogVM CastEntityToModel(BugAndDefectLog entity)
        {
            try
            {
                BugAndDefectLogVM model = _mapper.Map<BugAndDefectLogVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<BugAndDefectLogVM> CastEntityToModel(IQueryable<BugAndDefectLog> entity)
        {
            try
            {
                IList<BugAndDefectLogVM> colorList = _mapper.Map<IList<BugAndDefectLogVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
