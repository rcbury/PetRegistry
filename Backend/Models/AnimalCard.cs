using DocumentFormat.OpenXml.Office2010.Excel;
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
            int id,
            string chipId,
            string name,
            AnimalCategory animalCategory,
            Shelter shelter,
            int? yearOfBirth,
            bool isBoy,
            string photo)
        {
            Id = id;
            IsBoy = isBoy;
            Name = name;
            Photo = photo;
            YearOfBirth = yearOfBirth;
            ChipId = chipId;
            AnimalCategory = animalCategory;
            Shelter = shelter;
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
