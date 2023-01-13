using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using PIS_PetRegistry.Backend;

namespace EmailNotification
{
    internal class Program
    {
        private static void Main()
        {
            var startTimeSpan = TimeSpan.FromHours(8);
            var periodTimeSpan = TimeSpan.FromHours(24);

            var timer = new System.Threading.Timer((e) =>
            {
                EmailNotificationSystem.OnExecuteNotification();
            }, null, startTimeSpan, periodTimeSpan);

#if DEBUG
            EmailNotificationSystem.OnExecuteNotification();
#endif
            Console.WriteLine("[email-notification] start");
            Console.ReadLine();
        }
    }
}