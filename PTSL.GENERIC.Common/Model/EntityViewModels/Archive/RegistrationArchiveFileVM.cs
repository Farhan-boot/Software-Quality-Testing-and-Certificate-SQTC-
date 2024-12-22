using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Archive
{
    public class RegistrationArchiveFileVM : BaseModel
    {
        //Fk
        public long RegistrationArchiveId { get; set; }
        public RegistrationArchive? RegistrationArchive { get; set; }
        public string? FileName { get; set; }
        public string? DocumentUrl { get; set; }

    }
}
