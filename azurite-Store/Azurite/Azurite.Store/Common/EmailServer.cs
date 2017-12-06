using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

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
            _smtpClient.EnableSsl = _configuration.UseSSL;
        }

        public void Send(OrderW order, CustomerW customer)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.FromEmail);
            mailMessage.To.Add(_configuration.ToEmail);

            //Add subject
            mailMessage.Subject = "Имате нова поръчка - " + order.Number;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //Add Body
            mailMessage.Body = "";
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

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