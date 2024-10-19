using EcotrackBusiness.Notifications;

namespace EcotrackBusiness.Interfaces
{
    public interface INotificador
    { 
        // Verifica se tem notificacao 
        bool TemNotificacao();

        // Obtem as notificacoes
        List<Notification> ObterNotifications();

        // Manipula a notificacao
        void Handle(Notification notification);
    }
}