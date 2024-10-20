using EcotrackBusiness.Models;
using EcotrackBusiness.Validations.DocValidation;
using FluentValidation;

namespace EcotrackBusiness.Validations 
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            // Verifica se o nome do cliente esta nulo e o tamanho
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(5,40).WithMessage("O campo {PropertyName} precisa conter entre {MinLength} e {MaxLength} caracteres");
            
            // Verifica se o email do cliente esta nulo e valido
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .EmailAddress().WithMessage("O {PropertyName} fornecido não é valido");
            
            // Verifica se o cpf do cliente esta com tamanho correto
            RuleFor(c => c.Cpf.Length).Equal(CpfValidation.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

            // Verifica se o cpf do cliente esta valido
            RuleFor(c => CpfValidation.Validar(c.Cpf)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        }
    }
}