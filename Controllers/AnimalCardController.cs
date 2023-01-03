using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
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

            AnimalCard animalCardModel;

            using (var context = new RegistryPetsContext())
            {
                animalCardModel = context.AnimalCards.Where(x => x.Id.Equals(animalCardDTO.Id)).FirstOrDefault();

                if (animalCardModel == null)
                    throw new Exception("trying to change unexisting animal card");

                animalCardModel.ChipId = animalCardDTO.ChipId;
                animalCardModel.Name = animalCardDTO.Name;
                animalCardModel.FkCategory = animalCardDTO.FkCategory;
                animalCardModel.YearOfBirth = animalCardDTO.YearOfBirth;
                animalCardModel.IsBoy = animalCardDTO.IsBoy;
                animalCardModel.Photo = animalCardDTO.Photo;

                context.SaveChanges();
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
    }
}
