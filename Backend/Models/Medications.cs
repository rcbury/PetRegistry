﻿using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Medications
    {
        public Medications()
        {
            MedicationList = ParasiteTreatmentMedicationService
                .GetParasiteTreatmentMedications()
                .Select(x => new Medication(x.Id, x.Name))
                .ToList();
        }

        public Medication? GetMedicationById(int id)
        {
            return MedicationList.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Medication> MedicationList { get; set; }
    }
}
