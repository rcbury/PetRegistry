using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;

namespace PIS_PetRegistry.Forms
{
    public partial class ParasiteTreatmentForm : Form
    {
        public ParasiteTreatmentForm(int FKAnimal)
        {
            var medications = ParasiteTreatmentController.GetMedications();

            parasiteTreatmentDTO = new ParasiteTreatmentDTO();
            parasiteTreatmentDTO.FkAnimal = FKAnimal;

            InitializeComponent();

            medicationComboBox.DataSource = medications;
            medicationComboBox.ValueMember = "Id";
            medicationComboBox.DisplayMember = "Name";

            FillFields();
        }


        public ParasiteTreatmentForm(ParasiteTreatmentDTO parasiteTreatmentDTO)
        {
            var medications = ParasiteTreatmentController.GetMedications();

            this.parasiteTreatmentDTO = parasiteTreatmentDTO;

            InitializeComponent();

            medicationComboBox.DataSource = medications;
            medicationComboBox.ValueMember = "Id";
            medicationComboBox.DisplayMember = "Name";

            FillFields();
        }

        private ParasiteTreatmentDTO parasiteTreatmentDTO;

        public void FillFields()
        {
            medicationComboBox.SelectedValue = parasiteTreatmentDTO.FkMedication;
            if (parasiteTreatmentDTO.Date.Year != 1)
                parasiteTreatmentDatePicker.Value = DateTime.Parse(parasiteTreatmentDTO.Date.ToShortDateString());
        }

        private void ValidateFields()
        {
            if (medicationComboBox.SelectedValue == null)
                throw new Exception("Не выбран препарат");
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

            if (parasiteTreatmentDTO.FkUser == null)
            {
                var tempParasiteTreatmentDTO = new ParasiteTreatmentDTO
                {
                    FkMedication = int.Parse(medicationComboBox.SelectedValue.ToString()),
                    Date = DateOnly.Parse(parasiteTreatmentDatePicker.Value.ToShortDateString()),
                    FkAnimal = parasiteTreatmentDTO.FkAnimal,
                };

                try
                {
                    parasiteTreatmentDTO = ParasiteTreatmentController.AddParasiteTreatment(tempParasiteTreatmentDTO);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var tempParasiteTreatmentDTO = new ParasiteTreatmentDTO
                {
                    FkUser = parasiteTreatmentDTO.FkUser,
                    FkMedication = int.Parse(medicationComboBox.SelectedValue.ToString()),
                    Date = DateOnly.Parse(parasiteTreatmentDatePicker.Value.ToShortDateString()),
                    FkAnimal = parasiteTreatmentDTO.FkAnimal,
                };

                var authorizedUser = AuthorizationController.GetAuthorizedUser();

                try
                {
                    parasiteTreatmentDTO = ParasiteTreatmentController.UpdateParasiteTreatment(
                        parasiteTreatmentDTO,
                        tempParasiteTreatmentDTO, 
                        authorizedUser);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
