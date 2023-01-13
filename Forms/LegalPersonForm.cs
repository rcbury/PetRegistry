using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using System.Data;

namespace PIS_PetRegistry
{
    public partial class LegalPersonForm : Form
    {
        private LegalPersonDTO? mainLegalPerson;
        private AnimalCardRegistry? mainRegistry;
        AuthorizationController authorizationController;
        public LegalPersonForm(AnimalCardRegistry registry, AuthorizationController authorizationController) : this(selectedLegalPerson: null, registry: registry, authorizationController: authorizationController)
        {

        }
        public LegalPersonForm(AuthorizationController authorizationController, LegalPersonDTO? selectedLegalPerson, bool editAllowed = true, AnimalCardRegistry? registry = null)
        {
            this.authorizationController = authorizationController;
            mainLegalPerson = selectedLegalPerson;
            mainRegistry = registry;

            InitializeComponent();

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

        private void DisableEdit()
        {
            INNText.Enabled = false;
            NameText.Enabled = false;
            KPPText.Enabled = false;
            AdressText.Enabled = false;
            PhoneText.Enabled = false;
            EmailText.Enabled = false;
            CountryComboBox.Enabled = false;
            LocalityComboBox.Enabled = false;
            SaveLegalPersonButton.Enabled = false;
        }

        private void ListOfAnimalsButton_Click(object sender, EventArgs e)
        {
            if (mainLegalPerson == null)
            {
                MessageBox.Show("Это юридическое лицо еще не добавлено в реестр.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var countAnimalsByLegalPerson = mainRegistry.CountAnimalsByLegalPerson(mainLegalPerson.Id);
            if (countAnimalsByLegalPerson == 0)
            {
                MessageBox.Show("У этого юридического лица нет животных.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AnimalRegistryForm form = new AnimalRegistryForm(mainLegalPerson, authorizationController);
                form.ShowDialog();
                Show();
            }
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
                Id = mainLegalPerson != null ? mainLegalPerson.Id : 0
            };

            if (mainLegalPerson == null)
            {
                mainRegistry.AddLegalPerson(currentLegalPersonDTO);
            }
            else
            {
                mainRegistry.UpdateLegalPerson(currentLegalPersonDTO);
            }
            this.Close();
        }

        private void KPPText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void INNText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void PhoneText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
