using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    internal class PhysicalPersonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Phone { get; set; } 

        public string Address { get; set; }

        public string? Email { get; set; }

        public int FkLocality { get; set; }

        public int FkCountry { get; set; }
    }
}
