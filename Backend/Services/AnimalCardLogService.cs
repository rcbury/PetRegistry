using Microsoft.VisualBasic.ApplicationServices;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Services
{
    public class AnimalCardLogService
    {
        public static void LogDelete(AnimalCard animalCard, int fkUser)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(animalCard, options);

            var animalCardLog = new AnimalCardLog()
            {
                Description = jsonString,
                CreateTime = DateTime.Now.ToUniversalTime(),
                FkLogsType = (int)LogTypes.Deletion,
                FkUser = fkUser,

            };

            using (var context = new RegistryPetsContext())
            {
                context.AnimalCardLogs.Add(animalCardLog);
                context.SaveChanges();
            }
        }

        public static void LogUpdate(AnimalCard oldAnimalCard, AnimalCard animalCard, int fkUser)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(new List<AnimalCard>() { oldAnimalCard, animalCard }, options);

            var animalCardLog = new AnimalCardLog()
            {
                Description = jsonString,
                CreateTime = DateTime.Now.ToUniversalTime(),
                FkLogsType = (int)LogTypes.Change,
                FkUser = fkUser,
            };

            using (var context = new RegistryPetsContext())
            {
                context.AnimalCardLogs.Add(animalCardLog);
                context.SaveChanges();
            }
        }

        public static void LogCreate(AnimalCard animalCard, int fkUser)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(animalCard, options);

            var animalCardLog = new AnimalCardLog()
            {
                Description = jsonString,
                CreateTime = DateTime.Now.ToUniversalTime(),
                FkLogsType = (int)LogTypes.Creation,
                FkUser = fkUser,
            };

            using (var context = new RegistryPetsContext())
            {
                context.AnimalCardLogs.Add(animalCardLog);
                context.SaveChanges();
            }
        }
    }
}
