using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.Documents
{
    public class ProjectCertificationService : BaseService<ProjectCertificationVM, ProjectCertification>, IProjectCertificationService
    {
        public readonly IProjectCertificationBusiness _projectCertificationBusiness;
        public IMapper _mapper;
        public ProjectCertificationService(IProjectCertificationBusiness projectCertificationBusiness, IMapper mapper) : base(projectCertificationBusiness)
        {
            _projectCertificationBusiness = projectCertificationBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override ProjectCertification CastModelToEntity(ProjectCertificationVM model)
        {
            try
            {
                return _mapper.Map<ProjectCertification>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override ProjectCertificationVM CastEntityToModel(ProjectCertification entity)
        {
            try
            {
                ProjectCertificationVM model = _mapper.Map<ProjectCertificationVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<ProjectCertificationVM> CastEntityToModel(IQueryable<ProjectCertification> entity)
        {
            try
            {
                IList<ProjectCertificationVM> colorList = _mapper.Map<IList<ProjectCertificationVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
