using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Store.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(5, ErrorMessage = "Mínimo de caracteres: 5")]
    [MaxLength(80, ErrorMessage = "Máximo de caracteres: 80")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(255, ErrorMessage = "Máximo de caracteres: 255")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obrigatório")]
    [Range(1, 999999, ErrorMessage = "Valor deve ser maior que zero")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Category Category { get; set; }
}
