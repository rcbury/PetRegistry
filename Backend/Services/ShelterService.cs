﻿using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIS_PetRegistry.Backend.Services
{
    public class ShelterService
    {
        public static Shelter GetShelterById(int? shelterId)
        {
            using (var context = new RegistryPetsContext())
            {
                return context.Shelters
                    .Where(shelter => shelter.Id == shelterId)
                    .Include(shelter => shelter.FkLocationNavigation)
                    .FirstOrDefault();
            }
        }
    }
}
