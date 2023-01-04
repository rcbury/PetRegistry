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

namespace PIS_PetRegistry
{
    public partial class PetOwnersForm : Form
    {
        List<LegalPersonDTO> listLegalPersonDTOs;
        List<PhysicalPersonDTO> listPhysicalPersonDTOs;
        public PetOwnersForm()
        {
            InitializeComponent();
            listLegalPersonDTOs = new List<LegalPersonDTO>();
            listPhysicalPersonDTOs = new List<PhysicalPersonDTO>();
        }

        private void задатьУсловиеФильтрацииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var selectedPhysicalPerson = listPhysicalPersonDTOs[rowIndex];

            PhysicalPersonForm form = new PhysicalPersonForm();
            this.Hide();
            form.ShowDialog();
            Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;

            LegalPersonForm form = new LegalPersonForm(listLegalPersonDTOs[rowIndex]);
            Hide();
            form.ShowDialog();
            Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
