﻿using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class ParasiteTreatmentMedicationService
    {
        public static List<ParasiteTreatmentMedication> GetParasiteTreatmentMedications()
        {
            using (var context = new RegistryPetsContext())
            {
                return context.ParasiteTreatmentMedications.ToList();
            }
        }
    }
}
