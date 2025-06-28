using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class ListCategoriesPage:ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = [];
    public string Search { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Categories = result.Data ?? [];
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    public Func<Category, bool> Filter => category =>
    {
        if (string.IsNullOrWhiteSpace(Search))
            return true;

        if (category.Title.Contains(Search, StringComparison.OrdinalIgnoreCase))
            return true;

        if (category.Id.ToString().Contains(Search, StringComparison.OrdinalIgnoreCase))
            return true;

        if (category.Description is not null && category.Description.Contains(Search, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

    #endregion
}
