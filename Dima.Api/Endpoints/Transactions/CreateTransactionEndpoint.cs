using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Transaction : Create")
            .WithSummary("Cria uma transação.")
            .WithDescription("Cria uma transação.")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, CreateTransactionRequest request) 
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
       var response = await handler.CreateAsync(request);
       return response.IsSuccess ? Results.Created($"/{response.Data?.Id}",response): Results.BadRequest(response);
    }
}
