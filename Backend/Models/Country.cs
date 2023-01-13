﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Country
    {
        public Country(int id, string name) 
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
