using FitnessOsnova_Kam_Dav.DbModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class EditProfilePage : Page
    {
        private int userId; // ID пользователя
        private Clients currentClient; // Текущий клиент для редактирования

        public EditProfilePage(int id)
        {
            InitializeComponent();
            userId = id;
            LoadUserProfile();
        }

        // Метод для загрузки данных пользователя
        private void LoadUserProfile()
        {
            var client = ConnectionClass.connect.Clients.FirstOrDefault(c => c.UserID == userId);

            if (client != null)
            {
                currentClient = client;

                // Заполняем поля для редактирования данными из базы
                FirstNameTextBox.Text = client.FirstName;
                LastNameTextBox.Text = client.LastName;
                EmailTextBox.Text = client.Email;
                PhoneTextBox.Text = client.Phone;
                GoalsTextBox.Text = client.Goals ?? "";
                PreferencesTextBox.Text = client.Preferences ?? "";
            }
            else
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для сохранения изменений
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на валидность email и телефона
            if (!IsValidEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Неверный формат email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPhone(PhoneTextBox.Text))
            {
                MessageBox.Show("Неверный формат телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Обновляем данные клиента в базе
            currentClient.FirstName = FirstNameTextBox.Text;
            currentClient.LastName = LastNameTextBox.Text;
            currentClient.Email = EmailTextBox.Text;
            currentClient.Phone = PhoneTextBox.Text;
            currentClient.Goals = GoalsTextBox.Text;
            currentClient.Preferences = PreferencesTextBox.Text;

            try
            {
                ConnectionClass.connect.SaveChanges(); // Сохраняем изменения в базе
                MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для проверки валидности email
        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        // Метод для проверки валидности телефона
        private bool IsValidPhone(string phone)
        {
            var phoneRegex = new System.Text.RegularExpressions.Regex(@"^\+?[0-9]{10,15}$");
            return phoneRegex.IsMatch(phone);
        }

        // Кнопка "Отмена" для возврата на страницу профиля
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfilePage(userId));
        }
    }
}
