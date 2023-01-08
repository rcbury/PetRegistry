using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
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
            var vaccines = VaccineService.GetVaccines();
            foreach (var vaccine in vaccines)
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
            return vaccinesDTO;
        }

        public static List<VaccinationDTO> GetVaccinationsByAnimal(int fkAnimal)
        {
            var vaccinationsDTO = new List<VaccinationDTO>();
            var vaccinations = VaccinationService.GetVaccinationsByAnimal(fkAnimal);

            foreach (var vaccination in vaccinations)
            {
                vaccinationsDTO.Add(DTOModelConverter.ConvertVaccinationToDTO(vaccination));
            }

            return vaccinationsDTO;
        }

        public static VaccinationDTO AddVaccination(VaccinationDTO vaccinationDTO)
        {
            var vaccinationModel = new Vaccination()
            {
                FkAnimal = vaccinationDTO.FkAnimal,
                FkVaccine = vaccinationDTO.FkVaccine,
                DateEnd = vaccinationDTO.DateEnd,
            };

            var user = AuthorizationService.GetAuthorizedUser();

            vaccinationModel = VaccinationService.AddVaccination(vaccinationModel, user);

            var newVaccinationDTO = DTOModelConverter.ConvertVaccinationToDTO(vaccinationModel);

            return newVaccinationDTO;
        }

        public static VaccinationDTO UpdateVaccination(VaccinationDTO oldVaccinationDTO, VaccinationDTO modifiedVaccinationDTO)
        {
            var oldVaccinationModel = new Vaccination()
            {
                FkAnimal = oldVaccinationDTO.FkAnimal,
                FkUser = (int)oldVaccinationDTO.FkUser,
                FkVaccine = oldVaccinationDTO.FkVaccine,
                DateEnd = oldVaccinationDTO.DateEnd
            };

            var modifiedVaccinationModel = new Vaccination()
            {
                FkAnimal = modifiedVaccinationDTO.FkAnimal,
                FkUser = (int)modifiedVaccinationDTO.FkUser,
                FkVaccine = modifiedVaccinationDTO.FkVaccine,
                DateEnd = modifiedVaccinationDTO.DateEnd
            };

            var updatedVaccination = VaccinationService.UpdateVaccination(oldVaccinationModel, modifiedVaccinationModel);

            var newVaccinationDTO = DTOModelConverter.ConvertVaccinationToDTO(updatedVaccination);

            return newVaccinationDTO;
        }
    }
}
