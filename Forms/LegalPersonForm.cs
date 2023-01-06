﻿using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PIS_PetRegistry
{
    public partial class LegalPersonForm : Form
    {
        private LegalPersonDTO? mainLegalPerson;
        public LegalPersonForm() : this(null)
        {

        }
        public LegalPersonForm(LegalPersonDTO? selectedLegalPerson)
        {
            mainLegalPerson = selectedLegalPerson;

            InitializeComponent();
            

            var allCountries = PetOwnersController.GetCountries();
            var allLocalities = PetOwnersController.GetLocations();

            CountryComboBox.DataSource = allCountries;
            CountryComboBox.DisplayMember = "Name";
            CountryComboBox.ValueMember = "Id";

            LocalityComboBox.DataSource = allLocalities;
            LocalityComboBox.DisplayMember = "Name";
            LocalityComboBox.ValueMember = "Id";

            if (selectedLegalPerson != null)
            {
                INNText.Text = selectedLegalPerson.INN;
                NameText.Text = selectedLegalPerson.Name;
                KPPText.Text = selectedLegalPerson.KPP;
                AdressText.Text = selectedLegalPerson.Address;
                PhoneText.Text = selectedLegalPerson.Phone;
                EmailText.Text = selectedLegalPerson.Email;

                var startCountry = allCountries
                    .Where(x => x.Id.Equals(selectedLegalPerson.FkCountry))
                    .FirstOrDefault();

                var startLocality = allLocalities
                    .Where(x => x.Id.Equals(selectedLegalPerson.FkLocality))
                    .FirstOrDefault();

                CountryComboBox.SelectedItem= startCountry;
                LocalityComboBox.SelectedItem = startLocality;
            }
            else
            {
                CountryComboBox.SelectedIndex = 0;
                LocalityComboBox.SelectedIndex = 0;
            }
        }

        private void ListOfAnimalsButton_Click(object sender, EventArgs e)
        {
            AnimalRegistryForm form = new AnimalRegistryForm(/*currentLegalPerson*/);
            Hide();
            form.ShowDialog();
            Show();
        }

        private void SaveLegalPersonButton_Click(object sender, EventArgs e)
        {
            LegalPersonDTO currentLegalPersonDTO = new LegalPersonDTO()
            {
                INN = INNText.Text,
                Name = NameText.Text,
                KPP = KPPText.Text,
                Address = AdressText.Text,
                Phone = PhoneText.Text,
                Email = EmailText.Text,
                FkCountry = ((CountryDTO)CountryComboBox.SelectedItem).Id,
                FkLocality = ((LocationDTO)LocalityComboBox.SelectedItem).Id,
            };

            if (mainLegalPerson == null)
            {
                mainLegalPerson = PetOwnersController.AddLegalPerson(currentLegalPersonDTO);
            }
            else
            {
                mainLegalPerson = PetOwnersController.UpdateLegalPerson(currentLegalPersonDTO);
            }
        }
    }
}
