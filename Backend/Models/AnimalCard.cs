using Microsoft.VisualBasic.ApplicationServices;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class AnimalCard
    {
        public AnimalCard(
            PIS_PetRegistry.Models.AnimalCard animalCardDB)
        {
            Id = animalCardDB.Id;
            IsBoy = animalCardDB.IsBoy;
            Name = animalCardDB.Name;
            Photo = animalCardDB.Photo;
            YearOfBirth = animalCardDB.YearOfBirth;
            ChipId = animalCardDB.ChipId;
            AnimalCategory = new AnimalCategory(animalCardDB.FkCategoryNavigation);
            Shelter = new Shelter()
            {
                Id = animalCardDB.FkShelterNavigation.Id,
                Name = animalCardDB.FkShelterNavigation.Name
            };
        }

        public AnimalCard(AnimalCategory animalCategory, Shelter shelter, AnimalCardDTO animalCardDTO)
        {
            Id = animalCardDTO.Id;
            ChipId = animalCardDTO.ChipId;
            Name = animalCardDTO.Name;
            AnimalCategory = animalCategory;
            Shelter = shelter;
            YearOfBirth = animalCardDTO.YearOfBirth;
            IsBoy = animalCardDTO.IsBoy;
            Photo = animalCardDTO.Photo;
        }

        public int Id { get; set; }

        public bool IsBoy { get; set; }

        public string Name { get; set; } = null!;

        public string? Photo { get; set; }

        public int? YearOfBirth { get; set; }

        public string ChipId { get; set; } = null!;

        public  AnimalCategory AnimalCategory { get; set; } = null!;

        public  Shelter Shelter { get; set; } = null!;

        public ParasiteTreatments ParasiteTreatments { get; set; }

        public Vaccinations Vaccinations { get; set; }

        public VeterinaryAppointments VeterinaryAppointments { get; set; }

        public AnimalCardDTO ConvertToDTO()
        {
            var animalCardDTO = new AnimalCardDTO()
            {
                Id = Id,
                ChipId = ChipId,
                Name = Name,
                IsBoy = IsBoy,
                FkCategory = AnimalCategory.Id,
                FkShelter = Shelter.Id,
                YearOfBirth = YearOfBirth,
                Photo = Photo,
                CategoryName = AnimalCategory.Name
            };

            return animalCardDTO;
        }
    }
}
