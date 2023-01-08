using Microsoft.EntityFrameworkCore;
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

        public static AnimalCard GetAnimalCardById(int cardId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.AnimalCards
                    .Where(card => card.Id == cardId)
                    .Include(card => card.FkCategoryNavigation)
                    .FirstOrDefault();
            }
        }

        public static Contract SaveContract(PhysicalPersonDTO? physicalPersonDTO, LegalPersonDTO? legalPersonDTO, AnimalCardDTO animalCardDTO) 
        {
            var user = AuthorizationService.GetAuthorizedUser();
            var contract = new Contract();
            using (var context = new RegistryPetsContext())
            {
                var maxNum = context.Contracts
                    .Where(contract => contract.Date.Year == DateTime.Now.Year)
                    .Max(x => x.Number);
                contract.Number = maxNum == null ? 1 : maxNum + 1;
                contract.Date = DateOnly.FromDateTime(DateTime.Now);
                contract.FkAnimalCard = animalCardDTO.Id;
                contract.FkUser = user.Id;
                contract.FkPhysicalPerson = physicalPersonDTO.Id;
                if (legalPersonDTO != null)
                {
                    contract.FkLegalPerson = legalPersonDTO.Id;
                }
                context.Contracts.Add(contract);
                context.SaveChanges();
            }
            return contract;
        }
    }
}
