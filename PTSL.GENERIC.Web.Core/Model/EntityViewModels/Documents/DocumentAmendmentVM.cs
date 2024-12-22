namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents
{
    public class DocumentAmendmentVM : BaseModel
    {
        public long AllTypesOfDocumentId { get; set; }
        public AllTypesOfDocumentVM? AllTypesOfDocument { get; set; }
        public string? AmendmentComment { get; set; }
    }
}
