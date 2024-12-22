using Microsoft.EntityFrameworkCore.Storage;
using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Implementation
{
    public class DesignationBusiness : BaseBusiness<Designation>, IDesignationBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        public DesignationBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
