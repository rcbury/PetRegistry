using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class ParasiteTreatments
    {
        public ParasiteTreatments(int animalId)
        {
            var parasiteTreatmentDB = ParasiteTreatmentService.GetParasiteTreatmentsByAnimal(animalId);

            ParasiteTreatmentList = parasiteTreatmentDB.Select(x => new ParasiteTreatment(x)).ToList();
            Medications = ParasiteTreatmentMedicationService
                .GetParasiteTreatmentMedications()
                .Select(x => new Medication(x))
                .ToList();
        }

        public List<Medication> Medications { get; set; }

        public List<ParasiteTreatment> ParasiteTreatmentList { get; set; }
    }
}
