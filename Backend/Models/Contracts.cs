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
        public Contracts(AnimalCards cards, LegalPeople legalPeople, PhysicalPeople physicalPeople, Users users)
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
                        AnimalCard = cards.GetAnimalCardById(contractDB.FkAnimalCard),
                        LegalPerson = contractDB.FkLegalPerson != null ? legalPeople.GetLegalPersonById(contractDB.FkLegalPerson) : null,
                        PhysicalPerson = physicalPeople.GetPhysicalPersonById(contractDB.FkPhysicalPerson),
                        User = users.GetUserById(contractDB.FkUser)
                    });
                }
            }
        }

        public Contracts(IEnumerable<Contract> contracts)
        {
            ContractList = contracts.ToList();
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

        public Contracts GetContractsByLegalPerson(int legalPersonId) 
        {
            return new Contracts(ContractList
                    .Where(contract => contract.LegalPerson != null)
                    .Where(contract => contract.LegalPerson.Id == legalPersonId)
                    .ToList());
        }

        public Contracts GetContractsByPhysicalPerson(int physicalPersonId)
        {
            return new Contracts(ContractList
                    .Where(contract => contract.LegalPerson == null)
                    .Where(contract => contract.PhysicalPerson.Id == physicalPersonId)
                    .ToList());
        }

        public Contract SaveContract(PhysicalPerson physicalPerson, LegalPerson? legalPerson, AnimalCard card, User user) 
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
                PhysicalPerson = physicalPerson,
                LegalPerson = legalPerson
            };

            ContractList.Add(registryContract);

            return registryContract;
        }

        public void AddContract(Contract contract) 
        {
            ContractList.Add(contract);
        }

    }
}
