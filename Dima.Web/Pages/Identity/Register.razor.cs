using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    #region Dependencies
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IAccountHandler AccountHandler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationProvider { get; set; } = null!;
    #endregion

    #region Properties
    public bool IsBusy { get; set; } = false;
    public RegisterRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected async override Task OnInitializedAsync()
    {
        var authState = await AuthenticationProvider.GetAuthenticationStateAsync();
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
            var result = await AccountHandler.RegisterAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/login");
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
