using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.GeneralSetup

{
    public class TaskType : BaseEntity
    {
        public string TaskTypeName { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }   
        public TaskAuthority   TaskAuthority { get; set; }
    }
}