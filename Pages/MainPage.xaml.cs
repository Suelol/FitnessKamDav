using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу профиля пользователя
            NavigationService.Navigate(new UserProfilePage(CurrentUser.ID));
        }

        private void TrainingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу тренировок
            NavigationService.Navigate(new TrainingsPage(CurrentUser.ID));
        }

        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу отзывов
            NavigationService.Navigate(new FeedbackPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Выход из системы и переход на страницу авторизации
            NavigationService.Navigate(new AuthPage());
        }
    }
}