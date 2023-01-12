using DocumentFormat.OpenXml.Bibliography;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class VeterinaryAppointment
    {
        public VeterinaryAppointment(DateTime date, AnimalCard animalCard, User user, string name, bool isCompleted)
        {
            AnimalCard = animalCard;
            Date = date;
            User = user;
            Name = name;
            IsCompleted = isCompleted;
        }

        public DateTime Date { get; set; }

        public AnimalCard AnimalCard { get; set; }

        public User User { get; set; }

        public string Name { get; set; } = null!;

        public bool IsCompleted { get; set; }
    }
}
