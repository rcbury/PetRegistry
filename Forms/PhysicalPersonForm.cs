using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PIS_PetRegistry
{
    
    public partial class PhysicalPersonForm : Form
    {
        private PhysicalPersonDTO? mainPhysicalPerson;
        private Registry? mainRegistry;
        private AuthorizationController authorizationController;

        public PhysicalPersonForm(Registry registry, AuthorizationController authorizationController) : this(selectedPhysicalPerson: null, registry: registry, authorizationController: authorizationController) { }

        public PhysicalPersonForm(AuthorizationController authorizationController, PhysicalPersonDTO? selectedPhysicalPerson, Registry? registry, bool editAllowed = true)
        {
            this.authorizationController = authorizationController;
            InitializeComponent();

            mainRegistry = registry;
            mainPhysicalPerson = selectedPhysicalPerson;
            
            if (!editAllowed)
            {
                DisableEdit();
            }

            CountryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LocalityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            var allCountries = registry.GetCountries();
            var allLocalities = registry.GetLocations();

            CountryComboBox.DataSource = allCountries;
            CountryComboBox.DisplayMember = "Name";
            CountryComboBox.ValueMember = "Id";

            LocalityComboBox.DataSource = allLocalities;
            LocalityComboBox.DisplayMember = "Name";
            LocalityComboBox.ValueMember = "Id";

            if (selectedPhysicalPerson != null)
            {
                NumberText.Text = selectedPhysicalPerson.Phone;
                NameText.Text = selectedPhysicalPerson.Name;
                AdressText.Text = selectedPhysicalPerson.Address;
                EmailText.Text = selectedPhysicalPerson.Email;

                var startCountry = allCountries
                    .Where(x => x.Id.Equals(selectedPhysicalPerson.FkCountry))
                    .FirstOrDefault();

                var startLocality = allLocalities
                    .Where(x => x.Id.Equals(selectedPhysicalPerson.FkLocality))
                    .FirstOrDefault();

                CountryComboBox.SelectedItem = startCountry;
                LocalityComboBox.SelectedItem = startLocality;
            }
            else
            {
                CountryComboBox.SelectedIndex = 0;
                LocalityComboBox.SelectedIndex = 0;
            }
        }

        private void DisableEdit()
        {
            NumberText.Enabled = false;
            NameText.Enabled = false;
            AdressText.Enabled = false;
            EmailText.Enabled = false;
            CountryComboBox.Enabled = false;
            LocalityComboBox.Enabled = false;
            SaveButton.Enabled = false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            PhysicalPersonDTO currentPhysicalPersonDTO = new()
            {
                Name = NameText.Text,
                Phone = NumberText.Text,
                Address = AdressText.Text,
                Email = EmailText.Text,
                FkCountry = ((CountryDTO)CountryComboBox.SelectedItem).Id,
                FkLocality = ((LocationDTO)LocalityComboBox.SelectedItem).Id,
                Id = mainPhysicalPerson != null ? mainPhysicalPerson.Id : 0
            };

            if (mainPhysicalPerson == null)
            {
                mainRegistry.AddPhysicalPerson(currentPhysicalPersonDTO);
            }
            else
            {
                mainRegistry.UpdatePhysicalPerson(currentPhysicalPersonDTO);
            }
            this.Close();
        }

        private void ListAnimalsButton_Click(object sender, EventArgs e)
        {
            if (mainPhysicalPerson == null)
            {
                MessageBox.Show("Этот человек еще не добавлен в реестр.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var countAnimalsByPhysicalPerson = mainRegistry.CountAnimalsByPhysicalPerson(mainPhysicalPerson.Id);
            if (countAnimalsByPhysicalPerson == 0)
            {
                MessageBox.Show("У этого человека нет животных.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AnimalRegistryForm form = new AnimalRegistryForm(mainPhysicalPerson, authorizationController);
                form.ShowDialog();
                Show();
            }


        }

        private void NumberText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
