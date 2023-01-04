using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class ParasiteTreatmentDTO
    {
        public int? Id { get; set; } = null;

        public int FkAnimal { get; set; }

        public int FkUser { get; set; }

        public int FkMedication { get; set; }

        public DateOnly Date { get; set; }

        public string? UserName { get; set; }
        public string? MedicationName { get; set; }
    }
}
