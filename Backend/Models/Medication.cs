using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Medication
    {
        public Medication(PIS_PetRegistry.Models.ParasiteTreatmentMedication medicationDB)
        {
            Id = medicationDB.Id;
            Name = medicationDB.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
