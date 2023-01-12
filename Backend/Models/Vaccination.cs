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
        public Vaccination(PIS_PetRegistry.Models.Vaccination vaccinationDB)
        {
            DateEnd = vaccinationDB.DateEnd;
            FkAnimal = vaccinationDB.FkAnimal;
            FkUser = vaccinationDB.FkUser;
            Vaccine = new Vaccine(vaccinationDB.FkVaccineNavigation);
        }

        public DateOnly DateEnd { get; set; }

        public int FkAnimal { get; set; }

        public int FkUser { get; set; }

        public Vaccine Vaccine { get; set; }
    }
}
