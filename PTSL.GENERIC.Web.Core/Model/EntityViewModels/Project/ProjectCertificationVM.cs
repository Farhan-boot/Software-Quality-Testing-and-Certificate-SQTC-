using PTSL.GENERIC.Web.Core.Helper.Enum.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class ProjectCertificationVM : BaseModel
    {
        public long AllTypesOfDocumentId { get; set; }
        public string? CertificateFilePath { get; set; }
        public string? CertificateFileName { get; set; }
        public string? CertificateHashNo { get; set; }
        public string? CertificateContent { get; set; }
        public CertificationStatus CertificationStatus { get; set; }
        public AllTypesOfDocumentVM? AllTypesOfDocument { get; set; }
    }
}
