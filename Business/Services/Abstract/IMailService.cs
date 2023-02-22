using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IMailService
    {
        void SendEmailAsync(string toEmail, string subject, string message);
    }
}
