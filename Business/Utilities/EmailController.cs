using System.Net;
using System.Net.Mail;

namespace DeliverySoftware.Business.Utilities
{
    public class EmailController : IEmailController
    {
        private const string HOST_DOMAIN = "webhost.dynadot.com";
        private const string SENDER_EMAIL = "deliverysoftware@lampard.dev";
        private const string SENDER_PASS = "37067182";

        private SmtpClient CreateSMTPClient ()
        {
            SmtpClient _SMTPClient = new SmtpClient
            {
                Host = HOST_DOMAIN,
                Port = 587,
                Credentials = new NetworkCredential(SENDER_EMAIL, SENDER_PASS),
                EnableSsl = true,
                UseDefaultCredentials = false,
            };

            return _SMTPClient;
        }

        public async Task SendEmail (Email email)
        {
            MailMessage _Email = new MailMessage(
                SENDER_EMAIL,
                email.Recipient,
                email.Subject,
                email.Body
            );

            SmtpClient _EmailClient = CreateSMTPClient();

            _EmailClient.Send(_Email);

            _Email.Dispose();
        }

    }
}
