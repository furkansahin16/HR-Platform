using Business.Helper.MailOptions;
using Business.Services.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Business.Services.Concrete
{
    public class SendGridService
    {
        private readonly ILogger<SendGridService> _logger;

        public AuthMessageSenderOptions Options { get; set; }

        public SendGridService(ILogger<SendGridService> logger, IOptions<AuthMessageSenderOptions> options)
        {
            _logger = logger;
            this.Options = options.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.ApiKey)) throw new Exception("Hatali mail servis hizmeti.");

            await Execute(Options.ApiKey, subject, message, toEmail);
        }

        private async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("furkansahin_97@hotmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(toEmail));

            //For disable click tracking
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);

            _logger.LogInformation(response.IsSuccessStatusCode
                ? $"{toEmail} adresine iletim başarılı."
                : $"{toEmail} adresine mail gönderilirken bir hata oluştu");
        }
    }
}
