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
    public class Registry
    {
        public Registry(AuthorizationController authorizationController)
        {
            AuthorizationController = authorizationController;

            Medications = new Medications();
            Vaccines = new Vaccines();
            AnimalCategories = new AnimalCategories();
            Countries = new Countries();
            Locations = new Locations();
            Shelters = new Shelters(Locations);
            Users = new Users(Locations, Shelters);
            PhysicalPeople = new PhysicalPeople(Locations, Countries);
            LegalPeople = new LegalPeople(Locations, Countries);
            AnimalCards = new AnimalCards(Users, Vaccines, Medications);
            Contracts = new Contracts(AnimalCards, LegalPeople, PhysicalPeople, Users);
            PhysicalPeople.FillContracts(Contracts);
            LegalPeople.FillContracts(Contracts);

        }

        private AuthorizationController AuthorizationController { get; set; }
        private AnimalCategories AnimalCategories { get; set; }
        private Vaccines Vaccines { get; set; }
        private Medications Medications { get; set; }
        private AnimalCards AnimalCards { get; set; }
        private LegalPeople LegalPeople { get; set; }
        private PhysicalPeople PhysicalPeople { get; set; }
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
            var legalPeople = LegalPeople.GetLegalPeople(inn, kpp, name, email, address, phone, country, location);

            var res = new List<LegalPersonDTO>();

            foreach (var legalPerson in legalPeople)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(legalPerson));
            }

            return res;
        }

        public LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = LegalPeople.GetLegalPersonByInn(INN);

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }


        public LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = LegalPeople.GetLegalPersonById(personId);

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public List<PhysicalPersonDTO> GetPhysicalPeople(string phone, string name, string address, 
            string email, int country, int location)
        {
            var physicalPeople = PhysicalPeople.PhysicalPeopleList;

            if (phone != null && phone != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Phone.Contains(phone)).ToList();
            }
            if (name != null && name != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Name.Contains(name)).ToList();
            }
            if (address != null && address != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Address.Contains(name)).ToList();
            }
            if (email != null && email != "")
            {
                physicalPeople = physicalPeople.Where(person => person.Email.Contains(email)).ToList();
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
            var person = PhysicalPeople.PhysicalPeopleList.Where(x => x.Phone == phone).FirstOrDefault();

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }


        public PhysicalPersonDTO? GetPhysicalPersonById(int? personId)
        {
            var person = PhysicalPeople.PhysicalPeopleList.Where(x => x.Id == personId).FirstOrDefault();

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
            var animalsListDto = AnimalCards.AnimalCardList.Select(item => DTOModelConverter.ConvertModelToDTO(item)).ToList();

            return animalsListDto;
        }

        public List<AnimalCardDTO> GetAnimals(AnimalFilterDTO animalFilter)
        {
            var animalCardsList = new List<AnimalCard> { };

            var animalCards = AnimalCards.AnimalCardList;


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

            var animalsListDto = animalCardsList.Select(item => new AnimalCardDTO(item)).ToList();

            return animalsListDto;
        }

        public AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var user = AuthorizationController.User;
            var animalCategory = AnimalCategories.GetAnimalCategoryById(animalCardDTO.FkCategory);


            var newAnimalCardDTO = AnimalCards.AddAnimalCard(animalCardDTO, user, animalCategory);

            return newAnimalCardDTO;
        }
        
        public List<ParasiteTreatmentDTO> GetParasiteTreatmentsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.GetAnimalCardById(animalId);

            return animalCard.ParasiteTreatments.ParasiteTreatmentList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<VaccinationDTO> GetVaccinationsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.GetAnimalCardById(animalId);

            return animalCard.Vaccinations.VaccinationList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public List<VeterinaryAppointmentDTO> GetVeterinaryAppointmentsByAnimal(int animalId)
        {
            var animalCard = AnimalCards.GetAnimalCardById(animalId);

            return animalCard.VeterinaryAppointments.VeterinaryAppointmentList
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();
        }

        public ParasiteTreatmentDTO AddParasiteTreatment(ParasiteTreatmentDTO parasiteTreatmentDTO)
        {
            var animalCard = AnimalCards.GetAnimalCardById(parasiteTreatmentDTO.FkAnimal);
            
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
            var animalCard = AnimalCards.GetAnimalCardById(oldParasiteTreatmentDTO.FkAnimal);

            var user = AuthorizationController.User;

            var oldParasiteTreatment = animalCard.ParasiteTreatments.GetParasiteTreatmentById(
                animalCard.Id, 
                user.Id, 
                oldParasiteTreatmentDTO.Date, 
                oldParasiteTreatmentDTO.FkMedication);

            var updatedVaccination = animalCard.ParasiteTreatments.UpdateParasiteTreatment(
                oldParasiteTreatment,
                modifiedParasiteTreatmentDTO.Date,
                animalCard,
                user,
                Medications.GetMedicationById(modifiedParasiteTreatmentDTO.FkMedication));

            var newVaccinationDTO = DTOModelConverter.ConvertModelToDTO(updatedVaccination);

            return newVaccinationDTO;
        }

        public VaccinationDTO AddVaccination(VaccinationDTO vaccinationDTO)
        {
            var animalCard = AnimalCards.GetAnimalCardById(vaccinationDTO.FkAnimal);

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
            var animalCard = AnimalCards.GetAnimalCardById(oldVaccinationDTO.FkAnimal);

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
            var animalCard = AnimalCards.GetAnimalCardById(vaccinationDTO.FkAnimal);

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
            var animalCard = AnimalCards.GetAnimalCardById(oldVaccinationDTO.FkAnimal);

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
            AnimalCards.DeleteAnimalCard(animalId); 
        }

        public AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var animalCategory = AnimalCategories.GetAnimalCategoryById(animalCardDTO.FkCategory);

            var modifiedAnimalCardDTO = AnimalCards.UpdateAnimalCard(animalCardDTO, animalCategory);

            return modifiedAnimalCardDTO;
        }

        public void UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            /*var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);*/

            LegalPeople.UpdateLegalPerson(legalPersonDTO);
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

            var modifiedPhysicalPerson = PhysicalPeople.PhysicalPeopleList.Where(x => x.Id == physicalPersonDTO.Id).FirstOrDefault();

            modifiedPhysicalPerson.Name = physicalPersonDTO.Name;
            modifiedPhysicalPerson.Address = physicalPersonDTO.Address;
            modifiedPhysicalPerson.Email = physicalPersonDTO.Email;
            modifiedPhysicalPerson.Phone = physicalPersonDTO.Phone;
            modifiedPhysicalPerson.Country = Countries.GetCountry(physicalPersonDTO.FkCountry);
            modifiedPhysicalPerson.Location = Locations.GetLocation(physicalPersonDTO.FkLocality);
        }

        public void AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var location = Locations.GetLocation(legalPersonDTO.FkLocality);
            var country = Countries.GetCountry(legalPersonDTO.FkCountry);
            var contracts = Contracts.GetContractsByLegalPerson(legalPersonDTO.Id);
            var legalPerson = LegalPeople.AddLegalPerson(legalPersonDTO, location, country);
            legalPerson.FillContracts(contracts);
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
            phys.Id = physicalPersonDB.Id;
            phys.Phone = physicalPersonDTO.Phone;
            phys.Name = physicalPersonDTO.Name;
            phys.Address = physicalPersonDTO.Address;
            phys.Email = physicalPersonDTO.Email;
            phys.Location = Locations.LocationsList.Where(x => x.Id == physicalPersonDTO.FkLocality).FirstOrDefault();
            phys.Country = Countries.CountryList.Where(x => x.Id == physicalPersonDTO.FkCountry).FirstOrDefault();
            phys.Contracts = new Contracts(Contracts.ContractList
                    .Where(contract => contract.LegalPerson == null)
                    .Where(contract => contract.PhysicalPerson.Id == phys.Id)
                    .ToList());

            PhysicalPeople.PhysicalPeopleList.Add(phys);
        }
        public List<AnimalCardDTO> GetAnimalsByLegalPerson(int legalPersonId)
        {
            var animalsListDTO = 
                LegalPeople.GetLegalPersonById(legalPersonId)
                .GetAnimals()
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();

            return animalsListDTO;
        }

        public List<AnimalCardDTO> GetAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var animalsListDTO = Contracts.ContractList
                .Where(x => x.PhysicalPerson.Id == physicalPersonId)
                .Where(x => x.LegalPerson == null)
                .Select(x => x.AnimalCard)
                .Select(x => DTOModelConverter.ConvertModelToDTO(x))
                .ToList();

            return animalsListDTO;
        }

        public ContractDTO? GetContractByAnimal(int animalCardId) 
        {
            var contract = Contracts.ContractList.Where(contract => contract.AnimalCard.Id == animalCardId).FirstOrDefault();
            if (contract == null) 
            {
                return null;
            }
            return DTOModelConverter.ConvertModelToDTO(contract);
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
            var physicalPerson = PhysicalPeople.PhysicalPeopleList.Where(person => person.Id == physicalPersonId).FirstOrDefault();

            return physicalPerson.GetAnimalCount();
        }

        public int CountAnimalsByLegalPerson(int legalPersonId)
        {
            var legalPerson = LegalPeople.GetLegalPersonById(legalPersonId);

            return legalPerson.GetAnimalCount();
        }

        public void MakeContract(string filePath, PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO legalPersonDTO, AnimalCardDTO animalCardDTO)
        {
            var physicalPerson = PhysicalPeople.PhysicalPeopleList.Where(item => item.Id == physicalPersonDTO.Id).FirstOrDefault();
            var legalPerson = LegalPeople.LegalPeopleList.Where(item => item.Id == legalPersonDTO.Id).FirstOrDefault();
            var card = AnimalCards.GetAnimalCardById(animalCardDTO.Id);
            var user = AuthorizationController.User;

            Exporter.MakeContract(filePath, physicalPerson, legalPerson, card, user);
        }

        public void SaveContract(PhysicalPersonDTO physicalPersonDTO, LegalPersonDTO legalPersonDTO, AnimalCardDTO animalCardDTO) 
        {
            var physicalPerson = PhysicalPeople.PhysicalPeopleList.Where(item => item.Id == physicalPersonDTO.Id).FirstOrDefault();
            var legalPerson = legalPersonDTO != null ? LegalPeople.LegalPeopleList.Where(item => item.Id == legalPersonDTO.Id).FirstOrDefault() 
                : null;
            var card = AnimalCards.GetAnimalCardById(animalCardDTO.Id);
            var user = AuthorizationController.User;

            var contract = Contracts.SaveContract(physicalPerson, legalPerson, card, user);

            if (legalPerson != null)
            {
                legalPerson.Contracts.AddContract(contract);
            }
            else if (physicalPerson != null) 
            {
                physicalPerson.Contracts.AddContract(contract);
            }
        }
    }
}
