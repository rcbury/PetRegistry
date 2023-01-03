using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;

namespace PIS_PetRegistry
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            UserDTO? userDTO;
            
            try
            {
                userDTO = AuthorizationController.Authorize(loginTextBox.Text, passwordTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка при подключении к базе данных");
                return;
            }

            if (userDTO == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            AnimalRegistryForm form = new AnimalRegistryForm();

            Hide();
            form.ShowDialog();
            Show();
        }
    }
}