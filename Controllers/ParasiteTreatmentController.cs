using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
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
            var parasiteTreatmentMedicationsDTO = new List<ParasiteTreatmentMedicationDTO>();
            using (var context = new RegistryPetsContext())
            {
                foreach (var parasiteTreatmentMedication in context.ParasiteTreatmentMedications)
                {
                    parasiteTreatmentMedicationsDTO.Add(
                        new ParasiteTreatmentMedicationDTO()
                        {
                            Id = parasiteTreatmentMedication.Id,
                            Name = parasiteTreatmentMedication.Name,
                        });
                }
            }
            return parasiteTreatmentMedicationsDTO;
        }

        public static List<ParasiteTreatmentDTO> GetParasiteTreatmentsByAnimal(int FkAnimal)
        {
            var parasiteTreatmentsDTO = new List<ParasiteTreatmentDTO>();
            using (var context = new RegistryPetsContext())
            {
                var parasiteTreatments = context.ParasiteTreatments.Where(x => x.FkAnimal == FkAnimal).ToList();

                foreach (var parasiteTreatment in parasiteTreatments)
                {
                    parasiteTreatmentsDTO.Add(
                        new ParasiteTreatmentDTO()
                        {
                            FkAnimal = parasiteTreatment.FkAnimal,
                            FkUser = parasiteTreatment.FkUser,
                            FkMedication = parasiteTreatment.FkMedication,
                            Date = parasiteTreatment.Date,
                            MedicationName = parasiteTreatment.FkMedicationNavigation.Name,
                            UserName = parasiteTreatment.FkUserNavigation.Name
                        });
                }
            }
            return parasiteTreatmentsDTO;
        }



        public static ParasiteTreatmentDTO AddParasiteTreatment(ParasiteTreatmentDTO parasiteTreatmentDTO, UserDTO userDTO)
        {

            var parasiteTreatmentModel = new ParasiteTreatment()
            {
                FkAnimal = parasiteTreatmentDTO.FkAnimal,
                FkUser = userDTO.Id,
                FkMedication = parasiteTreatmentDTO.FkMedication,
                Date = parasiteTreatmentDTO.Date,
            };

            var existingParasiteTreatment = GetParasiteTreatment(
                parasiteTreatmentDTO.FkAnimal,
                userDTO.Id,
                parasiteTreatmentDTO.Date,
                parasiteTreatmentDTO.FkMedication);

            if (existingParasiteTreatment != null)
                throw new Exception("Запись уже существует");

            using (var context = new RegistryPetsContext())
            {
                context.ParasiteTreatments.Add(parasiteTreatmentModel);
                context.SaveChanges();
            }

            var newParasiteTreatmentDTO = new ParasiteTreatmentDTO()
            {
                FkAnimal = parasiteTreatmentModel.FkAnimal,
                FkUser = parasiteTreatmentModel.FkUser,
                FkMedication = parasiteTreatmentModel.FkMedication,
                Date = parasiteTreatmentModel.Date,
            };

            return newParasiteTreatmentDTO;
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

        public static ParasiteTreatmentDTO UpdateParasiteTreatment(
            ParasiteTreatmentDTO oldParasiteTreatmentDTO,
            ParasiteTreatmentDTO parasiteTreatmentDTO,
            UserDTO userDTO)
        {
            ParasiteTreatment? parasiteTreatmentModel;

            using (var context = new RegistryPetsContext())
            {
                parasiteTreatmentModel = GetParasiteTreatment(
                    oldParasiteTreatmentDTO.FkAnimal,
                    (int)oldParasiteTreatmentDTO.FkUser,
                    oldParasiteTreatmentDTO.Date,
                    oldParasiteTreatmentDTO.FkMedication);

                if (parasiteTreatmentModel == null)
                    throw new Exception("trying to update non existent model");

                var existingParasiteTreatment = GetParasiteTreatment(
                    parasiteTreatmentDTO.FkAnimal,
                    parasiteTreatmentModel.FkUser,
                    parasiteTreatmentDTO.Date,
                    parasiteTreatmentDTO.FkMedication);

                if (existingParasiteTreatment != null)
                    throw new Exception("Данная запись уже существует");

                context.ParasiteTreatments.Remove(parasiteTreatmentModel);

                context.SaveChanges();

                parasiteTreatmentModel.Date = parasiteTreatmentDTO.Date;
                parasiteTreatmentModel.FkMedication = parasiteTreatmentDTO.FkMedication;

                context.ParasiteTreatments.Add(parasiteTreatmentModel);
                context.SaveChanges();


                var newParasiteTreatmentDTO = new ParasiteTreatmentDTO()
                {
                    FkAnimal = parasiteTreatmentModel.FkAnimal,
                    FkUser = parasiteTreatmentModel.FkUser,
                    FkMedication = parasiteTreatmentModel.FkMedication,
                    Date = parasiteTreatmentModel.Date,
                };

                return newParasiteTreatmentDTO;
            }
        }
    }
}
