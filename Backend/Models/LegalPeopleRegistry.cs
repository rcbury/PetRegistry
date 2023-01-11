using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml.Office;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class LegalPeopleRegistry
    {
        public LegalPeopleRegistry()
        {
            Countries = new Countries();
            Locations = new Locations();

            LegalPeople = new List<LegalPerson>();

            using(var context = new RegistryPetsContext())
            {
                var legalPeopleDB = context.LegalPeople.ToList();

                foreach(var legalPerson in legalPeopleDB)
                {
                    LegalPeople.Add(new Backend.Models.LegalPerson()
                    {
                        Id = legalPerson.Id,
                        Name = legalPerson.Name,
                        Location = Locations.GetLocation(legalPerson.FkLocality),
                        Country = Countries.GetCountry(legalPerson.FkCountry),
                        Address = legalPerson.Address,
                        Email = legalPerson.Email,
                        Inn = legalPerson.Inn,
                        Kpp = legalPerson.Kpp,
                        Phone = legalPerson.Phone,
                    });
                }

            }
        }

        private List<LegalPerson> LegalPeople;
        private Countries Countries;
        private Locations Locations;


        public List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email,
            string address, string phone, int country, int location)
        {
            var legalPeople = LegalPeople;

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
                legalPeople = legalPeople.Where(person => person.Country.Id == country).ToList();
            }
            if (location != 0)
            {
                legalPeople = legalPeople.Where(person => person.Location.Id == location).ToList();
            }

            var res = new List<LegalPersonDTO>();

            foreach (var legalPerson in legalPeople)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(legalPerson));
            }

            return res;
        }

        public LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = LegalPeople.Where(x => x.Inn == INN).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }


        public LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = LegalPeople.Where(x => x.Id == personId).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public void UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);
        }

    }
}
