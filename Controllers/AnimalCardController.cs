using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PISPetRegistry.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PIS_PetRegistry.Controllers
{
    internal class AnimalCardController
    {
        public static List<AnimalCategoryDTO> GetAnimalCategories()
        {
            var animalCategoriesDTO = new List<AnimalCategoryDTO>();
            using (var context = new RegistryPetsContext())
            {
                foreach (var animaCategory in context.AnimalCategories)
                {
                    animalCategoriesDTO.Add(
                        new AnimalCategoryDTO()
                        {
                            Id = animaCategory.Id,
                            Name = animaCategory.Name
                        });
                }
            }
            return animalCategoriesDTO;
        }

        public static AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {

            var animalCardModel = new AnimalCard()
            {
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
                FkCategory = animalCardDTO.FkCategory,
                FkShelter = (int)userDTO.ShelterId,
                YearOfBirth = animalCardDTO.YearOfBirth,
                IsBoy = animalCardDTO.IsBoy,
                Photo = animalCardDTO.Photo,
            };

            using (var context = new RegistryPetsContext())
            {
                context.AnimalCards.Add(animalCardModel);
                context.SaveChanges();

                AnimalCardLogController.LogCreate(animalCardModel, userDTO.Id);
            }

            var newAnimalCardDTO = new AnimalCardDTO()
            {
                Id = animalCardModel.Id,
                ChipId = animalCardModel.ChipId,
                Name = animalCardModel.Name,
                IsBoy = animalCardModel.IsBoy,
                FkCategory = animalCardModel.FkCategory,
                FkShelter = animalCardModel.FkShelter,
                YearOfBirth = animalCardModel.YearOfBirth,
                Photo = animalCardModel.Photo,
            };

            return newAnimalCardDTO;
        }

        public static AnimalCardDTO UpdateAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {
            AnimalCard oldAnimalCardModel;
            AnimalCard animalCardModel;

            using (var context = new RegistryPetsContext())
            {
                oldAnimalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCardDTO.Id)).FirstOrDefault();

                if (oldAnimalCardModel == null)
                    throw new Exception("trying to change unexisting animal card");
            }

            using (var context = new RegistryPetsContext())
            {
                animalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCardDTO.Id)).FirstOrDefault();

                animalCardModel.ChipId = animalCardDTO.ChipId;
                animalCardModel.Name = animalCardDTO.Name;
                animalCardModel.FkCategory = animalCardDTO.FkCategory;
                animalCardModel.YearOfBirth = animalCardDTO.YearOfBirth;
                animalCardModel.IsBoy = animalCardDTO.IsBoy;
                animalCardModel.Photo = animalCardDTO.Photo;

                context.SaveChanges();

                AnimalCardLogController.LogUpdate(oldAnimalCardModel, animalCardModel, userDTO.Id);
            }

            var newAnimalCardDTO = new AnimalCardDTO()
            {
                Id = animalCardModel.Id,
                ChipId = animalCardModel.ChipId,
                Name = animalCardModel.Name,
                IsBoy = animalCardModel.IsBoy,
                FkCategory = animalCardModel.FkCategory,
                FkShelter = animalCardModel.FkShelter,
                YearOfBirth = animalCardModel.YearOfBirth,
                Photo = animalCardModel.Photo,
            };

            return newAnimalCardDTO;
        }

        public static void DeleteAnimalCard(AnimalCardDTO animalCardDTO, UserDTO userDTO)
        {
            using (var context = new RegistryPetsContext())
            {
                var animalCard = context.AnimalCards.Where(x => x.Id == animalCardDTO.Id).FirstOrDefault();

                if (animalCard == null)
                    throw new Exception("trying to delete non existent model");

                var animalCardVaccinations = animalCard.Vaccinations.ToList();

                foreach(var animalCardVaccination in animalCardVaccinations)
                {
                    context.Vaccinations.Remove(animalCardVaccination);
                }

                var animalCardParasiteTreatments = animalCard.ParasiteTreatments.ToList();

                foreach (var animalCardParasiteTreatment in animalCardParasiteTreatments)
                {
                    context.ParasiteTreatments.Remove(animalCardParasiteTreatment);
                }

                var animalCardVeterinaryAppointments = animalCard.VeterinaryAppointmentAnimals.ToList();

                foreach (var animalCardVeterinaryAppointment in animalCardVeterinaryAppointments)
                {
                    context.VeterinaryAppointmentAnimals.Remove(animalCardVeterinaryAppointment);
                }

                var animalCardContracts = animalCard.Contracts.ToList();

                foreach (var animalCardContract in animalCardContracts)
                {
                    context.Contracts.Remove(animalCardContract);
                }

                context.AnimalCards.Remove(animalCard);

                AnimalCardLogController.LogDelete(animalCard, userDTO.Id);

                context.SaveChanges();
            }
        }
    }
}
