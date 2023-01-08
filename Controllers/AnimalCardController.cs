using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PISPetRegistry.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Spire.Doc;
using DocumentFormat.OpenXml.Bibliography;
using PIS_PetRegistry.Backend;
using ClosedXML.Excel;
using PIS_PetRegistry.Services;
using PIS_PetRegistry.Backend.Services;

namespace PIS_PetRegistry.Controllers
{
    internal class AnimalCardController
    {
        public static List<AnimalCategoryDTO> GetAnimalCategories()
        {
            var animalCategories = AnimalCardService.GetAnimalCategories();
            var animalCategoriesDTO = new List<AnimalCategoryDTO>();
            
            foreach (var animaCategory in animalCategories)
            {
                animalCategoriesDTO.Add(
                    new AnimalCategoryDTO()
                    {
                        Id = animaCategory.Id,
                        Name = animaCategory.Name
                    });
            }
            
            return animalCategoriesDTO;
        }

        public static AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {

            var animalCardModel = new AnimalCard()
            {
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = (int)userDTO.ShelterId,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo,
            };

            AnimalCardService.AddAnimalCard(animalCardModel);

            var newAnimalCardDTO = ConvertModelInDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {
            AnimalCard oldAnimalCardModel;
            AnimalCard animalCardModel;

            using (var context = new RegistryPetsContext())
            {
                oldAnimalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCardDTO.Id)).FirstOrDefault();

                if (oldAnimalCardModel == null)
                    throw new Exception("trying to change unexisting animal card");
            }

            using (var context = new RegistryPetsContext())
            {
                animalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCardDTO.Id)).FirstOrDefault();

                animalCardModel.ChipId = animalCardDTO.ChipId;
                animalCardModel.Name = animalCardDTO.Name;
                animalCardModel.FkCategory = animalCardDTO.FkCategory;
                animalCardModel.YearOfBirth = animalCardDTO.YearOfBirth;
                animalCardModel.IsBoy = animalCardDTO.IsBoy;
                animalCardModel.Photo = animalCardDTO.Photo;

                context.SaveChanges();

