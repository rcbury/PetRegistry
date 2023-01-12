using DocumentFormat.OpenXml.Bibliography;
using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Vaccinations
    {
        public Vaccinations(int animalCardId) 
        {
            Vaccines = VaccineService.GetVaccines().Select(x => new Vaccine(x)).ToList();
            var vaccinationsDB = VaccinationService.GetVaccinationsByAnimal(animalCardId);

            foreach (var vaccinationDB in vaccinationsDB)
            {
                VaccinationList.Add(new Vaccination(vaccinationDB));
            }
        }

        private List<Vaccine> Vaccines { get; set; }
        private List<Vaccination> VaccinationList { get; set; }
    }

}
