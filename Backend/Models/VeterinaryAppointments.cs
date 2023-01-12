using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic.ApplicationServices;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PIS_PetRegistry.Backend.Models
{
    public class VeterinaryAppointments
    {
        public VeterinaryAppointments(AnimalCard animalCard, Users users)
        {
            VeterinaryAppointmentList = new List<VeterinaryAppointment>();

            var veterinaryAppointmentsDB = VeterinaryAppointmentService.GetVeterinaryAppointmentsByAnimal(animalCard.Id);

            foreach (var veterinaryAppointmentDB in veterinaryAppointmentsDB)
            {
                VeterinaryAppointmentList.Add(new VeterinaryAppointment(
                    veterinaryAppointmentDB.Date,
                    animalCard,
                    users.UserList.Where(x => x.Id == veterinaryAppointmentDB.FkUser).FirstOrDefault(),
                    veterinaryAppointmentDB.Name,
                    veterinaryAppointmentDB.IsCompleted));
            }
        }

        public VeterinaryAppointment AddVeterinaryAppointment(
            DateTime date,
            AnimalCard animalCard,
            User user,
            string name,
            bool isCompleted)
        {
            var veterinaryAppointmentDB = new PIS_PetRegistry.Models.VeterinaryAppointmentAnimal()
            {
                FkAnimal = animalCard.Id,
                FkUser = user.Id,
                Name = name,
                IsCompleted = isCompleted,
                Date = date
            };

            VeterinaryAppointmentService.AddVeterinaryAppointment(veterinaryAppointmentDB);

            var veterinaryAppointment = new VeterinaryAppointment(
                date,
                animalCard,
                user,
                name,
                isCompleted);

            VeterinaryAppointmentList.Add(veterinaryAppointment);

            return veterinaryAppointment;
        }

        public VeterinaryAppointment UpdateVeterinaryAppointment(
            VeterinaryAppointment oldVeterinaryAppointment,
            DateTime modifiedDate,
            AnimalCard modifiedAnimalCard,
            User modifiedUser,
            string modifiedName,
            bool modifiedIsCompleted)
        {
            var oldVeterinaryAppointmentDB = new PIS_PetRegistry.Models.VeterinaryAppointmentAnimal()
            {
                FkAnimal = oldVeterinaryAppointment.AnimalCard.Id,
                FkUser = oldVeterinaryAppointment.User.Id,
                Name = oldVeterinaryAppointment.Name,
                IsCompleted = oldVeterinaryAppointment.IsCompleted,
                Date = oldVeterinaryAppointment.Date,
            };

            var modifiedVeterinaryAppointmentDB = new PIS_PetRegistry.Models.VeterinaryAppointmentAnimal()
            {
                FkAnimal = modifiedAnimalCard.Id,
                FkUser = modifiedUser.Id,
                Name = modifiedName,
                IsCompleted = modifiedIsCompleted,
                Date = modifiedDate,
            };

            VeterinaryAppointmentService.UpdateVeterinaryAppointment(oldVeterinaryAppointmentDB, modifiedVeterinaryAppointmentDB);

            oldVeterinaryAppointment.User = modifiedUser;
            oldVeterinaryAppointment.Name = modifiedName;
            oldVeterinaryAppointment.IsCompleted = modifiedIsCompleted;
            oldVeterinaryAppointment.Date = modifiedDate;
            oldVeterinaryAppointment.AnimalCard = modifiedAnimalCard;

            return oldVeterinaryAppointment;
        }

        public List<VeterinaryAppointment> VeterinaryAppointmentList { get; set; }
    }
}
