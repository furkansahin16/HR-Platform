using Business.Services.Abstract;
using System.Net;
using System.Net.Mail;

namespace Business.Services.Concrete
{
    public class MailService : IMailService
    {
        public void SendEmailAsync(string toEmail, string subject, string message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(toEmail);
            msg.From = new MailAddress("hrms-bilgeadam@outlook.com");
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("hrms-bilgeadam@outlook.com", "A123b456c789");
            client.Port = 587;
            client.Host = "smtp-mail.outlook.com";
            client.EnableSsl = true;
            var t = new Thread(new ThreadStart(() => client.Send(msg)));
            t.IsBackground = true;
            t.Start();
        }
    }
}
