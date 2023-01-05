using PIS_PetRegistry.Backend;
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
    public partial class AnimalRegistryForm : Form
    {
        private List<AnimalCardDTO> _listAnimalCards;
        private List<AnimalCategoryDTO> _animalCategories;
        private List<KeyValuePair<bool, string>> _animalSexTypes = new List<KeyValuePair<bool, string>>()
        {
            new KeyValuePair<bool, string>(true, "Мальчик"),
            new KeyValuePair<bool, string>(false, "Девочка"),
        };

        public AnimalRegistryForm(UserDTO user = null!)
        {
            InitializeComponent();
            
            _animalCategories = AnimalCardController.GetAnimalCategories();
            comboBoxCategory.DataSource = _animalCategories;
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";

            var nameCol = new DataGridViewTextBoxColumn();
            nameCol.Name = "Name";
            nameCol.HeaderText = "Кличка";
            nameCol.DataPropertyName = "Name";
            nameCol.ReadOnly = true;
            dataGridViewListAnimals.Columns.Add(nameCol);

            var chipCol = new DataGridViewTextBoxColumn();
            chipCol.Name = "chipId";
            chipCol.HeaderText = "Номер чипа";
            chipCol.DataPropertyName = "chipId";
            chipCol.ReadOnly = true;
            dataGridViewListAnimals.Columns.Add(chipCol);

            var yearCol = new DataGridViewTextBoxColumn();
            yearCol.Name = "YearOfBirth";
            yearCol.HeaderText = "Дата рождения";
            yearCol.DataPropertyName = "YearOfBirth";
            yearCol.ReadOnly = true;
            dataGridViewListAnimals.Columns.Add(yearCol);

            var sexTypeCol = new DataGridViewComboBoxColumn();
            sexTypeCol.DataSource = _animalSexTypes;
            sexTypeCol.HeaderText = "Пол животного";
            sexTypeCol.ValueMember = "Key";
            sexTypeCol.DisplayMember = "Value";
            sexTypeCol.DataPropertyName = "IsBoy";
            sexTypeCol.Name = "IsBoy";
            sexTypeCol.ReadOnly = true;
            dataGridViewListAnimals.Columns.Add(sexTypeCol);

            var typeAnimal = new DataGridViewComboBoxColumn();
            typeAnimal.Name = "AnimalCategory";
            typeAnimal.HeaderText = "Категория животного";
            typeAnimal.DataSource = _animalCategories;
            typeAnimal.ValueMember = "Id";
            typeAnimal.DisplayMember = "Name";
            typeAnimal.DataPropertyName = "FkCategory";
            typeAnimal.ReadOnly = true;
            dataGridViewListAnimals.Columns.Add(typeAnimal);

            dataGridViewListAnimals.AutoGenerateColumns = false;
            dataGridViewListAnimals.AllowUserToAddRows = false;
            dataGridViewListAnimals.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PetOwnersForm form = new PetOwnersForm();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //FilterOption form = new FilterOption();
            //form.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AnimalCardForm form = new AnimalCardForm();
            form.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonClickQuery_Click(object sender, EventArgs e)
        {
            _listAnimalCards = AnimalCardController.GetAnimalsUser(Authorization.AuthorizedUserDto);
            dataGridViewListAnimals.DataSource = _listAnimalCards;

            if (_listAnimalCards.Count == 0)
                MessageBox.Show("По вашему запросу нечего не найдено");
        }

        private void dataGridViewListAnimals_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AnimalCardForm animalForm = new AnimalCardForm(_listAnimalCards[e.RowIndex]);
            animalForm.ShowDialog();
        }
    }
}
