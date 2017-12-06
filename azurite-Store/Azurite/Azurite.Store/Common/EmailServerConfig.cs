using System.Configuration;

namespace Azurite.Store.Common
{
    public class EmailServerConfig
    {
        public EmailServerConfig()
        {
            SmtpClientName = ConfigurationManager.AppSettings["emailServerName"];
            SmtpClientPort = int.Parse(ConfigurationManager.AppSettings["emailServerPort"]);
            Username = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
            FromEmail = ConfigurationManager.AppSettings["fromEmail"];
            ToEmail = ConfigurationManager.AppSettings["toEmail"];
            UseSSL = bool.Parse(ConfigurationManager.AppSettings["requireSSL"]);
        }

        public string SmtpClientName { get; set; }

        public int SmtpClientPort { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public bool UseSSL { get; set; }
    }
}