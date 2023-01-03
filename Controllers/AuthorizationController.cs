using PIS_PetRegistry.Backend;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
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
            var dbContext = new RegistryPetsContext();

            MD5Hash mD5Hash = new MD5Hash();
            string hashedPassword = mD5Hash.HashPassword(password);

            var user = dbContext.Users
                .Where(x => x.Password.ToLower().Equals(hashedPassword.ToLower()) &&
                    x.Login.ToLower().Equals(login.ToLower()))
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }
            else
            {
                var userDTO = new UserDTO()
                {
                    Login = user.Login,
                    Id = user.Id,
                    ShelterId = user.FkShelter,
                    RoleId = user.FkRole,
                    LocationId = user.FkLocation,
                };
                
                Authorization.AuthorizedUserDto = userDTO;
                //TODO: Global entity framework filter for roles and locations
                return userDTO;
            }
        }

        public static UserDTO GetAuthorizedUser()
        {
            return Authorization.AuthorizedUserDto;
        }
    }
}
