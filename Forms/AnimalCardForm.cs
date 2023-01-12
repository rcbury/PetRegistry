using DocumentFormat.OpenXml.Office2010.Excel;
using PIS_PetRegistry.Backend;
using PIS_PetRegistry.Backend.Models;
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
        /// Opens animal card with existing data
        /// </summary>
        /// <param name="animalCardDTO"></param>
        public AnimalCardForm(AuthorizationController authorizationController, AnimalCardRegistry animalCardRegistry, AnimalCardDTO? animalCardDTO = null)
        {
            this.authorizationController = authorizationController;

            this.animalCardRegistry = animalCardRegistry;
            this.animalCardDTO = animalCardDTO;
            this.parasiteTreatmentsDTO = new List<ParasiteTreatmentDTO>();
            this.physicalLocationsDTO = animalCardRegistry.GetLocations();
            this.legalLocationsDTO = animalCardRegistry.GetLocations();

            InitializeComponent();
            
            SetupComboboxes();
            SetupDGV();
            SetupPermissions();
            Refetch();
            
            FillFields();
        }

        private AuthorizationController authorizationController;
        private AnimalCardRegistry animalCardRegistry;
        private AnimalCardDTO? animalCardDTO;
        private PhysicalPersonDTO? physicalPersonDTO;
        private LegalPersonDTO? legalPersonDTO;
        private List<ParasiteTreatmentDTO> parasiteTreatmentsDTO;
        private List<VeterinaryAppointmentDTO> veterinaryAppointmentsDTO;
        private List<VaccinationDTO> vaccinationsDTO;
        private List<LocationDTO> physicalLocationsDTO;
        private List<LocationDTO> legalLocationsDTO;
        private ContractDTO? contractDTO;
        private bool veterinaryShtukiModificationAllowed = true;

        public void SetupPermissions()
        {
            var authorizedUser = authorizationController.GetAuthorizedUser();

            if (animalCardDTO == null && authorizedUser.RoleId != (int)UserRoles.Veterinarian)
                throw new Exception("Данный пользователь не может создать учетную карточку животного");


            if (authorizedUser.RoleId != (int)UserRoles.Veterinarian)
            {
                animalCategoryComboBox.Enabled = false;
                animalSexComboBox.Enabled = false;
                animalNameTextBox.Enabled = false;
                animalChipIdTextBox.Enabled = false;
                animalCategoryComboBox.Enabled = false;
                animalBirthYearTextBox.Enabled = false;

                saveButton.Enabled = false;
                deleteAnimalCardButton.Enabled = false;
                uploadPictureButton.Enabled = false;
                addParasiteTreatmentButton.Enabled = false;
                addVaccinationButton.Enabled = false;
                addVeterinaryAppointmentButton.Enabled = false;
                veterinaryShtukiModificationAllowed = false;
                

                //Disable owners info

                textBox6.Enabled = false;
                textBox12.Enabled = false;
                button7.Enabled = false;
                checkBox2.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }

            //Enable owners info
            if (authorizedUser.RoleId == (int)UserRoles.ShelterOperator)
            {
                textBox6.Enabled = true;
                textBox12.Enabled = true;
                button7.Enabled = true;
                checkBox2.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
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

            var animalCategories = animalCardRegistry.GetAnimalCategories();

            animalCategoryComboBox.DataSource = animalCategories;
            animalCategoryComboBox.DisplayMember = "Name";
            animalCategoryComboBox.ValueMember = "Id";
            animalCategoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            animalSexComboBox.DataSource = animalSexDict;
            animalSexComboBox.ValueMember = "Key";
            animalSexComboBox.DisplayMember = "Value";
            animalSexComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            physicalLocationCombobox.DataSource = physicalLocationsDTO;
            physicalLocationCombobox.ValueMember = "Id";
            physicalLocationCombobox.DisplayMember = "Name";

            legalLocationCombobox.DataSource = legalLocationsDTO;
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
                
                if (animalBirthYearTextBox.Text == "")
                    tempAnimalCardDTO.YearOfBirth = null;
                else
                    tempAnimalCardDTO.YearOfBirth = int.Parse(animalBirthYearTextBox.Text);

                tempAnimalCardDTO.Name = animalNameTextBox.Text;
                tempAnimalCardDTO.ChipId = animalChipIdTextBox.Text;
                tempAnimalCardDTO.FkCategory = int.Parse(animalCategoryComboBox.SelectedValue.ToString());
                tempAnimalCardDTO.IsBoy = (bool)animalSexComboBox.SelectedValue;
                tempAnimalCardDTO.Photo = animalPictureBox.ImageLocation;

                this.animalCardDTO = animalCardRegistry.AddAnimalCard(tempAnimalCardDTO);

                MessageBox.Show("Карточка добавлена");
            }
            else
            {
                var tempAnimalCardDTO = new AnimalCardDTO();

                if (animalBirthYearTextBox.Text == "")
                    tempAnimalCardDTO.YearOfBirth = null;
                else
                    tempAnimalCardDTO.YearOfBirth = int.Parse(animalBirthYearTextBox.Text);

                tempAnimalCardDTO.Name = animalNameTextBox.Text;
                tempAnimalCardDTO.ChipId = animalChipIdTextBox.Text;

                tempAnimalCardDTO.FkCategory = int.Parse(animalCategoryComboBox.SelectedValue.ToString());
                tempAnimalCardDTO.IsBoy = (bool)animalSexComboBox.SelectedValue;
                tempAnimalCardDTO.Photo = animalPictureBox.ImageLocation;

                tempAnimalCardDTO.FkShelter = animalCardDTO.FkShelter;
                tempAnimalCardDTO.Id = animalCardDTO.Id;

                this.animalCardDTO = animalCardRegistry.UpdateAnimalCard(tempAnimalCardDTO);

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
            var form = new ParasiteTreatmentForm(animalCardRegistry, animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void Refetch()
        {
            if (animalCardDTO != null)
            {
                this.parasiteTreatmentsDTO = animalCardRegistry.GetParasiteTreatmentsByAnimal(animalCardDTO.Id);
                this.veterinaryAppointmentsDTO = animalCardRegistry.GetVeterinaryAppointmentsByAnimal(animalCardDTO.Id);
                this.vaccinationsDTO = animalCardRegistry.GetVaccinationsByAnimal(animalCardDTO.Id);
                
                //TODO:
                //contractDTO = animalCardRegistry.GetContractByAnimal(animalCardDTO.Id);

                if (contractDTO != null)
                {
                    checkBox2.Checked = true;
                    checkBox2.Enabled = false;
                    physicalPersonDTO = animalCardRegistry.GetPhysicalPersonById(contractDTO.FkPhysicalPerson);
                    FillPhysical();
                    if (contractDTO.FkLegalPerson != null)
                    {
                        checkBox1.Checked = true;
                        checkBox1.Enabled = false;
                        legalPersonDTO = animalCardRegistry.GetLegalPersonById(contractDTO.FkLegalPerson);
                        groupBox6.Show();
                        FillLegal();
                    }
                }

                parasiteTreatmentDGV.DataSource = parasiteTreatmentsDTO;
                veterinaryAppointmentDGV.DataSource = veterinaryAppointmentsDTO;
                vaccinationDGV.DataSource = vaccinationsDTO;
                
            }
        }

        private void parasiteTreatmentDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void veterinaryAppointmentDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void addVeterinaryAppointmentButton_Click(object sender, EventArgs e)
        {
            var form = new VeterinaryProcedure(animalCardRegistry, animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void addVaccinationButton_Click(object sender, EventArgs e)
        {
            var form = new VaccinationForm(animalCardRegistry, animalCardDTO.Id);
            form.ShowDialog();
            Refetch();
        }

        private void vaccinationDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
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

            animalCardRegistry.DeleteAnimalCard(animalCardDTO.Id);

            MessageBox.Show("Карточка успешно удалена");

            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var phone = textBox6.Text;
            physicalPersonDTO = animalCardRegistry.GetPhysicalPersonByPhone(phone);
            if (physicalPersonDTO == null)
            {
                MessageBox.Show("Физ. лицо с указанным номером телефона не найдено.");
            }
            else 
            {
                FillPhysical();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var inn = textBox12.Text;
            //TODO:
            //legalPersonDTO = PetOwnersController.GetLegalPersonByINN(inn);
            if (legalPersonDTO == null)
            {
                MessageBox.Show("Юр. лицо с указанным ИНН не найдено.");
            }
            else
            {
                FillLegal();
            }
        }

        private void FillLegal() 
        {
            textBox12.Text = legalPersonDTO.INN;
            textBox13.Text = legalPersonDTO.Name;
            textBox14.Text = legalPersonDTO.KPP;
            textBox15.Text = legalPersonDTO.Address;
            textBox16.Text = legalPersonDTO.Phone;
            textBox17.Text = legalPersonDTO.Email;
            legalLocationCombobox.SelectedValue = legalPersonDTO.FkLocality;
        }

        private void FillPhysical() 
        {
            textBox6.Text = physicalPersonDTO.Phone;
            textBox7.Text = physicalPersonDTO.Email;
            textBox8.Text = physicalPersonDTO.Address;
            textBox9.Text = physicalPersonDTO.Name;
            physicalLocationCombobox.SelectedValue = physicalPersonDTO.FkLocality;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox6.Show();
            }
            else 
            {
                legalPersonDTO = null;
                groupBox6.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.DefaultExt = ".docx";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = saveFileDialog.FileName;
                    animalCardRegistry.MakeContract(filePath, physicalPersonDTO, legalPersonDTO, animalCardDTO);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked && physicalPersonDTO != null) 
            {
                checkBox2.Enabled = false;
                if (legalPersonDTO == null) 
                {
                    checkBox1.Enabled = false;
                    groupBox6.Hide();
                }
                animalCardRegistry.SaveContract(physicalPersonDTO, legalPersonDTO, animalCardDTO); 
            }
        }

        private void parasiteTreatmentDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (ParasiteTreatmentDTO)parasiteTreatmentDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new ParasiteTreatmentForm(animalCardRegistry, clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void vaccinationDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (VaccinationDTO)vaccinationDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new VaccinationForm(animalCardRegistry, clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void veterinaryAppointmentDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!veterinaryShtukiModificationAllowed) return;

            var clickedDTO = (VeterinaryAppointmentDTO)veterinaryAppointmentDGV.Rows[e.RowIndex].DataBoundItem;

            var form = new VeterinaryProcedure(animalCardRegistry, clickedDTO);
            form.ShowDialog();
            Refetch();
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void AnimalCardForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
