using Microsoft.Extensions.Options;
using MonitoringApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Notifications
{
    public class EmailNotification
    {
        private readonly EmailOptions _options;
        
        //Gives the Email configuration from appsettings.json 
        public EmailNotification(IOptions<EmailOptions> options)
        {
            //when a class is created give me the email settings from configuration and save them
            _options = options.Value;
        }
        public void SendTestEmail(string subject,string body)
        {
           
            var client = new SmtpClient(_options.SmtpServer, _options.Port)
            {
                Credentials = new NetworkCredential(
                    Environment.GetEnvironmentVariable("SMTP_USER"),
                    Environment.GetEnvironmentVariable("SMTP_PASS")
                ),
                EnableSsl = true
            };

            client.Send(_options.From, _options.To, subject, body);
        }
     
    }
}
