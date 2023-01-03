using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    internal class AnimalCardController
    {
        public AnimalCardDTO AddAnimalCard(AnimalCardDTO animalCardDTO)
        {
            var animalCardModel = new AnimalCard()
            {
                ChipId = animalCardDTO.ChipId,
                Name = animalCardDTO.Name,
            };
        }
    }
}
