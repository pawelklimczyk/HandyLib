using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;

namespace Gmtl.HandyLib
{
    public class HLMailNotifier : IHLMailNotifier
    {
        //<system.net>
        //    <mailSettings>
        //        <smtp from = "test@mail.com" >
        //            < network defaultCredentials="false" enableSsl="true" host="mail.test.com" port="465" userName="test" password="test" />   
        //        </smtp>
        //    </mailSettings>
        //</system.net>

        private static readonly object locker = new object();
        private static HLMailNotifier _instance;
        private string from;

        public static HLMailNotifier Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new HLMailNotifier();

                            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                            _instance.from = smtpSection.Network.UserName;
                        }
                    }
                }

                return _instance;
            }
        }

        public void DisableCertificateCheck()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public bool TestConfiguration()
        {
            try
            {
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string username = smtpSection.Network.UserName;

                return SendMail("Mailer SelfTest", "Mailer SelfTest", username);
            }
            catch
            {
                return false;
            }
        }

        public bool SendMail(string subject, string body, string recipient)
        {
            using (var message = new MailMessage(from, recipient))
            {
                message.Subject = subject;
                message.Body = body;

                return SendMailInternal(message);
            }
        }

        public bool SendMail(string subject, string body, string recipient, List<string> attachementFilenames)
        {
            using (var message = new MailMessage(from, recipient))
            {
                message.Subject = subject;
                message.Body = body;

                foreach (var filename in attachementFilenames)
                {
                    message.Attachments.Add(new Attachment(filename));
                }

                return SendMailInternal(message);
            }
        }

        public bool SendMail(string subject, string body, string recipient, List<Tuple<Stream, string>> attachementStreams)
        {
            using (var message = new MailMessage(from, recipient))
            {
                message.Subject = subject;
                message.Body = body;

                foreach (var stream in attachementStreams)
                {
                    stream.Item1.Position = 0;
                    message.Attachments.Add(new Attachment(stream.Item1, stream.Item2));
                }

                return SendMailInternal(message);
            }
        }

        private static bool SendMailInternal(MailMessage message)
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception exc)
            {
                //TODO add logger
                return false;
            }
        }
    }
    
    public interface IHLMailNotifier
    {
        /// <summary>
        ///     Will try to send message to itself
        /// </summary>
        /// <returns></returns>
        bool TestConfiguration();

        bool SendMail(string subject, string body, string recipient);
        bool SendMail(string subject, string body, string recipient, List<string> attachementFilenames);
        bool SendMail(string subject, string body, string recipient, List<Tuple<Stream, string>> attachementStreams);
        void DisableCertificateCheck();
    }
}