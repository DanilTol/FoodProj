using System;
using System.Net.Mail;

namespace FoodService.Business.Services
{
    public static class MailService
    {
        internal static void SentEmail(string destination, string messageSubject, string messageBody)
        {
            //destination = "d.a.tolmachov@gmail.com";
            try
            {
                MailMessage mail = new MailMessage();
                //mail.To.Add(email);
                mail.To.Add(destination);
                mail.From = new MailAddress("mark.peters.jnr@gmail.com");
                mail.Subject = messageSubject;

                mail.Body = messageBody;
                mail.IsBodyHtml = true;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("mark.peters.jnr@gmail.com", "27022007qq"); // ***use valid credentials***
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);//Send(mail);
            }
            catch (Exception ex)
            {
                var f = ex.Message;
            }
        }
    }
}