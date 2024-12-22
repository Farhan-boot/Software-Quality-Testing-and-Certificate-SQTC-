using Microsoft.AspNetCore.Http;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;

namespace PTSL.GENERIC.Common.Entity.Sqtc_ClientLog
{
    public class ClientLog : BaseEntity
    {
        public long ClientID { get; set; }
        public Client Client { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

