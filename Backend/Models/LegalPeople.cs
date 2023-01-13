using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class LegalPeople
    {
        public LegalPeople(Locations locations, Countries countries)
        {
            var legalPeopleDB = PetOwnersService.GetLegalPeople();

            foreach (var legalPerson in legalPeopleDB)
            {
                LegalPeopleList.Add(new LegalPerson()
                {
                    Id = legalPerson.Id,
                    Name = legalPerson.Name,
                    Location = locations.GetLocation(legalPerson.FkLocality),
                    Country = countries.GetCountry(legalPerson.FkCountry),
                    Address = legalPerson.Address,
                    Email = legalPerson.Email,
                    Inn = legalPerson.Inn,
                    Kpp = legalPerson.Kpp,
                    Phone = legalPerson.Phone,
                });
            }
        }

        public void FillContracts(Contracts contracts)
        {
            foreach (var legalPerson in LegalPeopleList)
            {
                legalPerson.FillContracts(contracts);
            }
        }

        public List<LegalPerson> GetLegalPeople(string inn, string kpp, string name, string email,
            string address, string phone, int country, int location)
        {
            var legalPeople = LegalPeopleList;

            if (inn != null && inn != "")
            {
                legalPeople = legalPeople.Where(person => person.Inn.Contains(inn)).ToList();
            }
            if (kpp != null && kpp != "")
            {
                legalPeople = legalPeople.Where(person => person.Kpp.Contains(kpp)).ToList();
            }
            if (name != null && name != "")
            {
                legalPeople = legalPeople.Where(person => person.Name.Contains(name)).ToList();
            }
            if (email != null && email != "")
            {
                legalPeople = legalPeople.Where(person => person.Email.Contains(email)).ToList();
            }
            if (address != null && address != "")
            {
                legalPeople = legalPeople.Where(person => person.Address.Contains(address)).ToList();
            }
            if (phone != null && phone != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(phone)).ToList();
            }
            if (country != 0)
            {
                legalPeople = legalPeople.Where(person => person.Country.Id == country).ToList();
            }
            if (location != 0)
            {
                legalPeople = legalPeople.Where(person => person.Location.Id == location).ToList();
            }

            return legalPeople;
        }

        public LegalPerson? GetLegalPersonByInn(string INN) => LegalPeopleList.Where(x => x.Inn == INN).FirstOrDefault();

        public LegalPerson? GetLegalPersonById(int? personId) => LegalPeopleList.Where(x => x.Id == personId).FirstOrDefault();

        public void UpdateLegalPerson(LegalPersonDTO legalPersonDTO, Country country)
        {
            var legalPersonDB = new PIS_PetRegistry.Models.LegalPerson()
            {
                Id = legalPersonDTO.Id,
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            legalPersonDB = PetOwnersService.UpdateLegalPerson(legalPersonDB);

            var modifiedLegalPerson = GetLegalPersonById(legalPersonDB.Id);

            modifiedLegalPerson.Inn = legalPersonDTO.INN;
            modifiedLegalPerson.Kpp = legalPersonDTO.KPP;
            modifiedLegalPerson.Name = legalPersonDTO.Name;
            modifiedLegalPerson.Address = legalPersonDTO.Address;
            modifiedLegalPerson.Email = legalPersonDTO.Email;
            modifiedLegalPerson.Phone = legalPersonDTO.Phone;
            modifiedLegalPerson.Country = country;
        }

        public LegalPerson AddLegalPerson(LegalPersonDTO legalPersonDTO, Location location, Country country) 
        {
            var legalPersonDB = new PIS_PetRegistry.Models.LegalPerson()
            {
                Id = legalPersonDTO.Id,
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            PetOwnersService.AddLegalPerson(legalPersonDB);

            var legalPerson = new LegalPerson();
            legalPerson.Id = legalPersonDB.Id;
            legalPerson.Inn = legalPersonDTO.INN;
            legalPerson.Kpp = legalPersonDTO.KPP;
            legalPerson.Phone = legalPersonDTO.Phone;
            legalPerson.Name = legalPersonDTO.Name;
            legalPerson.Address = legalPersonDTO.Address;
            legalPerson.Email = legalPersonDTO.Email;
            legalPerson.Location = location;
            legalPerson.Country = country;

            LegalPeopleList.Add(legalPerson);

            return legalPerson;
        }

        public List<LegalPerson> LegalPeopleList { get; private set; } = new List<LegalPerson>();
    }
}
