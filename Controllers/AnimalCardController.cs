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
                Photo = model.Photo
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
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Учетные карточки");

                var heads = new string[5]
                {
                    "Кличка",
                    "Номер чипа",
                    "Дата рождения",
                    "Пол животного",
                    "Категория животного"
                };

                var cnt = 1;
                foreach (var head in heads)
                {
                    var currCell = worksheet.Cell(1, cnt);
                    currCell.Value = head;
                    currCell.Style.Alignment.WrapText = true;
                    currCell.Style.Font.Bold = true;
                    cnt++;
                }

                var len = cardsList.Count;
                if (len > 0)
                {
                    using (var context = new RegistryPetsContext())
                    {
                        var rowCnt = 2;
                        foreach (var card in cardsList)
                        {
                            var category = context.AnimalCategories.Where(category => category.Id == card.FkCategory).FirstOrDefault().Name;
                            worksheet.Cell(rowCnt, 1).Value = card.Name;
                            worksheet.Cell(rowCnt, 2).Value = card.ChipId;
                            worksheet.Cell(rowCnt, 3).Value = card.YearOfBirth;
                            worksheet.Cell(rowCnt, 4).Value = card.IsBoy ? "Мальчик" : "Девочка";
                            worksheet.Cell(rowCnt, 5).Value = category;
                            rowCnt++;
                        }
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }


        public static void DeleteAnimalCard(AnimalCardDTO animalCardDTO)
        {
            AnimalCardService.DeleteAnimalCard(animalCardDTO.Id);
        }

        public static void MakeContract(string filePath, PhysicalPersonDTO? physicalPerson, 
            LegalPersonDTO? legalPerson, AnimalCardDTO animalCard, UserDTO user) 
        {
            if (physicalPerson != null)
            {
                using (var context = new RegistryPetsContext()) 
                {
                    Document doc;
                    if (legalPerson != null)
                    {
                        var city = context.Locations.Where(location => location.Id == legalPerson.FkLocality)
                            .FirstOrDefault()
                            .Name;
                        doc = new Document("Договор юры.docx");
                        doc.Replace("<LegalPersonINN>", legalPerson.INN, false, true);
                        doc.Replace("<LegalPersonKPP>", legalPerson.KPP, false, true);
                        doc.Replace("<LegalPersonPhoneNumber>", legalPerson.Phone, false, true);
                        doc.Replace("<LegalPersonName>", legalPerson.Name, false, true);
                        doc.Replace("<LegalPersonLocation>", city, false, true);
                        doc.Replace("<LegalPersonAddress>", legalPerson.Address, false, true);
                        doc.Replace("<LegalPersonEmail>", legalPerson.Email, false, true);
                    }
                    else
                    {
                        doc = new Document("Договор физы.docx");
                    }
                    var shelter = context.Shelters.Where(shelter => shelter.Id == user.ShelterId)
                        .FirstOrDefault();
                    var personCity = context.Locations.Where(location => location.Id == physicalPerson.FkLocality)
                        .FirstOrDefault()
                        .Name;
                    var shelterCity = context.Locations
                        .Where(location => location.Id == context.Shelters
                            .Where(shelter => shelter.Id == animalCard.FkShelter).FirstOrDefault().FkLocation)
                        .FirstOrDefault()
                        .Name;
                    var animalCategoryName = context.AnimalCategories.Where(animalCategory => animalCategory.Id == animalCard.FkCategory)
                        .FirstOrDefault()
                        .Name
                        .ToLower();
                    var animalGender = animalCard.IsBoy ? "м" : "ж";
                    var day = DateTime.Now.Day.ToString();
                    var month = DateTime.Now.Month.ToString();
                    var year = DateTime.Now.Year.ToString();
                    var age = (int.Parse(year) - animalCard.YearOfBirth).ToString();
                    var loggedUserCreds = Utils.GetCredsFromFullName(user.Name);
                    var physicalPersonCreds = Utils.GetCredsFromFullName(physicalPerson.Name);
                    var loggedUserRole = context.Roles.Where(role => role.Id == user.RoleId).FirstOrDefault().Name;
                    doc.Replace("<ShelterCity>", shelterCity, false, true);
                    doc.Replace("<Day>", day, false, true);
                    doc.Replace("<Month>", month, false, true);
                    doc.Replace("<Year>", year, false, true);
                    doc.Replace("<ShelterName>", $"\"{shelter.Name}\"", false, true);
                    doc.Replace("<ShelterAddress>", shelter.Address, false, true);
                    doc.Replace("<PhysicalPersonName>", physicalPerson.Name, false, true);
                    doc.Replace("<PhysicalPersonLocation>", personCity, false, true);
                    doc.Replace("<PhysicalPersonAddress>", physicalPerson.Address, false, true);
                    doc.Replace("<AnimalCategoryName>", animalCategoryName, false, true);
                    doc.Replace("<AnimalAge>", age, false, true);
                    doc.Replace("<AnimalGender>", animalGender, false, true);
                    doc.Replace("<ChipId>", animalCard.ChipId, false, true);
                    doc.Replace("<AnimalName>", animalCard.Name, false, true);
                    doc.Replace("<LoggedUserCreds>", loggedUserCreds, false, true);
                    doc.Replace("<LoggedUserRole>", loggedUserRole, false, true);
                    doc.Replace("<PhysicalPersonCreds>", physicalPersonCreds, false, true);
                    doc.Replace("<PhysicalPersonNumber>", physicalPerson.Phone, false, true);
                    doc.Replace("<PhysicalPersonEmail>", physicalPerson.Email, false, true);
                    doc.SaveToFile(filePath, FileFormat.Docx);
                }
            }
        }

        public static ContractDTO SaveContract(PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO? legalPersonDTO, 
            AnimalCardDTO animalCardDTO, UserDTO currentUser) 
        {
            var res = new ContractDTO();
            using (var context = new RegistryPetsContext()) 
            {
                var contract = new Contract();
                var maxNum = context.Contracts
                    .Where(contract => contract.Date.Year == DateTime.Now.Year)
                    .Max(x => x.Number);
                contract.Number = maxNum == null ? 1 : maxNum + 1;
                res.Number = contract.Number;
                contract.Date = DateOnly.FromDateTime(DateTime.Now);
                res.Date = contract.Date;
                contract.FkAnimalCard = animalCardDTO.Id;
                res.FkAnimalCard = contract.FkAnimalCard;
                contract.FkUser = currentUser.Id;
                res.FkUser = contract.FkUser;
                contract.FkPhysicalPerson = physicalPersonDTO.Id;
                res.FkPhysicalPerson = contract.FkPhysicalPerson;
                if (legalPersonDTO != null)
                {
                    contract.FkLegalPerson = legalPersonDTO.Id;
                    res.FkLegalPerson = contract.FkLegalPerson;
                }
                context.Contracts.Add(contract);
                context.SaveChanges();
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
            var animalsByPhysicalPerson = GetAnimalsByPhysicalPerson(physicalPersonId);
            return animalsByPhysicalPerson.Count();
        }

        public static int CountAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsByLegalPerson = GetAnimalsByLegalPerson(legalPersonId);
            return animalsByLegalPerson.Count();
        }
    }
}
