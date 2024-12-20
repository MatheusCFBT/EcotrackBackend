using System.ComponentModel.DataAnnotations;

namespace EcotrackApi.ViewModels
{
    public class LoginClienteViewModel 
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = " O campo {0} está em formato inválido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "O campo {0} deve conter entre {2} a {1} caracteres.")]
        public string Senha { get; set; }
    }
}