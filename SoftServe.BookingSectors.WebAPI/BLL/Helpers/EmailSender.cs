using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers
{
    /// <summary>
    /// Class for send email (uses SmtpClient)
    /// </summary>
    public sealed class EmailSender
    {

        private static string Host => 
            ConfigurationManager.GetAppSettingsValue("EmailOptions:EmailHost");
        
        private static int Port => 
            Convert.ToInt16(ConfigurationManager.GetAppSettingsValue("EmailOptions:EmailPort"));

        private static string SenderAddress =>
            Startup.EmailConfiguration["EmailAccount"];
        
        private static string SenderPassword =>
            Startup.EmailConfiguration["EmailAccountPassword"];

        private static string SenderName => "Administrator's Booking Fishing Sectors";

        private string ToAddress { get; set; }
        private string RecipientName { get; set; }

        private string SubjectMessage { get; set; }
        private string BodyMessage { get; }
        

        public EmailSender(string bodyMessage)
        {
            BodyMessage = bodyMessage;
        }
      

        /// <summary>
        /// A method for sending a message to a specified mail
        /// </summary>
        /// <param name="subjectMessage">Subject Message</param>
        /// <param name="toAddress">Recipient's Adress </param>
        /// <param name="recipientName">Recipient's Name</param>
        /// <returns>Status Code</returns>
        public async Task SendAsync(string subjectMessage, string toAddress, string recipientName)
        {
            SubjectMessage = subjectMessage;
            ToAddress = toAddress;
            RecipientName = recipientName;

            var fromAddressData = new MailAddress(SenderAddress, SenderName);
            var toAddressData = new MailAddress(ToAddress, RecipientName);

            var smtp = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddressData.Address, SenderPassword)
            };

            using var message = new MailMessage(fromAddressData, toAddressData)
            {
                Subject = SubjectMessage,
                Body = BodyMessage
            };

            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}
