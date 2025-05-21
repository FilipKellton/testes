using NetLead.Records;

namespace NetLead.Services.Abstractions;

public interface ITransactionHandler
{
    TransactionType HandledType { get; }
    string Handle(TransactionRequest transaction);
}
