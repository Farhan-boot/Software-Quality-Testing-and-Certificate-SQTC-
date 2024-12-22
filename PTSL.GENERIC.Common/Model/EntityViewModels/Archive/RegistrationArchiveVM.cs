using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Archive
{
    public class RegistrationArchiveVM : BaseModel
    {

        //Other Info
        public string? ArchiveName { get; set; }
        public string? DocumentDescription { get; set; }
        public DateTime? UploadDate { get; set; }
        [SwaggerExclude]
        public List<RegistrationArchiveFile>? RegistrationArchiveFiles { get; set; }

    }
}
