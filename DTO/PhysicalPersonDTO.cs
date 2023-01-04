using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class PhysicalPersonDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int FkCountry { get; set; }
        public int FkLocality { get; set; }
    }
}
