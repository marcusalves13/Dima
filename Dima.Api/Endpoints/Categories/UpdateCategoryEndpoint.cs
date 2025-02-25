using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;
using System.Security.Principal;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Category : Update")
            .WithSummary("Atualiza uma categoria.")
            .WithDescription("Atualiza uma categoria.")
            .WithOrder(4)
            .Produces<Response<Category>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, long Id, UpdateCategoryRequest request, ICategoryHandler handler ) 
    {
        request.Id = Id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response); 
    }
}
