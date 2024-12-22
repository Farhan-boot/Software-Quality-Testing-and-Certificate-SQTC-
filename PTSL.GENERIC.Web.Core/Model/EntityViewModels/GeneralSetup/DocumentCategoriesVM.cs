using System.ComponentModel.DataAnnotations;

namespace PTSL.GENERIC.Web.Core.Model.GeneralSetup
{
    public class DocumentCategoriesVM : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

}
