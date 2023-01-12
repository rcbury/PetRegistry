using PIS_PetRegistry.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class VeterinaryAppointments
    {
        public VeterinaryAppointments(int animalId)
        {
            VeterinaryAppointmentList = VeterinaryAppointmentService
                .GetVeterinaryAppointmentsByAnimal(animalId)
                .Select(x => new VeterinaryAppointment(x))
                .ToList();
        }

        public List<VeterinaryAppointment> VeterinaryAppointmentList { get; set; }
    }
}
