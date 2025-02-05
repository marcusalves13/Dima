using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithDisplayName("Category : Get")
            .WithSummary("Retorna uma categoria por ID.")
            .WithDescription("Retorna uma categoria por ID.")
            .WithOrder(2)
            .Produces<Response<Category>>();
    }
    private static async Task<IResult> HandleAsync(long id, ICategoryHandler handler) 
    {
        var request = new GetCategoryByIdRequest 
        {
            Id = id
        };

        var response = await handler.GetAsync(request);
        return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        
    } 
}
