using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    internal class AnimalCategories
    {
        public AnimalCategories()
        {
            AnimalCategoryList = AnimalCardService
                .GetAnimalCategories()
                .Select(x => new AnimalCategory(x))
                .ToList();
        }

        public List<AnimalCategoryDTO> GetAnimalCategories()
        {
            return AnimalCategoryList.Select(x => new AnimalCategoryDTO(x)).ToList();
        }

        public List<AnimalCategory> AnimalCategoryList { get; set; }

    }
}
