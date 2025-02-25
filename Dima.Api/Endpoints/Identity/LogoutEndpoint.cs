using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", HandleAsync)
             .RequireAuthorization()
            .WithName("Identity : Logout")
            .WithSummary("Logout de uma conta.")
            .WithDescription("Faz logout de uma Conta.");
    }

    public static async Task<IResult> HandleAsync(SignInManager<User> signIn)
    {
        await signIn.SignOutAsync();
        return Results.Ok();
    }

}
