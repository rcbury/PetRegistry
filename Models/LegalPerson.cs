using System;
using System.Collections.Generic;

namespace PIS_PetRegistry.Models;

public partial class LegalPerson
{
    public int Id { get; set; }

    public string Inn { get; set; } = null!;

    public string Kpp { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int FkCountry { get; set; }

    public int FkLocality { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual Country FkCountryNavigation { get; set; } = null!;

    public virtual Location FkLocalityNavigation { get; set; } = null!;

    public int GetAnimalCount()
    {
        using (var context = new RegistryPetsContext())
        {
            var animalsCount = context.Contracts.Where(contract => contract.FkLegalPerson == this.Id).Count();

            return animalsCount;
        }
    }

    public int GetDogCount()
    {
        using (var context = new RegistryPetsContext())
        {
            var dogsCount = context.Contracts.Where(contract =>
                   contract.FkLegalPerson == this.Id &&
                   context.AnimalCards.Where(card => card.FkCategory == 1 && card.Id == contract.FkAnimalCard).Count() != 0)
                   .Count();

            return dogsCount;
        }
    }

    public int GetCatCount()
    {
        using (var context = new RegistryPetsContext())
        {
            var catsCount = context.Contracts.Where(contract =>
                   contract.FkLegalPerson == this.Id &&
                   context.AnimalCards.Where(card => card.FkCategory == 2 && card.Id == contract.FkAnimalCard).Count() != 0)
                   .Count();

            return catsCount;
        }
    }
}
