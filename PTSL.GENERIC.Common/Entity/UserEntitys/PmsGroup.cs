using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.Entity
{
    public class PmsGroup : BaseEntity
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public byte GroupStatus { get; set; }
        public byte IsVisible { get; set; }
        //public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
