// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="MailSettings.cs" project="Gmtl.HandyLib" date="2023-09-18 17:26">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Models.Mail
{
    public class MailSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}
