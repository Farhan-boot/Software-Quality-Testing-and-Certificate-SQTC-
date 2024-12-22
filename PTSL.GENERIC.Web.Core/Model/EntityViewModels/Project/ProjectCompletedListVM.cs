using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class ProjectCompletedListVM
    {
        public long? ProjectId { get; set; }
        public string ProjectCode { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public ProjectType? ProjectType { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public long? AllTypesOfDocumentId { get; set; }
        public string RequestDate { get; set; } = string.Empty;
        public CertificationStatus? CertificationStatus { get; set; }
        public int? CertificationStatusInt { get; set; }
        public bool IsShowCertificate { get; set; }
        public AllTypesOfDocumentVM? AllTypesOfDocument { get; set; }
    }
}
