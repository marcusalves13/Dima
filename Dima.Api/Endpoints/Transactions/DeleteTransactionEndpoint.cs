using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{Id}", HandleAsync)
            .WithName("Transaction : Delete")
            .WithSummary("Exclui uma transação.")
            .WithDescription("Exclui uma transação.")
            .WithOrder(5)
            .Produces<Response<Transaction>>();
    }
    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long Id) 
    {
        var request = new DeleteTransactionRequest()
        {
            Id = Id
        };
        var response = await handler.DeleteAsync(request);
        return response.IsSuccess ? Results.NoContent() : Results.NotFound(response);
    }
}
