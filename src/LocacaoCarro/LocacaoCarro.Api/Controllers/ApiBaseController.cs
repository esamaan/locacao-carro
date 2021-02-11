using Microsoft.AspNetCore.Mvc;
using Flunt.Notifications;
using System.Collections.Generic;
using LocacaoCarro.Api.Modelos;

namespace LocacaoCarro.Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected BadRequestObjectResult BadRequest(IReadOnlyCollection<Notification> notifications)
        {
            return new BadRequestObjectResult(new ErroModel(notifications));
        }

        protected NotFoundObjectResult NotFound(string message)
        {
            return new NotFoundObjectResult(new ErroModel(message));
        }

        protected UnprocessableEntityObjectResult UnprocessableEntity(IReadOnlyCollection<Notification> notifications)
        {
            return new UnprocessableEntityObjectResult(new ErroModel(notifications));
        }
    }
}
