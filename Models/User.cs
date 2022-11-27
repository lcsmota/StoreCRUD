using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(5, ErrorMessage = "Mínimo de caracteres: 5")]
    [MaxLength(80, ErrorMessage = "Máximo de caracteres: 80")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(8, ErrorMessage = "Mínimo de caracteres: 8")]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
