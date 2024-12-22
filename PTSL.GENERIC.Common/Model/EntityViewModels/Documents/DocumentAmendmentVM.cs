using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Documents
{
    public class DocumentAmendmentVM : BaseModel
    {
        public long AllTypesOfDocumentId { get; set; }
        public AllTypesOfDocumentVM? AllTypesOfDocument { get; set; }
        public string? AmendmentComment { get; set; }
    }
}
