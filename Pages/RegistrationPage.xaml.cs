using FitnessOsnova_Kam_Dav.DbModel;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class RegistrationPage : Page
    {
        private Users newUser;
        DbModel.Users us = new Users();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, доступна ли навигация
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                // Закрыть приложение, если нельзя вернуться назад
                Application.Current.Shutdown();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string email = EmailTextBox.Text; // Получаем email

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка на совпадение паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new FitnessClub_Kam_DavEntities())
            {
                // Проверка на существование пользователя с таким логином
                var existingUser = context.Users.FirstOrDefault(u => u.Login == login);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    us.Name = name;
                    us.Login = login;
                    us.Password = password;
                    us.Email = email; // Сохраняем email
                                      // Добавление нового пользователя
                    context.Users.Add(us);
                    context.SaveChanges(); // Сохранение изменений в базе данных

                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Перейти на страницу авторизации или на главную страницу
                    NavigationService.Navigate(new AuthPage());
                }
                catch (DbUpdateException ex)
                {
                    // Выводим больше информации об ошибке
                    var innerException = ex.InnerException?.InnerException;
                    string errorMessage = innerException != null ? innerException.Message : ex.Message;
                    MessageBox.Show($"Ошибка при обновлении базы данных: {errorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
    }
