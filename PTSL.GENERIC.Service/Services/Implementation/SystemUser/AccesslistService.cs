using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation
{
    public class AccesslistService : BaseService<AccesslistVM, Accesslist>, IAccesslistService
    {
        public readonly IAccesslistBusiness _AccesslistBusiness;
        public IMapper _mapper;
        public AccesslistService(IAccesslistBusiness AccesslistBusiness, IMapper mapper) : base(AccesslistBusiness)
        {
            _AccesslistBusiness = AccesslistBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here


        public override Accesslist CastModelToEntity(AccesslistVM model)
        {
            try
            {
                return _mapper.Map<Accesslist>(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override AccesslistVM CastEntityToModel(Accesslist entity)
        {
            try
            {
                AccesslistVM model = _mapper.Map<AccesslistVM>(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override IList<AccesslistVM> CastEntityToModel(IQueryable<Accesslist> entity)
        {
            try
            {
                IList<AccesslistVM> colorList = _mapper.Map<IList<AccesslistVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<(ExecutionState executionState, long data, string message)> GetAccessListId(string accessUrl)
        {
            return _AccesslistBusiness.GetAccessListId(accessUrl);
        }

        public async Task<(ExecutionState executionState, IList<AccesslistVM> entity, string message)> ListForApproval()
        {
            var result = await _AccesslistBusiness.ListForApproval();
            if (result.executionState == ExecutionState.Retrieved)
            {
                return (result.executionState, CastEntityToModel(result.entity), result.message);
            }

            return (result.executionState, null!, result.message);
        }
    }
}
