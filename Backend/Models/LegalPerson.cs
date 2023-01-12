﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class LegalPerson
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

        public Contracts Contracts { get; set; }

        public int GetAnimalCount()
        {
            var animalsCount = Contracts.ContractList.Where(contract => contract.LegalPerson.Id == this.Id).Count();

            return animalsCount;
        }

        public int GetDogCount()
        {
            var dogsCount = Contracts.ContractList.Where(contract =>
                   contract.LegalPerson.Id == this.Id &&
                   contract.AnimalCard.AnimalCategory.Id == 1)
                   .Count();

            return dogsCount;
        }

        public int GetCatCount()
        {
            var catsCount = Contracts.ContractList.Where(contract =>
                   contract.LegalPerson.Id == this.Id &&
                   contract.AnimalCard.AnimalCategory.Id == 2)
                   .Count();

            return catsCount;
        }
    }
}
