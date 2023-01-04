using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class VaccineDTO
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; } = null!;

        public int ValidityPeriod { get; set; }
    }
}
