using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class GymsPage : Page
    {
        public GymsPage()
        {
            InitializeComponent();
            LoadGyms();
        }

        private void LoadGyms()
        {
            using (var context = new FitnessClub_Kam_DavEntities())
            {
                var gyms = context.Gyms
                    .Select(g => new
                    {
                        g.GymID,
                        g.GymName,
                        g.Location,
                        g.Facilities,
                        g.Capacity
                    })
                    .ToList();

                GymsListView.ItemsSource = gyms;
            }
        }
        private void ShowEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGym = (dynamic)GymsListView.SelectedItem;

            if (selectedGym == null)
            {
                MessageBox.Show("Выберите зал для отображения инвентаря", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Передаем GymID на страницу инвентаря
            NavigationService.Navigate(new InventoryPage(selectedGym.GymID));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TrainingsPage(CurrentUser.ID));
        }
    }
}