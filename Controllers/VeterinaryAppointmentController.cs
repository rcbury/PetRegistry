using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class VeterinaryAppointmentController
    {
        public static List<VeterinaryAppointmentDTO> GetVeterinaryAppointmentsByAnimal(int FkAnimal)
        {
            var veterinaryAppointmentsDTO = new List<VeterinaryAppointmentDTO>();
            using (var context = new RegistryPetsContext())
            {
                var veterinaryAppointments = context.VeterinaryAppointmentAnimals.Where(x => x.FkAnimal == FkAnimal).ToList();

                foreach (var veterinaryAppointment in veterinaryAppointments)
                {
                    veterinaryAppointmentsDTO.Add(
                        new VeterinaryAppointmentDTO()
                        {
                            FkAnimal = veterinaryAppointment.FkAnimal,
                            FkUser = veterinaryAppointment.FkUser,
                            Name = veterinaryAppointment.Name,
                            //potential kal
                            Date = veterinaryAppointment.Date.ToLocalTime(),
                            IsCompleted = veterinaryAppointment.IsCompleted,
                            UserName = veterinaryAppointment.FkUserNavigation.Name
                        });
                }
            }
            return veterinaryAppointmentsDTO;
        }



        public static VeterinaryAppointmentDTO AddVeterinaryAppointment(VeterinaryAppointmentDTO veterinaryAppointmentDTO, UserDTO userDTO)
        {

            var veterinaryAppointmentModel = new VeterinaryAppointmentAnimal()
            {
                FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                FkUser = userDTO.Id,
                Date = veterinaryAppointmentDTO.Date,
                Name = veterinaryAppointmentDTO.Name,
                IsCompleted = veterinaryAppointmentDTO.IsCompleted,
            };

            var existingVeterinaryAppointmentModel = GetVeterinaryAppointment(
                veterinaryAppointmentDTO.FkAnimal,
                veterinaryAppointmentDTO.Date);

            if (existingVeterinaryAppointmentModel != null)
            {
                throw new Exception("Данная запись уже существует");
            }

            using (var context = new RegistryPetsContext())
            {
                context.VeterinaryAppointmentAnimals.Add(veterinaryAppointmentModel);
                context.SaveChanges();
            }

            var newVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO()
            {
                FkAnimal = veterinaryAppointmentModel.FkAnimal,
                FkUser = veterinaryAppointmentModel.FkUser,
                Name = veterinaryAppointmentModel.Name,
                Date = veterinaryAppointmentModel.Date,
                IsCompleted = veterinaryAppointmentModel.IsCompleted,
            };

            return newVeterinaryAppointmentDTO;
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

        public static VeterinaryAppointmentDTO UpdateVeterinaryAppointment(
            VeterinaryAppointmentDTO oldVeterinaryAppointmentDTO,
            VeterinaryAppointmentDTO veterinaryAppointmentDTO,
            UserDTO userDTO)
        {
            VeterinaryAppointmentAnimal? veterinaryAppointmentModel;

            using (var context = new RegistryPetsContext())
            {
                veterinaryAppointmentModel = GetVeterinaryAppointment(
                    oldVeterinaryAppointmentDTO.FkAnimal,
                    oldVeterinaryAppointmentDTO.Date);

                if (veterinaryAppointmentModel == null)
                    throw new Exception("trying to update non existent model");

                if (veterinaryAppointmentDTO.FkAnimal != oldVeterinaryAppointmentDTO.FkAnimal || 
                    veterinaryAppointmentDTO.Date != oldVeterinaryAppointmentDTO.Date)
                {
                    var existingVeterinaryAppointmentModel = GetVeterinaryAppointment(
                        veterinaryAppointmentDTO.FkAnimal,
                        veterinaryAppointmentDTO.Date);

                    if (existingVeterinaryAppointmentModel != null)
                        throw new Exception("Запись уже существует");
                }


                context.VeterinaryAppointmentAnimals.Remove(veterinaryAppointmentModel);
                context.SaveChanges();

                veterinaryAppointmentModel.Date = veterinaryAppointmentDTO.Date;
                veterinaryAppointmentModel.Name = veterinaryAppointmentDTO.Name;
                veterinaryAppointmentModel.IsCompleted = veterinaryAppointmentDTO.IsCompleted;

                context.VeterinaryAppointmentAnimals.Add(veterinaryAppointmentModel);
                context.SaveChanges();


                var newVaccinationDTO = new VeterinaryAppointmentDTO()
                {
                    FkAnimal = veterinaryAppointmentModel.FkAnimal,
                    FkUser = veterinaryAppointmentModel.FkUser,
                    Name = veterinaryAppointmentModel.Name,
                    Date = veterinaryAppointmentModel.Date,
                    IsCompleted = veterinaryAppointmentModel.IsCompleted
                };

                return newVaccinationDTO;
            }
        }
    }
}
