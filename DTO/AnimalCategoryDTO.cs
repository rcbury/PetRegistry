using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class AnimalCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AnimalCategoryDTO(PIS_PetRegistry.Backend.Models.AnimalCategory animalCategory)
        {
            Id = animalCategory.Id;
            Name = animalCategory.Name;
        }

        public AnimalCategoryDTO()
        {

        }

    }
}
