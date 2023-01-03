using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class AnimalCardDTO
    {
        public int Id { get; set; } = 0;
        public bool IsBoy { get; set; }
        public string Name { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public int? YearOfBirth { get; set; } = null;
        public int FkCategory { get; set; }
        public int FkShelter { get; set; }
        public string ChipId { get; set; } = null!;
    }
}
