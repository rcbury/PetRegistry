using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    internal class LegalPersonDTO
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Name { get; set; } 

        public string Address { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int FkCountry { get; set; }

        public int FkLocality { get; set; }
    }
}
