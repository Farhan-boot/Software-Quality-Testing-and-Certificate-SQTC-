using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class DocumentAmendment : BaseEntity
    {
        public long AllTypesOfDocumentId { get; set; }
        public AllTypesOfDocument? AllTypesOfDocument { get; set; }
        public string? AmendmentComment { get; set; }
    }
}
