using System.ComponentModel.DataAnnotations;

namespace EcotrackApi.ViewModels
{
    public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
    [EmailAddress(ErrorMessage = " O campo {0} está em formato inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
    public string ClientURI { get; set; } // URL do frontend para link de recuperação
}
}