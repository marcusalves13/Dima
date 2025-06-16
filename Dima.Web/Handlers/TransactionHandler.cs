﻿using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/transactions", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                      ?? new Response<Transaction?>(null, 400, "Falha ao criar transação.");
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                     ?? new Response<Transaction?>(null, 400, "Falha ao excluir transação.");
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        var result = await _client.GetFromJsonAsync<Response<Transaction?>>($"v1/transactions/{request.Id}");
        return result ?? new Response<Transaction?>(null, 400, "Falha ao retornar transação.");
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        var format= "yyyy-MM-dd";
        var startDate = request.StartDate?.ToString(format) ?? DateTime.Now.GetFirstDay().ToString(format);
        var endDate = request.EndDate?.ToString(format) ?? DateTime.Now.GetLastDay().ToString(format);
        var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";
        var result = await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url);
        return result ?? new PagedResponse<List<Transaction>?>(null, 400, "Falha ao retornar transações.");
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/transactions{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                     ?? new Response<Transaction?>(null, 400, "Falha ao atualizar transação.");
    }
}
