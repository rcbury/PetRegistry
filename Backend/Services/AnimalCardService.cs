using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;

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

        public static Contract GetContractByAnimalId(int animalId) 
        {
            using (var context = new RegistryPetsContext()) 
            {
                return context.Contracts.Where(contract => contract.FkAnimalCard == animalId).FirstOrDefault();
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
                oldAnimalCardModel = GetAnimalCardById(animalCard.Id);

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
                animalCardsList = context.AnimalCards
                    .Include(card => card.Vaccinations)
                    .Include(card => card.VeterinaryAppointmentAnimals)
                    .Include(card => card.ParasiteTreatments)
                    .Include(card => card.FkCategoryNavigation)
                    .Include(card => card.Contracts)
                    .ToList();
            }

            return animalCardsList;
        }

        public static void DeleteAnimalCard(int animalCardId)
        {
            using (var context = new RegistryPetsContext())
            {
                var user = AuthorizationService.GetAuthorizedUser();

                var animalCard = GetAnimalCardById(animalCardId);
                context.AnimalCards.Attach(animalCard);

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
        public static AnimalCard GetAnimalCardById(int cardId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.AnimalCards
                    .Where(card => card.Id == cardId)
                    .Include(card => card.FkCategoryNavigation)
                    .Include(card => card.Vaccinations)
                    .Include(card => card.VeterinaryAppointmentAnimals)
                    .Include(card => card.ParasiteTreatments)
                    .Include(card => card.Contracts)
                    .FirstOrDefault();
            }
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
