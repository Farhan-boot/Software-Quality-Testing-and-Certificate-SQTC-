using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
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
