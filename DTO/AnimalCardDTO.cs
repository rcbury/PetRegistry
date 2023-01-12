using PIS_PetRegistry.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class AnimalCardDTO
    {
        public AnimalCardDTO(AnimalCard animalCard)
        {
            Id = animalCard.Id;
            ChipId = animalCard.ChipId;
            Name = animalCard.Name;
            IsBoy = animalCard.IsBoy;
            FkCategory = animalCard.AnimalCategory.Id;
            FkShelter = animalCard.Shelter.Id;
            YearOfBirth = animalCard.YearOfBirth;
            Photo = animalCard.Photo;
            CategoryName = animalCard.AnimalCategory.Name;
        }

        public AnimalCardDTO() { }

        public int Id { get; set; } = 0;
        public bool IsBoy { get; set; }
        public string Name { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public int? YearOfBirth { get; set; } = null;
        public int FkCategory { get; set; }
        public int FkShelter { get; set; }
        public string ChipId { get; set; } = null!;
        public string CategoryName { get; set; }
    }
}
