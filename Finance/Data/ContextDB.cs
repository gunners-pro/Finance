using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data;

class ContextDB : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=finance.db");
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { Id = 1, Description = "Salário", Value = 3000, Date = DateTime.Today, Type = TypeTransaction.Income },
            new Transaction { Id = 2, Description = "Mercado", Value = 250, Date = DateTime.Today.AddDays(-1), Type = TypeTransaction.Outcome },
            new Transaction { Id = 3, Description = "Conta de luz", Value = 120, Date = DateTime.Today.AddDays(-2), Type = TypeTransaction.Outcome }
        );
    }
}
