using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests.Categories;
public class UpdateCategoryRequest:Request
{
    [JsonIgnore]    
    public long Id { get; set; }

    [Required(ErrorMessage = "Titulo é obrigatório.")]
    [MaxLength(80, ErrorMessage = "Titulo deve conter no máximo 80 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatório.")]
    [MaxLength(255, ErrorMessage = "Descrição deve conter no máximo 255 caracteres.")]
    public string Description { get; set; } = string.Empty;
}
