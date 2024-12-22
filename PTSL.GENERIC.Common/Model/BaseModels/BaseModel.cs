using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.Model.BaseModels
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

    public class DropdownVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DropdownLongVM
    {
        public long Id { get; set; }
        public string? Name { get; set; } = string.Empty;
    }
}
