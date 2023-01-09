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

namespace PIS_PetRegistry.Controllers
{
    public class ParasiteTreatmentController
    {
        public static List<ParasiteTreatmentMedicationDTO> GetMedications()
        {
            var parasiteTreatmentMedications = ParasiteTreatmentMedicationService.GetParasiteTreatmentMedications();
            var parasiteTreatmentMedicationsDTO = new List<ParasiteTreatmentMedicationDTO>();
            
            foreach (var parasiteTreatmentMedication in parasiteTreatmentMedications)
            {
                parasiteTreatmentMedicationsDTO.Add(
                    new ParasiteTreatmentMedicationDTO()
                    {
                        Id = parasiteTreatmentMedication.Id,
                        Name = parasiteTreatmentMedication.Name,
                    });
            }
            
            return parasiteTreatmentMedicationsDTO;
        }

        public static List<ParasiteTreatmentDTO> GetParasiteTreatmentsByAnimal(int FkAnimal)
        {
            var parasiteTreatments = ParasiteTreatmentService.GetParasiteTreatmentsByAnimal(FkAnimal);
            var parasiteTreatmentsDTO = new List<ParasiteTreatmentDTO>();
            
            foreach (var parasiteTreatment in parasiteTreatments)
            {
                parasiteTreatmentsDTO.Add(DTOModelConverter.ConvertModelToDTO(parasiteTreatment));
            }
            
            return parasiteTreatmentsDTO;
        }



        public static ParasiteTreatmentDTO AddParasiteTreatment(ParasiteTreatmentDTO parasiteTreatmentDTO)
        {
            var user = AuthorizationService.GetAuthorizedUser();

            var parasiteTreatmentModel = new ParasiteTreatment()
            {
                FkAnimal = parasiteTreatmentDTO.FkAnimal,
                FkUser = user.Id,
                FkMedication = parasiteTreatmentDTO.FkMedication,
                Date = parasiteTreatmentDTO.Date,
            };

            parasiteTreatmentModel = ParasiteTreatmentService.AddParasiteTreatment(parasiteTreatmentModel);

            var newParasiteTreatmentDTO = DTOModelConverter.ConvertModelToDTO(parasiteTreatmentModel);

            return newParasiteTreatmentDTO;
        }

        public static ParasiteTreatmentDTO UpdateParasiteTreatment(
            ParasiteTreatmentDTO oldParasiteTreatmentDTO,
            ParasiteTreatmentDTO modifiedParasiteTreatmentDTO)
        {
            var oldParasiteTreatmentModel = new ParasiteTreatment()
            {
                FkAnimal = oldParasiteTreatmentDTO.FkAnimal,
                FkUser = (int)oldParasiteTreatmentDTO.FkUser,
                Date = oldParasiteTreatmentDTO.Date,
                FkMedication = oldParasiteTreatmentDTO.FkMedication,
            };

            var modifiedParasiteTreatmentModel = new ParasiteTreatment()
            {
                FkAnimal = modifiedParasiteTreatmentDTO.FkAnimal,
                FkUser = (int)modifiedParasiteTreatmentDTO.FkUser,
                Date = modifiedParasiteTreatmentDTO.Date,
                FkMedication = modifiedParasiteTreatmentDTO.FkMedication,
            };

            var updatedParasiteTreatment = ParasiteTreatmentService.UpdateParasiteTreatment(
                oldParasiteTreatmentModel, 
                modifiedParasiteTreatmentModel);

            var updatedParasiteTreatmentDTO = DTOModelConverter.ConvertModelToDTO(updatedParasiteTreatment);

            return updatedParasiteTreatmentDTO;
        }
    }
}
