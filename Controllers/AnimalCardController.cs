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

namespace PIS_PetRegistry.Controllers
{
    internal class AnimalCardController
    {
        public static List<AnimalCategoryDTO> GetAnimalCategories()
        {
            var animalCategoriesDTO = new List<AnimalCategoryDTO>();
            using (var context = new RegistryPetsContext())
            {
                foreach (var animaCategory in context.AnimalCategories)
                {
                    animalCategoriesDTO.Add(
                        new AnimalCategoryDTO()
                        {
                            Id = animaCategory.Id,
                            Name = animaCategory.Name
                        });
                }
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

            using (var context = new RegistryPetsContext())
            {
                context.AnimalCards.Add(animalCardModel);
                context.SaveChanges();

                AnimalCardLogController.LogCreate(animalCardModel, userDTO.Id);
            }

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

                AnimalCardLogController.LogUpdate(oldAnimalCardModel, animalCardModel, userDTO.Id);
            }

            var newAnimalCardDTO = ConvertModelInDTO(animalCardModel);

            return newAnimalCardDTO;
        }

        public static List<AnimalCardDTO> GetAnimals(UserDTO user) 
        {
            var animalsList = AnimalService.GetAnimalCards(user.ShelterId);
            var animalsListDto = animalsList.Select(item => ConvertModelInDTO(item)).ToList();

            return animalsListDto;
        }
        public static List<AnimalCardDTO> GetAnimals(AnimalFilterDTO animalFilter)
        {
            var animalsList = AnimalService.GetAnimalCards(animalFilter);
            var animalsListDto = animalsList.Select(item => ConvertModelInDTO(item)).ToList();

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

                AnimalCardLogController.LogDelete(animalCard, userDTO.Id);

                context.SaveChanges();
            }
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
    }
}
