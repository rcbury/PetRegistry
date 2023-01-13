using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIS_PetRegistry.Backend.Models;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;

namespace PIS_PetRegistry.Forms
{
    public partial class VaccinationForm : Form
    {
        private Registry animalCardRegistry;

        public VaccinationForm(Registry animalCardRegistry, int FKAnimal)
        {
            this.animalCardRegistry = animalCardRegistry;

            var vaccines = animalCardRegistry.GetVaccines();

            vaccinationDTO = new VaccinationDTO();
            vaccinationDTO.FkAnimal = FKAnimal;

            InitializeComponent();

            vaccineComboBox.DataSource = vaccines;
            vaccineComboBox.ValueMember = "Id";
            vaccineComboBox.DisplayMember = "Name";
            vaccineComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            FillFields();
        }


        public VaccinationForm(Registry animalCardRegistry, VaccinationDTO vaccinationDTO)
        {
            this.animalCardRegistry = animalCardRegistry;

            var vaccines = animalCardRegistry.GetVaccines();

            this.vaccinationDTO = vaccinationDTO;

            InitializeComponent();

            vaccineComboBox.DataSource = vaccines;
            vaccineComboBox.ValueMember = "Id";
            vaccineComboBox.DisplayMember = "Name";
            vaccineComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            FillFields();
        }

        private VaccinationDTO vaccinationDTO;

        public void FillFields()
        {
            vaccineComboBox.SelectedValue = vaccinationDTO.FkVaccine;
            if (vaccinationDTO.DateEnd.Year != 1)
                vaccinationDatePicker.Value = DateTime.Parse(vaccinationDTO.DateEnd.ToShortDateString());
        }

        private void ValidateFields()
        {
            if (vaccineComboBox.SelectedValue == null)
                throw new Exception("Не выбрана вакцина");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ValidateFields();

            if (vaccinationDTO.FkUser == null)
            {
                var tempVaccinationDTO = new VaccinationDTO()
                {
                    FkVaccine = int.Parse(vaccineComboBox.SelectedValue.ToString()),
                    DateEnd = DateOnly.Parse(vaccinationDatePicker.Value.ToShortDateString()),
                    FkAnimal = vaccinationDTO.FkAnimal,
                };

                try
                {
                    vaccinationDTO = animalCardRegistry.AddVaccination(tempVaccinationDTO);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var tempVaccinationDTO = new VaccinationDTO()
                {
                    FkUser = vaccinationDTO.FkUser,
                    FkVaccine = int.Parse(vaccineComboBox.SelectedValue.ToString()),
                    DateEnd = DateOnly.Parse(vaccinationDatePicker.Value.ToShortDateString()),
                    FkAnimal = vaccinationDTO.FkAnimal,
                };

                try
                {
                    vaccinationDTO = animalCardRegistry.UpdateVaccination(vaccinationDTO, tempVaccinationDTO);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
