namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Archive
{
    public class RegistrationArchiveFileVM : BaseModel
    {
        //Fk
        public long RegistrationArchiveId { get; set; }
        public RegistrationArchiveVM? RegistrationArchive { get; set; }
        public string? FileName { get; set; }
        public string? DocumentUrl { get; set; }
    }
}