using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class ParasiteTreatment
    {
        public ParasiteTreatment(Medication medication, AnimalCard animalCard, User user, DateOnly date)
        {
            AnimalCard = animalCard;
            User = user;
            Medication = medication;
            Date = date;
        }

        public AnimalCard AnimalCard { get; set; }

        public User User { get; set; }

        public Medication Medication { get; set; }

        public DateOnly Date { get; set; }
    }
}
