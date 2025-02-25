using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{Id}", HandleAsync)
            .WithDisplayName("Transaction : Get")
            .WithSummary("Retorna uma transação por ID.")
            .WithDescription("Retorna uma transação por ID.")
            .WithOrder(2)
            .Produces<Response<Transaction>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long Id)
    {
        var request = new GetTransactionByIdRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = Id
        };
        var response = await handler.GetByIdAsync(request);
        return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
    }
}
