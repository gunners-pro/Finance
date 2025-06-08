using Finance.Data;
using Finance.Models;
using System.Collections.ObjectModel;

namespace Finance.ViewModels;

class TransactionsViewModel
{
    public ObservableCollection<Transaction> TransactionsList { get; set; }

    public TransactionsViewModel()
    {
        using var db = new ContextDB();
        db.Database.EnsureCreated();
        TransactionsList = new ObservableCollection<Transaction>([.. db.Transactions]);
    }
}
