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
        public VeterinaryProcedure(int FKAnimal)
        {
            veterinaryAppointmentDTO = new VeterinaryAppointmentDTO();
            veterinaryAppointmentDTO.FkAnimal = FKAnimal;

            InitializeComponent();
            FillFields();
        }

        public VeterinaryProcedure(VeterinaryAppointmentDTO parasiteTreatmentDTO)
        {
            this.veterinaryAppointmentDTO = parasiteTreatmentDTO;

            InitializeComponent();
            FillFields();
        }

        private VeterinaryAppointmentDTO veterinaryAppointmentDTO;

        public void FillFields()
        {
            veterinaryAppointmentNameTextBox.Text = veterinaryAppointmentDTO.Name;
            if (veterinaryAppointmentDTO.Date.Year != 1)
                veterinaryAppointmentDatePicker.Value = DateTime.Parse(veterinaryAppointmentDTO.Date.ToShortDateString());
            veterinaryAppointmentCompletedCheckBox.Checked = veterinaryAppointmentDTO.IsCompleted;
        }

        private void ValidateFields()
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ValidateFields();

            if (veterinaryAppointmentDTO.Id == null)
            {
                var tempVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO
                {
                    Name = veterinaryAppointmentNameTextBox.Text,
                    Date = DateOnly.Parse(veterinaryAppointmentDatePicker.Value.ToShortDateString()),
                    FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                    IsCompleted = veterinaryAppointmentCompletedCheckBox.Checked,
                };

                var authorizedUser = AuthorizationController.GetAuthorizedUser();

                veterinaryAppointmentDTO = VeterinaryAppointmentController.AddVeterinaryAppointment(tempVeterinaryAppointmentDTO, authorizedUser);
            }
            else
            {
                var tempVeterinaryAppointmentDTO = new VeterinaryAppointmentDTO
                {
                    Id = veterinaryAppointmentDTO.Id,
                    FkUser = veterinaryAppointmentDTO.FkUser,
                    Date = DateOnly.Parse(veterinaryAppointmentDatePicker.Value.ToShortDateString()),
                    FkAnimal = veterinaryAppointmentDTO.FkAnimal,
                    Name = veterinaryAppointmentNameTextBox.Text,
                    IsCompleted = veterinaryAppointmentCompletedCheckBox.Checked
                };

                var authorizedUser = AuthorizationController.GetAuthorizedUser();

                veterinaryAppointmentDTO = VeterinaryAppointmentController.UpdateVeterinaryAppointment(tempVeterinaryAppointmentDTO, authorizedUser);
            }
        }
    }
}
