namespace PIS_PetRegistry
{
    public partial class AuthWin : Form
    {
        public AuthWin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainWindow form = new MainWindow();
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}