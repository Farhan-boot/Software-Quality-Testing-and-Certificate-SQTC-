using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class ProjectStateLog : BaseEntity
    {
        public long ProjectRequestId { get; set;}
        public ProjectRequest? ProjectRequest { get; set;}
        public  ProjectState ProjectState { get; set;}
        public bool IsStateCompleted { get; set;}
    }
}
