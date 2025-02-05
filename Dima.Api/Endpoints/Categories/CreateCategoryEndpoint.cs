using Dima.Api.Common.Api;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;

namespace Dima.Api.EndPoints.Categories;

public class CreateCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Category : Create")
            .WithSummary("Cria uma categoria.")
            .WithDescription("Cria uma categoria.")
            .WithOrder(1)
            .Produces<Response<Category?>>();
    }

    public static async Task<IResult> HandleAsync(CreateCategoryRequest request,ICategoryHandler handler) 
    {
        var response = await handler.CreateCategoryAsync(request);
        return response.IsSuccess ? Results.Created($"/{response.Data?.Id}", response) : Results.BadRequest(response);
    }
}
