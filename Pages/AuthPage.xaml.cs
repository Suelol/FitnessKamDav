using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            using (FitnessClub_Kam_DavEntities db = new FitnessClub_Kam_DavEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == username && u.Password == password);

                if (user != null)
                {
                    CurrentUser.ID = user.UserID;
                    // User authenticated, navigate to main page
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to registration page
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Qr_kod qr = new Qr_kod();
            qr.Show();
        }
    }
}
