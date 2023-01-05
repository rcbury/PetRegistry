using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PISPetRegistry.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Aspose.Words;
using DocumentFormat.OpenXml.Bibliography;
using PIS_PetRegistry.Backend;

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

        public static PhysicalPersonDTO GetPhysicalPersonByPhone(string phone) 
        {
            using (var context = new RegistryPetsContext()) 
            {
                var person = context.PhysicalPeople.Where(person => person.Phone == phone).FirstOrDefault();
                if (person == null) 
                {
                    throw new Exception("Physical person with that phone number does not exists");
                }
                return new PhysicalPersonDTO()
                {
                    Phone = phone,
                    Name = person.Name,
                    Address = person.Address,
                    Email = person.Email,
                    FkLocality = person.FkLocality
                };
            }
        }

        public static LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            using (var context = new RegistryPetsContext())
            {
                var person = context.LegalPeople.Where(person => person.Inn == INN).FirstOrDefault();
                if (person == null)
                {
                    return null;
                }
                return new LegalPersonDTO()
                {
                    INN = INN,
                    KPP = person.Kpp,
                    Name = person.Name,
                    Address = person.Address,
                    Email = person.Email,
                    Phone = person.Phone,
                    FkLocality = person.FkLocality
                };
            }
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

            var newAnimalCardDTO = new AnimalCardDTO()
            {
                Id = animalCardModel.Id,
                ChipId = animalCardModel.ChipId,
                Name = animalCardModel.Name,
                IsBoy = animalCardModel.IsBoy,
                FkCategory = animalCardModel.FkCategory,
                FkShelter = animalCardModel.FkShelter,
                YearOfBirth = animalCardModel.YearOfBirth,
                Photo = animalCardModel.Photo,
            };

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

            var newAnimalCardDTO = new AnimalCardDTO()
            {
                Id = animalCardModel.Id,
                ChipId = animalCardModel.ChipId,
                Name = animalCardModel.Name,
                IsBoy = animalCardModel.IsBoy,
                FkCategory = animalCardModel.FkCategory,
                FkShelter = animalCardModel.FkShelter,
                YearOfBirth = animalCardModel.YearOfBirth,
                Photo = animalCardModel.Photo,
            };

            return newAnimalCardDTO;
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
                        doc.Range.Replace("<LegalPersonINN> ", legalPerson.INN);
                        doc.Range.Replace("<LegalPersonKPP> ", legalPerson.KPP);
                        doc.Range.Replace("<LegalPersonPhoneNumber> ", legalPerson.Phone);
                        doc.Range.Replace("<LegalPersonName> ", legalPerson.Name);
                        doc.Range.Replace("<LegalPersonLocation> ", city);
                        doc.Range.Replace("<LegalPersonAddress> ", legalPerson.Address);
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
                    var animalCategoryName = context.AnimalCategories.Where(animalCategory => animalCategory.Id == animalCard.FkCategory)
                        .FirstOrDefault()
                        .Name;
                    var animalGender = animalCard.IsBoy ? "М" : "Ж";
                    var day = DateTime.Now.Day.ToString();
                    var month = DateTime.Now.Month.ToString();
                    var year = DateTime.Now.Year.ToString();
                    var age = (int.Parse(year) - animalCard.YearOfBirth).ToString();
                    var loggedUserCreds = Utils.GetCredsFromFullName(user.Name);
                    var physicalPersonCreds = Utils.GetCredsFromFullName(physicalPerson.Name);
                    var loggedUserRole = context.Roles.Where(role => role.Id == user.RoleId).FirstOrDefault().Name;
                    doc.Range.Replace("<City>", personCity);
                    doc.Range.Replace("<Day>", day);
                    doc.Range.Replace("<Month>", month);
                    doc.Range.Replace("<Year>", year);
                    doc.Range.Replace("<ShelterName>", shelter.Name);
                    doc.Range.Replace("<Address>", shelter.Address);
                    doc.Range.Replace("<PhysicalPersonName>", physicalPerson.Name);
                    doc.Range.Replace("<PhysicalPersonLocation>", personCity);
                    doc.Range.Replace("<PhysicalPersonAddress> ", physicalPerson.Address);
                    doc.Range.Replace("<AnimalCategoryName> ", animalCategoryName);
                    doc.Range.Replace("<AnimalAge> ", age);
                    doc.Range.Replace("<AnimalGender> ", animalGender);
                    doc.Range.Replace("<ChipId> ", animalCard.ChipId);
                    doc.Range.Replace("<AnimalName> ", animalCard.Name);
                    doc.Range.Replace("<LoggedUserCreds> ", loggedUserCreds);
                    doc.Range.Replace("<LoggedUserRole> ", loggedUserRole);
                    doc.Range.Replace("<PhysicalPersonCreds> ", physicalPersonCreds);
                    doc.Range.Replace("<PhysicalPersonNumber> ", physicalPerson.Phone);
                    doc.Save(filePath);
                }
            }
        }
    }
}
