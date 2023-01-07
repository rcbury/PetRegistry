using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class ContractDTO
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public DateOnly Date { get; set; }
        public int FkAnimalCard { get; set; }
        public int FkUser { get; set; }
        public int FkPhysicalPerson { get; set; }
        public int? FkLegalPerson { get; set; }
    }
}
