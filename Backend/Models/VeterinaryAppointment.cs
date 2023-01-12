using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class VeterinaryAppointment
    {
        public VeterinaryAppointment(PIS_PetRegistry.Models.VeterinaryAppointmentAnimal veterinaryAppointment)
        {
            Date = veterinaryAppointment.Date;
            FkAnimal = veterinaryAppointment.FkAnimal;
            FkUser = veterinaryAppointment.FkUser;
            Name = veterinaryAppointment.Name;
            IsCompleted = veterinaryAppointment.IsCompleted;
        }

        public DateTime Date { get; set; }

        public int FkAnimal { get; set; }

        public int FkUser { get; set; }

        public string Name { get; set; } = null!;

        public bool IsCompleted { get; set; }
    }
}