                AnimalCardLogService.LogUpdate(oldAnimalCardModel, animalCardModel, userDTO.Id);
            }

            var newAnimalCardDTO = ConvertModelInDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static List<AnimalCardDTO> GetAnimals() 
        {
            var animalCardsList = new List<AnimalCard> { };

            using (var context = new RegistryPetsContext())
            {
                animalCardsList = context.AnimalCards.Include(card => card.FkCategoryNavigation).ToList();
            }

            var animalsListDto = animalCardsList.Select(item => ConvertModelInDTO(item)).ToList();

            return animalsListDto;
        }
        public static List<AnimalCardDTO> GetAnimals(AnimalFilterDTO animalFilter)
        {
            var animalCardsList = new List<AnimalCard> { };

            using (var context = new RegistryPetsContext())
            {
                var animalCards = context.AnimalCards.Include(card => card.FkCategoryNavigation).ToList();

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
            var animalsListDto = animalCardsList.Select(item => ConvertModelInDTO(item)).ToList();

            return animalsListDto;
        }

        public static AnimalCardDTO ConvertModelInDTO(AnimalCard model) 
        {
            var AnimalCardDTO = new AnimalCardDTO()
            {
                Id = model.Id,
                ChipId = model.ChipId,
                Name = model.Name,
                IsBoy = model.IsBoy,
                FkCategory = model.FkCategory,
                FkShelter = model.FkShelter,
                YearOfBirth = model.YearOfBirth,
                Photo = model.Photo,
                CategoryName = model.FkCategoryNavigation != null ? model.FkCategoryNavigation.Name : null
            };

            return AnimalCardDTO;
        }

        public static ContractDTO? GetContractByAnimal(int animalId) 
        {
            var res = new ContractDTO();
            using (var context = new RegistryPetsContext())
            {
                var contract = context.Contracts.Where(contract => contract.FkAnimalCard == animalId).FirstOrDefault();
                if (contract == null)
                {
                    return null;
                }
                else
                {
                    res.Number = contract.Number;
                    res.Date = contract.Date;
                    res.FkAnimalCard = contract.FkAnimalCard;
                    res.FkUser = contract.FkUser;
                    res.FkPhysicalPerson = contract.FkPhysicalPerson;
                    res.FkLegalPerson = contract.FkLegalPerson;
                }
            }
            return res;
        }

        public static void ExportCardsToExcel(string path, List<AnimalCardDTO> cardsList) 
        {
            Exporter.ExportCardsToExcel(path, cardsList);
        }


        public static void DeleteAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {
            using (var context = new RegistryPetsContext())
            {
                var animalCard = context.AnimalCards.Where(x => x.Id == animalCardDTO.Id).FirstOrDefault();

                if (animalCard == null)
                    throw new Exception("trying to delete non existent model");

                var animalCardVaccinations = animalCard.Vaccinations.ToList();

                foreach(var animalCardVaccination in animalCardVaccinations)
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

                AnimalCardLogService.LogDelete(animalCard, userDTO.Id);

                context.SaveChanges();
            }
        }

        public static void MakeContract(string filePath, PhysicalPersonDTO? physicalPersonDTO, 
            LegalPersonDTO? legalPersonDTO, AnimalCardDTO animalCardDTO) 
        {
            var physicalPerson = PetOwnersService.GetPhysicalPersonById(physicalPersonDTO.Id);
            var legalPerson = legalPersonDTO != null ? PetOwnersService.GetLegalPersonById(legalPersonDTO.Id) : null;
            var animalCard = AnimalCardService.GetAnimalCardById(animalCardDTO.Id);
            var user = AuthorizationService.GetAuthorizedUser();
            var shelter = ShelterService.GetShelterById(user.FkShelter);
            Exporter.MakeContract(filePath, physicalPerson, legalPerson, animalCard, user, shelter);
        }

        public static ContractDTO SaveContract(PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO? legalPersonDTO, 
            AnimalCardDTO animalCardDTO) 
        {
            var contract = AnimalCardService.SaveContract(physicalPersonDTO, legalPersonDTO, animalCardDTO);
            var res = new ContractDTO();
            res.Number = contract.Number;
            res.Date = contract.Date;
            res.FkAnimalCard = contract.FkAnimalCard;
            res.FkUser = contract.FkUser;
            res.FkPhysicalPerson = contract.FkPhysicalPerson;
            if (legalPersonDTO != null)
            {
                res.FkLegalPerson = contract.FkLegalPerson;
            }
            return res;
        }

        public static List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsByLegalPersonDTO = new List<AnimalCardDTO>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkLegalPerson.Equals(legalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .ToList();

                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByLegalPersonDTO.Add(
                        new AnimalCardDTO()
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

            return animalsByLegalPersonDTO;
        }

        public static List<AnimalCardDTO> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsByPhysicalPersonDTO = new List<AnimalCardDTO>();

            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkPhysicalPerson.Equals(physicalPersonId))
                    .Select(x => x.FkAnimalCard)
                    .ToList();

                foreach (var animalId in animalsNumber)
                {
                    var animal = context.AnimalCards
                        .Where(x => x.Id.Equals(animalId))
                        .FirstOrDefault();

                    animalsByPhysicalPersonDTO.Add(
                        new AnimalCardDTO()
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

            return animalsByPhysicalPersonDTO;
        }

        public static int CountAnimalsByPhysicalPerson(int physicalPersonId)
        {
            return PetOwnersService.GetPhysicalPersonAnimalCount(physicalPersonId);
        }

        public static int CountAnimalsByLegalPerson(int legalPersonId)
        {
            return PetOwnersService.GetLegalPersonAnimalCount(legalPersonId);
        }
    }
}
