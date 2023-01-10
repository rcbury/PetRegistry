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

        public static AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var user = AuthorizationService.GetAuthorizedUser();

            var animalCardModel = new AnimalCard()
            {
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = (int)user.FkShelter,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo,
            };

            animalCardModel = AnimalCardService.AddAnimalCard(animalCardModel);

            var newAnimalCardDTO = DTOModelConverter.ConvertModelToDTO(animalCardModel);

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

            var newAnimalCardDTO = DTOModelConverter.ConvertModelToDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static List<AnimalCardDTO> GetAnimals(AnimalFilterDTO? animalFilter = null)
        {
            var animalCardsList = AnimalCardService.GetAnimals(animalFilter);
            var animalsListDto = animalCardsList.Select(item => DTOModelConverter.ConvertModelToDTO(item)).ToList();

            return animalsListDto;
        }

        

        public static ContractDTO? GetContractByAnimal(int animalId) 
        {
            var contract = AnimalCardService.GetContractByAnimalId(animalId);
            if (contract == null)
            {
                return null;
            }
            var res = DTOModelConverter.ConvertModelToDTO(contract);
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

        public static void SaveContract(PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO? legalPersonDTO, 
            AnimalCardDTO animalCardDTO) 
        {
            AnimalCardService.SaveContract(physicalPersonDTO, legalPersonDTO, animalCardDTO);
        }

        public static List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsModel = AnimalCardService.GetAnimalsByPhysicalPerson(legalPersonId);
            List<AnimalCardDTO> animalsDTOByLegalPerson = new();

            foreach (var animalModel in animalsModel)
            {
                var animalDTO = DTOModelConverter.ConvertModelToDTO(animalModel);
                animalsDTOByLegalPerson.Add(animalDTO);
            }

            return animalsDTOByLegalPerson;
        }

        public static List<AnimalCardDTO> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsModel = AnimalCardService.GetAnimalsByPhysicalPerson(physicalPersonId);
            List<AnimalCardDTO> animalsDTOByPhysicalPerson = new();

            foreach (var animalModel in animalsModel)
            {
                var animalDTO = DTOModelConverter.ConvertModelToDTO(animalModel);
                animalsDTOByPhysicalPerson.Add(animalDTO);
            }

            return animalsDTOByPhysicalPerson;
        }        
    }
}
