using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class AnimalCards
    {
        public AnimalCards(Users users, Vaccines vaccines, Medications medications)
        {
            var animalCardsDB = AnimalCardService.GetAnimals();

            foreach (var animalCardDB in animalCardsDB)
            {
                var animalCard = new AnimalCard(animalCardDB);

                var vaccinations = new Vaccinations(animalCard, users, vaccines);
                var parasiteTreatments = new ParasiteTreatments(animalCard, users, medications);
                var veterinaryAppointments = new VeterinaryAppointments(animalCard, users);

                animalCard.Vaccinations = vaccinations;
                animalCard.VeterinaryAppointments = veterinaryAppointments;
                animalCard.ParasiteTreatments = parasiteTreatments;

                AnimalCardList.Add(animalCard);
            }
        }

        public List<AnimalCard> AnimalCardList { get; set; } = new List<AnimalCard>();

        public void DeleteAnimalCard(int animalId)
        {
            AnimalCardService.DeleteAnimalCard(animalId);

            AnimalCardList.RemoveAll(x => x.Id == animalId);
        }

        public AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO, AnimalCategory animalCategory)
        {
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

            var modifiedAnimalCard = GetAnimalCardById(animalCardDB.Id);

            modifiedAnimalCard.ChipId = animalCardDTO.ChipId;
            modifiedAnimalCard.Name = animalCardDTO.Name;
            modifiedAnimalCard.AnimalCategory = animalCategory;
            modifiedAnimalCard.YearOfBirth = animalCardDTO.YearOfBirth;
            modifiedAnimalCard.IsBoy = animalCardDTO.IsBoy;
            modifiedAnimalCard.Photo = animalCardDTO.Photo;

            return new AnimalCardDTO(modifiedAnimalCard);
        }

        public AnimalCard? GetAnimalCardById(int animalId)
        {
            return AnimalCardList.Where(x => x.Id == animalId).FirstOrDefault();

        }

        public AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO, User user, AnimalCategory animalCategory)
        {

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
                animalCategory,
                user.Shelter,
                animalCardDB.YearOfBirth,
                animalCardDB.IsBoy,
                animalCardDB.Photo);

            var vaccinations = new Vaccinations();
            var parasiteTreatments = new ParasiteTreatments();
            var veterinaryAppointments = new VeterinaryAppointments();

            animalCard.Vaccinations = vaccinations;
            animalCard.VeterinaryAppointments = veterinaryAppointments;
            animalCard.ParasiteTreatments = parasiteTreatments;

            AnimalCardList.Add(animalCard);

            return new AnimalCardDTO(animalCard);
        }
    }
}
