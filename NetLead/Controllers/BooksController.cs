using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetLead.Controllers;

[Route("api/v1/books")]
[Authorize]
public class BooksController : Controller
{
    [Authorize(Policy = "EditorOrAdminPolicy")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [Authorize(Policy = "AdminOrAuthorPolicy")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ok(id);
    }

}
