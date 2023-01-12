using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Models;
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
            new KeyValuePair<bool, string>(false, "Девочка"),
            new KeyValuePair<bool, string>(true, "Мальчик"),
        };
        private int _previousIndex;
        private bool _sortDirection;

        private PhysicalPersonDTO _physicalPersonFilter = null;
        private LegalPersonDTO _legalPersonFilter = null;
        private AnimalCardRegistry animalCardRegistry;

        

        public AnimalRegistryForm(PhysicalPersonDTO physicalPerson)
        {
            this.animalCardRegistry = new AnimalCardRegistry();
            InitForm();
            _physicalPersonFilter = physicalPerson;
            UpdateAnimalToFilter();
        }

        public AnimalRegistryForm(LegalPersonDTO legalPersonFilter)
        {
            this.animalCardRegistry = new AnimalCardRegistry();
            InitForm();
            _legalPersonFilter = legalPersonFilter;
            UpdateAnimalToFilter();
        }

        public AnimalRegistryForm()
        {
            this.animalCardRegistry = new AnimalCardRegistry();
            InitForm();
        }

        private void UpdateAnimalToFilter()
        {
            toolStrip1.Enabled = false;
            button1.Enabled = false;
            if (_physicalPersonFilter != null)
            {
                var animals = AnimalCardController.GetAnimalsByPhysicalPerson(_physicalPersonFilter.Id);
                SetListAnimalsInterface(animals);

                this.Text = String.Format("Список животных {0}", _physicalPersonFilter.Name);
            }
            else
            { 
                var animals = AnimalCardController.GetAnimalsByLegalPerson(_legalPersonFilter.Id);
                SetListAnimalsInterface(animals);

                this.Text = String.Format("Список животных {0}", _legalPersonFilter.Name);
            }
        }

        private void InitForm()
        {
            InitializeComponent();
            var defualtAnimalCategory = new AnimalCategoryDTO();
            defualtAnimalCategory.Id = -1;
            defualtAnimalCategory.Name = "Категория";

            _animalCategories = animalCardRegistry.GetAnimalCategories();
            _animalCategories.Insert(0, defualtAnimalCategory);
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

            if (AuthorizationController.GetAuthorizedUser().RoleId != (int)UserRoles.Veterinarian)
            { 
                this.dateTimePickerVetProcedure.Enabled = false;
                this.buttonSearchVetProcedure.Enabled = false;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PetOwnersForm form = new PetOwnersForm();
            form.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //FilterOption form = new FilterOption();
            //form.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //possible exception due to permission fail

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FetchAnimalCards()
        {
            if (IsSelectedFilters())
            {
                var filterParams = this.GenerateFilterDTO();
                _listAnimalCards = animalCardRegistry.GetAnimals(filterParams);
            }
            else
            {
                _listAnimalCards = animalCardRegistry.GetAnimals();
            }

            SetListAnimalsInterface(_listAnimalCards);
        }

        private void buttonClickQuery_Click(object sender, EventArgs e)
        {
            FetchAnimalCards();

            if (_listAnimalCards.Count == 0)
                MessageBox.Show("По вашему запросу нечего не найдено");
        }

        private AnimalFilterDTO GenerateFilterDTO()
        { 
            AnimalFilterDTO filterDTO = new AnimalFilterDTO();

            filterDTO.Name = this.textBoxName.Text;
            filterDTO.ChipId = this.textBoxChipId.Text;
            filterDTO.AnimalCategory = _animalCategories[this.comboBoxCategory.SelectedIndex];

            if (comboBoxSex.SelectedIndex != -1)
            { 
                filterDTO.IsBoy = _animalSexTypes[comboBoxSex.SelectedIndex].Key;
                filterDTO.IsSelectedSex = comboBoxSex.SelectedIndex >= 0;
            }

            filterDTO.LegalPerson = _legalPersonFilter;
            filterDTO.PhysicalPerson = _physicalPersonFilter;

            return filterDTO;
        }

        private Boolean IsSelectedFilters()
        {
            if (this.textBoxName.Text.Length != 0)
                return true;

            if (this.textBoxChipId.Text.Length != 0)
                return true;

            if (this.comboBoxSex.SelectedIndex >= 0)
                return true;

            if (this.comboBoxCategory.SelectedIndex > 0)
                return true;

            if (this._legalPersonFilter != null || this._physicalPersonFilter != null)
                return true;

            return false;
        }

        private void dataGridViewListAnimals_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true;

            _listAnimalCards = SortData(
                (List<AnimalCardDTO>)dataGridViewListAnimals.DataSource, dataGridViewListAnimals.Columns[e.ColumnIndex].Name, _sortDirection);
            dataGridViewListAnimals.DataSource = _listAnimalCards;

            _previousIndex = e.ColumnIndex;
        }

        public List<AnimalCardDTO> SortData(List<AnimalCardDTO> list, string column, bool ascending)
        {
            try
            {
                return ascending ?
                    list.OrderBy(_ => _.GetType().GetProperty(column).GetValue(_)).ToList() :
                    list.OrderByDescending(_ => _.GetType().GetProperty(column).GetValue(_)).ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка сортировки");
                return list;
            }
        }

        private void задатьУсловиеФильтрацииПоПолюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AnimalCardForm form = new AnimalCardForm(animalCardRegistry);
                form.ShowDialog();
                FetchAnimalCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportExcelMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    AnimalCardController.ExportCardsToExcel(filePath, _listAnimalCards);
                }
            }
        }

        private void dataGridViewListAnimals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AnimalCardForm animalForm = new AnimalCardForm(animalCardRegistry, _listAnimalCards[e.RowIndex]);
                animalForm.ShowDialog();
                FetchAnimalCards();
            }
        }

        private void SetListAnimalsInterface(List<AnimalCardDTO> list)
        {
            _listAnimalCards = list;
            dataGridViewListAnimals.DataSource = _listAnimalCards;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filterParams = this.GenerateFilterDTO();
            filterParams.SearchTimeVetProcedure = dateTimePickerVetProcedure.Value;
            
            var animalList = animalCardRegistry.GetAnimals(filterParams);
            SetListAnimalsInterface(animalList);
        }
    }
}
