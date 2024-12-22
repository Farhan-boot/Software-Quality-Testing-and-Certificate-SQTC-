using AutoMapper;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.GeneralSetup;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Service.BaseServices;
using PTSL.GENERIC.Service.Services.Interface.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.Implementation.GeneralSetup
{
    public class SkillService : BaseService<SkillVM, Skill>, ISkillService
    {
        public readonly ISkillBusiness _skillBusiness;
        public IMapper _mapper;
        public SkillService(ISkillBusiness skillBusiness, IMapper mapper) : base(skillBusiness)
        {
            _skillBusiness = skillBusiness;
            _mapper = mapper;
        }

        //Implement System Busniess Logic here

        public override Skill CastModelToEntity(SkillVM model)
        {
            try
            {
                return _mapper.Map<Skill>(model);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public override SkillVM CastEntityToModel(Skill entity)
        {
            try
            {
                SkillVM model = _mapper.Map<SkillVM>(entity);
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override IList<SkillVM> CastEntityToModel(IQueryable<Skill> entity)
        {
            try
            {
                IList<SkillVM> colorList = _mapper.Map<IList<SkillVM>>(entity).ToList();
                return colorList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
