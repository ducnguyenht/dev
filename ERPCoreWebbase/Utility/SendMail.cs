using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Utility
{
    public class SendMail
    {
        public static void SendMailForgotPassword(string toEmail, string name, string myBody)
        {
            string subject = "";
            string body = myBody;
            Utility.SendMail.SendMailEx(toEmail, name, subject, body);
        }
        public static void SendMailRegisterUser(string toEmail, string name)
        {
            const String subject = "";
            const String body = "";
            Utility.SendMail.SendMailEx(toEmail, name, subject, body);
        }
        public static void SendInviteUser(string toEmail, string name)
        {
            const String subject = "";
            const String body = "";
            Utility.SendMail.SendMailEx(toEmail, name, subject, body);
        }
        public static void SendMailEx(string toEmail, string name, string subject, string body)
        {
            var fromAddress = new MailAddress("blackvirusmaster@gmail.com", "From Name");
            var toAddress = new MailAddress(toEmail, name);
            if (toEmail == null || toEmail.Equals(""))
            {
                toAddress = new MailAddress(toEmail, name);
            }
            const string fromPassword = "doitamtoi145";
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
