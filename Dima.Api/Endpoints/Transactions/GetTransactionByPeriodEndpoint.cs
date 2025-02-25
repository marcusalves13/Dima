using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core;
using Microsoft.AspNetCore.Mvc;
using Dima.Core.Requests.Transactions;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint:IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Transaction : GetAllByPeriod")
            .WithSummary("Retorna todas as transações de um periodo.")
            .WithDescription("Retorna todas as transações de um periodo.")
            .WithOrder(3)
            .Produces<List<Transaction>>();
    }
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, 
                                                   ITransactionHandler handler,
                                                   [FromQuery]DateTime? StartDate = null,
                                                   [FromQuery]DateTime? EndDate = null,
                                                   [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                   [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionByPeriodRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            StartDate = StartDate,
            EndDate = EndDate,
            PagedNumber = pageNumber,
            PageSize = pageSize

        };
        var response = await handler.GetByPeriodAsync(request);
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest();
    }
}
