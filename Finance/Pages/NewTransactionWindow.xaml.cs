using Finance.Models;
using System.Windows;
using System.Windows.Controls;

namespace Finance.Pages;

/// <summary>
/// Lógica interna para NewTransactionWindow.xaml
/// </summary>
public partial class NewTransactionWindow : Window
{
    public Transaction? TransactionCreated { get; private set; }
    public NewTransactionWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if( cmbType.SelectedItem is ComboBoxItem item && item.Tag is string tag && !string.IsNullOrWhiteSpace(tag))
        {
            var type = Enum.Parse<TypeTransaction>(tag);

            TransactionCreated = new Transaction
            {
                Description = txtDescription.Text,
                Value = decimal.Parse(txtValue.Text),
                Date = dpDate.SelectedDate ?? DateTime.Now,
                Type = type
            };

            DialogResult = true;
            Close();
        }
        
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
