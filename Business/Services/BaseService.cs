using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Model;
using FluentValidation;
using FluentValidation.Results;

namespace EF_Pagination_Example.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
