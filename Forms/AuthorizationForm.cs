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
            authorizationController = new AuthorizationController();

            InitializeComponent();
#if DEBUG
            if (s_bDoDebugOnlyCode)
            {
                loginTextBox.Text = "mikhail1";
                passwordTextBox.Text = "mikhail1";
            }
        }
#endif

        private AuthorizationController authorizationController;

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool loginResult = false;

            try
            {
                loginResult = authorizationController.Authorize(loginTextBox.Text, passwordTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка при подключении к базе данных");
                return;
            }

            if (!loginResult)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            AnimalRegistryForm form = new AnimalRegistryForm(authorizationController);

            Hide();
            form.ShowDialog();
            Show();
        }
    }
}