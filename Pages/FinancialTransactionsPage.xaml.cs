using FitnessOsnova_Kam_Dav.DbModel;
using System.Linq;
using System.Windows.Controls;

namespace FitnessOsnova_Kam_Dav.Pages
{
    public partial class FinancialTransactionsPage : Page
    {
        public FinancialTransactionsPage()
        {
            InitializeComponent();
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (var context = new FitnessClub_Kam_DavEntities())
            {
                var transactions = context.FinancialTransactions
                    .Where(t => t.ClientID == CurrentUser.ID) // Загрузка транзакций текущего клиента
                    .Select(t => new
                    {
                        TransactionDate = t.TransactionDate.HasValue ? t.TransactionDate.Value.ToString("g") : "Не указана",
                        t.Amount,
                        t.TransactionType
                    })
                    .ToList();

                TransactionsListView.ItemsSource = transactions;
            }
        }
    }
}