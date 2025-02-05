using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;
public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Titulo é obrigatório.")]
    [MaxLength(80,ErrorMessage = "Titulo deve conter no máximo 80 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatório.")]
    [MaxLength(255, ErrorMessage = "Descrição deve conter no máximo 255 caracteres.")]
    public string Description { get; set; } = string.Empty ;
}
