using PIS_PetRegistry.DTO;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class VeterinaryAppointmentService
    {
        public static List<VeterinaryAppointmentAnimal> GetVeterinaryAppointmentsByAnimal(int FkAnimal)
        {
            using (var context = new RegistryPetsContext())
            {
                var veterinaryAppointments = context.VeterinaryAppointmentAnimals
                    .Include(x => x.FkUserNavigation)
                    .Where(x => x.FkAnimal == FkAnimal)
                    .ToList();

                return veterinaryAppointments;
            }
        }

        public static VeterinaryAppointmentAnimal AddVeterinaryAppointment(VeterinaryAppointmentAnimal veterinaryAppointment)
        {
            var existingVeterinaryAppointmentModel = GetVeterinaryAppointment(
                veterinaryAppointment.FkAnimal,
                veterinaryAppointment.Date);


            if (existingVeterinaryAppointmentModel != null)
            {
                throw new Exception("Данная запись уже существует");
            }

            using (var context = new RegistryPetsContext())
            {
                context.VeterinaryAppointmentAnimals.Add(veterinaryAppointment);
                context.SaveChanges();
            }

            return veterinaryAppointment;
        }

        public static VeterinaryAppointmentAnimal? GetVeterinaryAppointment(int fkAnimal, DateTime date)
        {
            using (var context = new RegistryPetsContext())
            {
                var veterinaryAppointmentModel = context.VeterinaryAppointmentAnimals
                    .Where(x => x.FkAnimal == fkAnimal)
                    .Where(x => x.Date == date)
                    .FirstOrDefault();

                return veterinaryAppointmentModel;
            }
        }

        public static VeterinaryAppointmentAnimal UpdateVeterinaryAppointment(
            VeterinaryAppointmentAnimal oldVeterinaryAppointment,
            VeterinaryAppointmentAnimal modifiedVeterinaryAppointment)
        {
            using (var context = new RegistryPetsContext())
            {
                var veterinaryAppointmentModel = GetVeterinaryAppointment(
                    oldVeterinaryAppointment.FkAnimal,
                    oldVeterinaryAppointment.Date);

                if (veterinaryAppointmentModel == null)
                    throw new Exception("trying to update non existent model");

                if (modifiedVeterinaryAppointment.FkAnimal != oldVeterinaryAppointment.FkAnimal ||
                    modifiedVeterinaryAppointment.Date != oldVeterinaryAppointment.Date)
                {
                    var existingVeterinaryAppointmentModel = GetVeterinaryAppointment(
                        modifiedVeterinaryAppointment.FkAnimal,
                        modifiedVeterinaryAppointment.Date);

                    if (existingVeterinaryAppointmentModel != null)
                        throw new Exception("Запись уже существует");
                }

                context.VeterinaryAppointmentAnimals.Remove(veterinaryAppointmentModel);
                context.SaveChanges();

                context.VeterinaryAppointmentAnimals.Add(modifiedVeterinaryAppointment);
                context.SaveChanges();

                return modifiedVeterinaryAppointment;
            }
        }
    }
}
