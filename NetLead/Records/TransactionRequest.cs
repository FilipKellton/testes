namespace NetLead.Records;

public class TransactionRequest
{
    public TransactionType Type { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public decimal? Amount { get; set; }
    public string? ReferenceCode { get; set; } // only for online transactions
}
