using FitnessOsnova_Kam_Dav.DbModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class TrainingsPage : Page
    {
        private int userId; // ID пользователя (клиента), который будет записан на тренировку

        public TrainingsPage(int id) // Получаем ID пользователя, чтобы использовать его для записи
        {
            InitializeComponent();
            userId = id;
            LoadTrainings();
        }

        // Загрузка доступных тренировок
        private void LoadTrainings()
        {
            var trainings = ConnectionClass.connect.Trainings.ToList()
                .Select(t => new
                {
                    t.TrainingID,
                    t.TrainingType,
                    t.Schedule,
                    t.Duration,
                    t.MaxParticipants
                })
                .ToList();

            TrainingsListView.ItemsSource = trainings;
        }

        // Кнопка "Записаться на тренировку"
        private void BookTrainingButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTraining = (dynamic)TrainingsListView.SelectedItem; // Получаем выбранную тренировку

            if (selectedTraining == null)
            {
                MessageBox.Show("Выберите тренировку для записи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка наличия активного абонемента у клиента
            var clientSubscription = ConnectionClass.connect.Clients
                .FirstOrDefault(cs => cs.ClientID == userId && cs.MembershipTypeID != null);

            if (clientSubscription == null)
            {
                MessageBox.Show("У вас нет активного абонемента или он не оплачен. Пожалуйста, оформите абонемент.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Получаем клиента по userId
                var client = ConnectionClass.connect.Clients
                    .FirstOrDefault(c => c.UserID == userId);

                if (client == null)
                {
                    MessageBox.Show("Клиент с таким ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Запись клиента на тренировку
                var bookings = new Bookings
                {
                    ClientID = client.ClientID, // Используем ClientID из найденного клиента
                    TrainingID = selectedTraining.TrainingID,
                    BookingDate = DateTime.Now,
                    Status = "Confirmed" // Статус "Confirmed" означает, что клиент записан на тренировку
                };

                // Добавляем запись в таблицу Bookings
                ConnectionClass.connect.Bookings.Add(bookings);
                ConnectionClass.connect.SaveChanges(); // Сохраняем изменения в базе данных

                MessageBox.Show("Вы успешно записались на тренировку!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new GymsPage()); // Переходим на страницу с тренажерными залами
            }
            catch (Exception ex)
            {
                // Логируем ошибку или выводим подробности исключения
                MessageBox.Show($"Произошла ошибка при записи на тренировку: {ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Gym_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GymsPage());
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
