using PIS_PetRegistry.DTO;
using PIS_PetRegistry.Models;
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
    public partial class LegalPersonForm : Form
    {
        public LegalPersonForm(LegalPersonDTO selectedLegalPerson = null!)
        {
            InitializeComponent();
            if (selectedLegalPerson != null)
            {
                INNText.Text = selectedLegalPerson.INN;
                NameText.Text = selectedLegalPerson.Name;
                KPPText.Text = selectedLegalPerson.KPP;
                AdressText.Text = selectedLegalPerson.Address;
                PhoneText.Text = selectedLegalPerson.Phone;
                EmailText.Text = selectedLegalPerson.Email;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var listAnimalRegistry = new List<AnimalCardDTO>();
            //...
            AnimalRegistryForm form = new AnimalRegistryForm(listAnimalRegistry);
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}
