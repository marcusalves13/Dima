using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Category : Delete")
            .WithSummary("Exclui uma categoria.")
            .WithDescription("Exclui uma categoria.")
            .WithOrder(5)
            .Produces<Response<Category>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, long Id, ICategoryHandler handler)
    {
        var request = new DeleteCategoryRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = Id
        };
        var response = await handler.DeleteAsync(request);
        return response.IsSuccess ? Results.NoContent() : Results.NotFound(response);
    }
}
