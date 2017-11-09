using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
namespace Gmtl.HandyLib
{
    public static class HLMailNotifier
    {
        //<system.net>
        //    <mailSettings>
        //        <smtp from = "test@mail.com" >
        //            < network defaultCredentials="false" enableSsl="true" host="mail.test.com" port="465" userName="test" password="test" />   
        //        </smtp>
        //    </mailSettings>
        //</system.net>

        static HLMailNotifier()
        {
            //SmtpClient will throw exception and crash because out certificates are not signed in mail system
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        }

        public static void Send(string to, string subject, string messageText)
        {
            try
            {
                var message = new MailMessage { Subject = subject, Body = messageText, IsBodyHtml = false };
                message.To.Add(new MailAddress(to));

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(message);
                }
            }
            catch (Exception exc)
            {
                //TODO use ILogger interface 
                throw;
            }
        }
    }
}

