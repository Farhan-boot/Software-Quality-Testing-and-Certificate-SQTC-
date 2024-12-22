using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser
{
    public class SendEmail
    {
        public string secretkey { get; set; }
        public string FromMail { get; set; }
        public List<string> ToMail { get; set; }
        public List<string>? BccList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TicketCopy { get; set; }
        public string Password { get; set; }
        public string? attach { get; set; }
    }
}
