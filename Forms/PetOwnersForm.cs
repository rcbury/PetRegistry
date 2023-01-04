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
    public partial class PetOwnersForm : Form
    {
        List<LegalPersonDTO> listLegalPersonDTOs;
        List<PhysicalPersonDTO> listPhysicalPersonDTOs;
        List<CountryDTO> countries;
        List<LocationDTO> locations;
        public PetOwnersForm()
        {
            InitializeComponent();
            listLegalPersonDTOs = new List<LegalPersonDTO>();
            listPhysicalPersonDTOs = new List<PhysicalPersonDTO>();
            countries = PetOwnersController.GetCountries();
            comboBox1.DataSource = countries;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            locations = PetOwnersController.GetLocations();
            comboBox2.DataSource = locations;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
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
            dataGridView1.Columns.Add(phoneCol);
            dataGridView1.Columns.Add(nameCol);
            dataGridView1.Columns.Add(addressCol);
            dataGridView1.Columns.Add(emailCol);
            dataGridView1.Columns.Add(countryCol);
            dataGridView1.Columns.Add(locationCol);
        }

        private void задатьУсловиеФильтрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var selectedPhysicalPerson = listPhysicalPersonDTOs[rowIndex];
            PhysicalPersonForm form = new PhysicalPersonForm();
            Hide();
            form.ShowDialog();
            Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;

            LegalPersonForm form = new LegalPersonForm(listLegalPersonDTOs[rowIndex]);
            Hide();
            form.ShowDialog();
            Show();
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
    }
}
