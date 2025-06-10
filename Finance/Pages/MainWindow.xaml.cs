using Finance.Data;
using Finance.Models;
using Finance.ViewModels;
using System.Windows;
using System.Windows.Controls;

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

    private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is Transaction transaction)
        {
            var resultado = MessageBox.Show(
            $"Tem certeza que deseja excluir a transação:\n\n{transaction.Description} - {transaction.Value:C2}?",
            "Confirmação",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
            );

            if (resultado == MessageBoxResult.Yes)
            {
                // Remover do banco
                using var db = new ContextDB();
                var transInDb = db.Transactions.Find(transaction.Id);
                if (transInDb != null)
                {
                    db.Transactions.Remove(transInDb);
                    db.SaveChanges();
                }

                // Remover da lista
                if (DataContext is TransactionsViewModel vm)
                {
                    vm.TransactionsList.Remove(transaction);
                }
            }
        }
    }
}