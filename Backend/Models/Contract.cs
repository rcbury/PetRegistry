using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;

namespace PIS_PetRegistry.Backend.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public DateOnly Date { get; set; }

        public AnimalCard AnimalCard { get; set; } = null!;

        public LegalPerson? LegalPerson { get; set; }

        public PhysicalPerson PhysicalPerson { get; set; } = null!;

        public User User { get; set; } = null!;

        public void MakeContract(string filePath, User user)
        {
            if (PhysicalPerson != null)
            {
                Document doc;
                if (LegalPerson != null)
                {
                    doc = new Document("Договор юры.docx");
                    doc.Replace("<LegalPersonINN>", LegalPerson.Inn, false, true);
                    doc.Replace("<LegalPersonKPP>", LegalPerson.Kpp, false, true);
                    doc.Replace("<LegalPersonPhoneNumber>", LegalPerson.Phone, false, true);
                    doc.Replace("<LegalPersonName>", LegalPerson.Name, false, true);
                    doc.Replace("<LegalPersonLocation>", LegalPerson.Location.Name, false, true);
                    doc.Replace("<LegalPersonAddress>", LegalPerson.Address, false, true);
                    doc.Replace("<LegalPersonEmail>", LegalPerson.Email, false, true);
                }
                else
                {
                    doc = new Document("Договор физы.docx");
                }
                var animalCategory = AnimalCard.AnimalCategory;
                var animalGender = AnimalCard.IsBoy ? "м" : "ж";
                var day = DateTime.Now.Day.ToString();
                var month = DateTime.Now.Month.ToString();
                var year = DateTime.Now.Year.ToString();
                var age = (int.Parse(year) - AnimalCard.YearOfBirth).ToString();
                var loggedUserCreds = Utils.GetCredsFromFullName(user.Name);
                var physicalPersonCreds = Utils.GetCredsFromFullName(PhysicalPerson.Name);
                doc.Replace("<ShelterCity>", User.Shelter.Location.Name, false, true);
                doc.Replace("<Day>", day, false, true);
                doc.Replace("<Month>", month, false, true);
                doc.Replace("<Year>", year, false, true);
                doc.Replace("<ShelterName>", $"\"{User.Shelter.Name}\"", false, true);
                doc.Replace("<ShelterAddress>", User.Shelter.Address, false, true);
                doc.Replace("<PhysicalPersonName>", PhysicalPerson.Name, false, true);
                doc.Replace("<PhysicalPersonLocation>", PhysicalPerson.Location.Name, false, true);
                doc.Replace("<PhysicalPersonAddress>", PhysicalPerson.Address, false, true);
                doc.Replace("<AnimalCategoryName>", animalCategory.Name, false, true);
                doc.Replace("<AnimalAge>", age, false, true);
                doc.Replace("<AnimalGender>", animalGender, false, true);
                doc.Replace("<ChipId>", AnimalCard.ChipId, false, true);
                doc.Replace("<AnimalName>", AnimalCard.Name, false, true);
                doc.Replace("<LoggedUserCreds>", loggedUserCreds, false, true);
                doc.Replace("<PhysicalPersonCreds>", physicalPersonCreds, false, true);
                doc.Replace("<PhysicalPersonNumber>", PhysicalPerson.Phone, false, true);
                doc.Replace("<PhysicalPersonEmail>", PhysicalPerson.Email, false, true);
                doc.SaveToFile(filePath, FileFormat.Docx);
            }
        }
    }
}
