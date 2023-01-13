using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Controllers
{
    public class AuthorizationController
    {
        private Locations Locations { get; set; } = new Locations();
        private Shelters Shelters { get; set; }
        private Users Users { get; set; }

        public Backend.Models.User AuthorizedUser { get; set; }

        public AuthorizationController()
        {
            Shelters = new Shelters(Locations);
            Users = new Users(Locations, Shelters);
        }

        public bool Authorize(string login, string password)
        {            
            MD5Hash mD5Hash = new MD5Hash();
            string hashedPassword = mD5Hash.HashPassword(password);

            var user = Users.GetUserByLoginAndPassword(login, hashedPassword);

            if (user == null) 
            {
                return false;
            }
            else
            {
                AuthorizedUser = user;
                AuthorizationService.Authorize(user.Id);

                return true;
            }
        }

        public UserDTO? GetAuthorizedUser()
        {
            if (AuthorizedUser == null)
                return null;
            
            var userDTO = DTOModelConverter.ConvertModelToDTO(AuthorizedUser);

            return userDTO;
        }
    }
}
