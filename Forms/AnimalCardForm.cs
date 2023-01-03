using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIS_PetRegistry
{
    public partial class AnimalCardForm : Form
    {
        /// <summary>
        /// opens empty animal card
        /// </summary>
        public AnimalCardForm() : this(null)
        {

        }

        /// <summary>
        /// Opens animal card with existing data
        /// </summary>
        /// <param name="animalCardDTO"></param>
        public AnimalCardForm(AnimalCardDTO? animalCardDTO)
        {
            this.animalCardDTO = animalCardDTO;

            var animalSexDict = new List<KeyValuePair<bool, string>>()
            {
                new KeyValuePair<bool, string>(true, "м"),
                new KeyValuePair<bool, string>(false, "ж"),
            };


            var animalCategories = AnimalCardController.GetAnimalCategories();

            InitializeComponent();

            animalCategoryComboBox.DataSource = animalCategories;
            animalCategoryComboBox.DisplayMember = "Name";
            animalCategoryComboBox.ValueMember = "Id";

            animalSexComboBox.DataSource = animalSexDict;
            animalSexComboBox.ValueMember = "Key";
            animalSexComboBox.DisplayMember = "Value";


            FillFields();
        }

        private AnimalCardDTO? animalCardDTO;

        public void FillFields()
        {
            if (animalCardDTO == null) return;

            animalCategoryComboBox.SelectedValue = animalCardDTO.FkCategory;
            animalSexComboBox.SelectedValue = animalCardDTO.IsBoy;

            animalNameTextBox.Text = animalCardDTO.Name;
            animalChipIdTextBox.Text = animalCardDTO.ChipId;
            animalBirthYearTextBox.Text = animalCardDTO.YearOfBirth.ToString();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (animalCardDTO == null)
            {
                var tempAnimalCardDTO = new AnimalCardDTO();

                tempAnimalCardDTO.Name = animalNameTextBox.Text;
                tempAnimalCardDTO.ChipId = animalChipIdTextBox.Text;
                tempAnimalCardDTO.FkCategory = int.Parse(animalCategoryComboBox.SelectedValue.ToString());
                tempAnimalCardDTO.IsBoy = (bool)animalSexComboBox.SelectedValue;
                tempAnimalCardDTO.YearOfBirth = int.Parse(animalBirthYearTextBox.Text);
                tempAnimalCardDTO.Photo = "";

                var authorizedUser = AuthorizationController.GetAuthorizedUser();

                this.animalCardDTO = AnimalCardController.AddAnimalCard(tempAnimalCardDTO, authorizedUser);

                MessageBox.Show("Карточка добавлена");
            }
            else
            {
                var tempAnimalCardDTO = new AnimalCardDTO();

                tempAnimalCardDTO.Name = animalNameTextBox.Text;
                tempAnimalCardDTO.ChipId = animalChipIdTextBox.Text;
                tempAnimalCardDTO.FkCategory = int.Parse(animalCategoryComboBox.SelectedValue.ToString());
                tempAnimalCardDTO.IsBoy = (bool)animalSexComboBox.SelectedValue;
                tempAnimalCardDTO.YearOfBirth = int.Parse(animalBirthYearTextBox.Text);
                tempAnimalCardDTO.Photo = "";

                tempAnimalCardDTO.FkShelter = animalCardDTO.FkShelter;
                tempAnimalCardDTO.Id = animalCardDTO.Id;

                var authorizedUser = AuthorizationController.GetAuthorizedUser();

                this.animalCardDTO = AnimalCardController.UpdateAnimalCard(tempAnimalCardDTO, authorizedUser);

                MessageBox.Show("Карточка изменена");
            }
        }

        private void ValidateFields()
        {
            if (animalCategoryComboBox.SelectedValue == null)
                throw new Exception("Не выбрана категория животного");

            if (animalSexComboBox.SelectedValue == null)
                throw new Exception("Не выбран пол животного");
        }

        private void animalBirthYearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
