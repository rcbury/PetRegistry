using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class VaccinationDTO
    {
        public DateOnly DateEnd { get; set; }

        public int FkAnimal { get; set; }

        public int? FkUser { get; set; } = null;

        public int FkVaccine { get; set; }

        public string? UserName { get; set; }

        public string? VaccineName { get; set; }
    }
}
