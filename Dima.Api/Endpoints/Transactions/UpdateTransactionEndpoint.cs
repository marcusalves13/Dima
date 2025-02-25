using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app) { 
        app.MapPut("/{Id}", HandleAsync)
            .WithName("Transaction : Update")
            .WithSummary("Atualiza uma transação.")
            .WithDescription("Atualiza uma transação.")
            .WithOrder(4)
            .Produces<Response<Transaction>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long Id, UpdateTransactionRequest request) 
    {
        request.Id = Id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess? Results.Ok(response) : Results.NotFound(response);
    }
}
