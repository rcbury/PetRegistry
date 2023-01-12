using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Vaccination
    {
        public Vaccination(PIS_PetRegistry.Models.Vaccination vaccinationDB, AnimalCard animalCard, User user)
        {
            DateEnd = vaccinationDB.DateEnd;
            AnimalCard = animalCard;
            User = user;
            Vaccine = new Vaccine(vaccinationDB.FkVaccineNavigation);
        }

        public Vaccination(Vaccine vaccine, DateOnly date, AnimalCard animalCard, User user)
        {
            DateEnd = date;
            AnimalCard = animalCard;
            User = user;
            Vaccine = vaccine;
        }



        public DateOnly DateEnd { get; set; }

        public AnimalCard AnimalCard { get; set; }

        public User User { get; set; }

        public Vaccine Vaccine { get; set; }
    }
}
