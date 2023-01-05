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
        public static List<AnimalCard> GetAnimalCards(AnimalFilterDTO animalFilter)
        {
            var animalCardsList = new List<AnimalCard> { };

            using (var context = new RegistryPetsContext())
            {
                var animalCards = context.AnimalCards.ToList();

                if (animalFilter.ChipId.Length > 0)
                {
                    animalCards = animalCards.Where(item => item.ChipId == animalFilter.ChipId).ToList();
                }
                else
                { 
                    if (animalFilter.Name.Length > 0)
                    {
                        animalCards = animalCards.Where(item => item.Name == animalFilter.Name).ToList();
                    }

                    if (animalFilter.IsSelectedSex)
                    {
                        animalCards = animalCards.Where(item => item.IsBoy == animalFilter.IsBoy).ToList();
                    }

                    if (animalFilter.AnimalCategory.Id != -1)
                    { 
                        animalCards = animalCards.Where(item => item.FkCategory == animalFilter.AnimalCategory.Id).ToList();
                    }
                }

                animalCardsList = animalCards;
            }

            return animalCardsList;
        }
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
