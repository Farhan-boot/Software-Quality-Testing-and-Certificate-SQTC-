using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.Model.EntityViewModels
{
    public class PmsGroupVM : BaseModel
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public byte GroupStatus { get; set; }
        public byte IsVisible { get; set; }
    }
}
