using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Dependencies
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IAccountHandler AccountHandler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    #endregion

    #region Properties
    public bool IsBusy { get; set; } = false;
    public LoginRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected async override Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            NavigationManager.NavigateTo("/");
        }
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {

        try
        {
            IsBusy = true;
            var result = await AccountHandler.LoginAsync(InputModel);
            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        finally
        {
            IsBusy = false;
        }

    }
    #endregion

}
