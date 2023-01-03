using PIS_PetRegistry.Models;
using Microsoft.EntityFrameworkCore;

namespace PIS_PetRegistry.Backend
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vaccine>().HasData(
                new Vaccine()
                {
                    Id = 1,
                    Name = "Вакцина против бешенества",
                    Number = 101777,
                    ValidityPeriod = 6
                },
                new Vaccine()
                {
                    Id = 2,
                    Name = "Вакцина против чумы",
                    Number = 102771,
                    ValidityPeriod = 6
                }
            );

            modelBuilder.Entity<ParasiteTreatmentMedication>().HasData(
                new ParasiteTreatmentMedication()
                {
                    Id = 1,
                    Name = "Препарат для дегельминтизации"
                },
                new ParasiteTreatmentMedication()
                {
                    Id = 2,
                    Name = "Препарат для обработки от эктопаразитов"
                }
            );

            modelBuilder.Entity<AnimalCategory>().HasData(
                new AnimalCategory()
                {
                    Id = 1,
                    Name = "Собака/пёс"
                },
                new AnimalCategory()
                {
                    Id = 2,
                    Name = "Кошка/кот"
                }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name = "РФ"
                },
                new Country()
                {
                    Id = 2,
                    Name = "Узбекистан"
                },
                new Country()
                {
                    Id = 3,
                    Name = "Казахстан"
                },
                new Country()
                {
                    Id = 4,
                    Name = "Китай"
                }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location()
                {
                    Id = 1,
                    Name = "Тюмень"
                },
                new Location()
                {
                    Id = 2,
                    Name = "Тобольск"
                },
                new Location()
                {
                    Id = 3,
                    Name = "Омск"
                }
            );

            modelBuilder.Entity<LogType>().HasData(
                new LogType()
                {
                    Id = 1,
                    Name = "Добавление"
                },
                new LogType()
                {
                    Id = 2,
                    Name = "Изменение"
                },
                new LogType()
                {
                    Id = 3,
                    Name = "Удаление"
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Ветврач"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Оператор приюта"
                },
                new Role()
                {
                    Id = 3,
                    Name = "Сотрудник ветслужбы"
                },
                new Role()
                {
                    Id = 4,
                    Name = "Сотрудник ОМСУ"
                },
                new Role()
                {
                    Id = 5,
                    Name = "Сотрудник приюта"
                }
            );

            modelBuilder.Entity<Shelter>().HasData(
                new Shelter()
                {
                    Id = 1,
                    Name = "Сытая морда",
                    FkLocation = 1,
                    Address = "Республики, 1"
                },
                new Shelter()
                {
                    Id = 2,
                    Name = "Счастливый ушастик",
                    FkLocation = 2,
                    Address = "​Семёна Ремезова, 123/2"
                },
                new Shelter()
                {
                    Id = 3,
                    Name = "Умка",
                    FkLocation = 3,
                    Address = "Рабиновича, 69а"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Login = "mikhail1",
                    Password = "b5f44ae34083deccec95df26067e02a0",
                    Name= "Михайлов Михаил Иванович",
                    Email = "mikhailivanovic@gmail.com",
                    FkRole = 1,
                    FkShelter = 1
                },
                new User()
                {
                    Id = 2,
                    Login = "elena2",
                    Password = "f7373d2b16a4a61a2ee9e9aa9a4b2bee",
                    Name = "Михайлова Елена Ивановна",
                    Email = "lenaivanova@gmail.com",
                    FkRole = 2,
                    FkShelter = 1
                },
                new User()
                {
                    Id = 3,
                    Login = "petr3",
                    Password = "b5f44ae34083deccec95df26067e02a0",
                    Name = "Петров Петр Петрович",
                    Email = "petrov@gmail.com",
                    FkRole = 3
                },
                new User()
                {
                    Id = 4,
                    Login = "gena4",
                    Password = "b5f44ae34083deccec95df26067e02a0",
                    Name = "Сидоров Геннадий Иванович",
                    Email = "sidorov@gmail.com",
                    FkRole = 4,
                    FkLocation = 1
                },
                new User()
                {
                    Id = 5,
                    Login = "ivan5",
                    Password = "b5f44ae34083deccec95df26067e02a0",
                    Name = "Иванов Иван Иванович",
                    Email = "ivanov@gmail.com",
                    FkRole = 5,
                    FkShelter = 1
                }
            );
        }
    }
}
