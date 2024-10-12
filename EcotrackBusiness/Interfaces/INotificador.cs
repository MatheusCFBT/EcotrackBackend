using EcotrackBusiness.Notifications;

namespace EcotrackBusiness.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notification> ObterNotifications();

        void Handle(Notification notification);
    }
}