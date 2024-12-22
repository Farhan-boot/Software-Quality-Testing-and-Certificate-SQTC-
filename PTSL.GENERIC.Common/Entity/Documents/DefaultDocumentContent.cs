using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Enum.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Documents
{
    public class DefaultDocumentContent : BaseEntity
    {
        public DocumentType DocumentType { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
