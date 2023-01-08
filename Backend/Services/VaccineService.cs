using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    internal class VaccineService
    {
        public static List<Vaccine> GetVaccines()
        {
            using(var context = new RegistryPetsContext())
            {
                return context.Vaccines.ToList();
            }
        }
    }
}
