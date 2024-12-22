using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class ProjectCertification : BaseEntity
    {
        public long AllTypesOfDocumentId { get; set; }
        public string? CertificateFilePath { get; set; }
        public string? CertificateFileName { get; set; }
        public string? CertificateHashNo { get; set; }
        public string? CertificateContent { get; set; }
        public CertificationStatus CertificationStatus { get; set; }
        public AllTypesOfDocument? AllTypesOfDocument { get; set; }
    }
}
