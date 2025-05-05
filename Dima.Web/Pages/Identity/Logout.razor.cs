using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
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
    #endregion

    #region Overrides
    protected async override Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await AccountHandler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
            await base.OnInitializedAsync();
        }
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {

        try
        {
            IsBusy = true;
            await AccountHandler.LogoutAsync();
            NavigationManager.NavigateTo("/");
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
