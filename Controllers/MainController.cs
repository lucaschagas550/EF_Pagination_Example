using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EF_Pagination_Example.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        //public readonly IUser AppUser;

        protected Guid UserId { get; set; }
        protected bool AuthenticatedUser { get; set; }

        protected MainController(INotifier notifier
                              /*IUser appUser*/)
        {
            _notifier = notifier;
            //AppUser = appUser;

            //if (appUser.IsAuthenticated())
            //{
            //    UserId = appUser.GetUserId();
            //    AuthenticatedUser = true;
            //}
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotifications();
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation()) return Ok(new ResponseSuccess(result));

            return BadRequest(new ResponseFailure(_notifier.GetNotifications().Select(n => n.Message)));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyInvalidModelError(modelState);
            return CustomResponse();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
        
        protected ActionResult CustomResponse(ResponseExternalResult response)
        {
            ResponsePossuiErros(response);

            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseExternalResult response)
        {
            if (response == null || !response.Errors.Messages.Any()) return false;

            foreach (var mensagem in response.Errors.Messages)
            {
                NotifyError(mensagem);
            }

            return true;
        }
        
        protected void ClearProcessingErrors() =>
            _notifier.ClearErrors();
    }
}