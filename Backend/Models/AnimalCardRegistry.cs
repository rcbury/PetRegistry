﻿using DocumentFormat.OpenXml.InkML;
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

            var legalPeopleDB = PetOwnersService.GetLegalPeople();

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
                LegalPeople.Add(new Backend.Models.LegalPerson()
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
        }

        private AuthorizationController AuthorizationController { get; set; }

        private AnimalCategories AnimalCategories { get; set; }
        private Vaccines Vaccines { get; set; }
        private Medications Medications { get; set; }
        private List<AnimalCard> AnimalCards { get; set; }

        private List<LegalPerson> LegalPeople;
        private Countries Countries;
        private Locations Locations;
        private Shelters Shelters;


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

        public void UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);
        }

        public List<AnimalCategoryDTO> GetAnimalCategories()
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
    }
}
