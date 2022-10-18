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
    public partial class PetOwners : Form
    {
        public PetOwners()
        {
            InitializeComponent();
        }

        private void задатьУсловиеФильтрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FilterOption form = new FilterOption();
            form.ShowDialog();
        }
    }
}
