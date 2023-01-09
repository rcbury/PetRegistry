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
        public static UserDTO? Authorize(string login, string password)
        {
            var user = AuthorizationService.Authorize(login, password);

            if (user == null)
                return null;

            var userDTO = DTOModelConverter.ConvertModelToDTO(user);

            return userDTO;
        }

        public static UserDTO GetAuthorizedUser()
        {
            var user = AuthorizationService.GetAuthorizedUser();
            var userDTO = DTOModelConverter.ConvertModelToDTO(user);

            return userDTO;
        }
    }
}
