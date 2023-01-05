﻿using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
            this.parasiteTreatmentsDTO = new List<ParasiteTreatmentDTO>();
            this.locationsDTO = PetOwnersController.GetLocations();

            InitializeComponent();
            
            SetupComboboxes();
            Refetch();
            SetupDGV();
            
            FillFields();
        }

        private AnimalCardDTO? animalCardDTO;
        private PhysicalPersonDTO? physicalPersonDTO;
        private LegalPersonDTO? legalPersonDTO;
        private List<ParasiteTreatmentDTO> parasiteTreatmentsDTO;
        private List<VeterinaryAppointmentDTO> veterinaryAppointmentsDTO;
        private List<VaccinationDTO> vaccinationsDTO;
        private List<LocationDTO> locationsDTO;
        private bool veterinaryShtukiModificationAllowed = true;

        public void SetupPermissions()
        {
            var authorizedUser = AuthorizationController.GetAuthorizedUser();

            if (animalCardDTO == null && authorizedUser.RoleId != (int)UserRoles.Veterinarian)
                throw new Exception("Данный пользователь не может создать учетную карточку животного");


            if (authorizedUser.RoleId != (int)UserRoles.Veterinarian)
            {
                animalCategoryComboBox.Enabled = false;
                animalSexComboBox.Enabled = false;
                animalNameTextBox.Enabled = false;
                animalChipIdTextBox.Enabled = false;
                animalCategoryComboBox.Enabled = false;

                saveButton.Enabled = false;
                uploadPictureButton.Enabled = false;
                addParasiteTreatmentButton.Enabled = false;
                addVaccinationButton.Enabled = false;
                addVeterinaryAppointmentButton.Enabled = false;
                veterinaryShtukiModificationAllowed = false;

                //TODO: Disable owners info
            }


        }

        public void FillFields()
        {
            if (animalCardDTO == null) return;

            animalCategoryComboBox.SelectedValue = animalCardDTO.FkCategory;
            animalSexComboBox.SelectedValue = animalCardDTO.IsBoy;

            animalNameTextBox.Text = animalCardDTO.Name;
            animalChipIdTextBox.Text = animalCardDTO.ChipId;
            animalBirthYearTextBox.Text = animalCardDTO.YearOfBirth.ToString();

            animalPictureBox.ImageLocation = animalCardDTO.Photo;
        }

        private void SetupVaccinationDGV()
        {
            vaccinationDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "Id",
                Visible = false,
            });
            vaccinationDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UserName",
                HeaderText = "Ветврач"
            });
            vaccinationDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DateEnd",
                HeaderText = "Дата проведения"
            });
            vaccinationDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "VaccineName",
                HeaderText = "Название вакцины"
            });
            vaccinationDGV.AutoGenerateColumns = false;
            vaccinationDGV.ReadOnly = true;
            vaccinationDGV.DataSource = vaccinationsDTO;
        }
        
        private void SetupVeterinaryAppointmentDGV()
        {
            veterinaryAppointmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "Id",
                Visible = false,
            });
            veterinaryAppointmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UserName",
                HeaderText = "Ветврач"
            });
            veterinaryAppointmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Date",
                HeaderText = "Запланированная дата проведения"
            });
            veterinaryAppointmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Name",
                HeaderText = "Название процедуры"
            });
            veterinaryAppointmentDGV.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "IsCompleted",
                HeaderText = "Факт проведения"
            });
            veterinaryAppointmentDGV.AutoGenerateColumns = false;
            veterinaryAppointmentDGV.ReadOnly = true;
            veterinaryAppointmentDGV.DataSource = parasiteTreatmentsDTO;
        }
        
        private void SetupParasiteTreatmentDGV()
        {
            parasiteTreatmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "Id",
                Visible = false,
            });
            parasiteTreatmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UserName",
                HeaderText = "Ветврач"
            });
            parasiteTreatmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Date",
                HeaderText = "Дата проведения"
            });
            parasiteTreatmentDGV.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MedicationName",
                HeaderText = "Название препарата"
            });
            parasiteTreatmentDGV.AutoGenerateColumns = false;
            parasiteTreatmentDGV.ReadOnly = true;
            parasiteTreatmentDGV.DataSource = parasiteTreatmentsDTO;
        }

        private void SetupDGV()
        {
            SetupParasiteTreatmentDGV();
            SetupVeterinaryAppointmentDGV();
            SetupVaccinationDGV();
        }

        private void SetupComboboxes()
        {
            var animalSexDict = new List<KeyValuePair<bool, string>>()
            {
                new KeyValuePair<bool, string>(true, "м"),
                new KeyValuePair<bool, string>(false, "ж"),
            };

            var animalCategories = AnimalCardController.GetAnimalCategories();

            animalCategoryComboBox.DataSource = animalCategories;
            animalCategoryComboBox.DisplayMember = "Name";
            animalCategoryComboBox.ValueMember = "Id";

            animalSexComboBox.DataSource = animalSexDict;
            animalSexComboBox.ValueMember = "Key";
            animalSexComboBox.DisplayMember = "Value";

            physicalLocationCombobox.DataSource = locationsDTO;
            physicalLocationCombobox.ValueMember = "Id";
            physicalLocationCombobox.DisplayMember = "Name";

            legalLocationCombobox.DataSource = locationsDTO;
            legalLocationCombobox.ValueMember = "Id";
            legalLocationCombobox.DisplayMember = "Name";
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
                tempAnimalCardDTO.Photo = animalPictureBox.ImageLocation;

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

        private void addParasiteTreatmentButton_Click(object sender, EventArgs e)
        {
            var form = new ParasiteTreatmentForm(animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void Refetch()
        {
            if (animalCardDTO != null)
            {
                this.parasiteTreatmentsDTO = ParasiteTreatmentController.GetParasiteTreatmentsByAnimal(animalCardDTO.Id);
                this.veterinaryAppointmentsDTO = VeterinaryAppointmentController.GetVeterinaryAppointmentsByAnimal(animalCardDTO.Id);
                this.vaccinationsDTO = VaccinationController.GetVaccinationsByAnimal(animalCardDTO.Id);

                parasiteTreatmentDGV.DataSource = parasiteTreatmentsDTO;
                veterinaryAppointmentDGV.DataSource = veterinaryAppointmentsDTO;
                vaccinationDGV.DataSource = vaccinationsDTO;
            }
        }

        private void parasiteTreatmentDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (ParasiteTreatmentDTO)parasiteTreatmentDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new ParasiteTreatmentForm(clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void veterinaryAppointmentDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (VeterinaryAppointmentDTO)veterinaryAppointmentDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new VeterinaryProcedure(clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void addVeterinaryAppointmentButton_Click(object sender, EventArgs e)
        {
            var form = new VeterinaryProcedure(animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void addVaccinationButton_Click(object sender, EventArgs e)
        {
            var form = new VaccinationForm(animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void vaccinationDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (VaccinationDTO)vaccinationDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new VaccinationForm(clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.animalCardDTO == null && tabControl1.SelectedIndex != 0)
            {
                MessageBox.Show("Сначала сохраните учетную карточку");
                tabControl1.SelectedIndex = 0;
            }
        }

        private void uploadPictureButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    var filePath = openFileDialog.FileName;

                    animalPictureBox.ImageLocation = filePath;
                }
            }
        }

        private void deleteAnimalCardButton_Click(object sender, EventArgs e)
        {
            var confirmationResult = MessageBox.Show(
                "Вы действительно хотите удалить учетную карточку?", 
                "Подтверждение удаления", 
                MessageBoxButtons.YesNo);

            if (confirmationResult == DialogResult.No)
                return;

            if (animalCardDTO == null)
                this.Close();

            var authorizedUser = AuthorizationController.GetAuthorizedUser();

            AnimalCardController.DeleteAnimalCard(this.animalCardDTO, authorizedUser);

            MessageBox.Show("Карточка успешно удалена");

            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var phone = textBox6.Text;
            physicalPersonDTO = AnimalCardController.GetPhysicalPersonByPhone(phone);
            if (physicalPersonDTO == null)
            {
                MessageBox.Show("Физ. лицо с указанным номером телефона не найдено.");
            }
            else 
            {
                textBox7.Text = physicalPersonDTO.Email;
                textBox8.Text = physicalPersonDTO.Address;
                textBox9.Text = physicalPersonDTO.Name;
                physicalLocationCombobox.SelectedValue = physicalPersonDTO.FkLocality;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var inn = textBox12.Text;
            legalPersonDTO = AnimalCardController.GetLegalPersonByINN(inn);
            if (legalPersonDTO == null)
            {
                MessageBox.Show("Юр. лицо с указанным ИНН не найдено.");
            }
            else
            {
                textBox13.Text = legalPersonDTO.Name;
                textBox14.Text = legalPersonDTO.KPP;
                textBox15.Text = legalPersonDTO.Address;
                textBox16.Text = legalPersonDTO.Phone;
                textBox17.Text = legalPersonDTO.Email;
                legalLocationCombobox.SelectedValue = legalPersonDTO.FkLocality;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox6.Show();
            }
            else 
            {
                groupBox6.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var currentUser = AuthorizationController.GetAuthorizedUser();
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.DefaultExt = ".docx";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    AnimalCardController.MakeContract(filePath, physicalPersonDTO, legalPersonDTO, animalCardDTO, currentUser);
                }
            }
        }
    }
}
