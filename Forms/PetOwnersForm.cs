using ClosedXML.Excel;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIS_PetRegistry
{
    public partial class PetOwnersForm : Form
    {
        List<LegalPersonDTO> listLegalPersonDTOs;
        List<PhysicalPersonDTO> listPhysicalPersonDTOs;
        List<CountryDTO> countries;
        List<LocationDTO> locations;
        private int _physicalPreviousIndex;
        private bool _physicalSortDirection;
        private int _legalPreviousIndex;
        private bool _legalSortDirection;
        public PetOwnersForm()
        {
            InitializeComponent();
            SetupPermissions();

            listLegalPersonDTOs = new List<LegalPersonDTO>();
            listPhysicalPersonDTOs = new List<PhysicalPersonDTO>();
            countries = PetOwnersController.GetCountries();
            comboBox1.DataSource = countries;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox4.DataSource = countries;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "Id";
            locations = PetOwnersController.GetLocations();
            comboBox2.DataSource = locations;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
            comboBox3.DataSource = locations;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = listPhysicalPersonDTOs;
            var phoneCol = new DataGridViewTextBoxColumn();
            phoneCol.Name = "Phone";
            phoneCol.HeaderText = "Номер телефона";
            phoneCol.DataPropertyName = "Phone";
            var nameCol = new DataGridViewTextBoxColumn();
            nameCol.Name = "Name";
            nameCol.HeaderText = "ФИО";
            nameCol.DataPropertyName = "Name";
            var addressCol = new DataGridViewTextBoxColumn();
            addressCol.Name = "Address";
            addressCol.HeaderText = "Фактический адрес проживания";
            addressCol.DataPropertyName = "Address";
            var emailCol = new DataGridViewTextBoxColumn();
            emailCol.Name = "Email";
            emailCol.HeaderText = "Адрес эл. почты";
            emailCol.DataPropertyName = "Email";
            var countryCol = new DataGridViewComboBoxColumn();
            countryCol.Name = "Country";
            countryCol.HeaderText = "Страна";
            countryCol.DataSource = countries;
            countryCol.ValueMember = "Id";
            countryCol.DisplayMember = "Name";
            countryCol.DataPropertyName = "FkCountry";
            var locationCol = new DataGridViewComboBoxColumn();
            locationCol.Name = "Location";
            locationCol.HeaderText = "Населенный пункт";
            locationCol.DataSource = locations;
            locationCol.ValueMember = "Id";
            locationCol.DisplayMember = "Name";
            locationCol.DataPropertyName = "FkLocality";
            var animalCountCol = new DataGridViewTextBoxColumn();
            animalCountCol.Name = "AnimalCount";
            animalCountCol.HeaderText = "Кол-во животных";
            animalCountCol.DataPropertyName = "AnimalCount";
            var catCountCol = new DataGridViewTextBoxColumn();
            catCountCol.Name = "CatCount";
            catCountCol.HeaderText = "Кол-во кошек/котов";
            catCountCol.DataPropertyName = "CatCount";
            var dogCountCol = new DataGridViewTextBoxColumn();
            dogCountCol.Name = "DogCount";
            dogCountCol.HeaderText = "Кол-во собак/псов";
            dogCountCol.DataPropertyName = "DogCount";
            dataGridView1.Columns.Add(phoneCol);
            dataGridView1.Columns.Add(nameCol);
            dataGridView1.Columns.Add(addressCol);
            dataGridView1.Columns.Add(emailCol);
            dataGridView1.Columns.Add(countryCol);
            dataGridView1.Columns.Add(locationCol);
            dataGridView1.Columns.Add(animalCountCol);
            dataGridView1.Columns.Add(catCountCol);
            dataGridView1.Columns.Add(dogCountCol);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = listLegalPersonDTOs;
            var legalAddressCol = new DataGridViewTextBoxColumn();
            legalAddressCol.Name = "Address";
            legalAddressCol.HeaderText = "Адрес";
            legalAddressCol.DataPropertyName = "Address";
            var innCol = new DataGridViewTextBoxColumn();
            innCol.Name = "Inn";
            innCol.HeaderText = "ИНН";
            innCol.DataPropertyName = "INN";
            var kppCol = new DataGridViewTextBoxColumn();
            kppCol.Name = "Kpp";
            kppCol.HeaderText = "КПП";
            kppCol.DataPropertyName = "KPP";
            var legalNameCol = new DataGridViewTextBoxColumn();
            legalNameCol.HeaderText = "Наименование организации";
            legalNameCol.DataPropertyName = "Name";
            legalNameCol.Name = "Name";
            var legalEmailCol = new DataGridViewTextBoxColumn();
            legalEmailCol.HeaderText = "Адрес эл. почты";
            legalEmailCol.DataPropertyName = "Email";
            legalEmailCol.Name = "Email";
            var legalPhoneCol = new DataGridViewTextBoxColumn();
            legalPhoneCol.HeaderText = "Номер телефона";
            legalPhoneCol.DataPropertyName = "Phone";
            legalPhoneCol.Name = "Phone";
            var legalCountryCol = new DataGridViewComboBoxColumn();
            legalCountryCol.Name = "Country";
            legalCountryCol.HeaderText = "Страна";
            legalCountryCol.DataSource = countries;
            legalCountryCol.ValueMember = "Id";
            legalCountryCol.DisplayMember = "Name";
            legalCountryCol.DataPropertyName = "FkCountry";
            var legalLocationCol = new DataGridViewComboBoxColumn();
            legalLocationCol.Name = "Location";
            legalLocationCol.HeaderText = "Населенный пункт";
            legalLocationCol.DataSource = locations;
            legalLocationCol.ValueMember = "Id";
            legalLocationCol.DisplayMember = "Name";
            legalLocationCol.DataPropertyName = "FkLocality";
            var legalAnimalCountCol = new DataGridViewTextBoxColumn();
            legalAnimalCountCol.Name = "AnimalCount";
            legalAnimalCountCol.HeaderText = "Кол-во животных";
            legalAnimalCountCol.DataPropertyName = "AnimalCount";
            var legalCatCountCol = new DataGridViewTextBoxColumn();
            legalCatCountCol.Name = "CatCount";
            legalCatCountCol.HeaderText = "Кол-во кошек/котов";
            legalCatCountCol.DataPropertyName = "CatCount";
            var legalDogCountCol = new DataGridViewTextBoxColumn();
            legalDogCountCol.Name = "DogCount";
            legalDogCountCol.HeaderText = "Кол-во собак/псов";
            legalDogCountCol.DataPropertyName = "DogCount";
            dataGridView2.Columns.Add(innCol);
            dataGridView2.Columns.Add(kppCol);
            dataGridView2.Columns.Add(legalNameCol);
            dataGridView2.Columns.Add(legalAddressCol);
            dataGridView2.Columns.Add(legalEmailCol);
            dataGridView2.Columns.Add(legalPhoneCol);
            dataGridView2.Columns.Add(legalCountryCol);
            dataGridView2.Columns.Add(legalLocationCol);
            dataGridView2.Columns.Add(legalAnimalCountCol);
            dataGridView2.Columns.Add(legalCatCountCol);
            dataGridView2.Columns.Add(legalDogCountCol);
        }

        private bool editAllowed = true;

        private void SetupPermissions()
        {
            var authorizedUser = AuthorizationController.GetAuthorizedUser();

            if (authorizedUser.RoleId != (int)UserRoles.Veterinarian && authorizedUser.RoleId != (int)UserRoles.ShelterOperator)
            {
                button3.Enabled = false;
                button4.Enabled = false;
                editAllowed = false;
            }

            if (authorizedUser.RoleId != (int)UserRoles.VetServiceStaff)
            {
                comboBox3.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void задатьУсловиеФильтрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*var rowIndex = e.RowIndex;
            var selectedPhysicalPerson = listPhysicalPersonDTOs[rowIndex];
            PhysicalPersonForm form = new PhysicalPersonForm(selectedPhysicalPerson);
            Hide();
            form.ShowDialog();
            Show();*/
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*var rowIndex = e.RowIndex;

            LegalPersonForm form = new LegalPersonForm(listLegalPersonDTOs[rowIndex]);
            Hide();
            form.ShowDialog();
            Show();*/
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var phone = textBox1.Text;
            var name = textBox3.Text;
            var address = textBox4.Text;
            var email = textBox2.Text;
            var country = Convert.ToInt16(comboBox1.SelectedValue);
            var location = Convert.ToInt16(comboBox2.SelectedValue);
            listPhysicalPersonDTOs = PetOwnersController.GetPhysicalPeople(phone, name, address, email, country, location);
            dataGridView1.DataSource = listPhysicalPersonDTOs;
        }

        private List<PhysicalPersonDTO> SortData(List<PhysicalPersonDTO> list, string column, bool ascending)
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

        private List<LegalPersonDTO> SortData(List<LegalPersonDTO> list, string column, bool ascending)
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

        private void button2_Click(object sender, EventArgs e)
        {
            var inn = textBox8.Text;
            var kpp = textBox6.Text;
            var name = textBox5.Text;
            var email = textBox7.Text;
            var address = textBox9.Text;
            var phone = textBox10.Text;
            var country = Convert.ToInt16(comboBox4.SelectedValue);
            var location = Convert.ToInt16(comboBox3.SelectedValue);
            listLegalPersonDTOs = PetOwnersController.GetLegalPeople(inn,kpp,name,email,address,phone,country,location);
            dataGridView2.DataSource = listLegalPersonDTOs;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    PetOwnersController.ExportPhysicalPeopleToExcel(filePath, listPhysicalPersonDTOs);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    PetOwnersController.ExportLegalPeopleToExcel(filePath, listLegalPersonDTOs);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PhysicalPersonForm form = new();
            form.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var selectedPhysicalPerson = listPhysicalPersonDTOs[rowIndex];
            PhysicalPersonForm form = new PhysicalPersonForm(selectedPhysicalPerson, editAllowed);

            form.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LegalPersonForm form = new();
            form.ShowDialog();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var selectedLegalPerson = listLegalPersonDTOs[rowIndex];
            LegalPersonForm form = new(selectedLegalPerson, editAllowed);

            form.ShowDialog();
            Show();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _physicalPreviousIndex)
                _physicalSortDirection ^= true;

            dataGridView1.DataSource = SortData(
                (List<PhysicalPersonDTO>)dataGridView1.DataSource, dataGridView1.Columns[e.ColumnIndex].Name, _physicalSortDirection);

            _physicalPreviousIndex = e.ColumnIndex;
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _legalPreviousIndex)
                _legalSortDirection ^= true;

            dataGridView2.DataSource = SortData(
                (List<LegalPersonDTO>)dataGridView2.DataSource, dataGridView2.Columns[e.ColumnIndex].Name, _legalSortDirection);

            _legalPreviousIndex = e.ColumnIndex;
        }
    }
}
