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
        public PetOwnersForm()
        {
            InitializeComponent();
        }

        private void задатьУсловиеФильтрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LegalPersonForm form = new LegalPersonForm();
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}
