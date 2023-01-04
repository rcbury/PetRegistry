using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class LegalPersonDTO
    {
        public int Id { get; set; } = 0;
        public string INN { get; set; } = null!;
        public string KPP { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int FkCountry { get; set; }
        public int FkLocality { get; set; }

    }
}
