using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class InventoryPage : Page
    {
        private int gymId; // ID зала

        public InventoryPage(int gymId)
        {
            InitializeComponent();
            this.gymId = gymId; // Сохраняем переданный GymID
            LoadInventory(); // Загружаем инвентарь для выбранного зала
        }

        private void LoadInventory()
        {
            var inventory = ConnectionClass.connect.Inventory
                .Where(i => i.GymID == gymId) // Фильтруем инвентарь по GymID
                .Select(i => new
                {
                    i.ItemName,
                    i.Quantity,
                    i.Condition,
                    i.LastMaintenanceDate
                })
                .ToList();

            InventoryListView.ItemsSource = inventory; // Отображаем инвентарь в ListView
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new GymsPage()); // Переходим назад на страницу с залами
        }
    }
}
