using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
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
                            Id = parasiteTreatment.Id,
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


            //Entity framework govno
            using (var context = new RegistryPetsContext())
            {
                context.ParasiteTreatments.Add(parasiteTreatmentModel);
                context.SaveChanges();
            }

            using (var context = new RegistryPetsContext())
            {
                parasiteTreatmentModel = context.ParasiteTreatments.Where(x => x.Id == parasiteTreatmentModel.Id).FirstOrDefault();


                var newParasiteTreatmentDTO = new ParasiteTreatmentDTO()
                {
                    Id = parasiteTreatmentDTO.Id,
                    FkAnimal = parasiteTreatmentModel.FkAnimal,
                    FkUser = parasiteTreatmentModel.FkUser,
                    FkMedication = parasiteTreatmentModel.FkMedication,
                    Date = parasiteTreatmentModel.Date,
                    UserName = parasiteTreatmentModel.FkUserNavigation.Name,
                    MedicationName = parasiteTreatmentModel.FkMedicationNavigation.Name
                };

                return newParasiteTreatmentDTO;
            }
        }

        public static ParasiteTreatmentDTO UpdateParasiteTreatment(ParasiteTreatmentDTO parasiteTreatmentDTO, UserDTO userDTO)
        {
            ParasiteTreatment? parasiteTreatmentModel;

            using (var context = new RegistryPetsContext())
            {
                parasiteTreatmentModel = context.ParasiteTreatments.Where(x => x.Id.Equals(parasiteTreatmentDTO.Id)).FirstOrDefault();

                if (parasiteTreatmentModel == null)
                    throw new Exception("trying to update non existent model");

                context.ParasiteTreatments.Remove(parasiteTreatmentModel);

                context.SaveChanges();

                parasiteTreatmentModel.Id = 0;
                parasiteTreatmentModel.Date = parasiteTreatmentDTO.Date;
                parasiteTreatmentModel.FkMedication = parasiteTreatmentDTO.FkMedication;

                context.ParasiteTreatments.Add(parasiteTreatmentModel);
                context.SaveChanges();


                var newVaccinationDTO = new ParasiteTreatmentDTO()
                {
                    Id = parasiteTreatmentModel.Id,
                    FkAnimal = parasiteTreatmentModel.FkAnimal,
                    FkUser = parasiteTreatmentModel.FkUser,
                    FkMedication = parasiteTreatmentModel.FkMedication,
                    Date = parasiteTreatmentModel.Date,
                    UserName = parasiteTreatmentModel.FkUserNavigation.Name,
                    MedicationName = parasiteTreatmentModel.FkMedicationNavigation.Name
                };

                return newVaccinationDTO;
            }
        }
    }
}
