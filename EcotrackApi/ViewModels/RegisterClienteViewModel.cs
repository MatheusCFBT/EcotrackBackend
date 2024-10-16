using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EcotrackApi.ViewModels
{
    public class RegisterClienteViewModel
    {  
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "O campo {0} precisa conter entre {2} e {1} caracteres" )]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo {0} precisa conter entre {2} e {1} caracteres")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
        [EmailAddress(ErrorMessage = " O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]    
        [StringLength(40, MinimumLength = 5, ErrorMessage = "O campo {0} deve conter entre {2} a {1} caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}