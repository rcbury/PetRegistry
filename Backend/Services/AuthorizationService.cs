using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Services
{
    internal class AuthorizationService
    {
        public static User? Authorize(string login, string password)
        {

            using (var dbContext = new RegistryPetsContext())
            {
                MD5Hash mD5Hash = new MD5Hash();
                string hashedPassword = mD5Hash.HashPassword(password);

                var user = dbContext.Users
                    .Where(x => x.Password.ToLower().Equals(hashedPassword.ToLower()) &&
                        x.Login.ToLower().Equals(login.ToLower()))
                    .Include(u => u.FkShelterNavigation)
                    .Include(u => u.FkRoleNavigation)
                    .FirstOrDefault();

                Authorization.AuthorizedUser = user;
                return user;
            }
        }

        public static User? GetAuthorizedUser()
        {
            return Authorization.AuthorizedUser;
        }
    }
}
