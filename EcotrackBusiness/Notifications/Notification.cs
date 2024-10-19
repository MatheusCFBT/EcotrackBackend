using System.Runtime.CompilerServices;
using EcotrackBusiness.Interfaces;

namespace EcotrackBusiness.Notifications
{
    public class Notification
    {
        public readonly string Mensagem;

        public Notification(string mensagem)
        {
            // Armazena o conteudo da notificacao
            Mensagem = mensagem;
        }
    }

    public class Notificador : INotificador
    {
        // A lista mantem todas as notificacoes geradas durante a aplicacao
        private List<Notification> _notifications;

        public Notificador()
        {
            // Inicializa a lista como uma nova lista vazia. Isso garante que, ao criar uma nova instancia de Notificador, ela comece sem notificacoes.
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            // Recebe uma notificacao do tipo Notification e a adiciona a lista _notifications
            _notifications.Add(notification);
        }

        public List<Notification> ObterNotifications()
        {
            // Retorna todas as notificacoes armazenadas na lista _notifications
            return _notifications;
        }

        public bool TemNotificacao()
        {
            // Verifica se ha alguma notificacao
            return _notifications.Any();
        }
    }
}