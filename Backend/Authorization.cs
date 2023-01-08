using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;


namespace PIS_PetRegistry.Backend
{
    public static class Authorization
    {
        public static User? AuthorizedUser { get; set; }

    }
}
