using Finance.Data;
using Finance.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
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

    //===================== gráficos
    public ISeries[] Series { get; private set; } = [];
    public Axis[] XAxes { get; set; } = [];

    // ============================

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
            OnPropertyChanged(nameof(Series));
            BuildSeries();
        };

        BuildSeries();

    }

    private void BuildSeries()
    {
        var grouped = TransactionsList.GroupBy(t => t.Date.Date).OrderBy(g => g.Key).ToList();

        var labels = grouped.Select(g => g.Key.ToString("dd/MM")).ToArray();

        var entradas = grouped.Select(g => g.Where(t => t.Type == TypeTransaction.Income).Sum(t => t.Value)).ToArray();

        var saidas = grouped.Select(g => g.Where(t => t.Type == TypeTransaction.Outcome).Sum(t => t.Value)).ToArray();

        Series =
        [
        new ColumnSeries<decimal>
        {
            Values = entradas,
            Name = "Entradas",            
            Fill = new SolidColorPaint(SKColors.Green),
            MaxBarWidth = 30
        },
        new ColumnSeries<decimal>
        {
            Values = saidas,
            Name = "Saídas",
            Fill = new SolidColorPaint(SKColors.Red),
            MaxBarWidth = 30
        }
        ];

        XAxes = [
            new Axis{
                Labels = labels,
                Name = "Data",
            }
        ];

        OnPropertyChanged(nameof(Series));
        OnPropertyChanged(nameof(XAxes));

    }


    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}
