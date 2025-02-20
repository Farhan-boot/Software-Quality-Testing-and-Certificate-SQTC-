﻿using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.Model.EntityViewModels
{
    public class SupplierModel : BaseModel
    {
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
