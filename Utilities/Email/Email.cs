using System.Net;
using System.Net.Mail;
using MethodParameters.VM;

namespace Utilities.Email
{
    public class Email
    {
        private SmtpClient client;
        private MailMessage email;
        public Email(EmailConfigVM emailConfigVM)
        {
            client = new SmtpClient(emailConfigVM.Host, emailConfigVM.Port)
            {
                EnableSsl = emailConfigVM.IsEnabledSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailConfigVM.User, emailConfigVM.Password)
            };
        }

        /// <summary>
        /// Send a Email 
        /// </summary>
        /// <param name="sender">Email of sender</param>
        /// <param name="receiver">Email of receiver</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="message">Body of the message or message text</param>
        /// <param name="isHtlm">Determines if the body of the message is html or not</param>
        public void SendEmail(string sender, string receiver, string subject, string message, bool isHtlm = false)
        {
            email = new MailMessage(sender, receiver, subject, message);
            email.IsBodyHtml = isHtlm;
            client.Send(email);
        }
    }
}
