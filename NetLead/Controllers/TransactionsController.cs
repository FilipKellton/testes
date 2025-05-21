using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLead.Context.Transactions.Commands.HandleTransaction;

namespace NetLead.Controllers;

[Route("api/v1/transactions")]
[AllowAnonymous]
public class TransactionsController(IMediator mediator) : Controller
{
    [HttpPost("handle")]
    public async Task<IActionResult> Handle([FromBody] HandleTransactionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
