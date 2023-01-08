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
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int FkCountry { get; set; }
        public int FkLocality { get; set; }
        public int AnimalCount { get; set; }
        public int CatCount { get; set; }
        public int DogCount { get; set; }
        public string CountryName { get; set; }
        public string LocationName { get; set; }
    }
}
