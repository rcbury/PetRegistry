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

                var user = AuthorizationService.GetAuthorizedUser();

                AnimalCardLogService.LogCreate(animalCardModel, user.Id);
                return animalCardModel;
            }
        }

        public static AnimalCard UpdateAnimalCard(AnimalCard animalCard)
        {
            AnimalCard oldAnimalCardModel;

            using (var context = new RegistryPetsContext())
            {
                oldAnimalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCard.Id)).FirstOrDefault();

                if (oldAnimalCardModel == null)
                    throw new Exception("trying to change unexisting animal card");
            }

            using (var context = new RegistryPetsContext())
            {
                context.Update<AnimalCard>(animalCard);
                
                context.SaveChanges();

                var user = AuthorizationService.GetAuthorizedUser();

                AnimalCardLogService.LogUpdate(oldAnimalCardModel, animalCard, user.Id);
            }

            return animalCard;
        }

        public static List<AnimalCard> GetAnimals()
        {
            var animalCardsList = new List<AnimalCard> { };

            using (var context = new RegistryPetsContext())
            {
                animalCardsList = context.AnimalCards.ToList();
            }

            return animalCardsList;
        }
        public static List<AnimalCard> GetAnimals(AnimalFilterDTO animalFilter)
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

        public static void DeleteAnimalCard(int animalCardId)
        {
            using (var context = new RegistryPetsContext())
            {
                var user = AuthorizationService.GetAuthorizedUser();

                var animalCard = context.AnimalCards.Where(x => x.Id == animalCardId).FirstOrDefault();

                if (animalCard == null)
                    throw new Exception("trying to delete non existent model");

                var animalCardVaccinations = animalCard.Vaccinations.ToList();

                foreach (var animalCardVaccination in animalCardVaccinations)
                {
                    context.Vaccinations.Remove(animalCardVaccination);
                }

                var animalCardParasiteTreatments = animalCard.ParasiteTreatments.ToList();

                foreach (var animalCardParasiteTreatment in animalCardParasiteTreatments)
                {
                    context.ParasiteTreatments.Remove(animalCardParasiteTreatment);
                }

                var animalCardVeterinaryAppointments = animalCard.VeterinaryAppointmentAnimals.ToList();

                foreach (var animalCardVeterinaryAppointment in animalCardVeterinaryAppointments)
                {
                    context.VeterinaryAppointmentAnimals.Remove(animalCardVeterinaryAppointment);
                }

                var animalCardContracts = animalCard.Contracts.ToList();

                foreach (var animalCardContract in animalCardContracts)
                {
                    context.Contracts.Remove(animalCardContract);
                }

                context.AnimalCards.Remove(animalCard);

                AnimalCardLogService.LogDelete(animalCard, user.Id);

                context.SaveChanges();
            }
        }
    }
}
