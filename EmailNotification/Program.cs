using PIS_PetRegistry.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

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
                OnExecuteNotification();
            }, null, startTimeSpan, periodTimeSpan);

            Console.WriteLine("[email-notification] start");
            Console.ReadLine();
        }

        private static void OnExecuteNotification()
        {
            String message = "";
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var smtpClient = new SmtpClient("smtp.gmail.com")
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

            using (var context = new RegistryPetsContext())
            {
                var animals = context.AnimalCards.Select(x => x.Vaccinations
                                                                  .Where(x => new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) >= x.DateEnd).ToList())
                                                                  .ToList();

                foreach (var item in animals)
                {
                    foreach (var itemTwo in item)
                    {
                        message = String.Format("{0}\nУ животного {1} ({2}) заканчивается срок действия `{3}`",
                            message, itemTwo.FkAnimalNavigation.Name, itemTwo.FkAnimalNavigation.ChipId, itemTwo.FkVaccineNavigation.Name);

                        if (message.Length > 0)
                        {
                            Console.WriteLine("[email-notification] send email {0}; message {1}", itemTwo.FkUserNavigation.Email, message);
                            smtpClient.Send("noreply.petregistry@gmail.com", itemTwo.FkUserNavigation.Email, "Уведомление о просрочке вакцины", message);
                        }
                    }
                }
            }
        }
    }
}