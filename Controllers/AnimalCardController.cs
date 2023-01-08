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
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

            animalCardModel = AnimalCardService.AddAnimalCard(animalCardModel);

            var newAnimalCardDTO = ConvertModelInDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO)
        {

            var animalCardModel = new AnimalCard()
            {
                Id = animalCardDTO.Id,
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = animalCardDTO.FkShelter,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo
            };

            AnimalCardService.UpdateAnimalCard(animalCardModel);

            var newAnimalCardDTO = ConvertModelInDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static List<AnimalCardDTO> GetAnimals() 
        {
            var animalCardsList = AnimalCardService.GetAnimals();
            var animalsListDto = animalCardsList.Select(item => ConvertModelInDTO(item)).ToList();

            return animalsListDto;
        }
        public static List<AnimalCardDTO> GetAnimals(AnimalFilterDTO animalFilter)
        {
            var animalCardsList = AnimalCardService.GetAnimals(animalFilter);
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
        public static void DeleteAnimalCard(AnimalCardDTO animalCardDTO)
        {
            AnimalCardService.DeleteAnimalCard(animalCardDTO.Id);
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
