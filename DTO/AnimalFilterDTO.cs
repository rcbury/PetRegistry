using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class AnimalFilterDTO
    {
        public string Name { get; set; }
        public string ChipId { get; set; }
        public bool IsBoy { get; set; }
        public bool IsSelectedSex { get; set; }
        public AnimalCategoryDTO AnimalCategory { get; set; }
        public LegalPersonDTO LegalPerson { get; set; }
        public PhysicalPersonDTO PhysicalPerson { get; set; }
    }
}
