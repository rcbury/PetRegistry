using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    public class PetOwnersService
    {
        public static List<PhysicalPerson> GetPhysicalPeople() 
        {
            using (var context = new RegistryPetsContext())
            {
                var physicalPeople = context.PhysicalPeople
                    .Include(x => x.FkLocalityNavigation)
                    .Include(x => x.FkCountryNavigation)
                    .ToList();

                return physicalPeople;
            }
        }

        public static List<LegalPerson> GetLegalPeople()
        {
            using (var context = new RegistryPetsContext())
            {
                var legalPeople = context.LegalPeople
                    .Include(x => x.FkLocalityNavigation)
                    .Include(x => x.FkCountryNavigation)
                    .ToList();

                return legalPeople;
            }
        }

        public static LegalPerson? GetLegalPersonById(int? personId)
        {
            if (personId == null) 
            {
                return null;
            }
            using (var context = new RegistryPetsContext()) 
            {
                return context.LegalPeople
                    .Where(person => person.Id == personId)
                    .Include(person => person.FkLocalityNavigation)
                    .Include(person => person.FkCountryNavigation)
                    .FirstOrDefault();
            }
        }

        public static PhysicalPerson? GetPhysicalPersonById(int personId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.PhysicalPeople
                    .Where(person => person.Id == personId)
                    .Include(person => person.FkLocalityNavigation)
                    .Include(person => person.FkCountryNavigation)
                    .FirstOrDefault();
            }
        }

        public static PhysicalPerson? GetPhysicalPersonByPhone(string phone)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.PhysicalPeople
                    .Where(person => person.Phone == phone)
                    .Include(person => person.FkLocalityNavigation)
                    .Include(person => person.FkCountryNavigation)
                    .FirstOrDefault();
            }
        }
        public static LegalPerson? GetLegalPersonByInn(string INN)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.LegalPeople
                    .Where(person => person.Inn == INN)
                    .Include(person => person.FkLocalityNavigation)
                    .Include(person => person.FkCountryNavigation)
                    .FirstOrDefault();
            }
        }
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
