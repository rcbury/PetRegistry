using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    internal class DTOModelConverter
    {
        public static AnimalCategoryDTO ConvertModelToDTO(AnimalCategory animalCategory)
        {
            var animalCategoryDTO = new AnimalCategoryDTO()
            {
                Name = animalCategory.Name,
                Id = animalCategory.Id,
            };

            return animalCategoryDTO;
        }

        public static ParasiteTreatmentMedicationDTO ConvertModelToDTO(Medication medication)
        {
            var medicationDTO = new ParasiteTreatmentMedicationDTO()
            {
                Name = medication.Name,
                Id = medication.Id,
            };

            return medicationDTO;
        }

        public static PhysicalPersonDTO ConvertModelToDTO(Models.PhysicalPerson physicalPersonModel)
        {
            var physicalPersonModelDTO = new PhysicalPersonDTO()
            {
                Id = physicalPersonModel.Id,
                Name = physicalPersonModel.Name,
                Address = physicalPersonModel.Address,
                Email = physicalPersonModel.Email,
                Phone = physicalPersonModel.Phone,
                AnimalCount = physicalPersonModel.GetAnimalCount(),
                CatCount = physicalPersonModel.GetCatCount(),
                DogCount = physicalPersonModel.GetDogCount(),
                CountryName = physicalPersonModel.Country.Name,
                LocationName = physicalPersonModel.Location.Name,
                FkLocality = physicalPersonModel.Location.Id,
                FkCountry = physicalPersonModel.Country.Id
            };

            return physicalPersonModelDTO;
        }

        public static LegalPersonDTO ConvertModelToDTO(Models.LegalPerson legalPersonModel)
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
                FkCountry = legalPersonModel.Country.Id,
                FkLocality = legalPersonModel.Location.Id,
                AnimalCount = legalPersonModel.GetAnimalCount(),
                CatCount = legalPersonModel.GetCatCount(),
                DogCount = legalPersonModel.GetDogCount(),
                CountryName = legalPersonModel.Country.Name,
                LocationName = legalPersonModel.Location.Name
            };

            return legalPersonModelDTO;
        }

        public static ParasiteTreatmentDTO ConvertModelToDTO(ParasiteTreatment parasiteTreatment)
        {
            var parasiteTreatmentDTO = new ParasiteTreatmentDTO()
            {
                FkAnimal = parasiteTreatment.AnimalCard.Id,
                FkUser = parasiteTreatment.User.Id,
                FkMedication = parasiteTreatment.Medication.Id,
                Date = parasiteTreatment.Date,
                MedicationName = parasiteTreatment.Medication.Name,
                UserName = parasiteTreatment.User.Name
            };

            return parasiteTreatmentDTO;
        }

        public static VaccinationDTO ConvertModelToDTO(Vaccination vaccination)
        {
            var vaccinationDTO = new VaccinationDTO()
            {
                FkAnimal = vaccination.AnimalCard.Id,
                FkUser = vaccination.User.Id,
                FkVaccine = vaccination.Vaccine.Id,
                DateEnd = vaccination.DateEnd,
                VaccineName = vaccination.Vaccine.Name,
                UserName = vaccination.User.Name
            };

            return vaccinationDTO;
        }

        public static VeterinaryAppointmentDTO ConvertModelToDTO(VeterinaryAppointment veterinaryAppointment)
        {
            var veterinaryAppointmentDTO = new VeterinaryAppointmentDTO()
            {
                FkAnimal = veterinaryAppointment.AnimalCard.Id,
                FkUser = veterinaryAppointment.User.Id,
                Name = veterinaryAppointment.Name,
                Date = veterinaryAppointment.Date.ToLocalTime(),
                IsCompleted = veterinaryAppointment.IsCompleted,
                UserName = veterinaryAppointment.User.Name,
            };

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

        public static AnimalCardDTO ConvertModelToDTO(AnimalCard animalCard)
        {
            var AnimalCardDTO = new AnimalCardDTO()
            {
                Id = animalCard.Id,
                ChipId = animalCard.ChipId,
                Name = animalCard.Name,
                IsBoy = animalCard.IsBoy,
                FkCategory = animalCard.AnimalCategory.Id,
                FkShelter = animalCard.Shelter.Id,
                YearOfBirth = animalCard.YearOfBirth,
                Photo = animalCard.Photo,
                CategoryName = animalCard.AnimalCategory.Name
            };

            return AnimalCardDTO;
        }

        public static UserDTO ConvertModelToDTO(User user)
        {
            var userDTO = new UserDTO()
            {
                Login = user.Login,
                Id = user.Id,
                RoleId = user.FkRole,
                Name = user.Name,
                Email = user.Email
            };

            if (user.Location != null) 
            {
                userDTO.LocationId = user.Location.Id;
            }

            if (user.Shelter != null)
            {
                userDTO.ShelterId = user.Shelter.Id;
                userDTO.ShelterLocationId = user.Shelter.Location.Id;
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
            var contractDTO = new ContractDTO()
            {
                Number = contract.Number,
                Date = contract.Date,
                FkAnimalCard = contract.AnimalCard.Id,
                FkUser = contract.User.Id,
                FkPhysicalPerson = contract.PhysicalPerson.Id
            };

            if (contract.LegalPerson != null)
            {
                contractDTO.FkLegalPerson = contract.LegalPerson.Id;
            }

            return contractDTO;
        }
    }
}
