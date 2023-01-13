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
        public static PIS_PetRegistry.Models.User GetUserById(int id)
        {
            using (var dbContext = new RegistryPetsContext())
            {
                var user = dbContext.Users.Where(x => x.Id == id).FirstOrDefault();

                return user;
            }
        }

        public static User? Authorize(int id)
        {
            var user = GetUserById(id);

            Authorization.AuthorizedUser = user;
            return user;
        }

        public static User? GetAuthorizedUser()
        {
            return Authorization.AuthorizedUser;
        }
    }
}
