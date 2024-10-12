using EcotrackBusiness.Models;
using EcotrackBusiness.Validations.DocValidation;
using FluentValidation;

namespace EcotrackBusiness.Validations 
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(5,40).WithMessage("O campo {PropertyName} precisa conter entre {MinLength} e {MaxLength} caracteres");
            
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .EmailAddress().WithMessage("O {PropertyName} fornecido não é valido");
            
            RuleFor(c => c.Cpf.Length).Equal(CpfValidation.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

            RuleFor(c => CpfValidation.Validar(c.Cpf)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        }
    }
}