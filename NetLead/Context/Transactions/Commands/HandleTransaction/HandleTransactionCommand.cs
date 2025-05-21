using MediatR;
using NetLead.Records;

namespace NetLead.Context.Transactions.Commands.HandleTransaction;

public record HandleTransactionCommand(TransactionRequest Transaction) : IRequest<string>;
