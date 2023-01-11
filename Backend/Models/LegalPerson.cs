using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    internal class LegalPerson
    {
        public int Id { get; set; }

        public string Inn { get; set; } = null!;

        public string Kpp { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Country Country { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public int GetAnimalCount()
        {
            //var animalsCount = context.Contracts.Where(contract => contract.FkLegalPerson == this.Id).Count();

            //return animalsCount;

            return 0;
        }

        public int GetDogCount()
        {
            //using (var context = new RegistryPetsContext())
            //{
            //    var dogsCount = context.Contracts.Where(contract =>
            //           contract.FkLegalPerson == this.Id &&
            //           context.AnimalCards.Where(card => card.FkCategory == 1 && card.Id == contract.FkAnimalCard).Count() != 0)
            //           .Count();

            //    return dogsCount;
            //}

            return 0;
        }

        public int GetCatCount()
        {
            //using (var context = new RegistryPetsContext())
            //{
            //    var catsCount = context.Contracts.Where(contract =>
            //           contract.FkLegalPerson == this.Id &&
            //           context.AnimalCards.Where(card => card.FkCategory == 2 && card.Id == contract.FkAnimalCard).Count() != 0)
            //           .Count();

            //    return catsCount;
            //}

            return 0;
        }
    }
}
