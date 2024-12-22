using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.Archive
{
    public class RegistrationArchiveFile : BaseEntity
    {
        //Fk
        public long RegistrationArchiveId { get; set; }
        public RegistrationArchive? RegistrationArchive { get; set; }
        public string? FileName { get; set; }
        public string? DocumentUrl { get; set; }
    }
}
