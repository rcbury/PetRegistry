using PIS_PetRegistry.Backend;
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
    internal static class AuthorizationController
    {
        public static UserDTO ConvertUserToDTO(User user)
        {
            var userDTO = new UserDTO()
            {
                Login = user.Login,
                Id = user.Id,
                ShelterId = user.FkShelter,
                RoleId = user.FkRole,
                LocationId = user.FkLocation,
                Name = user.Name,


            };

            if (user.FkShelter != null)
            {
                userDTO.ShelterLocationId = user.FkShelterNavigation.FkLocation;
            }

            return userDTO;
        }

        public static UserDTO? Authorize(string login, string password)
        {
            var user = AuthorizationService.Authorize(login, password);

            if (user == null)
                return null;

            var userDTO = ConvertUserToDTO(user);

            return userDTO;
        }

        public static UserDTO GetAuthorizedUser()
        {
            var user = AuthorizationService.GetAuthorizedUser();
            var userDTO = ConvertUserToDTO(user);

            return userDTO;
        }
    }
}
