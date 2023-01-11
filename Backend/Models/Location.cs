using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Location
    {
        public Location()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
