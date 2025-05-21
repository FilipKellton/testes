using NetLead.Records;
using NetLead.Services.Abstractions;

namespace NetLead.Services;

public class OnlineTransactionHandler : ITransactionHandler
{
    public TransactionType HandledType => TransactionType.Online;

    public string Handle(TransactionRequest transaction)
    {
        return $"Handled for transaction of type {transaction.Type}";
    }
}
