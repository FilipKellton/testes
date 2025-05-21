using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetLead.Controllers;

[Route("api/v1/transactions")]
[AllowAnonymous]
public class TransactionsController : Controller
{
    [HttpPost("handle")]
    public IActionResult Handle()
    {
        return Ok();
    }
}
