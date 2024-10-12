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
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            // propagar o erro ate a camada de apresentacao
            _notificador.Handle(new Notification(mensagem))

        }

        protected bool ExecutarValidacao<TV,TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}