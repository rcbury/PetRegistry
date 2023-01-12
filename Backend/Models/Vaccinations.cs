using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic.ApplicationServices;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Vaccinations
    {
        public Vaccinations(AnimalCard animalCard, Users users) 
        {
            Vaccines = VaccineService.GetVaccines().Select(x => new Vaccine(x)).ToList();
            var vaccinationsDB = VaccinationService.GetVaccinationsByAnimal(animalCard.Id);

            foreach (var vaccinationDB in vaccinationsDB)
            {
                VaccinationList.Add(new Vaccination(
                    Vaccines.Where(x => x.Id == vaccinationDB.FkVaccine).FirstOrDefault(),
                    vaccinationDB.DateEnd,
                    animalCard,
                    users.UserList.Where(x => x.Id == vaccinationDB.FkUser).FirstOrDefault()));
            }
        }

        public Vaccination AddVaccination(
            DateOnly date, 
            AnimalCard animalCard, 
            User user, 
            Vaccine vaccine)
        {
            var vaccinationDB = new PIS_PetRegistry.Models.Vaccination()
            {
                FkVaccine = vaccine.Id,
                FkAnimal = animalCard.Id,
                FkUser = user.Id,
                DateEnd = date,
            };

            VaccinationService.AddVaccination(vaccinationDB);

            var vaccination = new Vaccination(
                vaccine,
                date,
                animalCard,
                user);

            VaccinationList.Add(vaccination);

            return vaccination;
        }

        public Vaccination UpdateVaccination(
            Vaccination oldVaccination,
            DateOnly modifiedDate,
            AnimalCard modifiedAnimalCard,
            User modifiedUser,
            Vaccine modifiedVaccine)
        {
            var modifiedVaccinationDB = new PIS_PetRegistry.Models.Vaccination()
            {
                FkVaccine = modifiedVaccine.Id,
                FkAnimal = modifiedAnimalCard.Id,
                FkUser = modifiedUser.Id,
                DateEnd = modifiedDate,
            };

            var oldVaccinationDB = new PIS_PetRegistry.Models.Vaccination()
            {
                FkVaccine = oldVaccination.Vaccine.Id,
                FkAnimal = oldVaccination.AnimalCard.Id,
                FkUser = oldVaccination.User.Id,
                DateEnd = oldVaccination.DateEnd,
            };

            VaccinationService.UpdateVaccination(oldVaccinationDB, modifiedVaccinationDB);

            oldVaccination.Vaccine = modifiedVaccine;
            oldVaccination.User = modifiedUser;
            oldVaccination.DateEnd = modifiedDate;
            oldVaccination.AnimalCard = modifiedAnimalCard;

            return oldVaccination;
        }


        private List<Vaccine> Vaccines { get; set; }
        public List<Vaccination> VaccinationList { get; set; } = new List<Vaccination>();
    }

}
