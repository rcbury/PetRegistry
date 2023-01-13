using Microsoft.EntityFrameworkCore.Metadata;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class ParasiteTreatments
    {
        public ParasiteTreatments(AnimalCard animalCard, Users users, Medications medications)
        {
            ParasiteTreatmentList = new List<ParasiteTreatment>();

            var parasiteTreatmentsDB = ParasiteTreatmentService.GetParasiteTreatmentsByAnimal(animalCard.Id);

            foreach (var parasiteTreatmentDB in parasiteTreatmentsDB)
            {
                ParasiteTreatmentList.Add(new ParasiteTreatment(
                    medications.GetMedicationById(parasiteTreatmentDB.FkMedication),
                    animalCard,
                    users.GetUserById(parasiteTreatmentDB.FkUser),
                    parasiteTreatmentDB.Date));
            }
        }

        public ParasiteTreatments()
        {
            ParasiteTreatmentList = new List<ParasiteTreatment>();
        }

        public ParasiteTreatment? GetParasiteTreatmentById(int animalId, int userId, DateOnly date, int medicationId)
        {
            return ParasiteTreatmentList
                .Where(x => animalId == x.AnimalCard.Id)
                .Where(x => userId == x.User.Id)
                .Where(x => date == x.Date)
                .Where(x => medicationId == x.Medication.Id)
                .FirstOrDefault();
        }

        public ParasiteTreatment AddParasiteTreatment(
            DateOnly date,
            AnimalCard animalCard,
            User user,
            Medication medication)
        {
            var parasiteTreatmentDB = new PIS_PetRegistry.Models.ParasiteTreatment()
            {
                FkMedication = medication.Id,
                FkAnimal = animalCard.Id,
                FkUser = user.Id,
                Date = date,
            };

            ParasiteTreatmentService.AddParasiteTreatment(parasiteTreatmentDB);

            var parasiteTreatment = new ParasiteTreatment(
                medication,
                animalCard,
                user,
                date);

            ParasiteTreatmentList.Add(parasiteTreatment);

            return parasiteTreatment;
        }

        public ParasiteTreatment UpdateParasiteTreatment(
            ParasiteTreatment oldParasiteTreatment,
            DateOnly modifiedDate,
            AnimalCard modifiedAnimalCard,
            User modifiedUser,
            Medication modifiedMedication)
        {
            var modifiedVaccinationDB = new PIS_PetRegistry.Models.ParasiteTreatment()
            {
                FkMedication = modifiedMedication.Id,
                FkAnimal = modifiedAnimalCard.Id,
                FkUser = modifiedUser.Id,
                Date = modifiedDate,
            };

            var oldVaccinationDB = new PIS_PetRegistry.Models.ParasiteTreatment()
            {
                FkMedication = oldParasiteTreatment.Medication.Id,
                FkAnimal = oldParasiteTreatment.AnimalCard.Id,
                FkUser = oldParasiteTreatment.User.Id,
                Date = oldParasiteTreatment.Date,
            };

            ParasiteTreatmentService.UpdateParasiteTreatment(
                oldVaccinationDB, 
                modifiedVaccinationDB);

            oldParasiteTreatment.Medication = modifiedMedication;
            oldParasiteTreatment.User = modifiedUser;
            oldParasiteTreatment.Date = modifiedDate;
            oldParasiteTreatment.AnimalCard = modifiedAnimalCard;

            return oldParasiteTreatment;
        }

        public List<ParasiteTreatment> ParasiteTreatmentList { get; set; }
    }
}
