using PIS_PetRegistry.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification
{
    public static class EmailNotificationSystem
    {
        public static void OnExecuteNotification()
        {
            var notification = new Notification();
            var registry = new Registry();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var animals = registry.GetAnimals();
            var animalsVacinations = registry.GetAnimalsVaccinations(animals);
            
            var users = registry.GetUsers();
            notification.SendUsers(users, animals, animalsVacinations);
        }
    }
}
