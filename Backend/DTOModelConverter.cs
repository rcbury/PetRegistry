using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    internal class DTOModelConverter
    {
        public static LegalPerson ConvertDTOToModel(LegalPersonDTO legalPersonDTO)
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

        public static PhysicalPerson ConvertDTOToModel(PhysicalPersonDTO physicalPersonDTO)
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

        public static PhysicalPersonDTO ConvertModelToDTO(PhysicalPerson physicalPersonModel)
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
                AnimalCount = physicalPersonModel.GetAnimalCount(),
                CatCount = physicalPersonModel.GetCatCount(),
                DogCount = physicalPersonModel.GetDogCount(),
                CountryName = physicalPersonModel.FkCountryNavigation.Name,
                LocationName = physicalPersonModel.FkLocalityNavigation.Name
            };

            return physicalPersonModelDTO;
        }

        public static LegalPersonDTO ConvertModelToDTO(LegalPerson legalPersonModel)
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
                AnimalCount = legalPersonModel.GetAnimalCount(),
                CatCount = legalPersonModel.GetCatCount(),
                DogCount = legalPersonModel.GetDogCount(),
                CountryName = legalPersonModel.FkCountryNavigation.Name,
                LocationName = legalPersonModel.FkLocalityNavigation.Name
            };

            return legalPersonModelDTO;
        }

        public static ParasiteTreatmentDTO ConvertModelToDTO(ParasiteTreatment parasiteTreatment)
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

        public static VaccinationDTO ConvertModelToDTO(Vaccination vaccination)
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

        public static VeterinaryAppointmentDTO ConvertModelToDTO(VeterinaryAppointmentAnimal veterinaryAppointment)
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

        public static VaccineDTO ConvertModelToDTO(Vaccine vaccine)
        {
            var vaccineDTO = new VaccineDTO()
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Number = vaccine.Number,
                ValidityPeriod = vaccine.ValidityPeriod,
            };

            return vaccineDTO;
        }

        public static AnimalCardDTO ConvertModelToDTO(AnimalCard model)
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

        public static UserDTO ConvertModelToDTO(User user)
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

        public static CountryDTO ConvertModelToDTO(Country country)
        {
            var countryDTO = new CountryDTO()
            {
                Id= country.Id,
                Name = country.Name
            };

            return countryDTO;
        }

        public static LocationDTO ConvertModelToDTO(Location location)
        {
            var locationDTO = new LocationDTO()
            {
                Id = location.Id,
                Name = location.Name
            };

            return locationDTO;
        }

        public static ContractDTO ConvertModelToDTO(Contract contract) 
        {
            var res = new ContractDTO();
            res.Number = contract.Number;
            res.Date = contract.Date;
            res.FkAnimalCard = contract.FkAnimalCard;
            res.FkUser = contract.FkUser;
            res.FkPhysicalPerson = contract.FkPhysicalPerson;
            res.FkLegalPerson = contract.FkLegalPerson;
            return res;
        }
    }
}
