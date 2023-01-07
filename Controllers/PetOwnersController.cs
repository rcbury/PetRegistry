using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
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
                    var animalsCount = context.Contracts.Where(contract => 
                    contract.FkPhysicalPerson == personInfo.Id && contract.FkLegalPerson == null).Count();
                    var catsCount = context.Contracts.Where(contract => contract.FkPhysicalPerson == personInfo.Id &&
                        context.AnimalCards.Where(card => card.FkCategory == 2 && card.Id == contract.FkAnimalCard).Count() != 0)
                        .Count();
                    var dogsCount = animalsCount - catsCount;
                    physicalPeopleDTO.Add(new PhysicalPersonDTO()
                    {
                        Id = personInfo.Id,
                        Name = personInfo.Name,
                        Phone = personInfo.Phone,
                        Address = personInfo.Address,
                        Email = personInfo.Email,
                        FkLocality = personInfo.FkLocality,
                        FkCountry = personInfo.FkCountry,
                        AnimalCount = animalsCount,
                        CatCount = catsCount,
                        DogCount = dogsCount
                    });
                }
            }
            return physicalPeopleDTO;
        }

        public static PhysicalPersonDTO GetPhysicalPersonByPhone(string phone)
        {
            using (var context = new RegistryPetsContext())
            {
                var person = context.PhysicalPeople.Where(person => person.Phone == phone).FirstOrDefault();
                if (person == null)
                {
                    return null;
                }
                return new PhysicalPersonDTO()
                {
                    Id = person.Id,
                    Phone = phone,
                    Name = person.Name,
                    Address = person.Address,
                    Email = person.Email,
                    FkLocality = person.FkLocality
                };
            }
        }

        public static PhysicalPersonDTO? GetPhysicalPersonById(int personId)
        {
            using (var context = new RegistryPetsContext())
            {
                var person = context.PhysicalPeople.Where(person => person.Id == personId).FirstOrDefault();
                if (person == null)
                {
                    return null;
                }
                return new PhysicalPersonDTO()
                {
                    Id = person.Id,
                    Phone = person.Phone,
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
                    Id = person.Id,
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
        public static LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            using (var context = new RegistryPetsContext())
            {
                var person = context.LegalPeople.Where(person => person.Id == personId).FirstOrDefault();
                if (person == null)
                {
                    return null;
                }
                return new LegalPersonDTO()
                {
                    Id = person.Id,
                    INN = person.Inn,
                    KPP = person.Kpp,
                    Name = person.Name,
                    Address = person.Address,
                    Email = person.Email,
                    Phone = person.Phone,
                    FkLocality = person.FkLocality
                };
            }
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
                    var animalsCount = context.Contracts.Where(contract => contract.FkLegalPerson == personInfo.Id).Count();
                    var catsCount = context.Contracts.Where(contract => contract.FkLegalPerson == personInfo.Id &&
                        context.AnimalCards.Where(card => card.FkCategory == 2 && card.Id == contract.FkAnimalCard).Count() != 0)
                        .Count();
                    var dogsCount = animalsCount - catsCount;
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
                        FkCountry = personInfo.FkCountry,
                        AnimalCount = animalsCount,
                        CatCount = catsCount,
                        DogCount = dogsCount
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
       
        public static LegalPersonDTO UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            LegalPerson? legalPersonModel;

            using (var context = new RegistryPetsContext())
            {
                legalPersonModel = context.LegalPeople
                    .Where(x => x.Id.Equals(legalPersonDTO.Id))
                    .FirstOrDefault();

                if (legalPersonModel == null)
                {
                    throw new Exception("trying to change unexisting legal person");
                }

                legalPersonModel.Inn = legalPersonDTO.INN;
                legalPersonModel.Kpp = legalPersonDTO.KPP;
                legalPersonModel.Name= legalPersonDTO.Name;
                legalPersonModel.Address = legalPersonDTO.Address;
                legalPersonModel.Email = legalPersonDTO.Email;
                legalPersonModel.Phone = legalPersonDTO.Phone;
                legalPersonModel.FkCountry= legalPersonDTO.FkCountry;
                legalPersonModel.FkLocality = legalPersonDTO.FkLocality;

                context.SaveChanges();
            }

            var newLegalPersonDTO = new LegalPersonDTO()
            {
                Id = legalPersonModel.Id,
                INN = legalPersonModel.Inn,
                KPP = legalPersonModel.Kpp,
                Name = legalPersonModel.Name,
                Address = legalPersonModel.Address,
                Email = legalPersonModel.Email,
                Phone = legalPersonModel.Phone,
                FkCountry = legalPersonModel.FkCountry,
                FkLocality = legalPersonModel.FkLocality,
            };

            return newLegalPersonDTO;
        }

        public static LegalPersonDTO AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = new LegalPerson()
            {
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            using (var context = new RegistryPetsContext())
            {
                context.LegalPeople.Add(legalPersonModel);
                context.SaveChanges();
            }

            var newLegalPersonDTO = new LegalPersonDTO()
            {
                Id = legalPersonModel.Id,
                INN = legalPersonModel.Inn,
                KPP = legalPersonModel.Kpp,
                Name = legalPersonModel.Name,
                Address = legalPersonModel.Address,
                Email = legalPersonModel.Email,
                Phone = legalPersonModel.Phone,
                FkCountry = legalPersonModel.FkCountry,
                FkLocality = legalPersonModel.FkLocality,
            };

            return newLegalPersonDTO;
        }

        public static PhysicalPersonDTO AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = new PhysicalPerson()
            {
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            using (var context = new RegistryPetsContext())
            {
                context.PhysicalPeople.Add(physicalPersonModel);
                context.SaveChanges();
            }

            var newPhysicalPersonDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            return newPhysicalPersonDTO;
        }

        public static PhysicalPersonDTO UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            PhysicalPerson? physicalPersonModel;

            using (var context = new RegistryPetsContext())
            {
                physicalPersonModel = context.PhysicalPeople
                    .Where(x => x.Id.Equals(physicalPersonDTO.Id))
                    .FirstOrDefault();

                if(physicalPersonModel == null)
                {
                    throw new Exception("trying to change unexisting physical person");
                }

                physicalPersonModel.Name = physicalPersonDTO.Name;
                physicalPersonModel.Address = physicalPersonDTO.Address;
                physicalPersonModel.Email = physicalPersonDTO.Email;
                physicalPersonModel.Phone = physicalPersonDTO.Phone;
                physicalPersonModel.FkCountry = physicalPersonDTO.FkCountry;
                physicalPersonModel.FkLocality = physicalPersonDTO.FkLocality;

                context.SaveChanges();
            }

            var newPhysicalPersonDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonModel.Name,
                Address = physicalPersonModel.Address,
                Email = physicalPersonModel.Email,
                Phone = physicalPersonModel.Phone,
                FkCountry = physicalPersonModel.FkCountry,
                FkLocality= physicalPersonModel.FkLocality,
            };

            return newPhysicalPersonDTO;
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
