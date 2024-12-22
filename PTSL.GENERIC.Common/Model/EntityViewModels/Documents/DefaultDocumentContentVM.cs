using PTSL.GENERIC.Common.Enum.Documents;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Documents
{
    public class DefaultDocumentContentVM : BaseModel
    {
        public DocumentType DocumentType { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
