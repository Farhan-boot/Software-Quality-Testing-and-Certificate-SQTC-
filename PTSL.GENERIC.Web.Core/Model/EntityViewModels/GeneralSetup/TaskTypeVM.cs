using PTSL.GENERIC.Web.Core.Helper.Enum;
using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Model.GeneralSetup
{
    public class TaskTypeVM : BaseModel
    {
        public string TaskTypeName { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public TaskAuthority TaskAuthority { get; set; }
    }
}
