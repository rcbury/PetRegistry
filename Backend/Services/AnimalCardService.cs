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

                AnimalCardLogService.LogCreate(animalCardModel, AuthorizationService.GetAuthorizedUser().Id);
                return animalCardModel;
            }


        }
        
        public static List<AnimalCard> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsByPhysicalPerson = new List<AnimalCard>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkPhysicalPerson.Equals(physicalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .ToList();

                var animals = context.AnimalCards;

                foreach (var animalId in animalsNumber)
                {
                    var animal = animals
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByPhysicalPerson.Add(
                        new AnimalCard()
                        {
                            Id = animal.Id,
                            IsBoy = animal.IsBoy,
                            Name = animal.Name,
                            Photo = animal.Photo,
                            YearOfBirth = animal.YearOfBirth,
                            FkCategory = animal.FkCategory,
                            FkShelter = animal.FkShelter,
                            ChipId = animal.ChipId,
                        });
                }
            }

            return animalsByPhysicalPerson;
        }

        public static List<AnimalCard> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsByLegalPerson = new List<AnimalCard>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkLegalPerson.Equals(legalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .ToList();

                var animals = context.AnimalCards;

                foreach (var animalId in animalsNumber)
                {
                    var animal = animals
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByLegalPerson.Add(
                        new AnimalCard()
                        {
                            Id = animal.Id,
                            IsBoy = animal.IsBoy,
                            Name = animal.Name,
                            Photo = animal.Photo,
                            YearOfBirth = animal.YearOfBirth,
                            FkCategory = animal.FkCategory,
                            FkShelter = animal.FkShelter,
                            ChipId = animal.ChipId,
                        });
                }
            }

            return animalsByLegalPerson;
        }
    }
}
