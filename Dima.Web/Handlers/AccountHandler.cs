using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result =  await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login Realizado com sucesso!", 200, "Login Realizado com sucesso!") 
            : new Response<string>(null, 400, "Não foi possível realizar o login.");
    }

    public async Task LogoutAsync()
    {
        var resquest = new StringContent("", Encoding.UTF8, "application/json");
        var result = await _client.PostAsJsonAsync("v1/identity/logout", resquest);
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Cadastro Realizado com sucesso!", 200, "Cadastro Realizado com sucesso!")
            : new Response<string>(null, 400, "Não foi possível realizar o cadastro.");
    }
}
