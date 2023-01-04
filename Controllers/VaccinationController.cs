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
                var vaccinations = context.Vaccinations.Where(x => x.FkAnimal == FkAnimal).ToList();
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
                            FkVaccine = vaccination.FkVaccine,
                        });
                }
            }
            return vaccinationsDTO;
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

        public static VaccinationDTO AddVaccination(VaccinationDTO vaccinationDTO, UserDTO userDTO)
        {

            var vaccinationModel = new Vaccination()
            {
                FkAnimal = vaccinationDTO.FkAnimal,
                FkUser = userDTO.Id,
                FkVaccine = vaccinationDTO.FkVaccine,
                DateEnd = vaccinationDTO.DateEnd,
            };

            var existingModel = GetVaccination(
                vaccinationDTO.FkAnimal,
                userDTO.Id,
                vaccinationDTO.FkVaccine,
                vaccinationDTO.DateEnd);

            if (existingModel != null)
                throw new Exception("Данная запись уже существует");

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

        public static VaccinationDTO UpdateVaccination(VaccinationDTO oldVaccinationDTO, VaccinationDTO vaccinationDTO, UserDTO userDTO)
        {
            Vaccination? vaccinationModel;

            using (var context = new RegistryPetsContext())
            {
                vaccinationModel = GetVaccination(
                    oldVaccinationDTO.FkAnimal,
                    (int)oldVaccinationDTO.FkUser,
                    oldVaccinationDTO.FkVaccine,
                    oldVaccinationDTO.DateEnd);

                if (vaccinationModel == null)
                    throw new Exception("trying to update non existent model");

                var existingVaccinationModel = GetVaccination(
                    vaccinationDTO.FkAnimal,
                    (int)oldVaccinationDTO.FkUser,
                    vaccinationDTO.FkVaccine,
                    vaccinationDTO.DateEnd);

                if (existingVaccinationModel != null)
                    throw new Exception("Данная запись уже существует");

                context.Remove(vaccinationModel);
                context.SaveChanges();

                vaccinationModel.DateEnd = vaccinationDTO.DateEnd;
                vaccinationModel.FkVaccine = vaccinationDTO.FkVaccine;

                context.Add(vaccinationModel);
                context.SaveChanges();

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
}
