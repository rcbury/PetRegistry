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
        public AnimalCardDTO(AnimalCard model)
        {
            Id = model.Id;
            ChipId = model.ChipId;
            Name = model.Name;
            IsBoy = model.IsBoy;
            FkCategory = model.AnimalCategory.Id;
            FkShelter = model.Shelter.Id;
            YearOfBirth = model.YearOfBirth;
            Photo = model.Photo;
            CategoryName = model.AnimalCategory.Name;
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
