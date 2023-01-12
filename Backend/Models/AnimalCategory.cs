using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class AnimalCategory
    {
        public AnimalCategory(PIS_PetRegistry.Models.AnimalCategory animalCategoryDB)
        {
            Id = animalCategoryDB.Id;
            Name = animalCategoryDB.Name;
        }

        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
