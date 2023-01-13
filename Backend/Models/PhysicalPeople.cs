using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                physicalPerson.Contracts = new Contracts(contracts.ContractList
                    .Where(contract => contract.LegalPerson == null)
                    .Where(contract => contract.PhysicalPerson.Id == physicalPerson.Id)
                    .ToList());
            }
        }

        public List<PhysicalPerson> PhysicalPeopleList { get; private set; } = new List<PhysicalPerson>();
    }
}
