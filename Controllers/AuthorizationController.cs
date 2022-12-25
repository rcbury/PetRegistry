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
        public static UserDTO Authorize(string login, string password)
        {
            var dbContext = new RegistryPetsContext();
            
            MD5Hash mD5Hash = new MD5Hash();
            string hashedPassword = mD5Hash.HashPassword(password);

            var user = dbContext.Users.Where(x => x.Password.Equals(hashedPassword) && x.Login.Equals(login)).FirstOrDefault();
            
            if (user == null)
            {
                return null;
            }
            else
            {
                var userDTO = new UserDTO();
                userDTO.Login = user.Login;
                userDTO.Id = user.Id;
                //TODO: Добавить поля после обновления моделей
                Authorization.AuthorizedUserDto = userDTO;
                //TODO: Global entity framework filter for roles and locations
                return userDTO;
            }
        }
    }
}
