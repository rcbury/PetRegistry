using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend
{
    public class PersonInfo
    {
        public int AnimalCount { get; set; }
        public int CatCount { get; set; }
        public int DogCount { get; set; }
        public string CountryName { get; set; }
        public string LocationName { get; set; }
    }
}
