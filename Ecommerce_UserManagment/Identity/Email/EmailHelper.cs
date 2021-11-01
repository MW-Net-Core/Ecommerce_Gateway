using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
namespace Ecommerce_UserManagment.Identity.Email
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("pythoning7@gmail.com");
            //mailMessage.To.Add(new MailAddress(userEmail));

            //mailMessage.Subject = "Confirm your email";
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Body = confirmationLink;

            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials =
            //new System.Net.NetworkCredential("pythoning7@gmail.com", "3dsdsoft195*_Z");
            //try
            //{
            //    SmtpServer.Send(mailMessage);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    // log exception
            //}
            //return false;



            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(new MailAddress(userEmail));
            mail.From = new MailAddress("pythoning7@gmail.com", "Email head", System.Text.Encoding.UTF8);
            mail.Subject = "This mail is send from asp.net application";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = confirmationLink;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("pythoning7@gmail.com", "3dsdsoft195*_Z");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
            return false;




        }
    }
}