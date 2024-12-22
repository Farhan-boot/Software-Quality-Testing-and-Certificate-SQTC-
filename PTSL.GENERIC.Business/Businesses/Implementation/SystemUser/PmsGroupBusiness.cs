using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class PmsGroupBusiness : BaseBusiness<PmsGroup>, IPmsGroupBusiness
    {
        public PmsGroupBusiness(GENERICUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Implement System Busniess Logic here
    }
}
