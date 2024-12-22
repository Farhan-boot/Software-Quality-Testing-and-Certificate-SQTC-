namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client
{
    public class ClientLogVM : BaseModel
    {
        public long? ClientID { get; set; }
        public ClientVM Client { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedUserName { get; set; }
    }
}
