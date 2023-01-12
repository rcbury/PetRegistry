using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Models
{
    public class Users
    {
        public Users(Locations locations, Shelters shelters)
        {
            using (var context = new RegistryPetsContext())
            {
                var usersDB = context.Users.ToList();

                foreach (var userDB in usersDB)
                {
                    var user = new User(
                        locations.LocationsList.Where(x => x.Id == userDB.FkLocation).FirstOrDefault(),
                        shelters.ShelterList.Where(x => x.Id == userDB.FkShelter).FirstOrDefault(),
                        userDB);

                    UserList.Add(user);
                }
            }
        }
        
        public List<User> UserList { get; set; }


    }
}
