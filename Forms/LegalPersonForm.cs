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
        public LegalPersonForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnimalRegistryForm form = new AnimalRegistryForm();
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}
