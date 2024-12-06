using FitnessOsnova_Kam_Dav.DbModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class FeedbackPage : Page
    {
        private int selectedRating = 0;

        public FeedbackPage()
        {
            InitializeComponent();
            LoadFeedback();
            InitializeStars();
        }

        private void LoadFeedback()
        {
            using (var context = new FitnessClub_Kam_DavEntities())
            {
                var feedbacks = context.Feedback
                    .Where(f => f.ClientID == CurrentUser.ID) // Загрузка отзывов текущего клиента
                    .Select(f => new
                    {
                        f.Comment,
                        f.Rating,
                        FeedbackDate = f.FeedbackDate // Получаем дату без форматирования
                    })
                    .ToList();

                // Форматируем дату в C# после извлечения данных
                var formattedFeedbacks = feedbacks.Select(f => new
                {
                    f.Comment,
                    f.Rating,
                    FeedbackDate = f.FeedbackDate.HasValue ? f.FeedbackDate.Value.ToString("g") : "Не указана"
                }).ToList();

                FeedbackListView.ItemsSource = formattedFeedbacks;
            }
        }

        private void InitializeStars()
        {
            var stars = Enumerable.Range(1, 5).ToList();
            RatingStars.ItemsSource = stars;
        }

        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && int.TryParse(button.Tag.ToString(), out int rating))
            {
                selectedRating = rating;
                UpdateStarColors();
            }
        }

        private void UpdateStarColors()
        {
            foreach (var item in RatingStars.Items)
            {
                var button = RatingStars.ItemContainerGenerator.ContainerFromItem(item) as Button;
                if (button != null)
                {
                    button.Foreground = RatingStars.Items.IndexOf(item) < selectedRating ?
                        Brushes.Gold : Brushes.Gray;
                }
            }
        }

        private void AddFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            var comment = CommentTextBox.Text;

            if (string.IsNullOrEmpty(comment) || selectedRating == 0)
            {
                MessageBox.Show("Пожалуйста, оставьте отзыв и выберите оценку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new FitnessClub_Kam_DavEntities())
            {
                var newFeedback = new Feedback
                {
                    ClientID = CurrentUser.ID, // Укажите ID текущего клиента
                    TrainerID = null, // Укажите ID тренера, если это необходимо
                    Rating = selectedRating,
                    Comment = comment,
                    FeedbackDate = DateTime.Now
                };

                context.Feedback.Add(newFeedback);
                context.SaveChanges(); // Сохранение изменений в базе данных
            }

            MessageBox.Show("Ваш отзыв был успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadFeedback(); // Обновляем список отзывов
            CommentTextBox.Clear(); // Очищаем текстовое поле
            selectedRating = 0; // Сбрасываем выбранную оценку
            UpdateStarColors(); // Обновляем цвет звезд
        }
    }
}