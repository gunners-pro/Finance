namespace Finance.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public TypeTransaction Type { get; set; }

    public string TypeTransactionFormatted
    {
        get
        {
            return Type switch
            {
                TypeTransaction.Income => "Entrada",
                TypeTransaction.Outcome => "Saída",
                _ => "Desconhecido"
            };        
        }
    }
}

public enum TypeTransaction
{
    Income,
    Outcome
}