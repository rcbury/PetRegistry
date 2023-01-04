using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    public static class AnimalService
    {
        public static List<AnimalCard> GetAnimalCards(int? sheleterId) 
        {
            var animalCardsList = new List<AnimalCard> { };

            using (var context = new RegistryPetsContext())
            {
                animalCardsList = context.AnimalCards.Where(animal => animal.FkShelter.Equals(sheleterId)).ToList();
            }

            return animalCardsList;
        }
    }
}
