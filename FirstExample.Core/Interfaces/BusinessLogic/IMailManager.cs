using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace FirstExample.Core.Interfaces.BusinessLogic
{
    public interface IMailManager
    {
        string SmtpServer { get; }
        int SmtpPort { get; }

        string From { get; set; }
        string FromPassword { get; set; }

        void Send(string message, string email, bool isHtml = false);
    }
}
