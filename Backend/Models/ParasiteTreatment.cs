using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class ParasiteTreatment
    {
        public ParasiteTreatment(PIS_PetRegistry.Models.ParasiteTreatment parasiteTreatmentDB)
        {
            FkAnimal = parasiteTreatmentDB.FkAnimal;
            FkUser = parasiteTreatmentDB.FkUser;
            Medication = new Medication(parasiteTreatmentDB.FkMedicationNavigation);
            Date = parasiteTreatmentDB.Date;
        }

        public int FkAnimal { get; set; }

        public int FkUser { get; set; }

        public Medication Medication { get; set; }

        public DateOnly Date { get; set; }
    }
}
