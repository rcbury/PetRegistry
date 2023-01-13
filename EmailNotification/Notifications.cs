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
            Console.WriteLine("[email-notification] send email {0}; message {1}", recipients, body);
        }

        public void SendUsers(List<UserDTO> users, List<AnimalCardDTO> animals, List<List<VaccinationDTO>> vaccinations)
        {
            foreach (UserDTO user in users)
            {
                if (user.RoleId != 1)
                    continue;

                var message = this.GenerateMessage(user, animals, vaccinations);
                this.Send("noreply.petregistry@gmail.com", user.Email, "Уведомление о просрочке вакцины", message);
            }
        }

        public string GenerateMessage(UserDTO user, List<AnimalCardDTO> animals, List<List<VaccinationDTO>> vaccinations)
        {
            String message = "";

            foreach (var animalVaccinations in vaccinations)
            {
                foreach (var vacination in animalVaccinations)
                {
                    var animal = animals.Where(item => item.Id == vacination.FkAnimal).FirstOrDefault();

                    if (animal == null)
                        continue;

                    if (new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) < vacination.DateEnd)
                        continue;

                    if (user.ShelterId == animal.FkShelter)
                        continue;

                    message = String.Format("{0}\nУ животного {1} ({2}) заканчивается срок действия `{3}`",
                        message, animal.Name, animal.ChipId, vacination.VaccineName);

                }
            }

            return message;
        }
    }
}
