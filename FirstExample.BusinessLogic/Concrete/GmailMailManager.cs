using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using FirstExample.Core.Interfaces.BusinessLogic;

namespace FirstExample.BusinessLogic.Concrete
{
    public class GmailMailManager : IMailManager
    {
        public string SmtpServer { get; } = "smtp.gmail.com";
        public int SmtpPort { get; } = 587;

        public string From { get; set; } = "sure70about@gmail.com";
        public string FromPassword { get; set; } = "Test_D_river_TDU2";

        public void Send(string message, string email, bool isHtml = false)
        {
            MailMessage Message = new MailMessage(From, email, null, message);
            Message.IsBodyHtml = isHtml;

            SmtpClient client = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new NetworkCredential(From, FromPassword),
                EnableSsl = true
            };

            client.Send(Message);
        }
    }
}
