using bmw_fs.Service.face.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.impl.common
{
    public class MailServiceImpl : MailService
    {
        public async Task<bool> sendMail(string to, string from, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.Body = body;
            //message.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    /*
                    var credential = new NetworkCredential
                    {
                        UserName = "theuber.upm@gmail.com",
                        Password = "Uber51##"
                    };
                    smtp.Credentials = credential;
                    */
                    smtp.Host = "SMTP.MUC";
                    //smtp.Port = 25;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }catch(Exception e)
            {
                return false;
            }
            
        }
    }
}