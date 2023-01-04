using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class AnimalsLegalPersonController
    {
        public static List<AnimalCardDTO> AnimalsLegalPerson(int id)
        {
            var animalsLegalPersonDTO = new List<AnimalCardDTO>();
            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkPhysicalPerson == id)
                    .Select(x => x.FkAnimalCard)
                    .Distinct();
                //var animalsNumber = contracts.Select(x => x.FkAnimalCard).Distinct();
                
                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id == animalId)
                        .FirstOrDefault();
                    animalsLegalPersonDTO.Add(
                        new AnimalCardDTO()
                        {
                            Id = animal.Id,
                            IsBoy = animal.IsBoy,
                            Name = animal.Name,
                            Photo = animal.Photo,
                            YearOfBirth = animal.YearOfBirth,
                            FkCategory = animal.FkCategory,
                            FkShelter= animal.FkShelter,
                            ChipId = animal.ChipId,
                        });
                }
                return animalsLegalPersonDTO;
            }
        }
    }
}
