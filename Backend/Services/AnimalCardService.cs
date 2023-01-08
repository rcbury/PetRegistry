using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Services
{
    public class AnimalCardService
    {
        public static List<AnimalCategory> GetAnimalCategories()
        {
            using (var context = new RegistryPetsContext())
            {
                var categories = context.AnimalCategories.ToList();
                
                return categories;
            }
        }

        public static AnimalCard AddAnimalCard(AnimalCard animalCardModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.AnimalCards.Add(animalCardModel);
                context.SaveChanges();

                AnimalCardLogService.LogCreate(animalCardModel, );
                return animalCardModel;
            }


        }


    }
}
