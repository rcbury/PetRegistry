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
    public class PhysicalPeople
    {
        public PhysicalPeople(Locations locations, Countries countries) 
        {
            var physicalPeopleDB = PetOwnersService.GetPhysicalPeople();

            foreach (var physicalPerson in physicalPeopleDB)
            {
                PhysicalPeopleList.Add(new PhysicalPerson()
                {
                    Id = physicalPerson.Id,
                    Name = physicalPerson.Name,
                    Location = locations.GetLocation(physicalPerson.FkLocality),
                    Country = countries.GetCountry(physicalPerson.FkCountry),
                    Address = physicalPerson.Address,
                    Email = physicalPerson.Email,
                    Phone = physicalPerson.Phone
                });
            }
        }

        public void FillContracts(Contracts contracts) 
        {
            foreach (var physicalPerson in PhysicalPeopleList)
            {
                physicalPerson.FillContracts(contracts);
            }
        }

        public List<PhysicalPerson> GetPhysicalPeople(string phone, string name, string address,
            string email, int country, int location) 
        {
            var physicalPeople = PhysicalPeopleList;

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
                physicalPeople = physicalPeople.Where(person => person.Country.Id == country).ToList();
            }
            if (location != 0)
            {
                physicalPeople = physicalPeople.Where(person => person.Location.Id == location).ToList();
            }

            return physicalPeople;
        }

        public void UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO, Country country) 
        {
            var physicalPersonDB = new PIS_PetRegistry.Models.PhysicalPerson()
            {
                Id = physicalPersonDTO.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkLocality = physicalPersonDTO.FkLocality,
                FkCountry = physicalPersonDTO.FkCountry,
            };

            physicalPersonDB = PetOwnersService.UpdatePhysicalPerson(physicalPersonDB);

            var modifiedPhysicalPerson = GetPhysicalPersonById(physicalPersonDB.Id);

            modifiedPhysicalPerson.Name = physicalPersonDTO.Name;
            modifiedPhysicalPerson.Address = physicalPersonDTO.Address;
            modifiedPhysicalPerson.Email = physicalPersonDTO.Email;
            modifiedPhysicalPerson.Phone = physicalPersonDTO.Phone;
            modifiedPhysicalPerson.Country = country;
        }

        public PhysicalPerson AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO, Location location, Country country)
        {
            var physicalPersonDB = new PIS_PetRegistry.Models.PhysicalPerson()
            {
                Id = physicalPersonDTO.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            PetOwnersService.AddPhysicalPerson(physicalPersonDB);

            var physicalPerson = new PhysicalPerson();
            physicalPerson.Id = physicalPersonDB.Id;
            physicalPerson.Phone = physicalPersonDTO.Phone;
            physicalPerson.Name = physicalPersonDTO.Name;
            physicalPerson.Address = physicalPersonDTO.Address;
            physicalPerson.Email = physicalPersonDTO.Email;
            physicalPerson.Location = location;
            physicalPerson.Country = country;

            PhysicalPeopleList.Add(physicalPerson);

            return physicalPerson;
        }

        public PhysicalPerson GetPhysicalPersonById(int? personId) => PhysicalPeopleList.Where(person => person.Id == personId).FirstOrDefault();

        public PhysicalPerson GetPhysicalPersonByPhone(string phone) => PhysicalPeopleList.Where(person => person.Phone == phone).FirstOrDefault();

        public List<PhysicalPerson> PhysicalPeopleList { get; private set; } = new List<PhysicalPerson>();
    }
}
