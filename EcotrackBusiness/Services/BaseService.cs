using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using EcotrackBusiness.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace EcotrackBusiness.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador) 
        {
            // Adiciona uma instancia da interface notificador
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            // Obtem todos os erros e suas mensagens
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            // Propaga o erro ate a camada de apresentacao
            _notificador.Handle(new Notification(mensagem));

        }

        protected bool ExecutarValidacao<TV,TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            // Verifica se a validacao pegou algum erro
            if(validator.IsValid) return true; // Se nao ha erros o metodo retorna true e nao tem notficacao

            // passa os erros de validator para Notificar
            Notificar(validator);

            return false;
        }
    }
}