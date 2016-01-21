using System.Net.Mail;

namespace FoodService.Business.Services
{
    public static class MailService
    {
        private static string senderEmail = "mark.peters.jnr@gmail.com";
        private static string senderPassword = "27022007qq";

        internal static void SentEmail(string destination, string messageSubject, string messageBody)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(destination);
            mail.From = new MailAddress(senderEmail);
            mail.Subject = messageSubject;

            mail.Body = messageBody;
            mail.IsBodyHtml = true;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential
                 (senderEmail, senderPassword); // ***use valid credentials***
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}