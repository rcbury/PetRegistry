using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification
{
    public class Notification
    {
        public Notification()
        {
            _smptp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential
                (
                    ConfigurationManager.ConnectionStrings["SmtpLogin"].ToString(),
                    ConfigurationManager.ConnectionStrings["SmtpPassword"].ToString()
                )
            };
        }

        private SmtpClient _smptp = null;

        public void Send(string from, string recipients, string? subject, string? body)
        {
            _smptp.Send(from, recipients, subject, body);
        }
    }
}
