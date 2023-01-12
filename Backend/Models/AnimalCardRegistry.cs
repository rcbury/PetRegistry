using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using Spire.Doc.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class AnimalCardRegistry
    {
        public AnimalCardRegistry(AuthorizationController authorizationController)
        {
            AuthorizationController = authorizationController;

            Medications = new Medications();
            Vaccines = new Vaccines();
            AnimalCategories = new AnimalCategories();
            Countries = new Countries();
            Locations = new Locations();
            Shelters = new Shelters(Locations);
            Users = new Users(Locations, Shelters);

            var legalPeopleDB = PetOwnersService.GetLegalPeople();
            var physicalPeopleDB = PetOwnersService.GetPhysicalPeople();

            var animalCardsDB = AnimalCardService.GetAnimals();

            foreach (var animalCardDB in animalCardsDB)
            {
                var animalCard = new AnimalCard(animalCardDB);
                
                var vaccinations = new Vaccinations(animalCard, Users);
                var parasiteTreatments = new ParasiteTreatments(animalCard, Users);
                var veterinaryAppointments = new VeterinaryAppointments(animalCard, Users);

                animalCard.Vaccinations = vaccinations;
                animalCard.VeterinaryAppointments = veterinaryAppointments;
                animalCard.ParasiteTreatments = parasiteTreatments;

                AnimalCards.Add(animalCard);
            }
            foreach (var legalPerson in legalPeopleDB)
            {
                LegalPeople.Add(new  LegalPerson()
                {
                    Id = legalPerson.Id,
                    Name = legalPerson.Name,
                    Location = Locations.GetLocation(legalPerson.FkLocality),
                    Country = Countries.GetCountry(legalPerson.FkCountry),
                    Address = legalPerson.Address,
                    Email = legalPerson.Email,
                    Inn = legalPerson.Inn,
                    Kpp = legalPerson.Kpp,
                    Phone = legalPerson.Phone,
                });
            }
            foreach (var physicalPerson in physicalPeopleDB)
            {
                PhysicalPeople.Add(new PhysicalPerson()
                {
                    Id = physicalPerson.Id,
                    Name = physicalPerson.Name,
                    Location = Locations.GetLocation(physicalPerson.FkLocality),
                    Country = Countries.GetCountry(physicalPerson.FkCountry),
                    Address = physicalPerson.Address,
                    Email = physicalPerson.Email,
                    Phone = physicalPerson.Phone
                });
            }
            Contracts = new Contracts(AnimalCards, LegalPeople, PhysicalPeople, Users);
            foreach (var physicalPerson in PhysicalPeople)
            {
                physicalPerson.Contracts = new Contracts(Contracts.ContractList
                    .Where(contract => contract.LegalPerson == null)
                    .Where(contract => contract.PhysicalPerson.Id == physicalPerson.Id)
                    .ToList());
            }
            foreach (var legalPerson in LegalPeople) 
            {
                legalPerson.Contracts = new Contracts(Contracts.ContractList
                    .Where(contract => contract.LegalPerson != null)
                    .Where(contract => contract.LegalPerson.Id == legalPerson.Id)
                    
                    .ToList());
            }

        }

        private AuthorizationController AuthorizationController { get; set; }
        private AnimalCategories AnimalCategories { get; set; }
        private Vaccines Vaccines { get; set; }
        private Medications Medications { get; set; }
        private List<AnimalCard> AnimalCards { get; set; } = new List<AnimalCard>();
        private List<LegalPerson> LegalPeople { get; set; } = new List<LegalPerson>();
        private List<PhysicalPerson> PhysicalPeople { get; set; } = new List<PhysicalPerson>();
        private Countries Countries { get; set; }
        private Locations Locations { get; set; }
        private Shelters Shelters { get; set; }
        private Contracts Contracts { get; set; }
        
        private Users Users { get; set; }


        public List<CountryDTO> GetCountries()
        {
            return Countries.CountryList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<VaccineDTO> GetVaccines()
        {
            return Vaccines.VaccineList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<ParasiteTreatmentMedicationDTO> GetMedications()
        {
            return Medications.MedicationList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<LocationDTO> GetLocations()
        {
            return Locations.LocationsList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<AnimalCategoryDTO> GetAnimalCategories()
        {
            return AnimalCategories.AnimalCategoryList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email,
            string address, string phone, int country, int location)
        {
            var legalPeople = LegalPeople;

            if (inn != null && inn != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(inn)).ToList();
            }
            if (kpp != null && kpp != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(kpp)).ToList();
            }
            if (name != null && name != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(name)).ToList();
            }
            if (email != null && email != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(email)).ToList();
            }
            if (address != null && address != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(address)).ToList();
            }
            if (phone != null && phone != "")
            {
                legalPeople = legalPeople.Where(person => person.Phone.Contains(phone)).ToList();
            }
            if (country != 0)
            {
                legalPeople = legalPeople.Where(person => person.Country.Id == country).ToList();
            }
            if (location != 0)
            {
                legalPeople = legalPeople.Where(person => person.Location.Id == location).ToList();
            }

            var res = new List<LegalPersonDTO>();

            foreach (var legalPerson in legalPeople)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(legalPerson));
            }

            return res;
        }

        public LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = LegalPeople.Where(x => x.Inn == INN).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }


        public LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = LegalPeople.Where(x => x.Id == personId).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public List<PhysicalPersonDTO> GetPhysicalPeople(string phone, string name, string address, 
            string email, int country, int location)
        {
            var physicalPeople = PhysicalPeople;

            if (phone != null && phone != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Phone.Contains(phone)).ToList();
            }
            if (name != null && name != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Phone.Contains(name)).ToList();
            }
            if (address != null && address != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Phone.Contains(name)).ToList();
            }
            if (email != null && email != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Phone.Contains(email)).ToList();
            }
            if (country != 0)
            {
                physicalPeople = physicalPeople.Where(person => person.Country.Id == country).ToList();
            }
            if (location != 0)
            {
                physicalPeople = physicalPeople.Where(person => person.Location.Id == location).ToList();
            }

            var res = new List<PhysicalPersonDTO>();

            foreach (var physicalPerson in physicalPeople)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(physicalPerson));
            }

            return res;
        }

        public PhysicalPersonDTO? GetPhysicalPersonByPhone(string phone)
        {
            var person = PhysicalPeople.Where(x => x.Phone == phone).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }


        public PhysicalPersonDTO? GetPhysicalPersonById(int? personId)
        {
            var person = PhysicalPeople.Where(x => x.Id == personId).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public List<AnimalCategoryDTO> GetAnimalCardCategories()
        {
            return AnimalCategories.GetAnimalCategories();
        }

        public List<AnimalCardDTO> GetAnimals()
        {
            var animalsListDto = AnimalCards.Select(item => DTOModelConverter.ConvertModelToDTO(item)).ToList();

            return animalsListDto;
        }

        public List<AnimalCardDTO> GetAnimals(AnimalFilterDTO animalFilter)
        {
            var animalCardsList = new List<AnimalCard> { };

            var animalCards = AnimalCards;


            //TODO:
            //if (animalFilter.PhysicalPerson != null)
            //{
            //    animalCards = context.Contracts.Where(contract =>
            //    contract.FkPhysicalPerson == animalFilter.PhysicalPerson.Id
            //    && contract.FkLegalPerson == null)
            //        .Select(x => x.FkAnimalCardNavigation)
            //        .ToList();
            //}

            //if (animalFilter.LegalPerson != null)
            //{
            //    animalCards = context.Contracts.Where(contract =>
            //    contract.FkLegalPerson == animalFilter.LegalPerson.Id)
            //        .Select(x => x.FkAnimalCardNavigation)
            //        .ToList();
            //}

            if (animalFilter.ChipId.Length > 0)
            {
                animalCards = animalCards
                    .Where(item => item.ChipId == animalFilter.ChipId)
                    .ToList();
            }
            else
            {
                if (animalFilter.Name.Length > 0)
                {
                    animalCards = animalCards
                        .Where(item => item.Name == animalFilter.Name)
                        .ToList();
                }

                if (animalFilter.IsSelectedSex)
                {
                    animalCards = animalCards
                        .Where(item => item.IsBoy == animalFilter.IsBoy)
                        .ToList();
                }

                if (animalFilter.AnimalCategory.Id != -1)
                {
                    animalCards = animalCards
                        .Where(item => item.AnimalCategory.Id == animalFilter.AnimalCategory.Id)
                        .ToList();
                }

                if (animalFilter.SearchTimeVetProcedure != null)
                {
                    animalCards = animalCards
                        .Where(item => item.VeterinaryAppointments.VeterinaryAppointmentList
                            .Where(x => x.Date <= animalFilter.SearchTimeVetProcedure)
                            .Where(x => !x.IsCompleted)
                            .Count() > 0)
                        .ToList();
                }

                animalCardsList = animalCards;
            }

            var animalsListDto = AnimalCards.Select(item => new AnimalCardDTO(item)).ToList();

            return animalsListDto;
        }

        public AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var user = AuthorizationController.User;

            var animalCardDB = new PIS_PetRegistry.Models.AnimalCard()
            {
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = user.Shelter.Id,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo,
            };

            animalCardDB = AnimalCardService.AddAnimalCard(animalCardDB);

            var animalCard = new AnimalCard(
                animalCardDB.Id,
                animalCardDB.ChipId,
                animalCardDB.Name,
                AnimalCategories.AnimalCategoryList.Where(x => x.Id == animalCardDB.FkCategory).FirstOrDefault(),
                user.Shelter,
                animalCardDB.YearOfBirth,
                animalCardDB.IsBoy,
                animalCardDB.Photo);

            var vaccinations = new Vaccinations(animalCard, Users);
            var parasiteTreatments = new ParasiteTreatments(animalCard, Users);
            var veterinaryAppointments = new VeterinaryAppointments(animalCard, Users);

            animalCard.Vaccinations = vaccinations;
            animalCard.VeterinaryAppointments = veterinaryAppointments;
            animalCard.ParasiteTreatments = parasiteTreatments;

            AnimalCards.Add(animalCard);

            return new AnimalCardDTO(animalCard);
        }
        
        public List<ParasiteTreatmentDTO> GetParasiteTreatmentsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.Where(x => x.Id == animalId).FirstOrDefault();

            return animalCard.ParasiteTreatments.ParasiteTreatmentList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<VaccinationDTO> GetVaccinationsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.Where(x => x.Id == animalId).FirstOrDefault();

            return animalCard.Vaccinations.VaccinationList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<VeterinaryAppointmentDTO> GetVeterinaryAppointmentsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.Where(x => x.Id == animalId).FirstOrDefault();

            return animalCard.VeterinaryAppointments.VeterinaryAppointmentList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public ParasiteTreatmentDTO AddParasiteTreatment(ParasiteTreatmentDTO parasiteTreatmentDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == parasiteTreatmentDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;

            var parasiteTreatment = animalCard.ParasiteTreatments.AddParasiteTreatment(
                parasiteTreatmentDTO.Date,
                animalCard,
                user,
                Medications.MedicationList.Where(x => x.Id == parasiteTreatmentDTO.FkMedication).FirstOrDefault());

            var newParasiteTreatmentDTO = DTOModelConverter.ConvertModelToDTO(parasiteTreatment);

            return newParasiteTreatmentDTO;
        }

        
        public ParasiteTreatmentDTO UpdateParasiteTreatment(
            ParasiteTreatmentDTO oldParasiteTreatmentDTO, 
            ParasiteTreatmentDTO modifiedParasiteTreatmentDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == oldParasiteTreatmentDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;

            var oldVeterinaryAppointment = animalCard.ParasiteTreatments.ParasiteTreatmentList
                .Where(x => oldParasiteTreatmentDTO.FkAnimal == x.AnimalCard.Id)
                .Where(x => oldParasiteTreatmentDTO.FkUser == x.User.Id)
                .Where(x => oldParasiteTreatmentDTO.Date == x.Date)
                .Where(x => oldParasiteTreatmentDTO.FkMedication == x.Medication.Id)
                .FirstOrDefault();

            var updatedVaccination = animalCard.ParasiteTreatments.UpdateParasiteTreatment(
                oldVeterinaryAppointment,
                modifiedParasiteTreatmentDTO.Date,
                animalCard,
                user,
                Medications.MedicationList.Where(x => x.Id == modifiedParasiteTreatmentDTO.FkMedication).FirstOrDefault());

            var newVaccinationDTO = DTOModelConverter.ConvertModelToDTO(updatedVaccination);

            return newVaccinationDTO;
        }

        public VaccinationDTO AddVaccination(VaccinationDTO vaccinationDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == vaccinationDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;

            var vaccination = animalCard.Vaccinations.AddVaccination(
                vaccinationDTO.DateEnd,
                animalCard,
                user,
                Vaccines.VaccineList.Where(x => x.Id == vaccinationDTO.FkVaccine).FirstOrDefault());

            var newVaccinationDTO = DTOModelConverter.ConvertModelToDTO(vaccination);

            return newVaccinationDTO;
        }

        public VaccinationDTO UpdateVaccination(
            VaccinationDTO oldVaccinationDTO, 
            VaccinationDTO modifiedVaccinationDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == oldVaccinationDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;

            var oldVeterinaryAppointment = animalCard.Vaccinations.VaccinationList
                .Where(x => oldVaccinationDTO.FkAnimal == x.AnimalCard.Id)
                .Where(x => oldVaccinationDTO.FkUser == x.User.Id)
                .Where(x => oldVaccinationDTO.DateEnd == x.DateEnd)
                .Where(x => oldVaccinationDTO.FkVaccine == x.Vaccine.Id)
                .FirstOrDefault();

            var updatedVaccination = animalCard.Vaccinations.UpdateVaccination(
                oldVeterinaryAppointment,
                modifiedVaccinationDTO.DateEnd,
                animalCard,
                user,
                Vaccines.VaccineList.Where(x => x.Id == modifiedVaccinationDTO.FkVaccine).FirstOrDefault());

            var newVaccinationDTO = DTOModelConverter.ConvertModelToDTO(updatedVaccination);

            return newVaccinationDTO;
        }

        public VeterinaryAppointmentDTO AddVeterinaryAppointment(VeterinaryAppointmentDTO vaccinationDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == vaccinationDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;

            var veterinaryAppointment = animalCard.VeterinaryAppointments.AddVeterinaryAppointment(
                vaccinationDTO.Date,
                animalCard,
                user,
                vaccinationDTO.Name,
                vaccinationDTO.IsCompleted);

            var newVeterinaryAppointmentDTO = DTOModelConverter.ConvertModelToDTO(veterinaryAppointment);

            return newVeterinaryAppointmentDTO;
        }

        public VeterinaryAppointmentDTO UpdateVeterinaryAppointment(
            VeterinaryAppointmentDTO oldVaccinationDTO, 
            VeterinaryAppointmentDTO modifiedVeterinaryAppointmentDTO)
        {
            var animalCard = AnimalCards.Where(x => x.Id == oldVaccinationDTO.FkAnimal).FirstOrDefault();
            var user = AuthorizationController.User;


            var oldVeterinaryAppointment = animalCard.VeterinaryAppointments.VeterinaryAppointmentList
                .Where(x => oldVaccinationDTO.FkAnimal == x.AnimalCard.Id)
                .Where(x => oldVaccinationDTO.FkUser == x.User.Id)
                .Where(x => oldVaccinationDTO.Date == x.Date)
                .FirstOrDefault();

            var updatedVeterinaryAppointment = animalCard.VeterinaryAppointments.UpdateVeterinaryAppointment(
                oldVeterinaryAppointment,
                modifiedVeterinaryAppointmentDTO.Date,
                animalCard,
                user,
                modifiedVeterinaryAppointmentDTO.Name,
                modifiedVeterinaryAppointmentDTO.IsCompleted);

            var newVaccinationDTO = DTOModelConverter.ConvertModelToDTO(updatedVeterinaryAppointment);

            return newVaccinationDTO;
        }

        public void DeleteAnimalCard(int animalId)
        {
            var user = AuthorizationController.User;
            
            AnimalCardService.DeleteAnimalCard(animalId);
            
            AnimalCards.RemoveAll(x => x.Id == animalId);
        }

        public AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var user = AuthorizationController.User;

            var animalCardDB = new PIS_PetRegistry.Models.AnimalCard()
            {
                Id = animalCardDTO.Id,
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = animalCardDTO.FkShelter,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo,
            };

            animalCardDB = AnimalCardService.UpdateAnimalCard(animalCardDB);

            var modifiedAnimalCard = AnimalCards.Where(x => x.Id == animalCardDTO.Id).FirstOrDefault();

            modifiedAnimalCard.ChipId = animalCardDTO.ChipId;
            modifiedAnimalCard.Name = animalCardDTO.Name;
            modifiedAnimalCard.AnimalCategory = AnimalCategories.AnimalCategoryList.Where(x => x.Id == animalCardDB.FkCategory).FirstOrDefault();
            modifiedAnimalCard.YearOfBirth = animalCardDTO.YearOfBirth;
            modifiedAnimalCard.IsBoy = animalCardDTO.IsBoy;
            modifiedAnimalCard.Photo = animalCardDTO.Photo;

            return new AnimalCardDTO(modifiedAnimalCard);
        }

        public void UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            /*var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);*/

            var legalPersonDB = new PIS_PetRegistry.Models.LegalPerson()
            {
                Id = legalPersonDTO.Id,
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality= legalPersonDTO.FkLocality,
            };

            legalPersonDB = PetOwnersService.UpdateLegalPerson(legalPersonDB);

            var modifiedLegalPerson = LegalPeople.Where(x => x.Id == legalPersonDTO.Id).FirstOrDefault();

            modifiedLegalPerson.Inn = legalPersonDTO.INN;
            modifiedLegalPerson.Kpp = legalPersonDTO.KPP;
            modifiedLegalPerson.Name = legalPersonDTO.Name;
            modifiedLegalPerson.Address = legalPersonDTO.Address;
            modifiedLegalPerson.Email = legalPersonDTO.Email;
            modifiedLegalPerson.Phone = legalPersonDTO.Phone;
            modifiedLegalPerson.Country = Countries.GetCountry(legalPersonDTO.FkCountry);
            modifiedLegalPerson.Location = Locations.GetLocation(legalPersonDTO.FkLocality);
        }

        public void UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            /*var legalPersonModel = DTOModelConverter.ConvertDTOToModel(physicalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);*/

            var physicalPersonDB = new PIS_PetRegistry.Models.PhysicalPerson()
            {
                Id = physicalPersonDTO.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkLocality = physicalPersonDTO.FkLocality,
                FkCountry = physicalPersonDTO.FkCountry,
            };

            physicalPersonDB = PetOwnersService.UpdatePhysicalPerson(physicalPersonDB);

            var modifiedPhysicalPerson = PhysicalPeople.Where(x => x.Id == physicalPersonDTO.Id).FirstOrDefault();

            modifiedPhysicalPerson.Name = physicalPersonDTO.Name;
            modifiedPhysicalPerson.Address = physicalPersonDTO.Address;
            modifiedPhysicalPerson.Email = physicalPersonDTO.Email;
            modifiedPhysicalPerson.Phone = physicalPersonDTO.Phone;
            modifiedPhysicalPerson.Country = Countries.GetCountry(physicalPersonDTO.FkCountry);
            modifiedPhysicalPerson.Location = Locations.GetLocation(physicalPersonDTO.FkLocality);
        }

        public void AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonDB = new PIS_PetRegistry.Models.LegalPerson()
            {
                Id = legalPersonDTO.Id,
                Inn = legalPersonDTO.INN,
                Kpp = legalPersonDTO.KPP,
                Name = legalPersonDTO.Name,
                Address = legalPersonDTO.Address,
                Email = legalPersonDTO.Email,
                Phone = legalPersonDTO.Phone,
                FkCountry = legalPersonDTO.FkCountry,
                FkLocality = legalPersonDTO.FkLocality,
            };

            /*legalPersonDB = */
                PetOwnersService.AddLegalPerson(legalPersonDB);
        }

        public void AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonDB = new PIS_PetRegistry.Models.PhysicalPerson()
            {
                Id = physicalPersonDTO.Id,
                Name = physicalPersonDTO.Name,
                Address = physicalPersonDTO.Address,
                Email = physicalPersonDTO.Email,
                Phone = physicalPersonDTO.Phone,
                FkLocality = physicalPersonDTO.FkLocality,
                FkCountry = physicalPersonDTO.FkCountry,
            };

            PetOwnersService.AddPhysicalPerson(physicalPersonDB);

            var phys = new PhysicalPerson();
            phys.Id = physicalPersonDTO.Id;
            phys.Name = physicalPersonDTO.Name;
            phys.Address = physicalPersonDTO.Address;
            phys.Email = physicalPersonDTO.Email;
            phys.Location = Locations.LocationsList.Where(x => x.Id == physicalPersonDTO.FkLocality).FirstOrDefault();
            phys.Country = Countries.CountryList.Where(x => x.Id == physicalPersonDTO.FkCountry).FirstOrDefault();
            phys.Contracts = new Contracts(Contracts.ContractList
                    .Where(contract => contract.LegalPerson == null)
                    .Where(contract => contract.PhysicalPerson.Id == phys.Id)
                    .ToList());

            PhysicalPeople.Add(phys);
        }
        public List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsListDTO = Contracts.ContractList
                .Where(x => x.LegalPerson.Id == legalPersonId)
                .Select(x => x.AnimalCard)
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();

            return animalsListDTO;
        }

        public List<AnimalCardDTO> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsListDTO = Contracts.ContractList
                .Where(x => x.PhysicalPerson.Id == physicalPersonId)
                .Select(x => x.AnimalCard)
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();

            return animalsListDTO;
        }

        public ContractDTO GetContractByAnimal(int animalCardId) 
        {
            var contract = Contracts.ContractList.Where(contract => contract.AnimalCard.Id == animalCardId).FirstOrDefault();
            return DTOModelConverter.ConvertModelToDTO(contract);
        }

        public int CountAnimalsByLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var countAnimals = Contracts.ContractList
                .Where(x => x.LegalPerson.Id == legalPersonDTO.Id)
                .Select(x => x.AnimalCard)
                .Count();
            return countAnimals;
        }

        public int CountAnimalsByPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var countAnimals = Contracts.ContractList
                .Where(x => x.PhysicalPerson.Id == physicalPersonDTO.Id)
                .Select(x => x.AnimalCard)
                .Count();
            return countAnimals;
        }

        public void ExportPhysicalPeopleToExcel(string path, List<PhysicalPersonDTO> physicalPeopleDTO)
        {
            Exporter.ExportPhysicalPeopleToExcel(path, physicalPeopleDTO);
        }

        public void ExportLegalPeopleToExcel(string path, List<LegalPersonDTO> legalPeopleDTO)
        {
            Exporter.ExportLegalPeopleToExcel(path, legalPeopleDTO);
        }

        public void ExportCardsToExcel(string path, List<AnimalCardDTO> cardsDTO) 
        {
            Exporter.ExportCardsToExcel(path, cardsDTO);
        }

        public int CountAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var physicalPerson = PhysicalPeople.Where(person => person.Id == physicalPersonId).FirstOrDefault();

            return physicalPerson.GetAnimalCount();
        }

        public int CountAnimalsByLegalPerson(int legalPersonId)
        {
            var legalPerson = LegalPeople.Where(person => person.Id == legalPersonId).FirstOrDefault();

            return legalPerson.GetAnimalCount();
        }

        public void MakeContract(string filePath, PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO legalPersonDTO, AnimalCardDTO animalCardDTO)
        {
            var physicalPerson = PhysicalPeople.Where(item => item.Id == physicalPersonDTO.Id).FirstOrDefault();
            var legalPerson = LegalPeople.Where(item => item.Id == legalPersonDTO.Id).FirstOrDefault();
            var card = AnimalCards.Where(item => item.Id == animalCardDTO.Id).FirstOrDefault();
            var user = AuthorizationController.User;

            Exporter.MakeContract(filePath, physicalPerson, legalPerson, card, user);
        }

        public void SaveContract(PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO legalPersonDTO, AnimalCardDTO animalCardDTO) 
        {
            var physicalPerson = PhysicalPeople.Where(item => item.Id == physicalPersonDTO.Id).FirstOrDefault();
            var legalPerson = LegalPeople.Where(item => item.Id == legalPersonDTO.Id).FirstOrDefault();
            var card = AnimalCards.Where(item => item.Id == animalCardDTO.Id).FirstOrDefault();
            var user = AuthorizationController.User;

            Contracts.SaveContract(physicalPerson, legalPerson, card, user);
        }
    }
}
