using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Shelter
    {
        public Shelter(Location location, int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            Location = location;
        }

        public Shelter() { }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public Location Location { get; set; }
    }
}
