using Finance.Data;
using Finance.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.ViewModels;

public class TransactionsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Transaction> TransactionsList { get; set; }

    private decimal TotalIncomes => TransactionsList
        .Where(t => t.Type == TypeTransaction.Income)
        .Sum(t => t.Value);

    public string TotalIncomesFormatted => $"{TotalIncomes:C2}";

    private decimal TotalOutcomes => TransactionsList
        .Where(t => t.Type == TypeTransaction.Outcome)
        .Sum(t => t.Value);

    public string TotalOutcomesFormatted => $"{TotalOutcomes:C2}";

    private decimal Balance => TotalIncomes - TotalOutcomes;

    public string BalanceFormatted => $"{Balance:C2}";

    public TransactionsViewModel()
    {
        using var db = new ContextDB();
        db.Database.EnsureCreated();
        TransactionsList = new ObservableCollection<Transaction>([.. db.Transactions]);
        TransactionsList.CollectionChanged += (_, __) =>
        {
            OnPropertyChanged(nameof(TotalOutcomesFormatted));
            OnPropertyChanged(nameof(TotalIncomesFormatted));
            OnPropertyChanged(nameof(BalanceFormatted));
        };
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}
