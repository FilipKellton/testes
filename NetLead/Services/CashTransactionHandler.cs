using NetLead.Records;
using NetLead.Services.Abstractions;

namespace NetLead.Services;

public class CashTransactionHandler : ITransactionHandler
{
    public TransactionType HandledType => TransactionType.Cash;

    public string Handle(TransactionRequest transaction)
    {
        return $"Handled for transaction of type {transaction.Type}";
    }
}
