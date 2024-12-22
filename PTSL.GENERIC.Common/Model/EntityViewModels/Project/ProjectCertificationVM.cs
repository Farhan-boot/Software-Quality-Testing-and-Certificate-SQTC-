using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
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
