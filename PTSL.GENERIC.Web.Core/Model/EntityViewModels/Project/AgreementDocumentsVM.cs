using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class AgreementDocumentsVM : BaseModel
    {
		public string Agreement_FirstPage { get; set; }
		public string Agreement_SecondPage { get; set; }
		public string Agreement_ThirdPage { get; set; }
		public string Agreement_ForthPage { get; set; }
		public string Agreement_LastPage { get; set; }
        public long? ProjectRequestId { get; set; }
		public ProjectRequestVM? ProjectRequest { get; set; }

		public long? ClientId { get; set; }
		public ClientVM? Client { get; set; }

		public bool IsDefault { get; set; }
	}
	
}
