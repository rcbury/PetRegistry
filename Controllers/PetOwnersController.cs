using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PIS_PetRegistry.Controllers
{
    internal class PetOwnersController
    {
        public static List<PhysicalPersonDTO> GetPhysicalPeople(string phone, string name, string address, string email, int country, int location)
        {
            var physicalPeopleDTO = new List<PhysicalPersonDTO>();
            using (var context = new RegistryPetsContext()) 
            {
                var physicalPeople = context.PhysicalPeople.ToList();
                if (phone != null && phone != "") 
                {
                    physicalPeople = physicalPeople.Where(person => person.Phone.Contains(phone)).ToList();
                }
                if (name != null && name != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Name.Contains(name)).ToList();
                }
                if (address != null && address != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Address.Contains(address)).ToList();
                }
                if (email != null && email != "")
                {
                    physicalPeople = physicalPeople.Where(person => person.Email.Contains(email)).ToList();
                }
                if (country != 0)
                {
                    physicalPeople = physicalPeople.Where(person => person.FkCountry == country).ToList();
                }
                if (location != 0)
                {
                    physicalPeople = physicalPeople.Where(person => person.FkLocality == location).ToList();
                }
                foreach (var personInfo in physicalPeople)
                {
                    physicalPeopleDTO.Add(new PhysicalPersonDTO()
                    {
                        Id = personInfo.Id,
                        Name = personInfo.Name,
                        Phone = personInfo.Phone,
                        Address = personInfo.Address,
                        Email = personInfo.Email,
                        FkLocality = personInfo.FkLocality,
                        FkCountry = personInfo.FkCountry
                    });
                }
            }
            return physicalPeopleDTO;
        }

        public static List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email, 
            string address, string phone, int country, int location)
        {
            var legalPeopleDTO = new List<LegalPersonDTO>();
            using (var context = new RegistryPetsContext())
            {
                var legalPeople = context.LegalPeople.ToList();
                if (inn != null && inn != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(inn)).ToList();
                }
                if (kpp != null && kpp != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(kpp)).ToList();
                }
                if (name != null && name != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(name)).ToList();
                }
                if (email != null && email != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(email)).ToList();
                }
                if (address != null && address != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(address)).ToList();
                }
                if (phone != null && phone != "")
                {
                    legalPeople = legalPeople.Where(person => person.Phone.Contains(phone)).ToList();
                }
                if (country != 0)
                {
                    legalPeople = legalPeople.Where(person => person.FkCountry == country).ToList();
                }
                if (location != 0)
                {
                    legalPeople = legalPeople.Where(person => person.FkLocality == location).ToList();
                }
                foreach (var personInfo in legalPeople)
                {
                    legalPeopleDTO.Add(new LegalPersonDTO()
                    {
                        Id = personInfo.Id,
                        INN = personInfo.Inn,
                        KPP = personInfo.Kpp,
                        Name = personInfo.Name,
                        Phone = personInfo.Phone,
                        Address = personInfo.Address,
                        Email = personInfo.Email,
                        FkLocality = personInfo.FkLocality,
                        FkCountry = personInfo.FkCountry
                    });
                }
            }
            return legalPeopleDTO;
        }

        public static List<LocationDTO> GetLocations()
        {
            var locations = new List<LocationDTO>();
            locations.Add(new LocationDTO()
            {
                Id = 0,
                Name = ""
            });
            using (var context = new RegistryPetsContext()) 
            {
                foreach (var location in context.Locations.ToList()) 
                {
                    locations.Add(new LocationDTO()
                    {
                        Id = location.Id,
                        Name = location.Name
                    });
                }
            }
            return locations;
        }

        public static List<CountryDTO> GetCountries() 
        {
            var countries = new List<CountryDTO>();
            countries.Add(new CountryDTO()
            {
                Id = 0,
                Name = ""
            });
            using (var context = new RegistryPetsContext())
            {
                foreach (var country in context.Countries.ToList())
                {
                    countries.Add(new CountryDTO()
                    {
                        Id = country.Id,
                        Name = country.Name
                    });
                }
            }
            return countries;
        }

        public static List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsLegalPersonDTO = new List<AnimalCardDTO>();
            using (var context = new RegistryPetsContext())
            {
                var animalsNumber = context.Contracts
                    .Where(x => x.FkLegalPerson == legalPersonId)
                    .Select(x => x.FkAnimalCard)
                    .Distinct();

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
                            FkShelter = animal.FkShelter,
                            ChipId = animal.ChipId,
                        });
                }
                return animalsLegalPersonDTO;
            }
        }

        public static void ExportPhysicalPeopleToExcel(string path, List<PhysicalPersonDTO> physicalPeopleDTO)
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Физические лица");
                var heads = new string[9]
{
                        "ФИО",
                        "Номер телефона",
                        "Фактический адрес проживания",
                        "Адрес эл. почты",
                        "Страна",
                        "Населенный пункт",
                        "Количество животных",
                        "Количество кошек/котов",
                        "Количество собак/псов"
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
                var len = physicalPeopleDTO.Count;
                if (len > 0) 
                {
                    using (var context = new RegistryPetsContext()) 
                    {
                        var rowCnt = 2;
                        foreach (var person in physicalPeopleDTO)
                        {
                            var country = context.Countries.Where(country => country.Id == person.FkCountry).FirstOrDefault().Name;
                            var location = context.Locations.Where(country => country.Id == person.FkLocality).FirstOrDefault().Name;
                            worksheet.Cell(rowCnt, 1).Value = person.Name;
                            worksheet.Cell(rowCnt, 2).Value = person.Phone;
                            worksheet.Cell(rowCnt, 3).Value = person.Address;
                            worksheet.Cell(rowCnt, 4).Value = person.Email;
                            worksheet.Cell(rowCnt, 5).Value = country;
                            worksheet.Cell(rowCnt, 6).Value = location;
                            worksheet.Cell(rowCnt, 7).Value = person.AnimalCount;
                            worksheet.Cell(rowCnt, 8).Value = person.CatCount;
                            worksheet.Cell(rowCnt, 9).Value = person.DogCount;
                            rowCnt++;
                        }
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }

        public static void ExportLegalPeopleToExcel(string path, List<LegalPersonDTO> legalPeopleDTO) 
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Юридические лица");

                var heads = new string[11]
                {
                    "ИНН",
                    "КПП",
                    "Наименование организации",
                    "Адрес",
                    "Адрес эл. почты",
                    "Номер телефона",
                    "Страна",
                    "Населенный пункт",
                    "Количество животных",
                    "Количество кошек/котов",
                    "Количество собак/псов"
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

                var len = legalPeopleDTO.Count;
                if (len > 0)
                {
                    using (var context = new RegistryPetsContext())
                    {
                        var rowCnt = 2;
                        foreach (var person in legalPeopleDTO)
                        {
                            var country = context.Countries.Where(country => country.Id == person.FkCountry).FirstOrDefault().Name;
                            var location = context.Locations.Where(country => country.Id == person.FkLocality).FirstOrDefault().Name;
                            worksheet.Cell(rowCnt, 1).Value = person.INN;
                            worksheet.Cell(rowCnt, 2).Value = person.KPP;
                            worksheet.Cell(rowCnt, 3).Value = person.Name;
                            worksheet.Cell(rowCnt, 4).Value = person.Address;
                            worksheet.Cell(rowCnt, 5).Value = person.Email;
                            worksheet.Cell(rowCnt, 6).Value = person.Phone;
                            worksheet.Cell(rowCnt, 7).Value = country;
                            worksheet.Cell(rowCnt, 8).Value = location;
                            worksheet.Cell(rowCnt, 9).Value = person.AnimalCount;
                            worksheet.Cell(rowCnt, 10).Value = person.CatCount;
                            worksheet.Cell(rowCnt, 11).Value = person.DogCount;
                            rowCnt++;
                        }
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }
    }
}
