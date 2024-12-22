using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;

namespace PTSL.GENERIC.Common.Entity.Archive
{
    public class RegistrationArchive : BaseEntity
    {

        //Other Info
        public string? ArchiveName { get; set; }
        public string? DocumentDescription { get; set; }
        public DateTime? UploadDate { get; set; }
        public List<RegistrationArchiveFile>? RegistrationArchiveFiles { get; set; }
    }
}
