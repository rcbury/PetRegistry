using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PIS_PetRegistry.Services;
using PIS_PetRegistry.Backend.Services;
using PIS_PetRegistry.Backend;

namespace PIS_PetRegistry.Controllers
{
    internal class PetOwnersController
    {
        public static List<PhysicalPersonDTO> GetPhysicalPeople(string phone, string name, string address, string email, int country, int location)
        {
            var physicalPeople = PetOwnersService.GetPhysicalPeople(phone,name,address,email,country,location);
            var res = new List<PhysicalPersonDTO>();
            foreach (var physicalPerson in physicalPeople) 
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(physicalPerson));
            }
            return res;
        }

        public static PhysicalPersonDTO? GetPhysicalPersonByPhone(string phone)
        {
            var person = PetOwnersService.GetPhysicalPersonByPhone(phone);

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public static PhysicalPersonDTO? GetPhysicalPersonById(int personId)
        {
            var person = PetOwnersService.GetPhysicalPersonById(personId);

            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public static LegalPersonDTO? GetLegalPersonByINN(string INN)
        {
            var person = PetOwnersService.GetLegalPersonByInn(INN);
            
            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }
        public static LegalPersonDTO? GetLegalPersonById(int? personId)
        {
            var person = PetOwnersService.GetLegalPersonById(personId);
            
            if (person == null)
            {
                return null;
            }

            return DTOModelConverter.ConvertModelToDTO(person);
        }

        public static List<LegalPersonDTO> GetLegalPeople(string inn, string kpp, string name, string email, 
            string address, string phone, int country, int location)
        {
            var legalPeople = PetOwnersService.GetLegalPeople(inn, kpp, name, email, address, phone, country, location);
            var res = new List<LegalPersonDTO>();
            
            foreach (var legalPerson in legalPeople)
            {
                res.Add(DTOModelConverter.ConvertModelToDTO(legalPerson));
            }

            return res;
        }
       
        public static void UpdateLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.UpdateLegalPerson(legalPersonModel);
        }

        public static void AddLegalPerson(LegalPersonDTO legalPersonDTO)
        {
            var legalPersonModel = DTOModelConverter.ConvertDTOToModel(legalPersonDTO);
            PetOwnersService.AddLegalPerson(legalPersonModel);
        }

        public static void AddPhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertDTOToModel(physicalPersonDTO);
            PetOwnersService.AddPhysicalPerson(physicalPersonModel);
        }

        public static void UpdatePhysicalPerson(PhysicalPersonDTO physicalPersonDTO)
        {
            var physicalPersonModel = DTOModelConverter.ConvertDTOToModel(physicalPersonDTO);
            PetOwnersService.UpdatePhysicalPerson(physicalPersonModel);
        }

        public static void ExportPhysicalPeopleToExcel(string path, List<PhysicalPersonDTO> physicalPeopleDTO)
        {
            Exporter.ExportPhysicalPeopleToExcel(path, physicalPeopleDTO);
        }

        public static void ExportLegalPeopleToExcel(string path, List<LegalPersonDTO> legalPeopleDTO) 
        {
            Exporter.ExportLegalPeopleToExcel(path, legalPeopleDTO);
        }

        public static int CountAnimalsByPhysicalPerson(int physicalPersonId)
        {
            var physicalPerson = PetOwnersService.GetPhysicalPersonById(physicalPersonId);

            return physicalPerson.GetAnimalCount();
        }

        public static int CountAnimalsByLegalPerson(int legalPersonId)
        {
            var legalPerson = PetOwnersService.GetLegalPersonById(legalPersonId);

            return legalPerson.GetAnimalCount();
        }
    }
}
