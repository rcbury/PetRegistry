using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class VeterinaryAppointmentDTO
    {
        public DateOnly Date { get; set; }

        public int FkAnimal { get; set; }

        public int? FkUser { get; set; }

        public string Name { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public string? UserName { get; set; }
    }
}
