using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Shelter
    {
        public Shelter(Location location, PIS_PetRegistry.Models.Shelter shelterDB)
        {
            Id = shelterDB.Id;
            Name = shelterDB.Name;
            Address = shelterDB.Address;
            Location = location;
        }

        public Shelter() { }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public Location Location { get; set; }
    }
}
