using System.ComponentModel.DataAnnotations;

namespace Store.DTOs;

public class ProductForCreationDTO
{
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
}
