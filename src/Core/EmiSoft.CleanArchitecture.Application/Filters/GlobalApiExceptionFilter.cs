using EmiSoft.CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

namespace EmiSoft.CleanArchitecture.Application.Filters;

public class GlobalApiExceptionFilter : IExceptionFilter
{
    public GlobalApiExceptionFilter()
    {
    }

    /// <summary>
    /// Default olaraq ancaq unhandled exceptionlar loglayiriq
    /// </summary>
    public void OnException(ExceptionContext context)
    {
        //_logger.LogError(context.Exception, context.Exception.Message);

        // UI-da validation exception handle etdikde, Request StatusCode - BadRequest(400) olacaq və Detaildəki status ValidationError(1003) olaraq göndəriləcək
        if (context.Exception is ValidationException validationException)
        {
            var problemDetails = new ValidationProblemDetails()
            {
                Title = validationException.Message,
                Status = validationException.Status,
            };

            foreach (var item in validationException.Errors)
            {
                problemDetails.Errors.Add(item);
            }

            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        // Custom exceptionlar Status və Message olaraq tutulacaq, main Request statusu InternalServerError(500) və detaildəki Status isə dinamik olaraq göndəriləcək
        else if (context.Exception is ApiException apiException)
        {
            context.Result = new ObjectResult(new
            {
                apiException.Status,
                apiException.Message
            });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        // CancellationToken ilə sonlandırılış əməliyyatlar üçün
        else if (context.Exception is OperationCanceledException)
        {
            var canceledException = new ApiException(Enums.HttpResponseStatusType.OperationCancelled);
            context.Result = new ObjectResult(new
            {
                canceledException.Status,
                canceledException.Message
            });

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Gone;
            Log.Error(context.Exception, context.Exception.Message);
        }
        // Unhandled exceptionlar isə Internal Server Error olaraq göndəriləcək
        else
        {
            context.Result = new ObjectResult(new
            {
                Status = (int)HttpStatusCode.InternalServerError,
                context.Exception.Message
            });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Log.Error(context.Exception, context.Exception.Message);
        }
        context.ExceptionHandled = true;
    }
}
