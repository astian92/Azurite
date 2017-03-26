using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Azurite.Store.Common
{
    public class EmailServer
    {
        private readonly EmailServerConfig _configuration;
        private readonly SmtpClient _smtpClient;

        public EmailServer()
        {
            _configuration = new EmailServerConfig();
            _smtpClient = new SmtpClient(_configuration.SmtpClientName, _configuration.SmtpClientPort);
            _smtpClient.Credentials = new NetworkCredential(_configuration.Username, _configuration.Password);
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.EnableSsl = _configuration.UseSSL;
        }

        public void Send()
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.FromEmail);
            mailMessage.To.Add(_configuration.ToEmail);
            mailMessage.Subject = "Hello There";
            mailMessage.Body = "Hello my friend!";

            try
            {
                _smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                var asd = ex.ToString();
            }
        }
    }
}