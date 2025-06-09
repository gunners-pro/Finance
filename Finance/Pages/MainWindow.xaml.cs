using Finance.Data;
using Finance.ViewModels;
using System.Windows;

namespace Finance.Pages;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly TransactionsViewModel _transactionsViewModel;
    public MainWindow()
    {
        InitializeComponent();
        _transactionsViewModel = new TransactionsViewModel();
        DataContext = _transactionsViewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var newTransactionWindow = new NewTransactionWindow
        {
            Owner = this,            
        };

        if (newTransactionWindow.ShowDialog() == true && newTransactionWindow.TransactionCreated != null)
        {
            var newTransaction = newTransactionWindow.TransactionCreated;

            try
            {
                using var db = new ContextDB();
                db.Transactions.Add(newTransaction);
                db.SaveChanges();
                _transactionsViewModel.TransactionsList.Add(newTransaction);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ao Salvar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}