using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class PetOwnersService
    {
        public static LegalPerson AddLegalPerson(LegalPerson legalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.LegalPeople.Add(legalPersonModel);
                context.SaveChanges();
            }

            return legalPersonModel;
        }

        public static PhysicalPerson AddPhysicalPerson(PhysicalPerson physicalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.PhysicalPeople.Add(physicalPersonModel);
                context.SaveChanges();
            }

            return physicalPersonModel;
        }

        public static LegalPerson UpdateLegalPerson(LegalPerson legalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.Update(legalPersonModel);
                context.SaveChanges();
            }

            return legalPersonModel;
        }

        public static PhysicalPerson UpdatePhysicalPerson(PhysicalPerson physicalPersonModel)
        {
            using (var context = new RegistryPetsContext())
            {
                context.Update(physicalPersonModel);
                context.SaveChanges();
            }

            return physicalPersonModel;
        }

    }
}
