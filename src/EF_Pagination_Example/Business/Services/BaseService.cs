using System.Net;
using System.Text;
using System.Text.Json;
using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Communication;

namespace EF_Pagination_Example.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string message) =>
            _notifier.Handle(new Notification(message));

        protected T Notify<T>(string message, T item) where T : class
        {
           _notifier.Handle(new Notification(message));
            return item;
        }

        protected T Notify<T>(List<string> messages, T item) where T : class
        {
            foreach(var message in messages)
                _notifier.Handle(new Notification(message));

            return item;
        }

        protected StringContent SerializeObject(object data) =>
            new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

        protected async Task<T?> DeserializeObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseExternalResult ReturnOk() =>
            new ResponseExternalResult();
    }
}