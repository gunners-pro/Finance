using Finance.ViewModels;
using System.Windows;

namespace Finance.Pages;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new TransactionsViewModel();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var newTransactionWindow = new NewTransactionWindow
        {
            Owner = this,            
        };
        
        if(newTransactionWindow.ShowDialog() == true)
        {
            var newTransaction = newTransactionWindow.TransactionCreated;

            //Adiciona no banco de dados
        }
    }
}