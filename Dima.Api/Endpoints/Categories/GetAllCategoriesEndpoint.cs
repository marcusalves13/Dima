using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Category : GetAll")
            .WithSummary("Retorna todas as categorias.")
            .WithDescription("Retorna todas as categorias.")
            .WithOrder(3)
            .Produces<List<Category>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                   ICategoryHandler handler,
                                                   [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                   [FromQuery] int pageSize = Configuration.DefaultPageSize) 
    {
        var request = new GetAllCategoriesRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PagedNumber = pageNumber,
            PageSize = pageSize

        };
        var response = await handler.GetAllAsync(request);
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest();
    }
}
