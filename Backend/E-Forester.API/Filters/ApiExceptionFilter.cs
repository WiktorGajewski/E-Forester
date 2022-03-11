using E_Forester.Application.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace E_Forester.API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<string, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<string, Action<ExceptionContext>>
            {
                {nameof(UnauthorizedAccessException), HandleUnauthorizedAccessException},
                {nameof(NotFoundException), HandleNotFoundException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType().Name;

            if(_exceptionHandlers.ContainsKey(exceptionType))
            {
                _exceptionHandlers[exceptionType].Invoke(context);
            }
            else
            {
                HandleUnknownException(context);
            }
        }
        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized"
            };

            context.Result = new ObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "The specified resource was not found",
                Detail = exception.Message
            };

            context.Result = new ObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request"
            };

            context.Result = new ObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
