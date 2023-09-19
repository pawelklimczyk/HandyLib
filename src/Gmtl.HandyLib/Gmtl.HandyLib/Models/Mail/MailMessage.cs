// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="MailMessage.cs" project="Gmtl.HandyLib" date="2023-09-18 17:33">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Models.Mail
{
    public class MailMessage
    {
        public string To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailContentType ContentType { get; set; }
    }
}
