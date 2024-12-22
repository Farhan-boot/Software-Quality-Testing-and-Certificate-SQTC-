using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Model.GeneralSetup
{
    public class MeetingTypeVM : BaseModel
    {
        [MaxLength(100)]
        public string MeetingTypeName { get; set; } = string.Empty;
    }
}
