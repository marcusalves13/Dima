using Dima.Core.Requests.Categories;
using Dima.Web.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public class CreateCategoryPage:ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CreateCategoryRequest InputModel { get; set; } = new();
    #endregion

    #region Services
    [Inject]
    public CategoryHandler Handler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Methods
    protected async Task OnvalidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateCategoryAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Categoria criada com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/categories");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);   
            }
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

    #endregion
}
