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
        public Vaccine(PIS_PetRegistry.Models.Vaccine vaccineDB)
        {
            Id = vaccineDB.Id;
            Number = vaccineDB.Number;
            Name = vaccineDB.Name;
            ValidityPeriod = vaccineDB.ValidityPeriod;
        }

        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; } = null!;

        public int ValidityPeriod { get; set; }
    }
}
