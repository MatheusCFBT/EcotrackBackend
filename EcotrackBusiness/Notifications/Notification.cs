using System.Runtime.CompilerServices;
using EcotrackBusiness.Interfaces;

namespace EcotrackBusiness.Notifications
{
    public class Notification
    {
        public readonly string Mensagem;

        public Notification(string mensagem)
        {
            Mensagem = mensagem;
        }
    }

    public class Notificador : INotificador
    {
        private List<Notification> _notifications;

        public Notificador()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> ObterNotifications()
        {
            return _notifications;
        }

        public bool TemNotificacao()
        {
            return _notifications.Any();
        }
    }
}