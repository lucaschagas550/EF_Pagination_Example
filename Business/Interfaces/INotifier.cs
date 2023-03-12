using EF_Pagination_Example.Business.Notifications;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}
