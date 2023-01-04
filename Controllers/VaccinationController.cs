using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class VaccinationController
    {
        public static List<VaccineDTO> GetVaccines()
        {
            var vaccinesDTO = new List<VaccineDTO>();
            using (var context = new RegistryPetsContext())
            {
                foreach (var vaccine in context.Vaccines)
                {
                    vaccinesDTO.Add(
                        new VaccineDTO()
                        {
                            Id = vaccine.Id,
                            Name = vaccine.Name,
                            Number = vaccine.Number,
                            ValidityPeriod = vaccine.ValidityPeriod,                            
                        });
                }
            }
            return vaccinesDTO;
        }

        public static List<VaccinationDTO> GetVaccinationsByAnimal(int FkAnimal)
        {
            var vaccinationsDTO = new List<VaccinationDTO>();
            using (var context = new RegistryPetsContext())
            {
                var vaccinations = context.Vaccinations.Where(x => x.FkAnimal == FkAnimal);
                foreach (var vaccination in vaccinations)
                {
                    vaccinationsDTO.Add(
                        new VaccinationDTO()
                        {
                            VaccineName = vaccination.FkVaccineNavigation.Name,
                            UserName = vaccination.FkUserNavigation.Name,
                            DateEnd = vaccination.DateEnd,
                            FkUser = vaccination.FkUser,
                            FkAnimal = vaccination.FkAnimal,
                        });
                }
            }
            return vaccinationsDTO;
        }

        public static VaccinationDTO AddVaccination(VaccinationDTO vaccinationDTO, UserDTO userDTO)
        {

            var vaccinationModel = new Vaccination()
            {
                FkAnimal = vaccinationDTO.FkAnimal,
                FkUser = userDTO.Id,
                FkVaccine = vaccinationDTO.FkVaccine,
                DateEnd = vaccinationDTO.DateEnd,
            };

            using (var context = new RegistryPetsContext())
            {
                context.Vaccinations.Add(vaccinationModel);
                context.SaveChanges();
            }

            var newVaccinationDTO = new VaccinationDTO()
            {
                FkAnimal = vaccinationModel.FkAnimal,
                FkUser = vaccinationModel.FkUser,
                FkVaccine = vaccinationModel.FkVaccine,
                DateEnd = vaccinationModel.DateEnd,
            };

            return newVaccinationDTO;
        }

        public static VaccinationDTO UpdateVaccination(VaccinationDTO vaccinationDTO, UserDTO userDTO)
        {
            Vaccination? vaccinationModel;

            using (var context = new RegistryPetsContext())
            {
                vaccinationModel = context.Vaccinations.Where(x => x.DateEnd.Equals(vaccinationDTO.DateEnd) && 
                    x.FkVaccine.Equals(vaccinationDTO.FkVaccine) && 
                    x.FkAnimal.Equals(vaccinationDTO.FkAnimal)).FirstOrDefault();

                if (vaccinationModel == null)
                    throw new Exception("trying to update non existent model");

                vaccinationModel.DateEnd = vaccinationDTO.DateEnd;
                vaccinationModel.FkVaccine = vaccinationDTO.FkVaccine;

                context.SaveChanges();
            }

            var newVaccinationDTO = new VaccinationDTO()
            {
                FkAnimal = vaccinationModel.FkAnimal,
                FkUser = vaccinationModel.FkUser,
                FkVaccine = vaccinationModel.FkVaccine,
                DateEnd = vaccinationModel.DateEnd,
            };

            return newVaccinationDTO;
        }
    }
}
