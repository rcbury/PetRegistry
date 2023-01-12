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

namespace PIS_PetRegistry.Forms
{
    public partial class VeterinaryProcedure : Form
    {
        private AnimalCardRegistry animalCardRegistry;

        public VeterinaryProcedure(AnimalCardRegistry animalCardRegistry, int FKAnimal)
        {
            this.animalCardRegistry = animalCardRegistry;

            veterinaryAppointmentDTO = new VeterinaryAppointmentDTO();
            veterinaryAppointmentDTO.FkAnimal = FKAnimal;

            InitializeComponent();
            FillFields();
        }

        public VeterinaryProcedure(AnimalCardRegistry animalCardRegistry, VeterinaryAppointmentDTO parasiteTreatmentDTO)
        {
            this.animalCardRegistry = animalCardRegistry;

            this.veterinaryAppointmentDTO = parasiteTreatmentDTO;
            this.veterinaryAppointmentDTO.Date = this.veterinaryAppointmentDTO.Date.ToUniversalTime();
            
            InitializeComponent();
            FillFields();
        }

        private VeterinaryAppointmentDTO veterinaryAppointmentDTO;

        public void FillFields()
        {
            veterinaryAppointmentNameTextBox.Text = veterinaryAppointmentDTO.Name;
            if (veterinaryAppointmentDTO.Date.Year != 1)
                veterinaryAppointmentDatePicker.Value = veterinaryAppointmentDTO.Date.ToLocalTime();
            veterinaryAppointmentCompletedCheckBox.Checked = veterinaryAppointmentDTO.IsCompleted;
        }

        private void ValidateFields()
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ValidateFields();

            if (veterinaryAppointmentDTO.FkUser == null)
            {
                var tempVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO
                {
                    Name = veterinaryAppointmentNameTextBox.Text,
                    Date = veterinaryAppointmentDatePicker.Value.ToUniversalTime(),
                    FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                    IsCompleted = veterinaryAppointmentCompletedCheckBox.Checked,
                };

                try
                {
                    veterinaryAppointmentDTO = animalCardRegistry.AddVeterinaryAppointment(
                        tempVeterinaryAppointmentDTO);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var tempVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO
                {
                    FkUser = veterinaryAppointmentDTO.FkUser,
                    Date = veterinaryAppointmentDatePicker.Value.ToUniversalTime(),
                    FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                    Name = veterinaryAppointmentNameTextBox.Text,
                    IsCompleted = veterinaryAppointmentCompletedCheckBox.Checked
                };

                try
                {
                    veterinaryAppointmentDTO = animalCardRegistry.UpdateVeterinaryAppointment(
                        veterinaryAppointmentDTO, 
                        tempVeterinaryAppointmentDTO);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
