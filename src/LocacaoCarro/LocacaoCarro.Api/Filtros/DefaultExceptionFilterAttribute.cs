using LocacaoCarro.Aplicacao.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace LocacaoCarro.Api.Filtros
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, context.Exception.Message);

            context.Result = new ObjectResult(new ErroModel(context.Exception.Message))
            {
                StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
            };
        }
    }
}
