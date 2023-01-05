using PIS_PetRegistry.Controllers;
using PIS_PetRegistry.DTO;

namespace PIS_PetRegistry
{
    public partial class AuthorizationForm : Form
    {
#if DEBUG
        private /*static*/ bool s_bDoDebugOnlyCode = true;
#endif
        public AuthorizationForm()
        {
            InitializeComponent();
            if (s_bDoDebugOnlyCode)
            {
                loginTextBox.Text = "mikhail1";
                passwordTextBox.Text = "mikhail1";
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            UserDTO? userDTO;



            //try
            //{
                userDTO = AuthorizationController.Authorize(loginTextBox.Text, passwordTextBox.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка при подключении к базе данных");
            //    return;
            //}

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