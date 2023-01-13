using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class PhysicalPerson
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Country Country { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public Contracts Contracts { get; set; } 

        public int GetAnimalCount()
        {
            var animalsCount = Contracts.ContractList
                .Where(contract => contract.PhysicalPerson.Id == this.Id)
                .Where(contract => contract.LegalPerson == null)
                .Count();

            return animalsCount;
        }

        public int GetDogCount()
        {
            var dogsCount = Contracts.ContractList
                .Where(contract => contract.AnimalCard.AnimalCategory.Id == 1)
                .Count();

            return dogsCount;
        }

        public int GetCatCount()
        {
            var catsCount = Contracts.ContractList
                .Where(contract => contract.AnimalCard.AnimalCategory.Id == 2)
                .Count();

            return catsCount;
        }

        public void FillContracts(Contracts contracts)
        {
            Contracts = new Contracts(contracts.ContractList
                .Where(contract => contract.LegalPerson == null)
                .Where(contract => contract.PhysicalPerson.Id == Id));
        }

        public IEnumerable<AnimalCard> GetAnimals()
        {
            return Contracts.ContractList
                .Where(x => x.LegalPerson == null)
                .Select(x => x.AnimalCard);
        }
    }
}
