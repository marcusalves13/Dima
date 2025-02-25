﻿using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Identity;
using Dima.Api.Endpoints.Transactions;
using Dima.Api.EndPoints.Categories;
using Dima.Api.Models;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app) 
    {
        var endpoints = app.MapGroup("");

        endpoints
            .MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndPoint>()
            .MapEndpoint<GetCategoryByIdEndPoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization()
                .MapEndpoint<CreateTransactionEndPoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByPeriodEndpoint>();

        endpoints.MapGroup("v1/identity/")
                 .WithTags("Identity")
                 .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity/")
                 .WithTags("Identity")
                 .MapEndpoint<GetRolesEndpoint>()
                 .MapEndpoint<LogoutEndpoint>();

    }
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) 
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
