using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class ParasiteTreatmentService
    {
        public static List<ParasiteTreatment> GetParasiteTreatmentsByAnimal(int FkAnimal)
        {
            using (var context = new RegistryPetsContext())
            {
                var parasiteTreatments = context.ParasiteTreatments
                    .Include(x => x.FkUserNavigation)
                    .Include(x => x.FkMedicationNavigation)
                    .Where(x => x.FkAnimal == FkAnimal)
                    .ToList();
                
                return parasiteTreatments;
            }
        }

        public static ParasiteTreatment AddParasiteTreatment(ParasiteTreatment parasiteTreatment)
        {
            var user = AuthorizationService.GetAuthorizedUser();

            var existingParasiteTreatment = GetParasiteTreatment(
                parasiteTreatment.FkAnimal,
                user.Id,
                parasiteTreatment.Date,
                parasiteTreatment.FkMedication);

            if (existingParasiteTreatment != null)
                throw new Exception("Запись уже существует");

            using (var context = new RegistryPetsContext())
            {
                context.ParasiteTreatments.Add(parasiteTreatment);
                context.SaveChanges();
            }

            return parasiteTreatment;
        }

        public static ParasiteTreatment? GetParasiteTreatment(int fkAnimal, int fkUser, DateOnly date, int fkMedication)
        {
            using (var context = new RegistryPetsContext())
            {
                var parasiteTreatmentModel = context.ParasiteTreatments
                    .Where(x => x.FkAnimal == fkAnimal)
                    .Where(x => x.FkUser == fkUser)
                    .Where(x => x.Date == date)
                    .Where(x => x.FkMedication == fkMedication)
                    .FirstOrDefault();

                return parasiteTreatmentModel;
            }
        }

        public static ParasiteTreatment UpdateParasiteTreatment(
            ParasiteTreatment oldParasiteTreatment,
            ParasiteTreatment modifiedParasiteTreatment)
        {
            ParasiteTreatment? oldParasiteTreatmentModel;

            using (var context = new RegistryPetsContext())
            {
                oldParasiteTreatmentModel = GetParasiteTreatment(
                    oldParasiteTreatment.FkAnimal,
                    (int)oldParasiteTreatment.FkUser,
                    oldParasiteTreatment.Date,
                    oldParasiteTreatment.FkMedication);

                if (oldParasiteTreatmentModel == null)
                    throw new Exception("trying to update non existent model");

                var existingParasiteTreatment = GetParasiteTreatment(
                    modifiedParasiteTreatment.FkAnimal,
                    modifiedParasiteTreatment.FkUser,
                    modifiedParasiteTreatment.Date,
                    modifiedParasiteTreatment.FkMedication);

                if (existingParasiteTreatment != null)
                    throw new Exception("Данная запись уже существует");

                context.ParasiteTreatments.Remove(oldParasiteTreatmentModel);

                context.SaveChanges();

                context.ParasiteTreatments.Add(modifiedParasiteTreatment);
                context.SaveChanges();

                return modifiedParasiteTreatment;
            }
        }
    }
}
