using PTSL.GENERIC.Web.Core.Enum.Documents;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents
{
    public class DefaultDocumentContentVM : BaseModel
    {
        public DocumentType DocumentType { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
