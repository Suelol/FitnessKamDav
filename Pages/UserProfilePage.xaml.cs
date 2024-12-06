using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class UserProfilePage : Page
    {
        private int userId; // ID пользователя

        public UserProfilePage(int id)
        {
            InitializeComponent();
            userId = id;
            LoadUserProfile(); // Вызов метода без пробелов
        }

        private void LoadUserProfile() // Исправлено имя метода
        {
            using (var context = new FitnessClub_Kam_DavEntities())
            {
                // Получение данных о клиенте по ID
                var client = context.Clients.FirstOrDefault(c => c.UserID == userId);

                if (client != null)
                {
                    NameTextBlock.Text = client.FirstName;
                    SurnameTextBlock.Text = client.LastName;
                    EmailTextBlock.Text = client.Email;
                    PhoneTextBlock.Text = client.Phone;
                    RegistrationDateTextBlock.Text = client.RegistrationDate?.ToString("d") ?? "Не указана";
                    GoalsTextBlock.Text = client.Goals ?? "Не указаны";
                    PreferencesTextBlock.Text = client.Preferences ?? "Не указаны";
                    MemberBlockText.Text = client.MembershipTypeID.ToString() ?? "Не купил";
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage(userId));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}