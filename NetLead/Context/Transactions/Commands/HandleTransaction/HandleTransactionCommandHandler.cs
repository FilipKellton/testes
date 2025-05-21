using MediatR;
using NetLead.Services.Abstractions;

namespace NetLead.Context.Transactions.Commands.HandleTransaction;

public class HandleTransactionCommandHandler(IEnumerable<ITransactionHandler> transactionHandlers) : IRequestHandler<HandleTransactionCommand, string>
{
    public Task<string> Handle(HandleTransactionCommand request, CancellationToken cancellationToken)
    {
        var handler = transactionHandlers.FirstOrDefault(x => x.HandledType == request.Transaction.Type) ?? throw new ArgumentException("Handler not supported");

        return Task.FromResult(handler.Handle(request.Transaction));
    }
}
