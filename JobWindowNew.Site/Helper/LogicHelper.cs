using System;
using System.Net.Mail;

namespace JobWindowNew.Site.Helper
{
    public static class LogicHelper
    {
        public static void EmailSender(string fromAddress = "", string subject = "", string body = "")
        {
            var msg = new MailMessage();
            msg.To.Add(new MailAddress("contact@thejobwindow.com"));
            msg.From = new MailAddress(fromAddress);

            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = false;

            var client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("contact@thejobwindow.com", "C7VAlw0c6ddn"),
                Port = 587,
                Host = "smtp.office365.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            // You can use Port 25 if 587 is blocked (mine is!)

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }

}