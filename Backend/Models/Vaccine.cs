using DocumentFormat.OpenXml.Presentation;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Vaccine
    {
        public Vaccine(int id, int number, string name, int validityPeriod)
        {
            Id = id;
            Number = number;
            Name = name;
            ValidityPeriod = validityPeriod;
        }

        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; } = null!;

        public int ValidityPeriod { get; set; }
    }
}
