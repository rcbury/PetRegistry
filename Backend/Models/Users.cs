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
            UserList = new List<User>();

            using (var context = new RegistryPetsContext())
            {
                var usersDB = context.Users.ToList();

                foreach (var userDB in usersDB)
                {
                    var user = new User(
                        locations.LocationsList.Where(x => x.Id == userDB.FkLocation).FirstOrDefault(),
                        shelters.ShelterList.Where(x => x.Id == userDB.FkShelter).FirstOrDefault(),
                        userDB.Id, userDB.Login, userDB.Password, userDB.Name, userDB.Email, userDB.FkRole);

                    UserList.Add(user);
                }
            }
        }
        
        public List<User> UserList { get; set; }


        public User? GetUserById(int Id)
        {
            return UserList.Where(x => x.Id == Id).FirstOrDefault();
        }
    }
}
