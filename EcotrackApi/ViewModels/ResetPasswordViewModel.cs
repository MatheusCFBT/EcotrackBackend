using System.ComponentModel.DataAnnotations;

namespace EcotrackApi.ViewModels
{
    public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
    [EmailAddress(ErrorMessage = " O campo {0} está em formato inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
    public string Token { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
    [StringLength(40, MinimumLength = 5, ErrorMessage = "O campo {0} deve conter entre {2} a {1} caracteres.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; }
}
}