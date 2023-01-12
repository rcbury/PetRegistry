using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Contracts
    {
        public Contracts()
        {
            using (var context = new RegistryPetsContext())
            {
                ContractList = new List<Contract>();
                var countriesDB = context.Contracts.ToList();


                foreach (var countryDB in countriesDB)
                {
                    ContractList.Add(new Contract()
                    {

                    });
                }
            }
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

    }
}
