using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog
{
    public class ClientLogVM : BaseModel
    {
        public long? ClientID { get; set; }
        public ClientVM Client { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedUserName { get; set; }
    }
}
