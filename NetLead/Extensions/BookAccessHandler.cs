using Microsoft.AspNetCore.Authorization;

namespace NetLead.Extensions;

public class BookAccessHandler : AuthorizationHandler<BookAccessRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BookAccessRequirement requirement)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.Resource is HttpContext httpContext)
        {
            var bookIdString = httpContext.GetRouteValue("id")?.ToString();

            if (int.TryParse(bookIdString, out int bookIdFromRoute))
            {
                var booksClaim = context.User.FindFirst("books")?.Value;

                if (!string.IsNullOrEmpty(booksClaim))
                {
                    var ownedBookIds = booksClaim.Split(',')
                                                 .Select(s => int.TryParse(s.Trim(), out int id) ? id : (int?)null)
                                                 .Where(id => id.HasValue)
                                                 .Select(id => id.Value)
                                                 .ToList();

                    if (ownedBookIds.Contains(bookIdFromRoute))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}