namespace PIS_PetRegistry
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
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