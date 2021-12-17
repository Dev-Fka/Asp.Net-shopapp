using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace shopapp.webui.MailService
{
    public class SmtpEmailSender : IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        public SmtpEmailSender(string _host, int _port, bool _enableSSL, string _userName, string _passWord)
        {
            this.host = _host;
            this.enableSSL = _enableSSL;
            this.port = _port;
            this.userName = _userName;
            this.password = _passWord;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };

            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                }
            );
        }
    }
}