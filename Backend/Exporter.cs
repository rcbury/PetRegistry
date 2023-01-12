﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using PIS_PetRegistry.Services;

namespace PIS_PetRegistry.Backend
{
    public class Exporter
    {
        public static void ExportPhysicalPeopleToExcel(string path, List<PhysicalPersonDTO> physicalPeople) 
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Физические лица");
                var heads = new string[9]
                {
                        "ФИО",
                        "Номер телефона",
                        "Фактический адрес проживания",
                        "Адрес эл. почты",
                        "Страна",
                        "Населенный пункт",
                        "Количество животных",
                        "Количество кошек/котов",
                        "Количество собак/псов"
                };

                var cnt = 1;
                foreach (var head in heads)
                {
                    var currCell = worksheet.Cell(1, cnt);
                    currCell.Value = head;
                    currCell.Style.Alignment.WrapText = true;
                    currCell.Style.Font.Bold = true;
                    cnt++;
                }
                var len = physicalPeople.Count;
                if (len > 0)
                {
                    var rowCnt = 2;
                    foreach (var person in physicalPeople)
                    {
                        worksheet.Cell(rowCnt, 1).Value = person.Name;
                        worksheet.Cell(rowCnt, 2).Value = person.Phone;
                        worksheet.Cell(rowCnt, 3).Value = person.Address;
                        worksheet.Cell(rowCnt, 4).Value = person.Email;
                        worksheet.Cell(rowCnt, 5).Value = person.CountryName;
                        worksheet.Cell(rowCnt, 6).Value = person.LocationName;
                        worksheet.Cell(rowCnt, 7).Value = person.AnimalCount;
                        worksheet.Cell(rowCnt, 8).Value = person.CatCount;
                        worksheet.Cell(rowCnt, 9).Value = person.DogCount;
                        rowCnt++;
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }

        public static void ExportLegalPeopleToExcel(string path, List<LegalPersonDTO> legalPeople) 
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Юридические лица");

                var heads = new string[11]
                {
                    "ИНН",
                    "КПП",
                    "Наименование организации",
                    "Адрес",
                    "Адрес эл. почты",
                    "Номер телефона",
                    "Страна",
                    "Населенный пункт",
                    "Количество животных",
                    "Количество кошек/котов",
                    "Количество собак/псов"
                };

                var cnt = 1;
                foreach (var head in heads)
                {
                    var currCell = worksheet.Cell(1, cnt);
                    currCell.Value = head;
                    currCell.Style.Alignment.WrapText = true;
                    currCell.Style.Font.Bold = true;
                    cnt++;
                }

                var len = legalPeople.Count;
                if (len > 0)
                {
                    var rowCnt = 2;
                    foreach (var person in legalPeople)
                    {
                        worksheet.Cell(rowCnt, 1).Value = person.INN;
                        worksheet.Cell(rowCnt, 2).Value = person.KPP;
                        worksheet.Cell(rowCnt, 3).Value = person.Name;
                        worksheet.Cell(rowCnt, 4).Value = person.Address;
                        worksheet.Cell(rowCnt, 5).Value = person.Email;
                        worksheet.Cell(rowCnt, 6).Value = person.Phone;
                        worksheet.Cell(rowCnt, 7).Value = person.CountryName;
                        worksheet.Cell(rowCnt, 8).Value = person.LocationName;
                        worksheet.Cell(rowCnt, 9).Value = person.AnimalCount;
                        worksheet.Cell(rowCnt, 10).Value = person.CatCount;
                        worksheet.Cell(rowCnt, 11).Value = person.DogCount;
                        rowCnt++;
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }

        public static void ExportCardsToExcel(string path, List<AnimalCardDTO> cardsList) 
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Учетные карточки");

                var heads = new string[5]
                {
                    "Кличка",
                    "Номер чипа",
                    "Дата рождения",
                    "Пол животного",
                    "Категория животного"
                };

                var cnt = 1;
                foreach (var head in heads)
                {
                    var currCell = worksheet.Cell(1, cnt);
                    currCell.Value = head;
                    currCell.Style.Alignment.WrapText = true;
                    currCell.Style.Font.Bold = true;
                    cnt++;
                }

                var len = cardsList.Count;
                if (len > 0)
                {
                    var rowCnt = 2;
                    foreach (var card in cardsList)
                    {
                        worksheet.Cell(rowCnt, 1).Value = card.Name;
                        worksheet.Cell(rowCnt, 2).Value = card.ChipId;
                        worksheet.Cell(rowCnt, 3).Value = card.YearOfBirth;
                        worksheet.Cell(rowCnt, 4).Value = card.IsBoy ? "Мальчик" : "Девочка";
                        worksheet.Cell(rowCnt, 5).Value = card.CategoryName;
                        rowCnt++;
                    }
                }
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                workbook.SaveAs(path);
            }
        }
    }
}
