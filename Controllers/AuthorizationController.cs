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
        
        public Backend.Models.User User { get; set; }

        public AuthorizationController()
        {
            Shelters = new Shelters(Locations);
        }

        public bool Authorize(string login, string password)
        {
            var userDB = AuthorizationService.Authorize(login, password);

            if (userDB == null)
                return false;

            var user = new Backend.Models.User(
                Locations.LocationsList.Where(x => x.Id == userDB.FkLocation).FirstOrDefault(),
                Shelters.ShelterList.Where(x => x.Id == userDB.FkShelter).FirstOrDefault(),
                userDB);

            var userDTO = DTOModelConverter.ConvertModelToDTO(userDB);

            return true;
        }

        public UserDTO GetAuthorizedUser()
        {
            var user = AuthorizationService.GetAuthorizedUser();
            var userDTO = DTOModelConverter.ConvertModelToDTO(user);

            return userDTO;
        }
    }
}
