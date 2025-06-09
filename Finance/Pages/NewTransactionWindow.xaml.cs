using Finance.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
        HandlePlaceholderDatePick();
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

    private void HandlePlaceholderDatePick()
    {
        dpDate.Loaded += (s, e) =>
        {
            var datePickerTextBox = (DatePickerTextBox)dpDate.Template.FindName("PART_TextBox", dpDate);
            if (datePickerTextBox == null) return;

            // Esperar o template do DatePickerTextBox estar aplicado para acessar o watermark
            datePickerTextBox.ApplyTemplate();

            var watermark = (ContentControl)datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox);
            if (watermark == null) return;

            void UpdateWatermarkVisibility()
            {
                watermark.Visibility = string.IsNullOrEmpty(datePickerTextBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            // Inicializa o estado do placeholder
            UpdateWatermarkVisibility();

            // Atualiza a visibilidade toda vez que o texto muda
            datePickerTextBox.TextChanged += (sender, args) => UpdateWatermarkVisibility();
        };
    }
}
