using EF_Pagination_Example.Business.Interfaces;

namespace EF_Pagination_Example.Business.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier() =>
            _notifications = new List<Notification>();

        public void Handle(Notification notification) =>
            _notifications.Add(notification);
        
        public void ClearErrors() =>
            _notifications.Clear();

        public IEnumerable<Notification> GetNotifications() =>
            _notifications;

        public bool HasNotifications() =>
            _notifications.Any();
    }
}