using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class TrainersPage : Page
    {
        public TrainersPage()
        {
            InitializeComponent();
            LoadTrainers();
        }

        private void LoadTrainers()
        {
            using (var context = new FitnessClub_Kam_DavEntities())
            {
                var trainers = context.Trainers
                    .Select(t => new
                    {
                        t.FirstName,
                        t.LastName,
                        t.Specialization,
                        Rating = t.Rating.HasValue ? t.Rating.Value.ToString("F1") : "Не указан"
                    })
                    .ToList();

                TrainersListView.ItemsSource = trainers;
            }
        }
    }
}