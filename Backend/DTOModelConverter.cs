using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    internal class DTOModelConverter
    {
        public static LegalPerson ConvertLegalPersonDTOToModel(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = new LegalPerson()
            {
                Id= legalPersonDTO.Id,
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            return legalPersonModel;
        }

        public static PhysicalPerson ConvertPhysicalPersonDTOToModel(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = new PhysicalPerson()
            {
                Id = physicalPersonDTO.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkCountry = physicalPersonDTO.FkCountry,
                FkLocality = physicalPersonDTO.FkLocality,
            };

            return physicalPersonModel;
        }

        public static PhysicalPersonDTO ConvertPhysicalPersonModelToDTO(PhysicalPerson physicalPersonModel)
        {
            var physicalPersonModelDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonModel.Name,
                Address = physicalPersonModel.Address,
                Email = physicalPersonModel.Email,
                Phone = physicalPersonModel.Phone,
                FkCountry = physicalPersonModel.FkCountry,
                FkLocality = physicalPersonModel.FkLocality,
            };

            return physicalPersonModelDTO;
        }

        public static LegalPersonDTO ConvertLegalPersonModelToDTO(LegalPerson legalPersonModel)
        {
            var legalPersonModelDTO = new LegalPersonDTO()
            {
                Id = legalPersonModel.Id,
                INN = legalPersonModel.Inn,
                KPP = legalPersonModel.Kpp,
                Name = legalPersonModel.Name,
                Address = legalPersonModel.Address,
                Email = legalPersonModel.Email,
                Phone = legalPersonModel.Phone,
                FkCountry = legalPersonModel.FkCountry,
                FkLocality = legalPersonModel.FkLocality,
            };

            return legalPersonModelDTO;
        }

        public static ParasiteTreatmentDTO ConvertParasiteTreatmentToDTO(ParasiteTreatment parasiteTreatment)
        {
            var parasiteTreatmentDTO = new ParasiteTreatmentDTO()
            {
                FkAnimal = parasiteTreatment.FkAnimal,
                FkUser = parasiteTreatment.FkUser,
                FkMedication = parasiteTreatment.FkMedication,
                Date = parasiteTreatment.Date,
            };

            if (parasiteTreatment.FkUserNavigation != null)
            {
                parasiteTreatmentDTO.UserName = parasiteTreatment.FkUserNavigation.Name;
            }

            if (parasiteTreatment.FkMedicationNavigation != null)
            {
                parasiteTreatmentDTO.MedicationName = parasiteTreatment.FkMedicationNavigation.Name;
            }

            return parasiteTreatmentDTO;
        }

        public static VaccinationDTO ConvertVaccinationToDTO(Vaccination vaccination)
        {
            var vaccinationDTO = new VaccinationDTO()
            {
                FkAnimal = vaccination.FkAnimal,
                FkUser = vaccination.FkUser,
                FkVaccine = vaccination.FkVaccine,
                DateEnd = vaccination.DateEnd,
            };

            if (vaccination.FkVaccineNavigation != null)
            {
                vaccinationDTO.VaccineName = vaccination.FkVaccineNavigation.Name;
            }
            if (vaccination.FkUserNavigation != null)
            {
                vaccinationDTO.UserName = vaccination.FkUserNavigation.Name;
            }


            return vaccinationDTO;
        }

        public static VeterinaryAppointmentDTO ConvertVeterinaryAppointmentToDTO(VeterinaryAppointmentAnimal veterinaryAppointment)
        {
            var veterinaryAppointmentDTO = new VeterinaryAppointmentDTO()
            {
                FkAnimal = veterinaryAppointment.FkAnimal,
                FkUser = veterinaryAppointment.FkUser,
                Name = veterinaryAppointment.Name,
                Date = veterinaryAppointment.Date.ToLocalTime(),
                IsCompleted = veterinaryAppointment.IsCompleted,
            };

            if (veterinaryAppointment.FkUserNavigation != null)
            {
                veterinaryAppointmentDTO.UserName = veterinaryAppointment.FkUserNavigation.Name;
            }

            return veterinaryAppointmentDTO;
        }

        public static AnimalCardDTO ConvertAnimalCardToDTO(AnimalCard model)
        {
            var AnimalCardDTO = new AnimalCardDTO()
            {
                Id = model.Id,
                ChipId = model.ChipId,
                Name = model.Name,
                IsBoy = model.IsBoy,
                FkCategory = model.FkCategory,
                FkShelter = model.FkShelter,
                YearOfBirth = model.YearOfBirth,
                Photo = model.Photo,
                CategoryName = model.FkCategoryNavigation != null ? model.FkCategoryNavigation.Name : null
            };

            return AnimalCardDTO;
        }

        public static UserDTO ConvertUserToDTO(User user)
        {
            var userDTO = new UserDTO()
            {
                Login = user.Login,
                Id = user.Id,
                ShelterId = user.FkShelter,
                RoleId = user.FkRole,
                LocationId = user.FkLocation,
                Name = user.Name,
            };

            if (user.FkShelter != null)
            {
                userDTO.ShelterLocationId = user.FkShelterNavigation.FkLocation;
            }

            return userDTO;
        }
    }
}
