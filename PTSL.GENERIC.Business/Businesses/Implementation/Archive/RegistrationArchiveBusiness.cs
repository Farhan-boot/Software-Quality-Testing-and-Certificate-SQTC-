using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Business.Businesses.Interface.Archive;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using PTSL.GENERIC.DAL.UnitOfWork;
using PTSL.GENERIC.Common.Entity;

namespace PTSL.GENERIC.Business.Businesses.Implementation.Archive
{
    public class RegistrationArchiveBusiness : BaseBusiness<RegistrationArchive>, IRegistrationArchiveBusiness
    {
        public readonly GENERICUnitOfWork _unitOfWork;
        private readonly GENERICReadOnlyCtx _readOnlyCtx;

        public RegistrationArchiveBusiness(GENERICUnitOfWork unitOfWork, GENERICReadOnlyCtx context)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _readOnlyCtx = context;
        }
       

    }
}