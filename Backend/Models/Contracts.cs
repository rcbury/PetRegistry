using DocumentFormat.OpenXml.Bibliography;
using Microsoft.VisualBasic.ApplicationServices;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Contracts
    {
        public Contracts(List<AnimalCard> cards, List<LegalPerson> legalPeopleList, List<PhysicalPerson> physicalPeopleList)
        {
            using (var context = new RegistryPetsContext())
            {
                ContractList = new List<Contract>();
                var contractsDB = context.Contracts.ToList();


                foreach (var contractDB in contractsDB)
                {
                    ContractList.Add(new Contract()
                    {
                        Id = contractDB.Id,
                        Number = contractDB.Number,
                        Date = contractDB.Date,
                        AnimalCard = cards.Where(card => card.Id == contractDB.FkAnimalCard).FirstOrDefault(),
                        LegalPerson = legalPeopleList.Where(person => person.Id == contractDB.FkLegalPerson).FirstOrDefault(),
                        PhysicalPerson = physicalPeopleList.Where(person => person.Id == contractDB.FkPhysicalPerson).FirstOrDefault()
                    });
                }
            }
        }

        public Contracts(List<Contract> contracts)
        {
            ContractList = contracts;
        }

        public List<Contract> ContractList
        {
            get;
            private set;
        }

        public Contract? GetContract(int contractId)
        {
            return ContractList.Where(x => x.Id == contractId).FirstOrDefault();
        }

        public void SaveContract(PhysicalPerson physicalPerson, LegalPerson? legalPerson, AnimalCard card, User user) 
        {
            var maxNum = AnimalCardService.GetContractNumber();
            var contract = new PIS_PetRegistry.Models.Contract()
            {
                Number = maxNum,
                Date = DateOnly.FromDateTime(DateTime.Now),
                FkAnimalCard = card.Id,
                FkUser = user.Id,
                FkPhysicalPerson = physicalPerson.Id,
                FkLegalPerson = legalPerson == null ? null : legalPerson.Id
            };

            contract = AnimalCardService.SaveContract(contract);

            var registryContract = new Contract()
            {
                Id = contract.Id,
                Number = contract.Number,
                Date = contract.Date,
                AnimalCard = card,
                User = user,
                PhysicalPerson = physicalPerson
            };

            ContractList.Add(registryContract);
        }

    }
}
