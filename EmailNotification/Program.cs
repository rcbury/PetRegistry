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
                OnExecuteNotification();
            }, null, startTimeSpan, periodTimeSpan);

#if DEBUG
            OnExecuteNotification();
#endif
            Console.WriteLine("[email-notification] start");
            Console.ReadLine();
        }

        private static void OnExecuteNotification()
        {
            var animalRegistry = new Registry();

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
            
            var animals = animalRegistry.GetAnimals();
            var animalsVacination = animals.Select(animal => animalRegistry.GetVaccinationsByAnimal(animal.Id)).ToList();

            foreach (var animalVaccinations in animalsVacination)
            {
                foreach (var vacination in animalVaccinations)
                {
                    var animal = animals.Where(item => item.Id == vacination.FkAnimal).FirstOrDefault();

                    if (animal == null)
                        continue;

                    if (new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) < vacination.DateEnd)
                        continue;

                    message = String.Format("{0}\nУ животного {1} ({2}) заканчивается срок действия `{3}`",
                        message, animal.Name, animal.ChipId, vacination.VaccineName);

                }
            }

            var users = animalRegistry.GetUsers();

            if (message.Length > 0)
            {
                foreach (var user in users)
                {
                    if (user.RoleId != 1)
                        continue;

                    Console.WriteLine("[email-notification] send email {0}; message {1}", user.Email, message);
                    smtpClient.Send("noreply.petregistry@gmail.com", user.Email, "Уведомление о просрочке вакцины", message);
                }
            }
        }
    }
}