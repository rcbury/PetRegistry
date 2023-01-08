using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PIS_PetRegistry.Controllers
{
    public class VeterinaryAppointmentController
    {
        public static List<VeterinaryAppointmentDTO> GetVeterinaryAppointmentsByAnimal(int fkAnimal)
        {
            var veterinaryAppointmentsDTO = new List<VeterinaryAppointmentDTO>();

            var veterinaryAppointments = VeterinaryAppointmentService.GetVeterinaryAppointmentsByAnimal(fkAnimal);

            foreach (var veterinaryAppointment in veterinaryAppointments)
            {
                veterinaryAppointmentsDTO.Add(DTOModelConverter.ConvertVeterinaryAppointmentToDTO(veterinaryAppointment));
            }

            return veterinaryAppointmentsDTO;
        }

        

        public static VeterinaryAppointmentDTO AddVeterinaryAppointment(VeterinaryAppointmentDTO veterinaryAppointmentDTO)
        {
            var veterinaryAppointmentModel = new VeterinaryAppointmentAnimal()
            {
                FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                Date = veterinaryAppointmentDTO.Date,
                Name = veterinaryAppointmentDTO.Name,
                IsCompleted = veterinaryAppointmentDTO.IsCompleted,
            };

            var user = AuthorizationService.GetAuthorizedUser();

            veterinaryAppointmentModel = VeterinaryAppointmentService.AddVeterinaryAppointment(veterinaryAppointmentModel, user);

            var newVeterinaryAppointmentDTO = DTOModelConverter.ConvertVeterinaryAppointmentToDTO(veterinaryAppointmentModel);

            return newVeterinaryAppointmentDTO;
        }

        public static VeterinaryAppointmentDTO UpdateVeterinaryAppointment(
            VeterinaryAppointmentDTO oldVeterinaryAppointmentDTO,
            VeterinaryAppointmentDTO modifiedVeterinaryAppointmentDTO)
        {
            var oldVeterinaryAppointmentModel = new VeterinaryAppointmentAnimal()
            {
                FkUser = (int)oldVeterinaryAppointmentDTO.FkUser,
                FkAnimal = oldVeterinaryAppointmentDTO.FkAnimal,
                Date = oldVeterinaryAppointmentDTO.Date,
                Name = oldVeterinaryAppointmentDTO.Name,
                IsCompleted = oldVeterinaryAppointmentDTO.IsCompleted,
            };

            var modifiedVeterinaryAppointmentModel = new VeterinaryAppointmentAnimal()
            {
                FkUser = (int)modifiedVeterinaryAppointmentDTO.FkUser,
                FkAnimal = modifiedVeterinaryAppointmentDTO.FkAnimal,
                Date = modifiedVeterinaryAppointmentDTO.Date,
                Name = modifiedVeterinaryAppointmentDTO.Name,
                IsCompleted = modifiedVeterinaryAppointmentDTO.IsCompleted,
            };

            var updatedVeterinaryAppointment = VeterinaryAppointmentService.UpdateVeterinaryAppointment(
                oldVeterinaryAppointmentModel, 
                modifiedVeterinaryAppointmentModel);

            var newVaccinationDTO = DTOModelConverter.ConvertVeterinaryAppointmentToDTO(updatedVeterinaryAppointment);

            return newVaccinationDTO;
        }
    }
}
