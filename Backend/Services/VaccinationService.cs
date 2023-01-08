using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class VaccinationService
    {
        public static List<Vaccination> GetVaccinationsByAnimal(int FkAnimal)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.Vaccinations
                    .Include(x => x.FkUserNavigation)
                    .Include(x => x.FkVaccineNavigation)
                    .Where(x => x.FkAnimal == FkAnimal)
                    .ToList();
            }
        }

        public static Vaccination? GetVaccination(int fkAnimal, int fkUser, int fkVaccine, DateOnly dateEnd)
        {
            using (var context = new RegistryPetsContext())
            {
                var vaccinationModel = context.Vaccinations
                    .Where(x => x.FkUser == fkUser)
                    .Where(x => x.FkVaccine == fkVaccine)
                    .Where(x => x.DateEnd == dateEnd)
                    .Where(x => x.FkAnimal == fkAnimal)
                    .FirstOrDefault();

                return vaccinationModel;
            }
        }

        public static Vaccination AddVaccination(Vaccination vaccination, User user)
        {
            var existingVaccination = GetVaccination(
                vaccination.FkAnimal,
                user.Id,
                vaccination.FkVaccine,
                vaccination.DateEnd);

            if (existingVaccination != null)
                throw new Exception("Данная запись уже существует");

            vaccination.FkUser = user.Id;

            using (var context = new RegistryPetsContext())
            {
                context.Vaccinations.Add(vaccination);
                context.SaveChanges();
            }

            return vaccination;
        }

        public static Vaccination UpdateVaccination(Vaccination oldVaccination, Vaccination modifiedVaccination)
        {
            using (var context = new RegistryPetsContext())
            {
                var vaccinationModel = GetVaccination(
                    oldVaccination.FkAnimal,
                    (int)oldVaccination.FkUser,
                    oldVaccination.FkVaccine,
                    oldVaccination.DateEnd);

                if (vaccinationModel == null)
                    throw new Exception("trying to update non existent model");

                var existingModifiedVaccinationModel = GetVaccination(
                    modifiedVaccination.FkAnimal,
                    (int)oldVaccination.FkUser,
                    modifiedVaccination.FkVaccine,
                    modifiedVaccination.DateEnd);

                if (existingModifiedVaccinationModel != null)
                    throw new Exception("Данная запись уже существует");

                context.Remove(vaccinationModel);
                context.SaveChanges();

                context.Add(modifiedVaccination);
                context.SaveChanges();

                return modifiedVaccination;
            }
        }
    }
}
