using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.DTO
{
    public class UserDTO
    {
        public int Id { get; set; } = 0;
        public string Login { get; set; } = "";
        public string Name { get; set; } = "";
        public int? ShelterId { get; set; }
        public int RoleId { get; set; }
        public int? LocationId { get; set; }
    }
}
