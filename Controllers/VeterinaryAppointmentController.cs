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
                            Id = veterinaryAppointment.Id,
                            FkAnimal = veterinaryAppointment.FkAnimal,
                            FkUser = veterinaryAppointment.FkUser,
                            Name = veterinaryAppointment.Name,
                            Date = veterinaryAppointment.Date,
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


            //Entity framework govno
            using (var context = new RegistryPetsContext())
            {
                context.VeterinaryAppointmentAnimals.Add(veterinaryAppointmentModel);
                context.SaveChanges();
            }

            using (var context = new RegistryPetsContext())
            {
                veterinaryAppointmentModel = context.VeterinaryAppointmentAnimals.Where(x => x.Id == veterinaryAppointmentModel.Id).FirstOrDefault();


                var newVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO()
                {
                    Id = veterinaryAppointmentDTO.Id,
                    FkAnimal = veterinaryAppointmentModel.FkAnimal,
                    FkUser = veterinaryAppointmentModel.FkUser,
                    Name = veterinaryAppointmentModel.Name,
                    Date = veterinaryAppointmentModel.Date,
                    IsCompleted = veterinaryAppointmentModel.IsCompleted,
                    UserName = veterinaryAppointmentModel.FkUserNavigation.Name,
                };

                return newVeterinaryAppointmentDTO;
            }
        }

        public static VeterinaryAppointmentDTO UpdateVeterinaryAppointment(VeterinaryAppointmentDTO veterinaryAppointmentDTO, UserDTO userDTO)
        {
            VeterinaryAppointmentAnimal? veterinaryAppointmentModel;

            using (var context = new RegistryPetsContext())
            {
                veterinaryAppointmentModel = context.VeterinaryAppointmentAnimals.Where(x => x.Id.Equals(veterinaryAppointmentDTO.Id)).FirstOrDefault();

                if (veterinaryAppointmentModel == null)
                    throw new Exception("trying to update non existent model");

                context.VeterinaryAppointmentAnimals.Remove(veterinaryAppointmentModel);

                context.SaveChanges();

                veterinaryAppointmentModel.Id = 0;
                veterinaryAppointmentModel.Date = veterinaryAppointmentDTO.Date;
                veterinaryAppointmentModel.Name = veterinaryAppointmentDTO.Name;
                veterinaryAppointmentModel.IsCompleted = veterinaryAppointmentDTO.IsCompleted;

                context.VeterinaryAppointmentAnimals.Add(veterinaryAppointmentModel);
                context.SaveChanges();


                var newVaccinationDTO = new VeterinaryAppointmentDTO()
                {
                    Id = veterinaryAppointmentModel.Id,
                    FkAnimal = veterinaryAppointmentModel.FkAnimal,
                    FkUser = veterinaryAppointmentModel.FkUser,
                    Name = veterinaryAppointmentModel.Name,
                    Date = veterinaryAppointmentModel.Date,
                    UserName = veterinaryAppointmentModel.FkUserNavigation.Name,
                    IsCompleted = veterinaryAppointmentModel.IsCompleted
                };

                return newVaccinationDTO;
            }
        }
    }
}
