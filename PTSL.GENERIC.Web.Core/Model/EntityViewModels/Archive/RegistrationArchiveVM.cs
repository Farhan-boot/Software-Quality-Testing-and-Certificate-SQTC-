namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Archive
{
    public class RegistrationArchiveVM : BaseModel
    {

      

        

        //Other Info
        public string? ArchiveName { get; set; }
        public string? DocumentDescription { get; set; }
        public DateTime? UploadDate { get; set; }
        public List<RegistrationArchiveFileVM>? RegistrationArchiveFiles { get; set; } = new List<RegistrationArchiveFileVM>();
    }
}