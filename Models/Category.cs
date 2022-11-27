using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(5, ErrorMessage = "Mínimo de caracteres: 5")]
    [MaxLength(80, ErrorMessage = "Máximo de caracteres: 80")]
    public string Title { get; set; } = string.Empty;
}
