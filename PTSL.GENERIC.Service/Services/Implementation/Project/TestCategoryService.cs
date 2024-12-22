using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Service.Services.Implementation.Project
{
    public class TestCategoryService : BaseService<TestCategoryVM, TestCategory>, ITestCategoryService
    {
        public readonly ITestCategoryBusiness _TestCategoryBusiness;
        public IMapper _mapper;
        public TestCategoryService(ITestCategoryBusiness TestCategoryBusiness, IMapper mapper) : base(TestCategoryBusiness)
        {
            _TestCategoryBusiness = TestCategoryBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override TestCategory CastModelToEntity(TestCategoryVM model)
        {
            try
            {
                return _mapper.Map<TestCategory>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override TestCategoryVM CastEntityToModel(TestCategory entity)
        {
            try
            {
                TestCategoryVM model = _mapper.Map<TestCategoryVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<TestCategoryVM> CastEntityToModel(IQueryable<TestCategory> entity)
        {
            try
            {
                IList<TestCategoryVM> colorList = _mapper.Map<IList<TestCategoryVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProjectRequestVM> CastProjectEntityListToModel(List<ProjectRequest> entity)
        {
            return _mapper.Map<List<ProjectRequestVM>>(entity);
        }

        public List<TaskTypeVM> CastTaskTypeEntityListToModel(List<TaskType> entity)
        {
            return _mapper.Map<List<TaskTypeVM>>(entity);
        }
    }
}
