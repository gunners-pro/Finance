namespace Finance.Models;

class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public TypeTransaction Type { get; set; }
}

public enum TypeTransaction
{
    Income,
    Outcome
}