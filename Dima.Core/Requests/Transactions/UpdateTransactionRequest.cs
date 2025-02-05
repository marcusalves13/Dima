using Dima.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests.Transactions;
public class UpdateTransactionRequest:Request
{
    [JsonIgnore]
    public long Id { get; set; }

    [Required(ErrorMessage = "Titulo é obrigatório.")]
    [MaxLength(80, ErrorMessage = "Titulo deve conter no máximo 80 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de Pagamento/Recebimento é obrigatório.")]
    public DateTime PaidOrReceivedAt { get; set; }

    [Required(ErrorMessage = "Tipo é obrigatório.")]
    public ETransactionType Type { get; set; }

    [Required(ErrorMessage = "Valor é obrigatório.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Categoria é obrigatório.")]
    public long CategoryId { get; set; }
}
