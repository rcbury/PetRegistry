using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
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
            Contracts = new Contracts();

            var legalPeopleDB = PetOwnersService.GetLegalPeople();
            var physicalPeopleDB = PetOwnersService.GetPhysicalPeople();

            var animalCardsDB = AnimalCardService.GetAnimals();

            foreach (var animalCardDB in animalCardsDB)
            {
                var vaccinations = new Vaccinations(animalCardDB.Id);
                var parasiteTreatments = new ParasiteTreatments(animalCardDB.Id);
                var veterinaryAppointments = new VeterinaryAppointments(animalCardDB.Id);

                var animalCard = new AnimalCard(vaccinations, veterinaryAppointments, parasiteTreatments, animalCardDB);
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

        }

        private AuthorizationController AuthorizationController { get; set; }

        private AnimalCategories AnimalCategories { get; set; }
        private Vaccines Vaccines { get; set; }
        private Medications Medications { get; set; }
        private List<AnimalCard> AnimalCards { get; set; }

        private List<LegalPerson> LegalPeople;
        private List<PhysicalPerson> PhysicalPeople;
        private Countries Countries;
        private Locations Locations;
        private Shelters Shelters;
        private Contracts Contracts;


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
            var animalsListDto = AnimalCards.Select(item => new AnimalCardDTO(item)).ToList();

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

            var vaccinations = new Vaccinations(animalCardDB.Id);
            var parasiteTreatments = new ParasiteTreatments(animalCardDB.Id);
            var veterinaryAppointments = new VeterinaryAppointments(animalCardDB.Id);

            var animalCard = new AnimalCard(vaccinations, veterinaryAppointments, parasiteTreatments, animalCardDB);

            return new AnimalCardDTO(animalCard);
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

            var modifiedPhysicalPerson = LegalPeople.Where(x => x.Id == physicalPersonDTO.Id).FirstOrDefault();

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

            /*physicalPersonDB = */
                PetOwnersService.AddPhysicalPerson(physicalPersonDB);
        }
        public List<AnimalCard> GetAnimalsByLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var animalsListDTO = Contracts.ContractList
                .Where(x => x.LegalPerson.Id == legalPersonDTO.Id)
                .Select(x => x.AnimalCard)
                .ToList();
            return animalsListDTO;
        }

        public List<AnimalCard> GetAnimalsByPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var animalsListDTO = Contracts.ContractList
                .Where(x => x.PhysicalPerson.Id == physicalPersonDTO.Id)
                .Select(x => x.AnimalCard)
                .ToList();
            return animalsListDTO;
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
    }
}
