using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Vaccines
    {
        public Vaccines()
        {
            VaccineList = VaccineService.GetVaccines().Select(x => new Vaccine(x)).ToList();
        }

        public List<Vaccine> VaccineList { get; set; }
    }
}
