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
}